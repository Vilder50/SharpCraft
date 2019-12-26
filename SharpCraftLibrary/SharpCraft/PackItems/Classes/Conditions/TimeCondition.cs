using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the time is at the given time
    /// </summary>
    public class TimeCondition : BaseCondition
    {
        private Range time;

        /// <summary>
        /// Intializes a new <see cref="TimeCondition"/>
        /// </summary>
        /// <param name="modulo">the value of time to check for</param>
        /// <param name="time">The number to modulo the real time with</param>
        public TimeCondition(Range time, int? modulo) : base("minecraft:time_check")
        {
            Time = time;
            Modulo = modulo;
        }

        /// <summary>
        /// the value of time to check for
        /// </summary>
        [DataTag("value", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public Range Time { get => time; set => time = value ?? throw new ArgumentNullException(nameof(Time),"Time may not be null"); }

        /// <summary>
        /// The number to modulo the real time with
        /// </summary>
        [DataTag("period", JsonTag = true)]
        public int? Modulo { get; set; }
    }
}

