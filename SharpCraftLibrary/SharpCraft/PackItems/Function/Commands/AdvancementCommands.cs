﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which grants/revokes all advancements for some players
    /// </summary>
    public class AdvancementAllCommand : ICommand
    {
        private Selector selector;

        /// <summary>
        /// Intializes a new <see cref="AdvancementAllCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke all advancements for</param>
        /// <param name="grant">True if the advancements should be granted. False if they should be revoked</param>
        public AdvancementAllCommand(Selector selector, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke all advancements for
        /// </summary>
        public Selector Selector
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
        public string GetCommandString()
        {
            return $"advancement {(Grant ? "Grant" : "Revoke")} {Selector} everything";
        }
    }

    /// <summary>
    /// Command which grants/revokes a single advancement or advancement criterion for some players
    /// </summary>
    public class AdvancementSingleCommand : ICommand
    {
        private Selector selector;
        private Advancement advancement;

        /// <summary>
        /// Intializes a new <see cref="AdvancementSingleCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke the advancement or advancement criterion for</param>
        /// <param name="grant">True if the advancement should be granted. False if it should be revoked</param>
        /// <param name="advancement">The advancement to grant/revoke</param>
        /// <param name="criterion">The advancement criterion to grant/revoke. Leave null to only grant/revoke the advancement</param>
        public AdvancementSingleCommand(Selector selector, Advancement advancement, Advancement.Trigger criterion, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
            Advancement = advancement;
            Criterion = criterion;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke the advancement or advancement criterion for
        /// </summary>
        public Selector Selector
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
        public Advancement Advancement
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
        public Advancement.Trigger Criterion { get; set; }

        /// <summary>
        /// True if the advancement should be granted. False if it should be revoked
        /// </summary>
        public bool Grant { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>advancement grant/revoke [Selector] only [Advancement] (Criterion)</returns>
        public string GetCommandString()
        {
            return $"advancement {(Grant ? "Grant" : "Revoke")} {Selector} only {Advancement.GetNamespacedName()} {Criterion?.Name ?? ""}";
        }
    }

    /// <summary>
    /// Command which grants/revokes some advancement for some players based on the advancements relative position to another advancement
    /// </summary>
    public class AdvancementSomeCommand : ICommand
    {
        private Selector selector;
        private Advancement advancement;

        /// <summary>
        /// Intializes a new <see cref="AdvancementSomeCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to grant/revoke the advancement or advancement criterion for</param>
        /// <param name="grant">True if the advancement should be granted. False if it should be revoked</param>
        /// <param name="advancement">The advancement to grant/revoke</param>
        /// <param name="select">The advancements to grant/revoke</param>
        public AdvancementSomeCommand(Selector selector, Advancement advancement, ID.RelativeAdvancement select, bool grant = true)
        {
            Selector = selector;
            Grant = grant;
            Advancement = advancement;
            Select = select;
        }

        /// <summary>
        /// Selector for selecting players to grant/revoke the advancement or advancement criterion for
        /// </summary>
        public Selector Selector
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
        public Advancement Advancement
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
        public string GetCommandString()
        {
            return $"advancement {(Grant ? "Grant" : "Revoke")} {Selector} {Select} {Advancement.GetNamespacedName()}";
        }
    }
}
