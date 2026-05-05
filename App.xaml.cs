using HybridTrackerApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HybridTrackerApp;

public partial class App : Application
{
	public static DatabaseService DbService { get; private set; }

	public App(DatabaseService DatabaseService)
	{
		InitializeComponent();
        // The dependency injection process will automatically resolve the DatabaseService param and pass it to the constructor
        DbService = DatabaseService;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}