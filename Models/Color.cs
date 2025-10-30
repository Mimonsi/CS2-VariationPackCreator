using System.Text.Json;
using System.Text.Json.Serialization;

namespace VariationPackCreator.Models
{
    public class Color
    {
        public string Hex
        {
            get => $"#{R:X2}{G:X2}{B:X2}{A:X2}";
            set
            {
                // Hier wird der Hex-Wert geparsed und die RGB-Werte gesetzt.
                if (value.StartsWith("#"))
                {
                    if (value.Length == 7)
                    {
                        R = Convert.ToInt32(value.Substring(1, 2), 16);
                        G = Convert.ToInt32(value.Substring(3, 2), 16);
                        B = Convert.ToInt32(value.Substring(5, 2), 16);
                    }
                    else if (value.Length == 9)
                    {
                        R = Convert.ToInt32(value.Substring(1, 2), 16);
                        G = Convert.ToInt32(value.Substring(3, 2), 16);
                        B = Convert.ToInt32(value.Substring(5, 2), 16);
                        A = Convert.ToInt32(value.Substring(7, 2), 16);
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid color format: Length {value.Length} is not supported, must be 7 or 9");
                }
            }
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; } = 255;
    }


    public class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var colorString = reader.GetString();

            if (string.IsNullOrEmpty(colorString) || colorString.Length != 6)
            {
                Console.WriteLine($"Invalid color: {colorString}");
                return new Color();
            }

            try
            {
                // Additional alpha channel is supported
                if (colorString.Length == 8)
                {
                    var colorParts = new[]
                    {
                        Convert.ToInt32(colorString.Substring(0, 2), 16),
                        Convert.ToInt32(colorString.Substring(2, 2), 16),
                        Convert.ToInt32(colorString.Substring(4, 2), 16),
                        Convert.ToInt32(colorString.Substring(6, 2), 16)
                    };
                    return new Color { R = colorParts[0], G = colorParts[1], B = colorParts[2], A = colorParts[3]};
                }
                else
                {
                    var colorParts = new[]
                    {
                        Convert.ToInt32(colorString.Substring(0, 2), 16),
                        Convert.ToInt32(colorString.Substring(2, 2), 16),
                        Convert.ToInt32(colorString.Substring(4, 2), 16),
                    };
                    return new Color { R = colorParts[0], G = colorParts[1], B = colorParts[2]};
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing color {colorString}: {ex.Message}");
                return new Color();
            }
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            // Write hex without #
            writer.WriteStringValue(value.Hex.Replace("#", ""));
        }
    }

}