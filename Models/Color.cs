using System.Text.Json;
using System.Text.Json.Serialization;

namespace VariationPackCreator.Models
{
    public class Color
    {
        public string Hex
        {
            get => $"#{R:X2}{G:X2}{B:X2}";
            set
            {
                // Hier wird der Hex-Wert geparsed und die RGB-Werte gesetzt.
                if (value.Length == 7 && value.StartsWith("#"))
                {
                    R = Convert.ToInt32(value.Substring(1, 2), 16);
                    G = Convert.ToInt32(value.Substring(3, 2), 16);
                    B = Convert.ToInt32(value.Substring(5, 2), 16);
                }
            }
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }


    // Der ColorConverter sollte in diesem Namensraum sein
    public class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var colorString = reader.GetString();

            if (string.IsNullOrEmpty(colorString) || colorString.Length != 7 || colorString[0] != '#')
            {
                throw new JsonException("Invalid color string format");
            }

            var colorParts = new[]
            {
                Convert.ToInt32(colorString.Substring(1, 2), 16),
                Convert.ToInt32(colorString.Substring(3, 2), 16),
                Convert.ToInt32(colorString.Substring(5, 2), 16)
            };

            return new Color { R = colorParts[0], G = colorParts[1], B = colorParts[2] };
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Hex);
        }
    }
}