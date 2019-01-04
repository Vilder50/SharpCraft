using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for ranges
    /// </summary>
    public class Range
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
        public Range(double? Minimum, double? Maximum)
        {
            Min = Minimum;
            Max = Maximum;
        }

        /// <summary>
        /// Creates a range which only contains one number
        /// </summary>
        /// <param name="Equals">The only number the range contains</param>
        public Range(double Equals)
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
        /// Converts a single number into a range only containing that number
        /// </summary>
        /// <param name="exactNumber">The number the range contains</param>
        public static implicit operator Range(double exactNumber)
        {
            return new Range(exactNumber);
        }
    }
}
