using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cocoa bean blocks
        /// </summary>
        public class CocoaBean : Block
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new cocoa bean block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CocoaBean(ID.Block? type = SharpCraft.ID.Block.cocoa) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CocoaBean(Group group) : base(group) { }

            /// <summary>
            /// How much the cocoa bean has grown
            /// (0-2. 2 == fully grown)
            /// </summary>
            [BlockData("age")]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 2))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 2");
                    }
                    _sAge = value;
                }
            }

            /// <summary>
            /// The direction of the log the bean is placed on.
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? Facing { get; set; }
        }
    }
}
