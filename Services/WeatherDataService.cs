using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using weather_app.Constants;
using weather_app.Model;

namespace weather_app.Services
{
    public class WeatherDataService:IWeatherDataService
    {      
        private string GetBaseRequestUri(string type)
        {
           return $"{ApiConstants.BaseUrl}{type}.json?key={ApiConstants.ApiKey}";            
        }

        public async Task<WeatherInfo> GetWeatherDataAsync(string query)
        {
            using (var httpClient = new HttpClient())
            {                   
                var response = await httpClient.GetStringAsync($"{GetBaseRequestUri("current")}&q={query}");
                var weatherData = JsonSerializer.Deserialize<WeatherInfo>(response);
                return weatherData;
            }
        }

        public async Task<WeatherInfo> GetWeatherForecastDataAsync(string location, int numberOfDayes)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"{GetBaseRequestUri("forecast")}&q={location}&days={numberOfDayes}");
                var weatherData = JsonSerializer.Deserialize<WeatherInfo>(response);
                return weatherData;
            }
        }
    }
}
