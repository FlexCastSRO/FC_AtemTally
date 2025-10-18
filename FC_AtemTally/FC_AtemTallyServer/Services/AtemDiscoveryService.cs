using LibAtem.Net;
using LibAtem.Commands;
using LibAtem.Commands.MixEffects;
using LibAtem.Commands.Settings;


namespace FC_AtemTallyServer.Services
{
    internal class AtemDiscoveryService : IAtemDiscoveryService
    {

        private int _ExternalInputsCount = 0;
        public int ExternalInputsCount
        {
            get => _ExternalInputsCount;
            set
            {
                if (_ExternalInputsCount != value)
                {
                    _ExternalInputsCount = value;
                    if (ExternalInputsCountChanged != null)
                    {
                        ExternalInputsCountChanged();
                    }
                }
            }
        }

        private string _ProgramInputs = string.Empty;
        public string ProgramInputs
        {
            get => _ProgramInputs;
            set
            {
                if (_ProgramInputs != value)
                {
                    _ProgramInputs = value;
                    if (ProgramChanged != null)
                    {
                        ProgramChanged();
                    }
                }
            }
        }

        private string _PreviewInputs = string.Empty;
        public string PreviewInputs
        {
            get => _PreviewInputs;
            set
            {
                if (_PreviewInputs != value)
                {
                    _PreviewInputs = value;
                    if (PreviewChanged != null)
                    {
                        PreviewChanged();
                    }
                }
            }
        }


        public Action? ExternalInputsCountChanged { get; set; }
        public Action? ProgramChanged { get; set; }
        public Action? PreviewChanged { get; set; }
        public Action? AtemConnected { get; set; }
        public Action? AtemDisconnected { get; set; }


        private AtemClient? _client;

        public void Init(string atemIpAddress)
        {
            _client = new AtemClient(atemIpAddress, false);
            _client.OnConnection += (object sender) => 
            {
                if (AtemConnected != null)
                {
                    AtemConnected();
                }
            };

            _client.OnDisconnect += (object sender) =>
            {
                if (AtemDisconnected != null)
                {
                    AtemDisconnected();
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
            int externalInputsCount = 0;

            foreach (ICommand cmd in commands)
            {
                try
                {
                    if (cmd is InputPropertiesGetCommand)
                    {
                        HandleInputPropertiesGetCommand(ref externalInputsCount, (InputPropertiesGetCommand)cmd);
                    }
                    else if (cmd is ProgramInputGetCommand)
                    {
                        HandleProgramInputGetCommand((ProgramInputGetCommand)cmd);
                    }
                    else if (cmd is PreviewInputGetCommand)
                    {
                        HandlePreviewInputGetCommand((PreviewInputGetCommand)cmd);
                    }
                } 
                catch (Exception e)
                {
                    // TODO: ERROR HANDLING
                }
            }

            if (externalInputsCount > 0)
            {
                ExternalInputsCount = externalInputsCount;
            }
        }

        private void HandleInputPropertiesGetCommand(ref int externalInputsCount, InputPropertiesGetCommand cmd)
        {
            if (cmd.InternalPortType == LibAtem.Common.InternalPortType.External)
            {
                externalInputsCount++;
            }
        }

        private void HandleProgramInputGetCommand(ProgramInputGetCommand cmd)
        {
            ProgramInputs = cmd.Source.ToString();
        }

        private void HandlePreviewInputGetCommand(PreviewInputGetCommand cmd)
        {
            PreviewInputs = cmd.Source.ToString();
        }
    }
}
