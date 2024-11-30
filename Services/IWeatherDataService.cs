﻿using weather_app.Model;

namespace weather_app.Services
{
    public interface IWeatherDataService
    {
        Task<WeatherInfo> GetWeatherDataAsync();
    }
}
