using System.Text.Json.Serialization;
using weather_app.Converters;

namespace weather_app.Model
{ 
    public class WeatherInfo
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("current")]
        public Current Current { get; set; }
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



}