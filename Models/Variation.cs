namespace VariationPackCreator.Models
{
    public class Variation
    {
        private Color _color1;
        private Color _color2;
        private Color _color3;

        public Color Color1
        {
            get => _color1 ??= new Color(); // Fallback auf eine leere Farbe, wenn null
            set => _color1 = value;
        }

        public Color Color2
        {
            get => _color2 ??= new Color();
            set => _color2 = value;
        }

        public Color Color3
        {
            get => _color3 ??= new Color();
            set => _color3 = value;
        }

        public int Probability { get; set; }
    }
}