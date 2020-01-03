using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which grants/revokes all advancements for some players
    /// </summary>
    public class AdvancementAllCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="AdvancementAllCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke all advancements for</param>
        /// <param name="grant">True if the advancements should be granted. False if they should be revoked</param>
        public AdvancementAllCommand(BaseSelector selector, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke all advancements for
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
        /// True if the advancements should be granted. False if they should be revoked
        /// </summary>
        public bool Grant { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>advancement grant/revoke [Selector] everything</returns>
        public override string GetCommandString()
        {
            return $"advancement {(Grant ? "grant" : "revoke")} {Selector} everything";
        }
    }

    /// <summary>
    /// Command which grants/revokes a single advancement or advancement criterion for some players
    /// </summary>
    public class AdvancementSingleCommand : BaseCommand
    {
        private BaseSelector selector;
        private IAdvancement advancement;

        /// <summary>
        /// Intializes a new <see cref="AdvancementSingleCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke the advancement or advancement criterion for</param>
        /// <param name="grant">True if the advancement should be granted. False if it should be revoked</param>
        /// <param name="advancement">The advancement to grant/revoke</param>
        /// <param name="criterion">The advancement criterion to grant/revoke. Leave null to only grant/revoke the advancement</param>
        public AdvancementSingleCommand(BaseSelector selector, IAdvancement advancement, AdvancementObjects.ITrigger criterion, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
            Advancement = advancement;
            Criterion = criterion;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke the advancement or advancement criterion for
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
        /// The advancement to grant/revoke
        /// </summary>
        public IAdvancement Advancement
        {
            get => advancement;
            set
            {
                advancement = value ?? throw new ArgumentNullException(nameof(Advancement), "Advancement may not be null.");
            }
        }

        /// <summary>
        /// The advancement criterion to grant/revoke. Leave null to only grant/revoke the advancement
        /// </summary>
        public AdvancementObjects.ITrigger Criterion { get; set; }

        /// <summary>
        /// True if the advancement should be granted. False if it should be revoked
        /// </summary>
        public bool Grant { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>advancement grant/revoke [Selector] only [Advancement] (Criterion)</returns>
        public override string GetCommandString()
        {
            return $"advancement {(Grant ? "grant" : "revoke")} {Selector} only {Advancement.GetNamespacedName()}{(Criterion is null ? "" : " " + Criterion.Name)}";
        }
    }

    /// <summary>
    /// Command which grants/revokes some advancement for some players based on the advancements relative position to another advancement
    /// </summary>
    public class AdvancementSomeCommand : BaseCommand
    {
        private BaseSelector selector;
        private IAdvancement advancement;

        /// <summary>
        /// Intializes a new <see cref="AdvancementSomeCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke the advancement or advancement criterion for</param>
        /// <param name="grant">True if the advancement should be granted. False if it should be revoked</param>
        /// <param name="advancement">The advancement to grant/revoke</param>
        /// <param name="select">The advancements to grant/revoke</param>
        public AdvancementSomeCommand(BaseSelector selector, IAdvancement advancement, ID.RelativeAdvancement select, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
            Advancement = advancement;
            Select = select;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke the advancement or advancement criterion for
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
        /// The advancement to grant/revoke
        /// </summary>
        public IAdvancement Advancement
        {
            get => advancement;
            set
            {
                advancement = value ?? throw new ArgumentNullException(nameof(Advancement), "Advancement may not be null.");
            }
        }

        /// <summary>
        /// The advancements to grant/revoke
        /// </summary>
        public ID.RelativeAdvancement Select { get; set; }

        /// <summary>
        /// True if the advancement should be granted. False if it should be revoked
        /// </summary>
        public bool Grant { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>advancement grant/revoke [Selector] [Select] [Advancement]</returns>
        public override string GetCommandString()
        {
            return $"advancement {(Grant ? "grant" : "revoke")} {Selector} {Select} {Advancement.GetNamespacedName()}";
        }
    }
}
