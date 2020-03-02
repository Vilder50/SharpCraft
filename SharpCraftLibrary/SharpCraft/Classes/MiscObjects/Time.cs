using System;
using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used to define time
    /// </summary>
    public class Time : IConvertableToDataTag
    {
        /// <summary>
        /// The type of time to output
        /// </summary>
        public enum TimerType
        {
            /// <summary>
            /// Will output a time which can be in an int
            /// </summary>
            Int,
            /// <summary>
            /// Will output a time which can be in an short
            /// </summary>
            Short
        };

        readonly int time;
        readonly ID.TimeType timeType;

        /// <summary>
        /// Creates a new time object with the specified time
        /// </summary>
        /// <param name="time">the amount of time</param>
        /// <param name="timeType">the time measuring type</param>
        public Time(int time, ID.TimeType timeType)
        {
            this.time = Convert.ToInt32(time);
            this.timeType = timeType;
        }

        /// <summary>
        /// Converts the time into raw data used by the game
        /// </summary>
        /// <returns>Raw data used by the game</returns>
        public string GetTimeString()
        {
            return timeType switch
            {
                ID.TimeType.days => time + "d",
                ID.TimeType.seconds => time + "s",
                _ => time + "t",
            };
        }

        /// <summary>
        /// Converts a number into a new time object in ticks
        /// </summary>
        /// <param name="ticks">The amount of ticks the time should be</param>
        public static implicit operator Time(int ticks)
        {
            return new Time(ticks, ID.TimeType.ticks);
        }

        /// <summary>
        /// Returns true if the time in this time object is negative
        /// </summary>
        /// <returns>True if the time in this time object is negative</returns>
        public bool IsNegative()
        {
            return time < 0;
        }

        /// <summary>
        /// Outputs this time amount in ticks
        /// </summary>
        /// <param name="type">The type of output</param>
        /// <returns>This time amount in ticks</returns>
        public int AsTicks(TimerType type = TimerType.Int)
        {
            checked
            {
                if (type == TimerType.Int)
                {
                    try
                    {
                        return timeType switch
                        {
                            ID.TimeType.days => time * 24000,
                            ID.TimeType.seconds => time * 20,
                            _ => time,
                        };
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("The time cannot be over " + int.MaxValue + " or less than " + int.MinValue + " ticks in total");
                    }
                }
                else
                {
                    try
                    {
                        return timeType switch
                        {
                            ID.TimeType.days => (short)(time * 24000),
                            ID.TimeType.seconds => (short)(time * 20),
                            _ => (short)time,
                        };
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("The time cannot be over " + float.MaxValue + " or less than " + float.MinValue + " ticks in total");
                    }
                }
            }
        }

        /// <summary>
        /// Converts this time into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">The type of tag</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            int savedTime = timeType switch
            {
                ID.TimeType.days => time * 24000,
                ID.TimeType.seconds => time * 20,
                _ => time,
            };
            return asType switch
            {
                ID.NBTTagType.TagInt => new DataPartTag(savedTime),
                ID.NBTTagType.TagShort => new DataPartTag((short)savedTime),
                ID.NBTTagType.TagLong => new DataPartTag((long)savedTime),
                _ => throw new ArgumentException("Cannot convert time into a " + asType + " object"),
            };
        }
    }
}
