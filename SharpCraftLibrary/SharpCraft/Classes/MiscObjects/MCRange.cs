using System.Collections.Generic;
using SharpCraft.Data;
using System;

namespace SharpCraft
{
    /// <summary>
    /// An object for ranges
    /// </summary>
    public class MCRange : IConvertableToDataObject
    {
        /// <summary>
        /// Creates a range.
        /// </summary>
        /// <param name="minimum">The smallest number in the range</param>
        /// <param name="maximum">The highest number in the range</param>
        public MCRange(double? minimum, double? maximum)
        {
            if (minimum is null && maximum is null)
            {
                throw new ArgumentNullException("Both minimum and maximum may not both be null");
            }
            if (!(minimum is null || maximum is null) && maximum < minimum)
            {
                throw new ArgumentException("Maximum may not be less than minimum");
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Creates a range which only contains one number
        /// </summary>
        /// <param name="equals">The only number the range contains</param>
        public MCRange(double equals) : this(equals, equals)
        {
            
        }

        /// <summary>
        /// The smallest number in the range
        /// </summary>
        public double? Minimum { get; protected set; }

        /// <summary>
        /// The highest number in the range
        /// </summary>
        public double? Maximum { get; protected set; }

        /// <summary>
        /// Gets the raw data used in commands
        /// x..y or name=x..y
        /// </summary>
        /// <param name="name">The thing which has to be equal the range</param>
        /// <returns>Raw data used in commands</returns>
        public string SelectorString(string name)
        {
            return name + "=" + SelectorString();
        }

        /// <summary>
        /// Gets string in the format x..y
        /// </summary>
        /// <returns>x..y</returns>
        public string SelectorString()
        {
            if (Minimum != Maximum)
            {
                string tempString = "";
                if (!(Minimum is null)) { tempString += Minimum.ToMinecraftDouble(); }
                tempString += "..";
                if (!(Maximum is null)) { tempString += Maximum.ToMinecraftDouble(); }
                return tempString;
            }
            else
            {
                return Minimum.ToMinecraftDouble();
            }
        }

        /// <summary>
        /// Converts this range into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: min path name, 1: max path name, 2: type of the range values, 3: is json</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object?[]? conversionData)
        {
            if (conversionData is null)
            {
                throw new ArgumentNullException(nameof(conversionData), "ConversionData may not be null");
            }

            if (conversionData.Length < 3 || conversionData.Length > 4)
            {
                throw new ArgumentException("There has to be exacly 3-4 conversion params to convert a range to a data object.");
            }

            bool isJson = false;
            if (conversionData.Length == 4)
            {
                isJson = (bool?)conversionData[3] ?? false;
            }

            if (conversionData[0] is string minName && conversionData[1] is string maxName && conversionData[2] is ID.NBTTagType forceType)
            {
                DataPartObject dataObject = new DataPartObject();
                if (forceType == ID.NBTTagType.TagShort)
                {
                    if (!(Minimum is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag((short?)Minimum, isJson: isJson), isJson));
                    }
                    if (!(Maximum is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag((short?)Maximum, isJson: isJson), isJson));
                    }
                }
                else if (forceType == ID.NBTTagType.TagDouble)
                {
                    if (!(Minimum is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag(Minimum, isJson: isJson), isJson));
                    }
                    if (!(Maximum is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag(Maximum, isJson: isJson), isJson));
                    }
                }
                else if (forceType == ID.NBTTagType.TagInt)
                {
                    if (!(Minimum is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag((int?)Minimum, isJson: isJson), isJson));
                    }
                    if (!(Maximum is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag((int?)Maximum, isJson: isJson), isJson));
                    }
                }
                else
                {
                    throw new ArgumentException("Range values cannot convert to the given type");
                }

                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 2 conversion params has be strings and 1 has to be an NBT enum to convert a range to a data object.");
            }
        }

        /// <summary>
        /// Converts a single number into a range only containing that number
        /// </summary>
        /// <param name="exactNumber">The number the range contains</param>
        public static implicit operator MCRange(double exactNumber)
        {
            return new MCRange(exactNumber);
        }

        /// <summary>
        /// Implicit converts a <see cref="Range"/> into a <see cref="MCRange"/>
        /// </summary>
        /// <param name="range">The range to convert</param>
        public static implicit operator MCRange(Range range)
        {
            int val1 = range.Start.IsFromEnd ? -range.Start.Value : range.Start.Value;
            int val2 = range.End.IsFromEnd ? -range.End.Value : range.End.Value;
            
            if (val1 > val2 && val1 != 0 && val2 != 0)
            {
                throw new InvalidCastException("Failed to convert Range to MCRange. Value1 has to be lower than value2.");
            }

            if (val1 == 0)
            {
                if (val1 > val2)
                {
                    return new MCRange(null, val2);
                }
                else
                {
                    throw new InvalidCastException("Failed to convert Range to MCRange. Failed to convert range starting position: Couldn't see if position doesn't exist or is 0. Use MCRange instead.");
                }
            }
            if (val2 == 0)
            {
                if (val1 > val2)
                {
                    return new MCRange(val1, null);
                }
                else
                {
                    throw new InvalidCastException("Failed to convert Range to MCRange. Failed to convert range ending position: Couldn't see if position doesn't exist or is 0. Use MCRange instead.");
                }
            }
            return new MCRange(val1,val2);
        }

        /// <summary>
        /// Converts a <see cref="Index"/> into a <see cref="Range"/>
        /// </summary>
        /// <param name="index">The index to convert</param>
        public static implicit operator MCRange(Index index)
        {
            int value = index.Value;
            if (index.IsFromEnd)
            {
                value = -Math.Abs(value);
            }

            return new MCRange(value);
        }
    }
}
