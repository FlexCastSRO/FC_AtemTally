using System.ComponentModel;

using FC_AtemTallyServer.Services;


namespace FC_AtemTallyServer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }

        private string _ProgramInputValue = string.Empty;
        public string ProgramInputValue
        {
            set
            {
                if (_ProgramInputValue != value)
                {
                    _ProgramInputValue = value;
                    OnPropertyChanged(nameof(ProgramInputValue));
                }
            }
            get => _ProgramInputValue;
        }

        private string _PreviewInputValue = string.Empty;
        public string PreviewInputValue
        {
            set
            {
                if (_PreviewInputValue != value)
                {
                    _PreviewInputValue = value;
                    OnPropertyChanged(nameof(PreviewInputValue));
                }
            }
            get => _PreviewInputValue;
        }

        private const string _applicationName = "Atem Tally Server";

        AtemDiscoveryService _service;

        public MainWindowViewModel()
        {
            Title = _applicationName;
            _service = new AtemDiscoveryService();
            _service.ProgramChanged = UpdateProgramInputValue;
            _service.PreviewChanged = UpdatePreviewInputValue;
            _service.Start();
        }

        private void UpdateProgramInputValue()
        {
            ProgramInputValue = _service.ProgramInputs;
        }

        private void UpdatePreviewInputValue()
        {
            PreviewInputValue = _service.PreviewInputs;
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
