using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// Class containing methods for validating things
    /// </summary>
    public static class Validators
    {
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
            foreach (T element in array)
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
