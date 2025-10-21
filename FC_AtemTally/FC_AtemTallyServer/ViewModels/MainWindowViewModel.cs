using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

using FC_AtemTallyServer.Commands;
using FC_AtemTallyServer.Services;
using FC_AtemTallyServer.ViewModels.Controls;


namespace FC_AtemTallyServer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _AtemIpAddress = "192.168.1.100";
        public string AtemIpAddress
        {
            set
            {
                if (_AtemIpAddress != value)
                {
                    _AtemIpAddress = value;
                    OnPropertyChanged(nameof(AtemIpAddress));
                }
            }
            get => _AtemIpAddress;
        }

        private string _AtemConnectionStatusMessage = "Not connected";
        public string AtemConnectionStatusMessage
        {
            set
            {
                if (_AtemConnectionStatusMessage != value)
                {
                    _AtemConnectionStatusMessage = value;
                    OnPropertyChanged(nameof(AtemConnectionStatusMessage));
                }
            }
            get => _AtemConnectionStatusMessage;
        }

        public string ApplicationVersion
        {
            get
            {
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                if (version != null)
                {
                    return version.ToString();
                }
                return "N/A";
            }
        }

        public ObservableCollection<AtemInputControlViewModel> AtemInputsVMs { get; set; }

        #endregion

        #region Commands

        public ICommand ConnectToAtemCmd { get; set; }

        #endregion

        private IAtemControlService _atemControlService;

        public MainWindowViewModel(IAtemControlService atemControlService)
        {

            // Services
            _atemControlService = atemControlService;

            // Collections
            AtemInputsVMs = new ObservableCollection<AtemInputControlViewModel>();

            // Commands
            ConnectToAtemCmd = new ConnectToAtemCommand(this, _atemControlService);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
