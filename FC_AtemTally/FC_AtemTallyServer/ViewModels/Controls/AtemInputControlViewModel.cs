using System.ComponentModel;


namespace FC_AtemTallyServer.ViewModels.Controls
{
    public class AtemInputControlViewModel : INotifyPropertyChanged
    {
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
