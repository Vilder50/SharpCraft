using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for chest blocks
        /// </summary>
        public class Chest : BaseContainer<Chest>
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a new chest block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Chest(ID.Block? type = SharpCraft.ID.Block.chest) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Chest(Group group) : base(group) { }

            /// <summary>
            /// If the chest is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }

            /// <summary>
            /// The direction the chest is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// How the chest is connected to another chest
            /// </summary>
            [BlockData("type")]
            public ID.StateChestType? SConnectionType { get; set; }

            /// <summary>
            /// The item's inside the chest.
            /// (0-26)
            /// </summary>
            public override Item[] DItems
            {
                get => _dItems;
                set
                {
                    if (DItems != null && DItems.Length > 27)
                    {
                        throw new ArgumentException("Too many slots specified");
                    }
                    _dItems = value;
                }
            }
        }
    }
}
