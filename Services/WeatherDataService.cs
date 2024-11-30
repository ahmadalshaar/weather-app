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
        private string GetRequestUri()
        {
           return $"{ApiConstants.BaseUrl}?key={ApiConstants.ApiKey}&q=Cairo";            
        }

        public async Task<WeatherInfo> GetWeatherDataAsync()
        {
            using (var httpClient = new HttpClient())
            {                   
                var response = await httpClient.GetStringAsync(GetRequestUri());
                var weatherData = JsonSerializer.Deserialize<WeatherInfo>(response);
                return weatherData;
            }
        }        
    }
}
