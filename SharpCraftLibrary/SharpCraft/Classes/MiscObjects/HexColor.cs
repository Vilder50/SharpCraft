﻿using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object for rgb colors
    /// </summary>
    public class HexColor : IConvertableToDataTag
    {
        int _Red;
        /// <summary>
        /// The amount of red this color is (goes from 0-255)
        /// </summary>
        public int Red
        {
            get
            {
                return _Red;
            }
            set
            {
                if (value < 0) { _Red = 0; }
                else if (value > 255) { _Red = 255; }
                else { _Red = value; }
            }
        }

        int _Green;
        /// <summary>
        /// The amount of green this color is (goes from 0-255)
        /// </summary>
        public int Green
        {
            get
            {
                return _Green;
            }
            set
            {
                if (value < 0) { _Green = 0; }
                else if (value > 255) { _Green = 255; }
                else { _Green = value; }
            }
        }

        int _Blue;
        /// <summary>
        /// The amount of blue this color is (goes from 0-255)
        /// </summary>
        public int Blue
        {
            get
            {
                return _Blue;
            }
            set
            {
                if (value < 0) { _Blue = 0; }
                else if (value > 255) { _Blue = 255; }
                else { _Blue = value; }
            }
        }

        /// <summary>
        /// Makes a rgb color with the specified colors
        /// </summary>
        /// <param name="RedColor">the amount of red</param>
        /// <param name="GreenColor">the amount of green</param>
        /// <param name="BlueColor">the amount of blue</param>
        public HexColor(int RedColor, int GreenColor, int BlueColor)
        {
            Red = RedColor;
            Green = GreenColor;
            Blue = BlueColor;
        }

        /// <summary>
        /// Makes an rgb color out of a string with a hex color value.
        /// (Like #56a5fd)
        /// </summary>
        /// <param name="hexColor">the string to convert</param>
        public HexColor(string hexColor)
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
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(ColorInt);
        }

        /// <summary>
        /// Converts a string with a hex color value into an <see cref="HexColor"/> object
        /// </summary>
        /// <param name="hexColor">the string to convert</param>
        public static implicit operator HexColor (string hexColor)
        {
            return new HexColor(hexColor);
        }
    }
}
