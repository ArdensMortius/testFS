using System.Drawing;

namespace testFS.Supports
{
    public static class ColorConverter
    {
        public static string ARGBtoHEX(int argb_color)
        {
            return Color.FromArgb(argb_color).ToHex();
        }
        public static int HEXtoARGB(string hex_color)
        {
            try
            {
                if (hex_color.StartsWith("#"))
                {
                    hex_color = hex_color.Substring(1);
                }
                return Int32.Parse(hex_color, System.Globalization.NumberStyles.HexNumber);
            }
            catch { return 0; }
        }

        public static string ToHex(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";
    }
}
