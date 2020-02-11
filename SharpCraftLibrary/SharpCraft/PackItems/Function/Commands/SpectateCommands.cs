using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which makes a player spectate an entity
    /// </summary>
    public class SpectateCommand : BaseCommand
    {
        private BaseSelector spectate;
        private BaseSelector spectator;

        /// <summary>
        /// Intializes a new <see cref="SpectateCommand"/>
        /// </summary>
        /// <param name="spectate">Selector selecting the entity to spectate</param>
        /// <param name="spectator">Selector selecting the player which should be spectating. (Player has to be in spectator mode)</param>
        public SpectateCommand(BaseSelector spectate, BaseSelector spectator)
        {
            Spectate = spectate;
            Spectator = spectator;
        }

        /// <summary>
        /// Selector selecting the entity to spectate
        /// </summary>
        public BaseSelector Spectate
        {
            get => spectate;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Spectate), "Spectate may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                spectate = value;
            }
        }

        /// <summary>
        /// Selector selecting the player which should be spectating. (Player has to be in spectator mode)
        /// </summary>
        public BaseSelector Spectator
        {
            get => spectator;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Spectator), "Spectator may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(BaseSelector));
                }
                spectator = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>spectate [Spectate] [Spectator]</returns>
        public override string GetCommandString()
        {
            return $"spectate {Spectate.GetSelectorString()} {Spectator.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Makes the executing player stop spectating
    /// </summary>
    public class SpectateStopCommand : BaseCommand
    {
        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>spectate</returns>
        public override string GetCommandString()
        {
            return "spectate";
        }
    }
}
