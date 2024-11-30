using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using weather_app.Model;

namespace weather_app.Converters
{
    public class WeatherDescriptionConverter : JsonConverter<ConditionCode>
    {
        // Dictionary mapping weather descriptions to their codes
        private static readonly Dictionary<ConditionCode, List<int>> WeatherCodes = new Dictionary<ConditionCode, List<int>>
    {
        { ConditionCode.Clear, new List<int> { 1000 } },
        { ConditionCode.PartlyCloudy, new List<int> { 1003 } },
        { ConditionCode.Cloudy, new List<int> { 1006, 1009, 1135, 1147 } },
        { ConditionCode.Rain, new List<int> { 1063, 1180, 1183, 1186, 1189, 1192, 1195, 1240, 1243, 1246 } },
        { ConditionCode.LightRain, new List<int> { 1150, 1153, 1180, 1183 } },
        { ConditionCode.Storm, new List<int> { 1087, 1273, 1276, 1279, 1282 } }
    };

        // Reverse mapping for quick access
        private static readonly Dictionary<int, ConditionCode> CodeToCondition;

        static WeatherDescriptionConverter()
        {
            CodeToCondition = new Dictionary<int, ConditionCode>();
            foreach (var kvp in WeatherCodes)
            {
                foreach (var code in kvp.Value)
                {
                    CodeToCondition[code] = kvp.Key;
                }
            }
        }

        public override ConditionCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the integer value from JSON
            if (reader.TokenType == JsonTokenType.Number)
            {
                int code = reader.GetInt32();
                if (CodeToCondition.TryGetValue(code, out var condition))
                {
                    return condition;
                }
            }

            throw new JsonException($"Unknown weather code: {reader.GetInt32()}");
        }

        public override void Write(Utf8JsonWriter writer, ConditionCode value, JsonSerializerOptions options)
        {
            // Write the integer value of the enum
            writer.WriteNumberValue((int)value);
        }
    }
}
