using System.Windows.Input;

using FC_AtemTallyServer.Services;
using FC_AtemTallyServer.ViewModels;


namespace FC_AtemTallyServer.Commands
{
    class ConnectToAtemCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
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
            _atemDiscoveryService.Connect();
        }

        private void AtemConnectedHandler()
        {
            _viewModel.AtemConnectionStatusMessage = "Atem (" + _viewModel.AtemIpAddress + ") connected.";
            _atemConnected = true;
            CommandManager.InvalidateRequerySuggested();
        }

        private void AtemDisconnectedHandler()
        {
            _viewModel.AtemConnectionStatusMessage = "Atem disconnected!";
            _atemConnected = false;
            CommandManager.InvalidateRequerySuggested();
        }

    }
}
