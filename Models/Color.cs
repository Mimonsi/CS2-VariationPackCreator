using System.Text.Json;
using System.Text.Json.Serialization;

namespace VariationPackCreator.Models
{
    public class Color
    {
        /// <summary>
        /// Full hex string including alpha: #RRGGBBAA
        /// </summary>
        public string Hex
        {
            get => $"#{R:X2}{G:X2}{B:X2}{A:X2}";
            set
            {
                if (!value.StartsWith("#"))
                    throw new ArgumentException($"Invalid color format: must start with #");

                if (value.Length == 7)
                {
                    R = Convert.ToInt32(value.Substring(1, 2), 16);
                    G = Convert.ToInt32(value.Substring(3, 2), 16);
                    B = Convert.ToInt32(value.Substring(5, 2), 16);
                    A = 255;
                }
                else if (value.Length == 9)
                {
                    R = Convert.ToInt32(value.Substring(1, 2), 16);
                    G = Convert.ToInt32(value.Substring(3, 2), 16);
                    B = Convert.ToInt32(value.Substring(5, 2), 16);
                    A = Convert.ToInt32(value.Substring(7, 2), 16);
                }
                else
                {
                    throw new ArgumentException($"Invalid color format: length {value.Length} not supported, must be 7 (#RRGGBB) or 9 (#RRGGBBAA)");
                }
            }
        }

        /// <summary>
        /// RGB-only hex for the HTML color picker: #RRGGBB
        /// </summary>
        public string HexRgb
        {
            get => $"#{R:X2}{G:X2}{B:X2}";
            set
            {
                if (value.StartsWith("#") && value.Length == 7)
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
        public int A { get; set; } = 255;

        public bool HasAlpha => A < 255;

        /// <summary>
        /// Creates a deep copy of this color instance.
        /// </summary>
        public Color Clone() => new() { R = R, G = G, B = B, A = A };
    }


    public class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var colorString = reader.GetString();

            if (string.IsNullOrEmpty(colorString) || (colorString.Length != 6 && colorString.Length != 8))
            {
                Console.WriteLine($"Invalid color: {colorString}");
                return new Color();
            }

            try
            {
                if (colorString.Length == 8)
                {
                    return new Color
                    {
                        R = Convert.ToInt32(colorString.Substring(0, 2), 16),
                        G = Convert.ToInt32(colorString.Substring(2, 2), 16),
                        B = Convert.ToInt32(colorString.Substring(4, 2), 16),
                        A = Convert.ToInt32(colorString.Substring(6, 2), 16)
                    };
                }
                else
                {
                    return new Color
                    {
                        R = Convert.ToInt32(colorString.Substring(0, 2), 16),
                        G = Convert.ToInt32(colorString.Substring(2, 2), 16),
                        B = Convert.ToInt32(colorString.Substring(4, 2), 16)
                    };
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
            // Always include alpha channel in output
            writer.WriteStringValue($"{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}");
        }
    }
}