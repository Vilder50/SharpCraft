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
        private BaseSelector spectate = null!;
        private BaseSelector spectator = null!;

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
                spectate = Validators.ValidateSingleSelectSelector(value, nameof(Spectate), nameof(SpectateCommand));
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
                spectator = Validators.ValidateSingleSelectSelector(value, nameof(Spectator), nameof(SpectateCommand));
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
