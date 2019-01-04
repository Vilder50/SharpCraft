using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for redstone wire blocks
        /// </summary>
        public class RedstoneDust : Block
        {
            private int? _sPower;

            /// <summary>
            /// Creates a new redstone wire block
            /// </summary>
            /// <param name="type">The type of block</param>
            public RedstoneDust(ID.Block? type = SharpCraft.ID.Block.redstone_wire) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public RedstoneDust(Group group) : base(group) { }

            /// <summary>
            /// How much power the dust has
            /// (0-15)
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

            /// <summary>
            /// The way the redstone is connected in the direction
            /// </summary>
            [BlockData("east")]
            public ID.StateRedstoneConnection? SEast { get; set; }

            /// <summary>
            /// The way the redstone is connected in the direction
            /// </summary>
            [BlockData("north")]
            public ID.StateRedstoneConnection? SNorth { get; set; }

            /// <summary>
            /// The way the redstone is connected in the direction
            /// </summary>
            [BlockData("south")]
            public ID.StateRedstoneConnection? SSouth { get; set; }

            /// <summary>
            /// The way the redstone is connected in the direction
            /// </summary>
            [BlockData("west")]
            public ID.StateRedstoneConnection? SWest { get; set; }
        }
    }
}
