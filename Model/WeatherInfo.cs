using System.Text.Json.Serialization;
using weather_app.Converters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace weather_app.Model
{ 
    public class WeatherInfo
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("current")]
        public Current Current { get; set; }
        [JsonPropertyName("forecast")]
        public Forecast Forecast { get; set; }
    }

    public class Current
    {
     
        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }
        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }
        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }   
        [JsonPropertyName("wind_kph")]
        public double Wind { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
        [JsonPropertyName("feelslike_c")]
        public double Feelslike { get; set; }
        [JsonPropertyName("uv")]
        public double UvIndex { get; set; }
        [JsonPropertyName("vis_km")]
        public double Visibility { get; set; }
        [JsonPropertyName("precip_mm")]
        public double Precipitation { get; set; }
        


    }

    public class Condition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("code")]
        [JsonConverter(typeof(WeatherDescriptionConverter))]
        public ConditionCode Code { get; set; }

        public string WeatherImage => Code switch
        {
            
            ConditionCode.Clear => "clear_day_icon.png",
            ConditionCode.PartlyCloudy => "fewclouds_day_icon.png",
            ConditionCode.Cloudy => "cloudy_day_icon.png",
            ConditionCode.Rain => "rain_day_icon.png",
            ConditionCode.LightRain => "rain_day_icon.png",
            ConditionCode.Storm => "storm_day_icon.png",
            _ => "fewclouds_day_icon.png",
        };
    }

    public class Location
    {
        [JsonPropertyName("name")]
        public string City { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("localtime")]
        public string LocalTime {  get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName ("forecastday")]
        public List<Forecastday> Forecasts { get; set; }
    }

    public class Forecastday 
    {
        [JsonPropertyName("date")]
        public string Data {  get; set; }
        [JsonPropertyName("day")]
        public Day Day { get; set; }

        public string FormattedDate
        {
            get
            {
                DateTime forecastDate = DateTime.Parse(Data);
                DateTime today = DateTime.Today;

                if (forecastDate.Date == today)
                {
                    return "Today";
                }
                else if (forecastDate.Date == today.AddDays(1))
                {
                    return "Tomorrow";
                }
                else
                {
                    return forecastDate.ToString("MMM d") + "th"; // e.g., "Nov 30th"
                }
            }
        }
    }

    public class Day
    {
        [JsonPropertyName ("maxtemp_c")]
        public double MaxTemp { get; set; }
        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

    }
}