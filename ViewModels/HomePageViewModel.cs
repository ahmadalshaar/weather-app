using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using weather_app.Model;
using weather_app.Services;
using weather_app.ViewModels.Base;

namespace weather_app.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        private readonly IWeatherDataService _weatherService;

        private ConditionCode _currentCondition;
        private bool _isDay;
        

        public string WeatherImage => _currentCondition switch
        {
            ConditionCode.Clear => _isDay ? "clear_day_icon.png": "clear_night_icon.png",
            ConditionCode.PartlyCloudy => _isDay ? "fewclouds_day_icon.png" : "fewclouds_night_icon.png",
            ConditionCode.Cloudy => _isDay ? "cloudy_day_icon.png" : "cloudy_night_icon.png",          
            ConditionCode.Rain => _isDay ? "rain_day_icon.png" : "rain_night_icon.png",            
            ConditionCode.LightRain => _isDay ? "rain_day_icon.png" : "rain_night_icon.png",
            ConditionCode.Storm => _isDay ? "storm_day_icon.png" : "storm_night_icon.png",            
            _ => _isDay ? "fewclouds_day_icon.png" : "fewclouds_night_icon.png",
        };
   
        public string WeatherBackgroundImage => _currentCondition switch
        {
            ConditionCode.Clear => _isDay ? "clear_day.png" : "clear_night.png",
            ConditionCode.PartlyCloudy => _isDay ? "fewclouds_day.png" : "fewclouds_night.png",
            ConditionCode.Cloudy => _isDay ? "cloudy_day.png" : "cloudy_night.png",
            ConditionCode.Rain => _isDay ? "rain_day.png" : "rain_night.png",
            ConditionCode.LightRain => _isDay ? "rain_day.png" : "rain_night.png",
            ConditionCode.Storm => _isDay ? "storm_day.png" : "storm_night.png",
            _ => _isDay ? "fewclouds_day.png" : "fewclouds_night.png",
        };

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

        public HomePageViewModel(IWeatherDataService weatherService)
        {
            _weatherService = weatherService;
        }


        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            var IsLoading = true;
            var IsWeatherCardVisible = false;

            try
            {
                var weatherData = await _weatherService.GetWeatherDataAsync();

                temperature = $"{weatherData.Current.TempC} °C";
                locationName = $"{weatherData.Location.City}, {weatherData.Location.Country}";
                locationDate = weatherData.Location.LocalTime;
                condition = weatherData.Current.Condition.Text;
                wind = $"{weatherData.Current.Wind} Km/h";
                humidity = $"{weatherData.Current.Humidity} %";
                feelslike = $"{weatherData.Current.Feelslike} °C";
                uvIndex = weatherData.Current.UvIndex.ToString();
                visibility = weatherData.Current.Visibility.ToString();
                precipitation = $"{weatherData.Current.Precipitation} %";


                _isDay = weatherData.Current.IsDay != 0;
                _currentCondition = weatherData.Current.Condition.Code;


            }
            catch (Exception ex)
            {
                // Handle errors (e.g., show an alert)
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        });
    }
}
