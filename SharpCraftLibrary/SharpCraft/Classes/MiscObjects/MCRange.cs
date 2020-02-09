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
        /// The smallest number in the range
        /// </summary>
        public double? Min;

        /// <summary>
        /// The highest number in the range
        /// </summary>
        public double? Max;

        /// <summary>
        /// Creates a range.
        /// </summary>
        /// <param name="Minimum">The smallest number in the range</param>
        /// <param name="Maximum">The highest number in the range</param>
        public MCRange(double? Minimum, double? Maximum)
        {
            Min = Minimum;
            Max = Maximum;
        }

        /// <summary>
        /// Creates a range which only contains one number
        /// </summary>
        /// <param name="Equals">The only number the range contains</param>
        public MCRange(double Equals)
        {
            Min = Equals;
            Max = Equals;
        }

        /// <summary>
        /// Gets the raw data used in JSON files
        /// "name":{"min":x,"max":y}
        /// </summary>
        /// <param name="Name">the name the object should have in the file</param>
        /// <returns>Raw data used in JSON files</returns>
        public string JSONString(string Name)
        {
            if (Min != Max)
            {
                List<string> TempList = new List<string>();
                if (Min != null) { TempList.Add("\"min\":" + Min.ToMinecraftDouble()); }
                if (Max != null) { TempList.Add("\"max\":" + Max.ToMinecraftDouble()); }
                return "\"" + Name + "\": {" + string.Join(",", TempList) + "}";
            }
            else
            {
                return "\"" + Name + "\":" + Min.ToMinecraftDouble();
            }
        }
        /// <summary>
        /// Gets the raw data used in commands
        /// x..y or name=x..y
        /// </summary>
        /// <param name="Name">The thing which has to be equal the range. If null the output will be x..y</param>
        /// <returns>Raw data used in commands</returns>
        public string SelectorString(string Name = null)
        {
            
            if (Min != Max)
            {
                string TempString = "";
                if (Name != null)
                {
                    TempString = Name + "=";
                }
                if (Min != null) { TempString += Min.ToMinecraftDouble(); }
                TempString += "..";
                if (Max != null) { TempString += Max.ToMinecraftDouble(); }
                return TempString;
            }
            else
            {
                if (Name != null)
                {
                    return Name + "=" + Min;
                }
                else
                {
                    return Min.ToString();
                }
            }
        }

        /// <summary>
        /// Converts this range into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: min path name, 1: max path name, 2: type of the range values, 3: is json</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length < 3 || conversionData.Length > 4)
            {
                throw new ArgumentException("There has to be exacly 3-4 conversion params to convert a range to a data object.");
            }

            bool isJson = false;
            if (conversionData.Length == 4)
            {
                isJson = (bool)conversionData[3];
            }

            if (conversionData[0] is string minName && conversionData[1] is string maxName && conversionData[2] is ID.NBTTagType forceType)
            {
                DataPartObject dataObject = new DataPartObject();
                if (forceType == ID.NBTTagType.TagShort)
                {
                    if (!(Min is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag((short?)Min, isJson: isJson), isJson));
                    }
                    if (!(Max is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag((short?)Max, isJson: isJson), isJson));
                    }
                }
                else if (forceType == ID.NBTTagType.TagDouble)
                {
                    if (!(Min is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag(Min, isJson: isJson), isJson));
                    }
                    if (!(Max is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag(Max, isJson: isJson), isJson));
                    }
                }
                else if (forceType == ID.NBTTagType.TagInt)
                {
                    if (!(Min is null))
                    {
                        dataObject.AddValue(new DataPartPath(minName, new DataPartTag((int?)Min, isJson: isJson), isJson));
                    }
                    if (!(Max is null))
                    {
                        dataObject.AddValue(new DataPartPath(maxName, new DataPartTag((int?)Max, isJson: isJson), isJson));
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
