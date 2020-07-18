using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Static class containing extension methods for inbuilt data types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds escape characters to the given text
        /// </summary>
        /// <param name="text">The text to escape</param>
        /// <returns>The escaped text</returns>
        public static string Escape(this string text)
        {
            return text.Replace("\"", "?-SBackSlash-?" + "\"").Replace("\\", "\\\\").Replace("?-SBackSlash-?", "\\").Replace("\n", "\\n");
        }

        /// <summary>
        /// Converts the given double into a double Minecraft can use
        /// </summary>
        /// <param name="Double">The double to convert</param>
        /// <returns>The converted double</returns>
        public static string ToMinecraftDouble(this double Double)
        {
            return Double.ToString().Replace(",", ".");
        }
        /// <summary>
        /// Converts the given double into a double Minecraft can use
        /// </summary>
        /// <param name="Double">The double to convert</param>
        /// <returns>The converted double</returns>
        public static string ToMinecraftDouble(this double? Double)
        {
            if (Double is null)
            {
                return "0";
            }
            return Double.Value.ToString().Replace(",", ".");
        }

        /// <summary>
        /// Converts the given float into a double Minecraft can use
        /// </summary>
        /// <param name="Float">The float to convert</param>
        /// <returns>The converted float</returns>
        public static string ToMinecraftFloat(this float Float)
        {
            return Float.ToString().Replace(",", ".");
        }
        /// <summary>
        /// Converts the given float into a float Minecraft can use
        /// </summary>
        /// <param name="Float">The float to convert</param>
        /// <returns>The converted float</returns>
        public static string ToMinecraftFloat(this float? Float)
        {
            if (Float is null)
            {
                return "0";
            }
            return Float.Value.ToString().Replace(",", ".");
        }

        /// <summary>
        /// Takes a bool and converts it into a value Minecraft can use
        /// </summary>
        /// <param name="Bool">The bool to convert</param>
        /// <returns>The converted bool</returns>
        public static string ToMinecraftBool(this bool Bool)
        {
            return Bool.ToString().ToLower();
        }

        /// <summary>
        /// Takes a bool and converts it into a value Minecraft can use
        /// </summary>
        /// <param name="Bool">The bool to convert</param>
        /// <returns>The converted bool</returns>
        public static string ToMinecraftBool(this bool? Bool)
        {
            if (Bool is null)
            {
                return "false";
            }
            return Bool.Value.ToString().ToLower();
        }
    }
}
