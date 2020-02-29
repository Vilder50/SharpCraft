using System;
using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// A class containing helpful method extensions
    /// </summary>
    public static class Utils
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
        /// Takes the item and converts it into an value Minecraft can use
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The converted item</returns>
        public static string MinecraftValue(this ID.Item item)
        {
            return item.ToString().ToLower();
        }

        /// <summary>
        /// Takes the item and converts it into an value Minecraft can use
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The converted item</returns>
        public static string MinecraftValue(this ID.Item? item)
        {
            if (item is null)
            {
                return "0";
            }
            return item.Value.ToString().ToLower();
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

        /// <summary>
        /// Converts the given <see cref="ID.Item"/> into an <see cref="ID.Block"/>
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The block</returns>
        public static ID.Block ConvertToBlock(this ID.Item item)
        {
            try
            {
                return (ID.Block)Enum.Parse(typeof(ID.Block), item.ToString());
            }
            catch
            {
                throw new InvalidCastException("item doesn't have a block type");
            }
        }

        /// <summary>
        /// Converts the given <see cref="ID.Item"/> into an <see cref="ID.Block"/>
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The block</returns>
        public static ID.Block? ConvertToBlock(this ID.Item? item)
        {
            if (item is null)
            {
                return null;
            }
            return ConvertToBlock(item.Value);
        }

        /// <summary>
        /// Converts the given <see cref="ID.Block"/> into an <see cref="ID.Item"/>
        /// </summary>
        /// <param name="block">The block to convert</param>
        /// <returns>The item</returns>
        public static ID.Item ConvertToItem(this ID.Block block)
        {
            try
            {
                return (ID.Item)Enum.Parse(typeof(ID.Item), block.ToString());
            }
            catch
            {
                throw new InvalidCastException("block doesn't have a item type");
            }
        }

        /// <summary>
        /// Converts the given <see cref="ID.Item"/> into an <see cref="ID.Block"/>
        /// </summary>
        /// <param name="block">The block to convert</param>
        /// <returns>The item</returns>
        public static ID.Item? ConvertToItem(this ID.Block? block)
        {
            if (block is null)
            {
                return null;
            }
            return ConvertToItem(block.Value);
        }

        private const string namePattern = @"^[a-z\-_\./0-9]+$";
        /// <summary>
        /// Checks if the given name is valid or not
        /// </summary>
        /// <param name="name">The name to check</param>
        /// <param name="allowCapitialized">If capitialized letters are allowed</param>
        /// <param name="allowSlash">If / is allowed</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateName(string name, bool allowCapitialized, bool allowSlash)
        {
            if (name is null)
            {
                return false;
            }

            string checkString = name;
            if (allowCapitialized)
            {
                checkString = checkString.ToLower();
            }
            if (!allowSlash && name.Contains("/"))
            {
                return false;
            }
            return Regex.IsMatch(checkString, namePattern);
        }
    }
}
