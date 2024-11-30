using Microsoft.Extensions.Logging;
using weather_app.Services;
using weather_app.ViewModels;
using weather_app.Views;

namespace weather_app
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            RegisterServices(builder.Services);
            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //Register Services
            services.AddSingleton<IWeatherDataService, WeatherDataService>();

            //Register ViewModels
            
            services.AddSingleton<HomePageViewModel>();
           
            services.AddSingleton<HomePage>();
        }
    }

}
