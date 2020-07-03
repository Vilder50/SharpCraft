﻿using System;
using System.Collections.Generic;
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
        public static string FixEnum(this ID.Item item)
        {
            return item.ToString().ToLower();
        }

        /// <summary>
        /// Takes the item and converts it into an value Minecraft can use
        /// </summary>
        /// <param name="item">The item to convert</param>
        /// <returns>The converted item</returns>
        public static string FixEnum(this ID.Item? item)
        {
            if (item is null)
            {
                return "air";
            }
            else
            {
                return item.Value.FixEnum();
            }
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

        /// <summary>
        /// Fixes the name of attribute
        /// </summary>
        /// <param name="attribute">The attribute to fix</param>
        /// <returns>The fixed attribute string</returns>
        public static string FixEnum(this ID.AttributeType attribute)
        {
            string asString = attribute.ToString();
            int firstIndex = asString.IndexOf("_");
            return asString.Substring(0, firstIndex) + "." + asString.Substring(firstIndex + 1);
        }

        /// <summary>
        /// Fixes the name of attribute
        /// </summary>
        /// <param name="attribute">The attribute to fix</param>
        /// <returns>The fixed attribute string</returns>
        public static string FixEnum(this ID.AttributeType? attribute)
        {
            if (attribute is null)
            {
                return ID.AttributeType.generic_max_health.FixEnum();
            } 
            else
            {
                return attribute.Value.FixEnum();
            }
        }

        private const string namePattern = @"^[a-z\-_\./0-9]+$";
        /// <summary>
        /// Checks if the given name is valid or not
        /// </summary>
        /// <param name="name">The name to check</param>
        /// <param name="allowCapitialized">If capitialized letters are allowed</param>
        /// <param name="allowSlash">If / is allowed</param>
        /// <param name="maxLength">The maximum length of the name</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateName(string name, bool allowCapitialized, bool allowSlash, int? maxLength)
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
            if (!(maxLength is null) && name.Length > maxLength)
            {
                return false;
            }
            return Regex.IsMatch(checkString, namePattern);
        }

        /// <summary>
        /// Throws an exception if the given selector either is null or selects more than one entity
        /// </summary>
        /// <param name="selector">The selector to check</param>
        /// <param name="selectorName">The name of the selector variable</param>
        /// <param name="classHolderName">The name of the class holding the selector variable</param>
        /// <exception cref="ArgumentNullException">If the selector is null</exception>
        /// <exception cref="ArgumentException">If the selector selects too many entities</exception>
        /// <returns>The checked selector</returns>
        public static BaseSelector ValidateSingleSelectSelector(BaseSelector selector, string selectorName, string classHolderName)
        {
            if (!(selector ?? throw new ArgumentNullException(selectorName, selectorName + " may not be null.")).IsLimited())
            {
                throw new ArgumentException(classHolderName + " doesn't allow " + selectorName + " to select multiple entities", selectorName);
            }

            return selector;
        }

        /// <summary>
        /// Throws an exception if the array either is null or it contains null
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="array">The array to check</param>
        /// <param name="arrayName">The name of the array variable</param>
        /// <param name="classHolderName">The name of the class holding the array</param>
        /// <exception cref="ArgumentNullException">If the array is null or contains null</exception>
        /// <returns>The checked array</returns>
        public static T[] ValidateNoneNullArray<T>(T[] array, string arrayName, string classHolderName)
        {
            if (array is null)
            {
                throw new ArgumentNullException(arrayName, classHolderName + " doesn't allow " + arrayName + " to be null.");
            }
            foreach(T element in array)
            {
                if (element is null)
                {
                    throw new ArgumentNullException(arrayName, classHolderName + " doesn't allow element inside of " + arrayName + " to be null.");
                }
            }
            return array;
        }

        /// <summary>
        /// Throws an exception if the list either is null or it contains null
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="list">The list to check</param>
        /// <param name="listName">The name of the list variable</param>
        /// <param name="classHolderName">The name of the class holding the list</param>
        /// <exception cref="ArgumentNullException">If the list is null or contains null</exception>
        /// <returns>The checked list</returns>
        public static List<T> ValidateNoneNullList<T>(List<T> list, string listName, string classHolderName)
        {
            ValidateNoneNullArray(list.ToArray(), listName, classHolderName);

            return list;
        }

        /// <summary>
        /// Throws an exception if the given value is outside of the range
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="minimum">The minimum the value can be</param>
        /// <param name="maximum">The maximum the value can be</param>
        /// <param name="valueName">The name of the value variable</param>
        /// <param name="classHolderName">The name of the class holding the value</param>
        /// <exception cref="ArgumentOutOfRangeException">If the value it outside the range</exception>
        /// <returns>The checked value</returns>
        public static int ValidateRange(int value, int minimum, int maximum, string valueName, string classHolderName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentOutOfRangeException(valueName, classHolderName + " does not allow " + valueName + " to be outside the range " + minimum + " - " + maximum + ".");
            }
            return value;
        }

        /// <summary>
        /// Throws an exception if the given value is outside of the range
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="minimum">The minimum the value can be</param>
        /// <param name="maximum">The maximum the value can be</param>
        /// <param name="valueName">The name of the value variable</param>
        /// <param name="classHolderName">The name of the class holding the value</param>
        /// <exception cref="ArgumentOutOfRangeException">If the value it outside the range</exception>
        /// <returns>The checked value</returns>
        public static double ValidateRange(double value, double minimum, double maximum, string valueName, string classHolderName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentOutOfRangeException(valueName, classHolderName + " does not allow " + valueName + " to be outside the range " + minimum + " - " + maximum + ".");
            }
            return value;
        }
    }
}
