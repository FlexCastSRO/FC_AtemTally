using LibAtem.Net;
using LibAtem.Commands;
using LibAtem.Commands.Settings;


namespace FC_AtemTallyServer.Services
{
    public class AtemControlService : IAtemControlService
    {
        public Action? AtemConnected { get; set; }
        public Action? AtemDisconnected { get; set; }
        public Action<List<InputPropertiesGetCommand>>? ExternalInputsLoaded { get; set; }

        public event Action<TallyByInputCommand>? TallyStatusChanged;
        public event Action<string>? ErrorMessageChanged;

        private AtemClient? _client;

        private bool _loadExternalInputCommands;
        private List<InputPropertiesGetCommand> _externalInputsCommands;


        private TallyByInputCommand? _lastTallyStatus;
        private TallyByInputCommand? _currentTallyStatus;

        public AtemControlService()
        {
            _loadExternalInputCommands = true;
            _externalInputsCommands = new List<InputPropertiesGetCommand>();
        }

        public void Init(string atemIpAddress)
        {
            _client = new AtemClient(atemIpAddress, false);
            _client.OnConnection += (object sender) => 
            {
                if (AtemConnected != null)
                {
                    AtemConnected();
                    _externalInputsCommands.Clear();
                }
            };

            _client.OnDisconnect += (object sender) =>
            {
                if (AtemDisconnected != null)
                {
                    AtemDisconnected();
                    _loadExternalInputCommands = true;
                }
            };

            _client.OnReceive += OnReceiveHandler;
        }

        public void Connect()
        {
            _client?.Connect();
        }

        private void OnReceiveHandler(object sender, IReadOnlyList<ICommand> commands)
        {
            foreach (ICommand cmd in commands)
            {
                try
                {
                    if (cmd is InputPropertiesGetCommand)
                    {
                        if (_loadExternalInputCommands)
                        {
                            HandleInputPropertiesGetCommand((InputPropertiesGetCommand)cmd);
                        }
                    }
                    else if (cmd is TallyByInputCommand)
                    {
                        HandleTallyByInputCommand((TallyByInputCommand)cmd);
                    }
                } 
                catch (Exception e)
                {
                    if (ErrorMessageChanged != null)
                    {
                        ErrorMessageChanged(e.Message);
                    }
                }
            }

            if (ExternalInputsLoaded != null && _loadExternalInputCommands)
            {
                ExternalInputsLoaded(_externalInputsCommands);
            }

            _loadExternalInputCommands = false;
        }

        private void HandleInputPropertiesGetCommand(InputPropertiesGetCommand cmd)
        {
            if (cmd.InternalPortType == LibAtem.Common.InternalPortType.External)
            {
                _externalInputsCommands.Add(cmd);
            }
        }

        private void HandleTallyByInputCommand(TallyByInputCommand cmd)
        {
            _currentTallyStatus = cmd;
            if (_lastTallyStatus == null)
            {
                _lastTallyStatus = cmd;
                if (TallyStatusChanged != null)
                {
                    TallyStatusChanged(_lastTallyStatus);
                }
                return;
            }

            if (CheckTallyStatusChange())
            {
                if (TallyStatusChanged != null)
                {
                    if (_lastTallyStatus != null)
                    {
                        TallyStatusChanged(_lastTallyStatus);
                    }
                }
            }
        }

        private bool CheckTallyStatusChange()
        {
            bool tallyChanged = false;

            if (_currentTallyStatus != null && _lastTallyStatus != null)
            {
                if (_lastTallyStatus.Tally.Count != _currentTallyStatus.Tally.Count)
                {
                    // If List sizes different, program bus had to change, we replace _last with _current
                    tallyChanged = true;
                    _lastTallyStatus?.Tally.Clear();

                    foreach (var item in _currentTallyStatus.Tally)
                    {
                        _lastTallyStatus?.Tally.Add(item);
                    }
                }
                else
                {
                    // If list sizes are the same we need to check element by element
                    for (int i = 0; i < _currentTallyStatus.Tally.Count; i++)
                    {
                        if (_lastTallyStatus.Tally[i].Item1 != _currentTallyStatus.Tally[i].Item1 
                            || _lastTallyStatus.Tally[i].Item2 != _currentTallyStatus.Tally[i].Item2)
                        {
                            tallyChanged = true;
                            _lastTallyStatus.Tally[i] = new Tuple<bool, bool>(_currentTallyStatus.Tally[i].Item1,
                                _currentTallyStatus.Tally[i].Item2);
                        }
                    }
                }
            }

            return tallyChanged;
        }
    }
}
