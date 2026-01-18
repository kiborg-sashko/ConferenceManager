using Microsoft.Extensions.Logging;
using ConferenceManager.Client.Services; 
using ConferenceManager.Client.ViewModels; 
using ConferenceManager.Client.Views; 

namespace ConferenceManager.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddSingleton<Auth0Client>();

        // Реєструємо ViewModel
        builder.Services.AddSingleton<MainViewModel>();

        // Реєструємо сторінки
        builder.Services.AddSingleton<MainPage>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
