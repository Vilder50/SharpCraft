using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for beacon blocks
        /// </summary>
        public class Beacon : Block
        {
            private int? _dLevels;

            /// <summary>
            /// Creates a new beacon block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Beacon(ID.Block? type = SharpCraft.ID.Block.beacon) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Beacon(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.beacon;
            }

            /// <summary>
            /// The inventory's lock.
            /// (Players have to use an item with the given name to open the block)
            /// </summary>
            [BlockData]
            public string DLock { get; set; }

            /// <summary>
            /// The number of layers the beacon pyramide has
            /// </summary>
            [BlockData]
            public int? DLevels
            {
                get => _dLevels;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(DLevels) + " has to be equel to or between 0 and 3");
                    }
                    _dLevels = value;
                }
            }
            /// <summary>
            /// The primary effect chosen in the beacon
            /// </summary>
            [BlockData]
            public ID.Effect? DPrimary { get; set; }
            /// <summary>
            /// The secondary effect chosen in the beacon
            /// </summary>
            [BlockData]
            public ID.Effect? DSecondary { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DLock != null) { TempList.Add("Lock:\"" + DLock.Escape() + "\""); }
                if (DLevels != null) { TempList.Add("Levels:" + DLevels); }
                if (DPrimary != null) { TempList.Add("Primary:" + (int)DPrimary); }
                if (DSecondary != null) { TempList.Add("Secondary:" + (int)DSecondary); }

                return string.Join(",", TempList);
            }
        }
    }
}
