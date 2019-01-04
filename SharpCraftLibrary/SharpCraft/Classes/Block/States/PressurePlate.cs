﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pressure plate blocks
        /// </summary>
        public class PressurePlate : Block
        {
            private int? _sPower;

            /// <summary>
            /// Creates a new pressure plate block
            /// </summary>
            /// <param name="type">The type of block</param>
            public PressurePlate(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public PressurePlate(Group group) : base(group) { }

            /// <summary>
            /// If the plate is pushed down
            /// </summary>
            [BlockData("powered")]
            public bool? SDown { get; set; }

            /// <summary>
            /// How much redstone power the plate outputs
            /// (0-15)
            /// Note: only works for metal plates
            /// </summary>
            [BlockData("power")]
            public int? SPower
            {
                get => _sPower;
                set
                {
                    if (value != null && (value < 0 || value > 15))
                    {
                        throw new ArgumentException(nameof(SPower) + " has to be equel to or between 0 and 15");
                    }
                    _sPower = value;
                }
            }
        }
    }
}
