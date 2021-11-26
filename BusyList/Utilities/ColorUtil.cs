using System;
using Xamarin.Forms;

namespace BusyList.Utilities
{
    /// <summary>
    /// Utility class filled with helper functions
    /// </summary>
    public static class ColorUtil
    {
        /// <summary>
        /// Color array filled with only flat ui colors
        /// </summary>
        private static readonly Color[] _colors = new Color[] {
            Color.FromHex("#fc5c65"),
            Color.FromHex("#eb3b5a"),
            Color.FromHex("#fd9644"),
            Color.FromHex("#fa8231"),
            Color.FromHex("#fed330"),
            Color.FromHex("#f7b731"),
            Color.FromHex("#26de81"),
            Color.FromHex("#20bf6b"),
            Color.FromHex("#2bcbba"),
            Color.FromHex("#0fb9b1"),
            Color.FromHex("#45aaf2"),
            Color.FromHex("#2d98da"),
            Color.FromHex("#4b7bec"),
            Color.FromHex("#3867d6"),
            Color.FromHex("#a55eea"),
            Color.FromHex("#8854d0"),
            Color.FromHex("#d1d8e0"),
            Color.FromHex("#a5b1c2"),
            Color.FromHex("#778ca3"),
            Color.FromHex("#4b6584"),
        };

        /// <summary>
        /// Get a random color with 100% opacity
        /// </summary>
        /// <returns></returns>
        public static Color Random()
        {
            var rnd = new Random();
            return Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        /// <summary>
        /// Get a random color from the known list of flat UI colors.
        /// </summary>
        /// <returns>random color</returns>
        /// <see cref="ColorUtil._colors"/>
        public static Color RandomFlatColor()
        {
            var rnd = new Random();
            return _colors[rnd.Next(_colors.Length) % _colors.Length];
        }
    }
}
