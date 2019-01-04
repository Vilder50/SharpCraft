using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for shulker box and barral blocks
        /// </summary>
        public class ShulkerBoxBarrel : BaseContainer<ShulkerBoxBarrel>
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a new shulker box or barrel block
            /// </summary>
            /// <param name="type">The type of block</param>
            public ShulkerBoxBarrel(ID.Block? type) : base(type) { }
            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public ShulkerBoxBarrel(Group group) : base(group) { }


            /// <summary>
            /// The direction the shulker box / barrel is facing (the way it opens out into )
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull SFacing { get; set; }

            /// <summary>
            /// The item's inside the shulker box / barrel.
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
