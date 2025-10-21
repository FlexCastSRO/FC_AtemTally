using System.ComponentModel;


namespace FC_AtemTallyServer.ViewModels.Controls
{
    public class AtemInputControlViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _InputNumber = string.Empty;
        public string InputNumber
        {
            set
            {
                if (_InputNumber != value)
                {
                    _InputNumber = value;
                    OnPropertyChanged(nameof(InputNumber));
                }
            }
            get => _InputNumber;
        }

        private string _ShortName = string.Empty;
        public string ShortName
        {
            set
            {
                if (_ShortName != value)
                {
                    _ShortName = value;
                    OnPropertyChanged(nameof(ShortName));
                }
            }
            get => _ShortName;
        }

        private string _LongName = string.Empty;
        public string LongName
        {
            set
            {
                if (_LongName != value)
                {
                    _LongName = value;
                    OnPropertyChanged(nameof(LongName));
                }
            }
            get => _LongName;
        }

        private bool _ProgramOn;
        public bool ProgramOn
        {
            set
            {
                if (_ProgramOn != value)
                {
                    _ProgramOn = value;
                    OnPropertyChanged(nameof(ProgramOn));
                }
            }
            get => _ProgramOn;
        }

        private bool _PreviewOn;
        public bool PreviewOn
        {
            set
            {
                if (_PreviewOn != value)
                {
                    _PreviewOn = value;
                    OnPropertyChanged(nameof(PreviewOn));
                }
            }
            get => _PreviewOn;
        }

        #endregion

        public AtemInputControlViewModel()
        {

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
