using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using weather_app.Model;
using weather_app.Services;
using weather_app.ViewModels.Base;
using weather_app.Views;

namespace weather_app.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        private readonly IWeatherDataService _weatherService;
        private readonly ILocationService _locationService;
    
        private string getWeatherIcon(ConditionCode CurrentCondition, bool isDay)
        {
            switch (CurrentCondition)
            {
                case ConditionCode.Clear:
                    return isDay ? "clear_day_icon.png" : "clear_night_icon.png";
                case ConditionCode.PartlyCloudy:
                    return isDay ? "fewclouds_day_icon.png" : "fewclouds_night_icon.png";
                case ConditionCode.Cloudy:
                    return isDay ? "cloudy_day_icon.png" : "cloudy_night_icon.png";
                case ConditionCode.Rain:
                    return isDay ? "rain_day_icon.png" : "rain_night_icon.png";
                case ConditionCode.LightRain:
                    return isDay ? "rain_day_icon.png" : "rain_night_icon.png";
                case ConditionCode.Storm:
                    return isDay ? "storm_day_icon.png" : "storm_night_icon.png";
                default:
                    return "";
            }
        }
        private string getWeatherBackgroundImage(ConditionCode CurrentCondition, bool isDay)
        {
            switch (CurrentCondition)
            {
                case ConditionCode.Clear:
                    return isDay ? "clear_day.png" : "clear_night.png";                   
                case ConditionCode.PartlyCloudy:
                    return isDay ? "fewclouds_day.png" : "fewclouds_night.png";
                case ConditionCode.Cloudy:
                    return isDay ? "cloudy_day.png" : "cloudy_night.png";
                case ConditionCode.Rain:
                    return isDay ? "rain_day.png" : "rain_night.png";
                case ConditionCode.LightRain:
                    return isDay ? "rain_day.png" : "rain_night.png";
                case ConditionCode.Storm:
                    return isDay ? "storm_day.png" : "storm_night.png";
                default:
                    return "";
            }            
        }
        [ObservableProperty]
        public string temperature;
        [ObservableProperty]
        public string locationName;
        [ObservableProperty]
        public string locationDate;
        [ObservableProperty]
        public string condition;
        [ObservableProperty]
        public string wind;
        [ObservableProperty]
        public string humidity;
        [ObservableProperty]
        public string feelslike;
        [ObservableProperty]
        public string uvIndex;
        [ObservableProperty]
        public string visibility;
        [ObservableProperty]
        public string precipitation;
        [ObservableProperty]
        public string weatherImage;
        [ObservableProperty]
        public string weatherBackgroundImage;
        public HomePageViewModel(IWeatherDataService weatherService, ILocationService locationService, IConnectivityService connectivityService, IToastMessageService toastMessageService) : base(connectivityService, toastMessageService)
        {
            _weatherService = weatherService;
            _locationService = locationService;
            DoPerformSearch(string.Empty);
        }

        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            await DoPerformSearch(query);
        });
        
        private async Task DoPerformSearch(string query)
        {
            try
            {                
                var position = await _locationService.GetCurrentLocationAsync();

                if (string.IsNullOrEmpty(query) && position != null)
                {
                    query = $"{position.Latitude},{position.Longitude}";
                }

                var weatherData = await _weatherService.GetWeatherDataAsync(query);

                Temperature = $"{weatherData.Current.TempC} °C";
                LocationName = $"{weatherData.Location.City}, {weatherData.Location.Country}";
                LocationDate = weatherData.Location.LocalTime;
                Condition = weatherData.Current.Condition.Text;
                Wind = $"{weatherData.Current.Wind} Km/h";
                Humidity = $"{weatherData.Current.Humidity} %";
                Feelslike = $"{weatherData.Current.Feelslike} °C";
                UvIndex = weatherData.Current.UvIndex.ToString();
                Visibility = weatherData.Current.Visibility.ToString();
                Precipitation = $"{weatherData.Current.Precipitation} %";
                WeatherImage = getWeatherIcon(weatherData.Current.Condition.Code, weatherData.Current.IsDay == 1);
                WeatherBackgroundImage = getWeatherBackgroundImage(weatherData.Current.Condition.Code, weatherData.Current.IsDay == 1);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Location not found.", "OK");
            }

        }


        [RelayCommand]
        private async Task DailyForecast5Days()
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherForecastDataAsync(LocationName.Split(',').First(), 5);
                Dictionary<string, object> daysParameter = new()
                {
                    [nameof(weatherData.Forecast)] = weatherData.Forecast
                };

                await Shell.Current.GoToAsync(
                    state: nameof(DailyForecastPage),
                    animate: true,
                    parameters: daysParameter);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }           
        }
    }
}
