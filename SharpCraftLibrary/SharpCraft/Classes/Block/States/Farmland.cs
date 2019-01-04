using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for farm land blocks
        /// </summary>
        public class Farmland : Block
        {
            private int? _sHydration;

            /// <summary>
            /// Creates a new farm land block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Farmland(ID.Block? type = SharpCraft.ID.Block.farmland) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Farmland(Group group) : base(group) { }

            /// <summary>
            /// How hydrated the farmland is.
            /// (0-7. 7 == fully hydrated)
            /// </summary>
            [BlockData("moisture")]
            public int? SHydration
            {
                get => _sHydration;
                set
                {
                    if (value != null && (value < 0 || value > 7))
                    {
                        throw new ArgumentException(nameof(SHydration) + " has to be equel to or between 0 and 7");
                    }
                    _sHydration = value;
                }
            }
        }
    }
}
