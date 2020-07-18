using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the suspicious stew effect
    /// </summary>
    public class StewChange : BaseChange
    {
        private StewEffect[] effects = null!;

        /// <summary>
        /// Intializes a new <see cref="StewChange"/>
        /// </summary>
        public StewChange(StewEffect[] effects) : base("minecraft:set_stew_effect")
        {
            Effects = effects;
        }

        /// <summary>
        /// The stew effects
        /// </summary>
        [DataTag("effects", JsonTag = true)]
        public StewEffect[] Effects { get => effects; set => effects = value ?? throw new ArgumentNullException(nameof(Effects), "Effects may not be null"); }

        /// <summary>
        /// Class for stew effects
        /// </summary>
        public class StewEffect : DataHolderBase
        {
            /// <summary>
            /// Intializes a new stew effect
            /// </summary>
            /// <param name="effect">The effect</param>
            /// <param name="duration">The duration of the effect</param>
            public StewEffect(ID.Effect effect, int duration)
            {
                Effect = effect;
                Duration = duration;
            }

            /// <summary>
            /// The effect
            /// </summary>
            [DataTag("effects", JsonTag = true)]
            public ID.Effect Effect { get; set; }

            /// <summary>
            /// The duration of the effect
            /// </summary>
            [DataTag("duration", JsonTag = true)]
            public int Duration { get; set; }
        }
    }
}
