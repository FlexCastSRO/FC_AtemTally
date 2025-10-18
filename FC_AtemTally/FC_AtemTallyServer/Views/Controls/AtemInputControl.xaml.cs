using System.Windows.Controls;

using FC_AtemTallyServer.ViewModels.Controls;


namespace FC_AtemTallyServer.Views.Controls
{
    /// <summary>
    /// Interaction logic for AtemInputControl.xaml
    /// </summary>
    public partial class AtemInputControl : UserControl
    {
        public AtemInputControl(AtemInputControlViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
