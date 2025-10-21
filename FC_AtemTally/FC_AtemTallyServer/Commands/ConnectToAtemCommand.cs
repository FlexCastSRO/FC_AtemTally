using LibAtem.Commands;
using LibAtem.Commands.Settings;

using FC_AtemTallyServer.Services;
using FC_AtemTallyServer.ViewModels;


namespace FC_AtemTallyServer.Commands
{
    class ConnectToAtemCommand : System.Windows.Input.ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { System.Windows.Input.CommandManager.RequerySuggested += value; }
            remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
        }

        private readonly MainWindowViewModel _viewModel;
        private readonly IAtemDiscoveryService _atemDiscoveryService;

        private bool _atemConnected;

        public ConnectToAtemCommand(MainWindowViewModel viewModel, IAtemDiscoveryService atemDiscoveryService)
        {
            _viewModel = viewModel;
            _atemDiscoveryService = atemDiscoveryService;
        }

        public bool CanExecute(object? parameter)
        {
            return !_atemConnected;
        }

        public void Execute(object? parameter)
        {
            _atemDiscoveryService.Init(_viewModel.AtemIpAddress);
            _atemDiscoveryService.AtemConnected = AtemConnectedHandler;
            _atemDiscoveryService.AtemDisconnected = AtemDisconnectedHandler;
            _atemDiscoveryService.ExternalInputsLoaded = ExternalInputsLoadedHandler;
            _atemDiscoveryService.TallyStatusChanged += TallyStatusChangedHandler;
            _atemDiscoveryService.ErrorMessageChanged += ErrorMessageChangedHandler;
            _atemDiscoveryService.Connect();
        }

        private void AtemConnectedHandler()
        {
            _viewModel.AtemConnectionStatusMessage = "Atem (" + _viewModel.AtemIpAddress + ") connected.";
            _atemConnected = true;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        private void AtemDisconnectedHandler()
        {
            _viewModel.AtemConnectionStatusMessage = "Atem disconnected!";
            _atemConnected = false;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        private void ExternalInputsLoadedHandler(List<InputPropertiesGetCommand> externalInputsCommands)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
            (Action)(() =>
            {
                foreach (var item in externalInputsCommands)
                {

                    _viewModel.AtemInputsVMs.Add(
                        new ViewModels.Controls.AtemInputControlViewModel()
                        {
                            InputNumber = item.Id.ToString(),
                            ShortName = item.ShortName,
                            LongName = item.LongName
                        }
                   );
                }
            }));
        }

        private void TallyStatusChangedHandler(TallyByInputCommand tallyInputCommand)
        {
            for (int i = 0; i < _viewModel.AtemInputsVMs.Count; i++)
            {
                _viewModel.AtemInputsVMs[i].ProgramOn = tallyInputCommand.Tally[i].Item1;
                _viewModel.AtemInputsVMs[i].PreviewOn = tallyInputCommand.Tally[i].Item2;
            }
        }

        private void ErrorMessageChangedHandler(string errorMessage)
        {
            for (int i = 0; i < _viewModel.AtemInputsVMs.Count; i++)
            {
                _viewModel.AtemConnectionStatusMessage = errorMessage;
            }
        }
    }
}
