using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using FC_AtemTallyServer.ViewModels;
using FC_AtemTallyServer.Views;


namespace FC_AtemTallyServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

    public static class ServiceColletionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }

}
