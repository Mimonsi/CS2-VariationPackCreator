namespace VariationPackCreator.models;

public class Color
{
    public string Hex => $"#{R:X2}{G:X2}{B:X2}";

    public int R;
    public int G;
    public int B;
}