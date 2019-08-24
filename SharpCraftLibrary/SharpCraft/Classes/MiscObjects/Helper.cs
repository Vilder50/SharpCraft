using System;

namespace SharpCraft
{
    /// <summary>
    /// A class containing helpful method extensions
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Adds escape characters to the given text
        /// </summary>
        /// <param name="text">The text to escape</param>
        /// <param name="escape">The letter to escape</param>
        /// <returns>The escaped text</returns>
        public static string Escape(this string text, char escape = '"')
        {
            return text.Replace(escape.ToString(), "?-SBackSlash-??-SStringThing-?").Replace("\\", "?-SBackSlash-??-SBackSlash-?").Replace("?-SBackSlash-?", "\\").Replace("?-SStringThing-?", escape.ToString());
        }

        /// <summary>
        /// Converts the given double into a double Minecraft can use
        /// </summary>
        /// <param name="Double">The double to convert</param>
        /// <returns>The converted double</returns>
        public static string ToMinecraftDouble(this double Double)
        {
            return Double.ToString("F16").TrimEnd('0').TrimEnd(',').Replace(",", ".");
        }
        /// <summary>
        /// Converts the given double into a double Minecraft can use
        /// </summary>
        /// <param name="Double">The double to convert</param>
        /// <returns>The converted double</returns>
        public static string ToMinecraftDouble(this double? Double)
        {
            return Double.Value.ToString("F16").TrimEnd('0').TrimEnd(',').Replace(",", ".");
        }

        /// <summary>
        /// Converts the given float into a double Minecraft can use
        /// </summary>
        /// <param name="Float">The float to convert</param>
        /// <returns>The converted float</returns>
        public static string ToMinecraftFloat(this float Float)
        {
            return Float.ToString("F16").TrimEnd('0').TrimEnd(',').Replace(",", ".");
        }
        /// <summary>
        /// Converts the given float into a float Minecraft can use
        /// </summary>
        /// <param name="Float">The float to convert</param>
        /// <returns>The converted float</returns>
        public static string ToMinecraftFloat(this float? Float)
        {
            return Float.Value.ToString("F16").TrimEnd('0').TrimEnd(',').Replace(",", ".");
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
                throw new ArgumentException("item doesn't have a block type", nameof(item));
            }
        }

        /// <summary>
        /// Converts the given <see cref="ID.Item"/> into an <see cref="ID.Block"/>
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The block</returns>
        public static ID.Block ConvertToBlock(this ID.Item? item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "item may not be null");
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
                throw new ArgumentException("block doesn't have a item type", nameof(block));
            }
        }

        /// <summary>
        /// Converts the given <see cref="ID.Item"/> into an <see cref="ID.Block"/>
        /// </summary>
        /// <param name="block">The block to convert</param>
        /// <returns>The item</returns>
        public static ID.Item ConvertToItem(this ID.Block? block)
        {
            if (block is null)
            {
                throw new ArgumentNullException(nameof(block), "block may not be null");
            }
            return ConvertToItem(block.Value);
        }

        /// <summary>
        /// Converts an array of JSON strings into raw data string
        /// </summary>
        /// <param name="JSONArray">The array to convert</param>
        /// <param name="forceArray">If the array should only should use extra</param>
        /// <returns>Raw data string made out of the JSON array</returns>
        public static string GetString(this JSON[] JSONArray, bool forceArray = true)
        {
            if (!forceArray)
            {
                string firstItemString = JSONArray[0].ToString();
                if (JSONArray.Length == 1)
                {
                    return firstItemString;
                }
                string[] TempArray = new string[JSONArray.Length - 1];
                for (int i = 1; i < JSONArray.Length; i++)
                {
                    TempArray[i] = JSONArray[i].ToString();
                }
                return firstItemString.Substring(0, firstItemString.Length - 1) + ",extra:[" + string.Join(",", TempArray) + "]}";
            }
            else
            {
                string[] TempArray = new string[JSONArray.Length];
                for (int i = 0; i < JSONArray.Length; i++)
                {
                    TempArray[i] = JSONArray[i].ToString();
                }
                return "[" + string.Join(",", TempArray) + "]";
            }
        }
    }
}
