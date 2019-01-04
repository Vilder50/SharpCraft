using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for hopper blocks
        /// </summary>
        public class Hopper : BaseContainer<Hopper>
        {
            private Item[] _dItems;

            /// <summary>
            /// Creates a new hopper block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Hopper(ID.Block? type = SharpCraft.ID.Block.hopper) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Hopper(Group group) : base(group) { }

            /// <summary>
            /// If the hopper is allowed to transfer items
            /// </summary>
            [BlockData("enabled")]
            public bool? SEnabled { get; set; }
            /// <summary>
            /// The direction the hopper is facing
            /// </summary>
            [BlockData("facing")]
            public ID.StateHopperFacing? SFacing { get; set; }

            /// <summary>
            /// The item's inside the hopper.
            /// (0-4)
            /// </summary>
            public override Item[] DItems
            {
                get => _dItems;
                set
                {
                    if (DItems != null && DItems.Length > 5)
                    {
                        throw new ArgumentException("Too many slots specified");
                    }
                    _dItems = value;
                }
            }

            /// <summary>
            /// The time till the hopper again will transfer an item.
            /// </summary>
            [BlockData]
            public Time DCooldown { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                List<string> TempList = new List<string>();

                string InventoryData = base.GetDataString();
                if (InventoryData.Length != 0) { TempList.Add(InventoryData); }
                if (DCooldown != null) { TempList.Add("TransferCooldown:" + DCooldown.AsTicks()); }

                return string.Join(",", TempList);
            }
        }
    }
}
