using Trackerino.App.Shells;

namespace Trackerino.App
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.MinimumHeight = 720;
            window.MinimumWidth = 1080;
            window.Title = "Trackerino";
            return window;
        }
    }
}