using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Trackerino.BL;
using Microsoft.EntityFrameworkCore.Migrations;
using Trackerino.App.Services;

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
                fonts.AddFont("Inter-Bold.ttf", "InterBold");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
            });


        builder.Services
            .AddAppServices()
            .AddDALServices()
            .AddBLServices();
        var app = builder.Build();

        app.Services.GetRequiredService<IDbMigrator>().Migrate();
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