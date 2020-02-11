using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which changes the time in the world
    /// </summary>
    public class TimeModifyCommand : BaseCommand
    {
        private Time time;

        /// <summary>
        /// Intializes a new <see cref="TimeModifyCommand"/>
        /// </summary>
        /// <param name="time">The value to modify with</param>
        /// <param name="modifier">The way to use the modify value</param>
        public TimeModifyCommand(Time time, ID.AddSetModifier modifier)
        {
            Time = time;
            Modifier = modifier;
        }

        /// <summary>
        /// The value to modify with
        /// </summary>
        public Time Time
        {
            get => time;
            set 
            { 
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Time), "Time may not be null");
                }
                if (value.AsTicks() < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Time), "Time may not be negative");
                }
                time = value; 
            }
        }

        /// <summary>
        /// The way to use the modify value
        /// </summary>
        public ID.AddSetModifier Modifier { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>time [Modifier] [Time]</returns>
        public override string GetCommandString()
        {
            return $"time {Modifier} {Time.GetTimeString()}";
        }
    }

    /// <summary>
    /// Command which gets the time in the world
    /// </summary>
    public class TimeQueryCommand : BaseCommand
    {
        /// <summary>
        /// Intializes a new <see cref="TimeQueryCommand"/>
        /// </summary>
        /// <param name="query">The thing to get</param>
        public TimeQueryCommand(ID.QueryTime query)
        {
            Query = query;
        }

        /// <summary>
        /// The thing to get
        /// </summary>
        public ID.QueryTime Query { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>time query [Query]</returns>
        public override string GetCommandString()
        {
            return $"time query {Query}";
        }
    }
}
