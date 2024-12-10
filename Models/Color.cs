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


    public class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var colorString = reader.GetString();

            // Prüfen, ob der String eine gültige Länge hat (6 Zeichen für RGB-Hex-Werte)
            if (string.IsNullOrEmpty(colorString) || colorString.Length != 6)
            {
                Console.WriteLine($"Ungültiger Farbwert: {colorString}");
                return new Color(); // Rückgabe eines leeren Farbobjekts bei ungültigem Wert
            }

            try
            {
                // Umwandlung der Hex-Werte in RGB
                var colorParts = new[]
                {
                    Convert.ToInt32(colorString.Substring(0, 2), 16),
                    Convert.ToInt32(colorString.Substring(2, 2), 16),
                    Convert.ToInt32(colorString.Substring(4, 2), 16)
                };

                return new Color { R = colorParts[0], G = colorParts[1], B = colorParts[2] };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Parsen der Farbe {colorString}: {ex.Message}");
                return new Color(); // Rückgabe eines Default-Werts im Fehlerfall
            }
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            // Write hex without #
            writer.WriteStringValue(value.Hex.Replace("#", ""));
        }
    }

}