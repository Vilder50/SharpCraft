using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for dispensers / dropper blocks
        /// </summary>
        public class DropperDispenser : BaseContainer<DropperDispenser>, IBlock.IPowered, IBlock.IFacingFull
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a new chest dispenser / dropper
            /// </summary>
            /// <param name="type">The type of block</param>
            public DropperDispenser(ID.Block? type = SharpCraft.ID.Block.dispenser) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public DropperDispenser(Group group) : base(group) { }

            /// <summary>
            /// The direction the dispenser / dropper is facing
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }
            /// <summary>
            /// If the dispenser / dropper is powered right now
            /// </summary>
            [BlockData("triggered")]
            public bool? SPowered { get; set; }

            /// <summary>
            /// The item's inside the dispenser / dropper.
            /// (0-8)
            /// </summary>
            public override Item[] DItems
            {
                get => _dItems;
                set
                {
                    if (DItems != null && DItems.Length > 9)
                    {
                        throw new ArgumentException("Too many slots specified");
                    }
                    _dItems = value;
                }
            }
        }
    }
}
