using System;

namespace BikeBook.Views
{


    /**
     * Singleton holder for colors used throughout the application
     */
    public class UIColors
    {
        public const string COLOR_PRIMARY_TEXT = "#FFFFFF";
        public const string COLOR_PLACEHOLDER_TEXT = "#4c4d61";
        public const string COLOR_SECONDARY_TEXT = "#8a8989";
        public const string COLOR_TERTIARY_TEXT = "#474759";
        public const string COLOR_DEFAULT_BUTTON_BACKGROUND = "#1B8DED";
        public const string COLOR_WIDGET_HIGHLIGHT = "#1BEDE4";
        public const string COLOR_FACEBOOK_BLUE = "#3B5998";
        public const string COLOR_PAGE_BACKGROUND = "#272731";
        public const string COLOR_GENERAL_ACCENT = "#3F3E50";
        public const string COLOR_NESTED_ACCENT_BACKGROUND = "#48485B";
        public const string COLOR_SUBMIT_BUTTON_BACKGROUND = "#ED1B24";



        /**
         * Converts a Hex color value to an android-preferred ARGB color.
         * 
         * @param int hex - the hex color to convert
         * @param float opacity - the desired opacity (alpha) of the resulting color, constrained between 0 and 1
         */
        public static int HexToARGB(string hex, float transparency = 1)
        {
            int alpha = (int)(0xFF * Math.Max(Math.Min(transparency, 1), 0))<<24;
            int hexAsInt = Int32.Parse(hex.Replace('#',' '), System.Globalization.NumberStyles.HexNumber);
            return alpha | hexAsInt;
        }
    }
}
