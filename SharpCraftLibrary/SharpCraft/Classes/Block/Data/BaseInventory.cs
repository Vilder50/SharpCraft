using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// A base class ofr blocks with inventories
        /// </summary>
        public abstract class BaseInventory : Block
        {
            /// <summary>
            /// Creates a new inventory block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BaseInventory(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BaseInventory(Group group) : base(group) { }

            /// <summary>
            /// The inventory's lock.
            /// (Players have to use an item with the given name to open the block)
            /// </summary>
            [BlockData]
            public string DLock { get; set; }
            /// <summary>
            /// The custom name displayed at the top left corner when you open the block
            /// </summary>
            [BlockData]
            public JSON[] DCustomName { get; set; }
            /// <summary>
            /// The items in the inventory
            /// </summary>
            [BlockData]
            public abstract Item[] DItems { get; set; }

            /// <summary>
            /// Gets the raw data for the inventory data
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DLock != null) { TempList.Add("Lock:\"" + DLock.Escape() + "\""); }
                if (DCustomName != null) { TempList.Add("CustomName:\"" + DCustomName.GetString().Escape() + "\""); }
                if (DItems != null)
                {
                    string TempString = "Items:[";
                    for (int a = 0; a < DItems.Length; a++)
                    {
                        if (a != 0) { TempString += ","; }
                        TempString += "{" + DItems[a].DataString + "}";
                    }
                    TempString += "]";
                    TempList.Add(TempString);
                }

                return string.Join(",", TempList);
            }
        }
    }
}
