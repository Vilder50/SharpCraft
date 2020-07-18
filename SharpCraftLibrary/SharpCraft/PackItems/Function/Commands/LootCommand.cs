using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Slots;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Targets for the loot command
    /// </summary>
    public static class LootTargets
    {
        /// <summary>
        /// Interface for the target to give the loot to
        /// </summary>
        public interface ILootTarget
        {
            /// <summary>
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            string GetTargetString();
        }

        /// <summary>
        /// Spawns the loot into the world
        /// </summary>
        public class SpawnTarget : ILootTarget
        {
            private Vector coordinates = null!;

            /// <summary>
            /// Intializes a new <see cref="SpawnTarget"/>
            /// </summary>
            /// <param name="coordinates">The coordinates to spawn the loot at</param>
            public SpawnTarget(Vector coordinates)
            {
                Coordinates = coordinates;
            }

            /// <summary>
            /// The coordinates to spawn the loot at
            /// </summary>
            public Vector Coordinates
            {
                get => coordinates;
                set
                {
                    coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
                }
            }

            /// <summary>
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            public string GetTargetString()
            {
                return $"spawn {Coordinates.GetVectorString()}";
            }
        }

        /// <summary>
        /// Gives the loot to one or more entities
        /// </summary>
        public class EntityTarget : ILootTarget
        {
            private BaseSelector selector = null!;
            private IItemSlot slot = null!;

            /// <summary>
            /// Intializes a new <see cref="EntityTarget"/>
            /// </summary>
            /// <param name="selector">Selector selecting the entities who should be given the loot</param>
            /// <param name="slot">The slot to insert the loot into</param>
            public EntityTarget(BaseSelector selector, IItemSlot slot)
            {
                Selector = selector;
                Slot = slot;
            }

            /// <summary>
            /// Selector selecting the entities who should be given the loot
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
            /// The slot to insert the loot into
            /// </summary>
            public IItemSlot Slot
            {
                get => slot;
                set
                {
                    slot = value ?? throw new ArgumentNullException(nameof(Slot), "Slot may not be null.");
                }
            }

            /// <summary>
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            public string GetTargetString()
            {
                return $"replace entity {Selector.GetSelectorString()} {Slot.GetSlotString()}";
            }
        }

        /// <summary>
        /// Gives the loot to a block
        /// </summary>
        public class BlockTarget : ILootTarget
        {
            private Vector coordinates = null!;
            private IItemSlot slot = null!;

            /// <summary>
            /// Intializes a new <see cref="BlockTarget"/>
            /// </summary>
            /// <param name="coordinates">The coordinates to spawn the loot at</param>
            /// <param name="slot">The slot to insert the loot into</param>
            public BlockTarget(Vector coordinates, IItemSlot slot)
            {
                Coordinates = coordinates;
                Slot = slot;
            }

            /// <summary>
            /// The coordinates to spawn the loot at
            /// </summary>
            public Vector Coordinates
            {
                get => coordinates;
                set
                {
                    coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
                }
            }

            /// <summary>
            /// The slot to insert the loot into
            /// </summary>
            public IItemSlot Slot
            {
                get => slot;
                set
                {
                    slot = value ?? throw new ArgumentNullException(nameof(Slot), "Slot may not be null.");
                }
            }

            /// <summary>
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            public string GetTargetString()
            {
                return $"replace block {Coordinates.GetVectorString()} {Slot.GetSlotString()}";
            }
        }

        /// <summary>
        /// Gives the loot to one or more players
        /// </summary>
        public class GiveTarget : ILootTarget
        {
            private BaseSelector selector = null!;

            /// <summary>
            /// Intializes a new <see cref="EntityTarget"/>
            /// </summary>
            /// <param name="selector">Selector selecting the players who should be given the loot</param>
            public GiveTarget(BaseSelector selector)
            {
                Selector = selector;
            }

            /// <summary>
            /// Selector selecting the players who should be given the loot
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
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            public string GetTargetString()
            {
                return $"give {Selector.GetSelectorString()}";
            }
        }

        /// <summary>
        /// Spawns the loot in a block
        /// </summary>
        public class InsertTarget : ILootTarget
        {
            private Vector coordinates = null!;

            /// <summary>
            /// Intializes a new <see cref="InsertTarget"/>
            /// </summary>
            /// <param name="coordinates">The coordinates to spawn the loot in</param>
            public InsertTarget(Vector coordinates)
            {
                Coordinates = coordinates;
            }

            /// <summary>
            /// The coordinates to spawn the loot in
            /// </summary>
            public Vector Coordinates
            {
                get => coordinates;
                set
                {
                    coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
                }
            }

            /// <summary>
            /// Returns a string for selecting the target
            /// </summary>
            /// <returns>A string for selecting the target</returns>
            public string GetTargetString()
            {
                return $"insert {Coordinates.GetVectorString()}";
            }
        }
    }

    /// <summary>
    /// Sources for the loot command
    /// </summary>
    public static class LootSources
    {
        /// <summary>
        /// Interface for the source to get the loot from
        /// </summary>
        public interface ILootSource
        {
            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            string GetSourceString();
        }

        /// <summary>
        /// Gets loot like it was gotten from fishing using an item an entity is holding
        /// </summary>
        public class FishHandSource : ILootSource
        {
            private ILootTable loottable = null!;
            private Vector fishLocation = null!;

            /// <summary>
            /// Intailizes a new <see cref="FishHandSource"/>
            /// </summary>
            /// <param name="loottable">The loot table the "fish" is coming from</param>
            /// <param name="fishLocation">The location the fish was caught</param>
            /// <param name="mainHand">True if the fish was caught with the executing entity's mainhand. False if with off hand</param>
            public FishHandSource(ILootTable loottable, Vector fishLocation, bool mainHand)
            {
                Loottable = loottable;
                FishLocation = fishLocation;
                MainHand = mainHand;
            }

            /// <summary>
            /// The loot table the "fish" is coming from
            /// </summary>
            public ILootTable Loottable 
            { 
                get => loottable; 
                set => loottable = value ?? throw new ArgumentNullException(nameof(Loottable), "Loottable may not be null"); 
            }
            /// <summary>
            /// The location the fish was caught
            /// </summary>
            public Vector FishLocation 
            { 
                get => fishLocation; 
                set => fishLocation = value ?? throw new ArgumentNullException(nameof(FishLocation), "FishLocation may not be null"); 
            }

            /// <summary>
            /// True if the fish was caught with the executing entity's mainhand. False if with off hand
            /// </summary>
            public bool MainHand { get; set; }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"fish {Loottable.GetNamespacedName()} {FishLocation.GetVectorString()} {(MainHand ? "mainhand" : "offhand")}";
            }
        }

        /// <summary>
        /// Gets loot like it was gotten from fishing using a specific item
        /// </summary>
        public class FishItemSource : ILootSource
        {
            private ILootTable loottable = null!;
            private Vector fishLocation = null!;
            private Item usedItem = null!;

            /// <summary>
            /// Intializes a new <see cref="FishItemSource"/>
            /// </summary>
            /// <param name="loottable">The loot table the "fish" is coming from</param>
            /// <param name="fishLocation">The location the fish was caught</param>
            /// <param name="usedItem">The item used to get the "fish"</param>
            public FishItemSource(ILootTable loottable, Vector fishLocation, Item usedItem)
            {
                Loottable = loottable;
                FishLocation = fishLocation;
                UsedItem = usedItem;
            }

            /// <summary>
            /// The loot table the "fish" is coming from
            /// </summary>
            public ILootTable Loottable
            {
                get => loottable;
                set => loottable = value ?? throw new ArgumentNullException(nameof(Loottable), "Loottable may not be null");
            }
            /// <summary>
            /// The location the fish was caught
            /// </summary>
            public Vector FishLocation
            {
                get => fishLocation;
                set => fishLocation = value ?? throw new ArgumentNullException(nameof(FishLocation), "FishLocation may not be null");
            }

            /// <summary>
            /// The item used to get the "fish"
            /// </summary>
            public Item UsedItem 
            { 
                get => usedItem; 
                set => usedItem = value ?? throw new ArgumentNullException(nameof(UsedItem), "UsedItem may not be null"); 
            }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"fish {Loottable.GetNamespacedName()} {FishLocation.GetVectorString()} {UsedItem.GetIDDataString()}";
            }
        }

        /// <summary>
        /// Gets loot from a loot table
        /// </summary>
        public class LoottableSource : ILootSource
        {
            private ILootTable loottable = null!;

            /// <summary>
            /// Intializes a new <see cref="LoottableSource"/>
            /// </summary>
            /// <param name="loottable">The loot table the get the loot from</param>
            public LoottableSource(ILootTable loottable)
            {
                Loottable = loottable;
            }

            /// <summary>
            /// The loot table the get the loot from
            /// </summary>
            public ILootTable Loottable
            {
                get => loottable;
                set => loottable = value ?? throw new ArgumentNullException(nameof(Loottable), "Loottable may not be null");
            }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"loot {Loottable.GetNamespacedName()}";
            }
        }

        /// <summary>
        /// Gets the loot the entity would drop if killed
        /// </summary>
        public class KillSource : ILootSource
        {
            private BaseSelector selector = null!;

            /// <summary>
            /// Intializes a new <see cref="KillSource"/>
            /// </summary>
            /// <param name="selector">Selector selecting the entity to get kill loot from</param>
            public KillSource(BaseSelector selector)
            {
                Selector = selector;
            }

            /// <summary>
            /// Selector selecting the entity to get kill loot from
            /// </summary>
            public BaseSelector Selector
            {
                get => selector;
                set
                {
                    selector = Validators.ValidateSingleSelectSelector(value, nameof(Selector), nameof(KillSource));
                }
            }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"kill {Selector.GetSelectorString()}";
            }
        }

        /// <summary>
        /// Gets the loot the block would drop if mined with an item an entity is holding
        /// </summary>
        public class MineHandSource : ILootSource
        {
            private Vector coordinates = null!;

            /// <summary>
            /// Intializes a new <see cref="MineHandSource"/>
            /// </summary>
            /// <param name="coordinates">The coordinates of the block get the loot from</param>
            /// <param name="mainHand">True if the block was mined with the main hand. False if with off hand</param>
            public MineHandSource(Vector coordinates, bool mainHand)
            {
                Coordinates = coordinates;
                MainHand = mainHand;
            }

            /// <summary>
            /// The coordinates of the block get the loot from
            /// </summary>
            public Vector Coordinates
            {
                get => coordinates;
                set
                {
                    coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
                }
            }

            /// <summary>
            /// True if the block was mined with executer's the main hand. False if with off hand
            /// </summary>
            public bool MainHand { get; set; }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"mine {Coordinates.GetVectorString()} {(MainHand ? "mainhand" : "offhand")}";
            }
        }

        /// <summary>
        /// Gets the loot the block would drop if mined with a specific item
        /// </summary>
        public class MineItemSource : ILootSource
        {
            private Vector coordinates = null!;
            private Item usedItem = null!;

            /// <summary>
            /// Intializes a new <see cref="MineHandSource"/>
            /// </summary>
            /// <param name="coordinates">The coordinates of the block get the loot from</param>
            /// <param name="usedItem">The item used for mining</param>
            public MineItemSource(Vector coordinates, Item usedItem)
            {
                Coordinates = coordinates;
                UsedItem = usedItem;
            }

            /// <summary>
            /// The coordinates of the block get the loot from
            /// </summary>
            public Vector Coordinates
            {
                get => coordinates;
                set
                {
                    coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
                }
            }

            /// <summary>
            /// The item used for mining
            /// </summary>
            public Item UsedItem
            {
                get => usedItem;
                set => usedItem = value ?? throw new ArgumentNullException(nameof(UsedItem), "UsedItem may not be null");
            }

            /// <summary>
            /// Returns a string for selecting the source
            /// </summary>
            /// <returns>A string for selecting the source</returns>
            public string GetSourceString()
            {
                return $"mine {Coordinates.GetVectorString()} {UsedItem.GetIDDataString()}";
            }
        }
    }

    /// <summary>
    /// Command which gets the loot from somewhere and places it somewhere else
    /// </summary>
    public class LootCommand : BaseCommand
    {
        private LootSources.ILootSource source = null!;
        private LootTargets.ILootTarget target = null!;

        /// <summary>
        /// Intializes a new <see cref="LootCommand"/>
        /// </summary>
        /// <param name="source">The source to get the loot from</param>
        /// <param name="target">The place to place the loot</param>
        public LootCommand(LootTargets.ILootTarget target, LootSources.ILootSource source)
        {
            Source = source;
            Target = target;
        }

        /// <summary>
        /// The source to get the loot from
        /// </summary>
        public LootSources.ILootSource Source { get => source; set => source = value ?? throw new ArgumentNullException(nameof(Source), "Source may not be null"); }

        /// <summary>
        /// The place to place the loot
        /// </summary>
        public LootTargets.ILootTarget Target { get => target; set => target = value ?? throw new ArgumentNullException(nameof(Target), "Target may not be null"); }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>loot [Source] [Target]</returns>
        public override string GetCommandString()
        {
            return $"loot {Target.GetTargetString()} {Source.GetSourceString()}";
        }
    }
}
