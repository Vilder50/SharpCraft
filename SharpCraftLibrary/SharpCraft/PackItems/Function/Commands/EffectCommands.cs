using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command for giving entities an effect
    /// </summary>
    public class EffectGiveCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private int seconds;

        /// <summary>
        /// Intializes a new <see cref="EffectGiveCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting entities to give the effect to</param>
        /// <param name="effect">The effect to give</param>
        /// <param name="seconds">The amount of seconds the entities will have the effect for</param>
        /// <param name="amplifier">The amplifier of the effect</param>
        /// <param name="hideParticles">If the effect shouldn't show particles</param>
        public EffectGiveCommand(BaseSelector selector, ID.Effect effect, int seconds, byte amplifier, bool hideParticles)
        {
            Selector = selector;
            Seconds = seconds;
            Amplifier = amplifier;
            Effect = effect;
            HideParticles = hideParticles;
        }

        /// <summary>
        /// Selector selecting entities to give the effect to
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }
        
        /// <summary>
        /// The amount of seconds the entities will have the effect for
        /// </summary>
        public int Seconds 
        {
            get => seconds; 
            set
            {
                if (value < 0 || value > 1000000)
                {
                    throw new ArgumentOutOfRangeException(nameof(Seconds), "Seconds may not be less than 0 or higher than 1000000");
                }
                seconds = value;
            }
        }

        /// <summary>
        /// The amplifier of the effect
        /// </summary>
        public byte Amplifier { get; set; }

        /// <summary>
        /// The effect to give
        /// </summary>
        public ID.Effect Effect { get; set; }

        /// <summary>
        /// If the effect shouldn't show particles
        /// </summary>
        public bool HideParticles { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>effect give [Selector] [Effect] [Seconds] [Amplifier] [HideParticles]</returns>
        public override string GetCommandString()
        {
            if (!HideParticles)
            {
                if (Amplifier == 0)
                {
                    if (Seconds == 30)
                    {
                        return $"effect give {Selector.GetSelectorString()} {Effect}";
                    }
                    else
                    {
                        return $"effect give {Selector.GetSelectorString()} {Effect} {Seconds}";
                    }
                }
                else
                {
                    return $"effect give {Selector.GetSelectorString()} {Effect} {Seconds} {Amplifier}";
                }
            }
            else
            {
                return $"effect give {Selector.GetSelectorString()} {Effect} {Seconds} {Amplifier} {HideParticles.ToMinecraftBool()}";
            }
        }
    }

    /// <summary>
    /// Command which removes one or more effects from some entities
    /// </summary>
    public class EffectClearCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="EffectClearCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the entities whose effects to clear</param>
        /// <param name="effect">The effect to clear. Set to null to clear all effects</param>
        public EffectClearCommand(BaseSelector selector, ID.Effect? effect)
        {
            Selector = selector;
            Effect = effect;
        }

        /// <summary>
        /// Selector selecting the entities whose effects to clear
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The effect to clear. Set to null to clear all effects
        /// </summary>
        public ID.Effect? Effect { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>effect clear [Selector] [Effect]</returns>
        public override string GetCommandString()
        {
            return $"effect clear {Selector.GetSelectorString()} {Effect}";
        }
    }
}
