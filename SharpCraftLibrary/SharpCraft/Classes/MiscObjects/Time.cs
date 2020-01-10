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

        readonly int _time;
        readonly ID.TimeType _timeType;

        /// <summary>
        /// Creates a new time object with the specified time
        /// </summary>
        /// <param name="time">the amount of time</param>
        /// <param name="timeType">the time measuring type</param>
        public Time(int time, ID.TimeType timeType)
        {
            _time = Convert.ToInt32(time);
            _timeType = timeType;
        }

        /// <summary>
        /// Converts the time into raw data used by the game
        /// </summary>
        /// <returns>Raw data used by the game</returns>
        public string GetTimeString()
        {
            switch(_timeType)
            {
                case ID.TimeType.days:
                    return _time + "d";
                case ID.TimeType.seconds:
                    return _time + "s";
                default:
                    return _time + "t";
            }
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
        /// <returns></returns>
        public bool IsNegative()
        {
            return _time < 0;
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
                        switch (_timeType)
                        {
                            case ID.TimeType.days:
                                return _time * 24000;
                            case ID.TimeType.seconds:
                                return _time * 20;
                            default:
                                return _time;
                        }
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
                        switch (_timeType)
                        {
                            case ID.TimeType.days:
                                return (short)(_time * 24000);
                            case ID.TimeType.seconds:
                                return (short)(_time * 20);
                            default:
                                return (short)_time;
                        }
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
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            long savedTime;
            switch (_timeType)
            {
                case ID.TimeType.days:
                    savedTime =  _time * 24000;
                    break;
                case ID.TimeType.seconds:
                    savedTime = _time * 20;
                    break;
                default:
                    savedTime = _time;
                    break;
            }
            switch (asType)
            {
                case ID.NBTTagType.TagInt:
                    return new DataPartTag((int)savedTime);
                case ID.NBTTagType.TagShort:
                    return new DataPartTag((short)savedTime);
                case ID.NBTTagType.TagLong:
                    return new DataPartTag(savedTime);
                default:
                    throw new ArgumentException("Cannot convert time into a " + asType + " object");
            }
        }
    }
}
