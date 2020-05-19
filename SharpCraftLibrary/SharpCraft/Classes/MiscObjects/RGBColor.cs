using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object for rgb colors
    /// </summary>
    public class RGBColor : IConvertableToDataTag
    {
        int red;
        int green;
        int blue;

        /// <summary>
        /// Makes a rgb color with the specified colors
        /// </summary>
        /// <param name="redColor">the amount of red</param>
        /// <param name="greenColor">the amount of green</param>
        /// <param name="blueColor">the amount of blue</param>
        public RGBColor(int redColor, int greenColor, int blueColor)
        {
            Red = redColor;
            Green = greenColor;
            Blue = blueColor;
        }

        /// <summary>
        /// Makes an rgb color out of a string with a hex color value.
        /// (Like #56a5fd)
        /// </summary>
        /// <param name="hexColor">the string to convert</param>
        public RGBColor(string hexColor)
        {
            hexColor = hexColor.TrimStart('#');
            if (hexColor.Length > 6)
            {
                throw new ArgumentException("The given hex color is too long.");
            }
            try
            {
                Red = int.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                Green = int.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                Blue = int.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The given hex color is invalid" + ex);
            }
        }

        /// <summary>
        /// The amount of red this color is (goes from 0-255)
        /// </summary>
        public int Red
        {
            get
            {
                return red;
            }
            set
            {
                red = ClampValue(value);
            }
        }

        /// <summary>
        /// The amount of green this color is (goes from 0-255)
        /// </summary>
        public int Green
        {
            get
            {
                return green;
            }
            set
            {
                green = ClampValue(value);
            }
        }

        /// <summary>
        /// The amount of blue this color is (goes from 0-255)
        /// </summary>
        public int Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = ClampValue(value);
            }
        }

        private int ClampValue(int value)
        {
            return Math.Min(255,Math.Max(0,value));
        }

        /// <summary>
        /// Outputs the color as an int
        /// </summary>
        public int ColorInt
        {
            get
            {
                return Red * 65536 + Green * 256 + Blue;
            }
        }

        /// <summary>
        /// Converts this HexColor into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(ColorInt);
        }

        /// <summary>
        /// Converts a string with a hex color value into an <see cref="RGBColor"/> object
        /// </summary>
        /// <param name="hexColor">the string to convert</param>
        public static implicit operator RGBColor (string hexColor)
        {
            return new RGBColor(hexColor);
        }
    }
}
