using Microsoft.Extensions.Logging;

namespace Trackerino.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inter-Bold.ttf", "InterBold");
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}