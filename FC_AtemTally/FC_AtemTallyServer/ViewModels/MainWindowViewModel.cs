using System.ComponentModel;
using System.Windows.Input;
using FC_AtemTallyServer.Commands;
using FC_AtemTallyServer.Services;


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

        #endregion

        #region Commands

        public ICommand ConnectToAtemCmd { get; set; }

        #endregion

        private IAtemDiscoveryService _atemDiscoveryService;

        public MainWindowViewModel()
        {
            // Services
            _atemDiscoveryService = new AtemDiscoveryService();

            // Commands
            ConnectToAtemCmd = new ConnectToAtemCommand(this, _atemDiscoveryService);
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
