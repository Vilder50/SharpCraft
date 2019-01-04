using System;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object used to define time
    /// </summary>
    public class Time
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
        public override string ToString()
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
    }
}
