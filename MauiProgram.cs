using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using weather_app.Services;
using weather_app.ViewModels;
using weather_app.Views;
using CommunityToolkit.Maui;

namespace weather_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            RegisterServices(builder.Services);
            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //Register Services
            services.AddSingleton<IWeatherDataService, WeatherDataService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IConnectivityService, ConnectivityService>();
            services.AddSingleton<IToastMessageService, ToastMessage>();
            //Register ViewModels
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton<DailyForecastPageViewModle>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<DailyForecastPage>();
        }
    }
}