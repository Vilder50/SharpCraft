using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used to define time
    /// </summary>
    public class Time<T> : IConvertableToDataTag
    {
        /// <summary>
        /// Creates a new time object with the specified time
        /// </summary>
        /// <param name="value">the amount of time</param>
        /// <param name="type">the time measuring type</param>
        public Time(T value, ID.TimeType type)
        {
            if (!(typeof(T) == typeof(int) || typeof(T) == typeof(short) || typeof(T) == typeof(long)))
            {
                throw new ArgumentException("Time only supports the types int, short and long");
            }

            Value = value;
            Type = type;
            _ = GetAsTicks();
        }

        /// <summary>
        /// The time value
        /// </summary>
        public virtual T Value { get; protected set; }

        /// <summary>
        /// The measurement
        /// </summary>
        public virtual ID.TimeType Type { get; protected set; }

        /// <summary>
        /// Converts the time into raw data used by the game
        /// </summary>
        /// <returns>Raw data used by the game</returns>
        public string GetTimeString()
        {
            return Type switch
            {
                ID.TimeType.days => Value + "d",
                ID.TimeType.seconds => Value + "s",
                _ => Value + "t",
            };
        }

        /// <summary>
        /// Converts a number into a new time object in ticks
        /// </summary>
        /// <param name="ticks">The amount of ticks the time should be</param>
        public static implicit operator Time<T>(T ticks)
        {
            return new Time<T>(ticks, ID.TimeType.ticks);
        }

        /// <summary>
        /// Outputs this time amount in ticks
        /// </summary>
        /// <returns>This time amount in ticks</returns>
        public T GetAsTicks()
        {
            checked
            {
                if (typeof(T) == typeof(int))
                {
                    try
                    {
                        int time = Convert.ToInt32(Value);
                        return (T)Convert.ChangeType(Type switch
                        {
                            ID.TimeType.days => time * 24000,
                            ID.TimeType.seconds => time * 20,
                            _ => time,
                        }, typeof(T));
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("The time cannot be over " + int.MaxValue + " or less than " + int.MinValue + " ticks in total");
                    }
                }
                else if (typeof(T) == typeof(short))
                {
                    try
                    {
                        short time = Convert.ToInt16(Value);
                        return (T)Convert.ChangeType(Type switch
                        {
                            ID.TimeType.days => time * 24000,
                            ID.TimeType.seconds => time * 20,
                            _ => time,
                        }, typeof(T));
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("The time cannot be over " + short.MaxValue + " or less than " + short.MinValue + " ticks in total");
                    }
                }
                else
                {
                    try
                    {
                        long time = Convert.ToInt64(Value);
                        return (T)Convert.ChangeType(Type switch
                        {
                            ID.TimeType.days => time * 24000,
                            ID.TimeType.seconds => time * 20,
                            _ => time,
                        }, typeof(T));
                    }
                    catch (OverflowException)
                    {
                        throw new OverflowException("The time cannot be over " + long.MaxValue + " or less than " + long.MinValue + " ticks in total");
                    }
                }
            }
        }

        /// <summary>
        /// Converts this time into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">Not used</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            if (typeof(T) == typeof(int))
            {
                return new DataPartTag(Convert.ToInt32(GetAsTicks()));
            }
            else if (typeof(T) == typeof(short))
            {
                return new DataPartTag(Convert.ToInt16(GetAsTicks()));
            }
            else
            {
                return new DataPartTag(Convert.ToInt64(GetAsTicks()));
            }
        }
    }

    /// <summary>
    /// An object used to define none negative time
    /// </summary>
    public class NoneNegativeTime<T> : Time<T>
    {
        /// <summary>
        /// Creates a new time object with the specified time
        /// </summary>
        /// <param name="value">the amount of time</param>
        /// <param name="type">the time measuring type</param>
        public NoneNegativeTime(T value, ID.TimeType type) : base(value, type)
        {

        }

        /// <summary>
        /// The time value
        /// </summary>
        public override T Value 
        { 
            get => base.Value;
            protected set 
            {
                long numericValue = Convert.ToInt64(value);
                if (numericValue < 0)
                {
                    throw new ArgumentOutOfRangeException("Time value may not be negative", nameof(Value));
                }
                base.Value = value; 
            }
        }

        /// <summary>
        /// Converts a number into a new time object in ticks
        /// </summary>
        /// <param name="ticks">The amount of ticks the time should be</param>
        public static implicit operator NoneNegativeTime<T>(T ticks)
        {
            return new NoneNegativeTime<T>(ticks, ID.TimeType.ticks);
        }
    }
}
