using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Trackerino.App.Services;
using Trackerino.BL;

[assembly: System.Resources.NeutralResourcesLanguage("en")]
namespace Trackerino.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFont("Inter-Bold.ttf", "InterBold");
            });

        builder.Services
            .AddDALServices()
            .AddAppServices()
            .AddBLServices();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        try
        {
            app.Services.GetRequiredService<IDbMigrator>().Migrate();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }

    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
}
