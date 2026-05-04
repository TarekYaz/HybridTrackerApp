using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.CalendarStore;

namespace HybridTrackerApp;

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
			}).UseMauiCommunityToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		//Services
		builder.Services.AddTransient<Views.CalendarPage>();
        builder.Services.AddSingleton<ICalendarStore>(CalendarStore.Default);

		//ViewModels
		builder.Services.AddTransient<ViewModels.DashboardViewModel>();

		//Pages
		builder.Services.AddTransient<Views.DashboardPage>();

        return builder.Build();
	}
}
