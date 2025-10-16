using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using FC_AtemTallyClient.ViewModels;
using FC_AtemTallyClient.Views;


namespace FC_AtemTallyClient
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
