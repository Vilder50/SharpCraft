﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for frosted ice blocks
        /// </summary>
        public class FrostedIce : Block
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new frosted ice block
            /// </summary>
            /// <param name="type">The type of block</param>
            public FrostedIce(ID.Block? type = SharpCraft.ID.Block.frosted_ice) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public FrostedIce(Group group) : base(group) { }

            /// <summary>
            /// The age of the ice.
            /// (0-3. When 3 it has a chance to break)
            /// </summary>
            [BlockData("age")]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 3");
                    }
                    _sAge = value;
                }
            }
        }
    }
}
