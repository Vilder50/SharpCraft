using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Slots;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which replaces an item in a block
    /// </summary>
    public class ReplaceitemBlockCommand : BaseCommand
    {
        private IItemSlot slot;
        private Coords coordinates;
        private Item item;
        private int count;

        /// <summary>
        /// Intializes a new <see cref="ReplaceitemBlockCommand"/>
        /// </summary>
        /// <param name="coordinates">The coordinates of the block to put the item into</param>
        /// <param name="slot">The slot to put the item into</param>
        /// <param name="item">The item to put into the block</param>
        /// <param name="count">The amount of the item</param>
        public ReplaceitemBlockCommand(Coords coordinates, IItemSlot slot, Item item, int count)
        {
            Coordinates = coordinates;
            Slot = slot;
            Item = item;
            Count = count;
        }

        /// <summary>
        /// The coordinates of the block to put the item into
        /// </summary>
        public Coords Coordinates { get => coordinates; set => coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null"); }

        /// <summary>
        /// The slot to put the item into
        /// </summary>
        public IItemSlot Slot { get => slot; set => slot = value ?? throw new ArgumentNullException(nameof(Slot), "Slot may not be null"); }

        /// <summary>
        /// The item to put into the block
        /// </summary>
        public Item Item { get => item; set => item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null"); }

        /// <summary>
        /// The amount of the item
        /// </summary>
        public int Count 
        { 
            get => count;
            set 
            {
                if (value < 1 || value > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be null");
                }
                count = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>replaceitem block [Coordinates] [Slot] [Item] [Count]</returns>
        public override string GetCommandString()
        {
            return $"replaceitem block {Coordinates} {Slot.GetSlotString()} {Item.IDDataString} {Count}";
        }
    }

    /// <summary>
    /// Command which replaces an item on one or more entities
    /// </summary>
    public class ReplaceitemEntityCommand : BaseCommand
    {
        private IItemSlot slot;
        private BaseSelector selector;
        private Item item;
        private int count;

        /// <summary>
        /// Intializes a new <see cref="ReplaceitemEntityCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the entities to replace the item on</param>
        /// <param name="slot">The slot to put the item into</param>
        /// <param name="item">The item to put into the block</param>
        /// <param name="count">The amount of the item</param>
        public ReplaceitemEntityCommand(BaseSelector selector, IItemSlot slot, Item item, int count)
        {
            Selector = selector;
            Slot = slot;
            Item = item;
            Count = count;
        }

        /// <summary>
        /// Selector selecting the entities to replace the item on
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The slot to put the item into
        /// </summary>
        public IItemSlot Slot { get => slot; set => slot = value ?? throw new ArgumentNullException(nameof(Slot), "Slot may not be null"); }

        /// <summary>
        /// The item to put into the block
        /// </summary>
        public Item Item { get => item; set => item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null"); }

        /// <summary>
        /// The amount of the item
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 1 || value > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be null");
                }
                count = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>replaceitem entity [Selector] [Slot] [Item] [Count]</returns>
        public override string GetCommandString()
        {
            return $"replaceitem entity {Selector} {Slot.GetSlotString()} {Item.IDDataString} {Count}";
        }
    }
}
