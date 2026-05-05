using CommunityToolkit.Maui;
using HybridTrackerApp.Services;
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
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "hybridtracker.db");
		builder.Services.AddSingleton<Services.DatabaseService>(s => ActivatorUtilities.CreateInstance<DatabaseService>(s,dbPath));
        //Unsure if required anymore but keeping it for now
        builder.Services.AddSingleton<ICalendarStore>(CalendarStore.Default);

        //ViewModels
        builder.Services.AddTransient<ViewModels.DashboardViewModel>();
		builder.Services.AddTransient<ViewModels.CalendarViewModel>();
		builder.Services.AddTransient<ViewModels.SettingsViewModel>();

        //Pages
        builder.Services.AddTransient<Views.DashboardPage>();
		builder.Services.AddTransient<Views.CalendarPage>();
		builder.Services.AddTransient<Views.SettingsPage>();

        return builder.Build();
	}
}
