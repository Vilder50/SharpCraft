using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which makes a function run at some point in time
    /// </summary>
    public class ScheduleAddCommand : BaseCommand
    {
        private IFunction function = null!;
        private NoneNegativeTime<int> time = null!;

        /// <summary>
        /// Intializes a new <see cref="ScheduleAddCommand"/>
        /// </summary>
        /// <param name="function">The function to schedule</param>
        /// <param name="time">The amount of time before the function should run</param>
        /// <param name="append">True if the function should append. False if it should replace other times the function has been scheduled.</param>
        public ScheduleAddCommand(IFunction function, NoneNegativeTime<int> time, bool append = false)
        {
            Function = function;
            Time = time;
            Append = append;
        }

        /// <summary>
        /// The function to schedule
        /// </summary>
        public IFunction Function { get => function; set => function = value ?? throw new ArgumentNullException(nameof(Function), "Function may not be null"); }

        /// <summary>
        /// The amount of time before the function should run
        /// </summary>
        public NoneNegativeTime<int> Time 
        { 
            get => time; 
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Time), "Time may not be null");
                }
                time = value;
            }
        }

        /// <summary>
        /// True if the function should append. False if it should replace other times the function has been scheduled.
        /// </summary>
        public bool Append { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>schedule function [Function] [Time] [Append]</returns>
        public override string GetCommandString()
        {
            if (Append)
            {
                return $"schedule function {Function.GetNamespacedName()} {Time.GetTimeString()} append";
            }
            else
            {
                return $"schedule function {Function.GetNamespacedName()} {Time.GetTimeString()}";
            }
        }
    }

    /// <summary>
    /// Command which clears a function's schedule
    /// </summary>
    public class ScheduleClearCommand : BaseCommand
    {
        private IFunction function = null!;

        /// <summary>
        /// Intializes a new <see cref="ScheduleClearCommand"/>
        /// </summary>
        /// <param name="function">The function to clear the schedule for</param>
        public ScheduleClearCommand(IFunction function)
        {
            Function = function;
        }

        /// <summary>
        /// The function to clear the schedule for
        /// </summary>
        public IFunction Function { get => function; set => function = value ?? throw new ArgumentNullException(nameof(Function), "Function may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>schedule [Function] [Time] [Append]</returns>
        public override string GetCommandString()
        {
            return $"schedule clear {Function.GetNamespacedName()}";
        }
    }
}
