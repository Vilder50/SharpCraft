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
        public class Barrel : BaseContainer<Barrel>, IBlock.IOpen, IBlock.IFacingFull
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a barrel block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Barrel(ID.Block? type = SharpCraft.ID.Block.barrel) : base(type) { }
            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Barrel(Group group) : base(group) { }


            /// <summary>
            /// The direction the barrel is facing (the way it opens out into )
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// If the barrel is open or not
            /// </summary>
            [BlockData("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// The item's inside the barrel.
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
