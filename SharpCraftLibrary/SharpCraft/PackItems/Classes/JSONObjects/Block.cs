using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// <see cref="object"/>s used in <see cref="LootTable"/>s and <see cref="IAdvancement"/>s
    /// </summary>
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining a <see cref="SharpCraft.Block"/>
        /// </summary>
        public class Block
        {
            /// <summary>
            /// The block to test for
            /// </summary>
            public Block(SharpCraft.Block block)
            {
                TheBlock = block;
            }

            /// <summary>
            /// The block to test for
            /// </summary>
            public SharpCraft.Block TheBlock;

            /// <summary>
            /// Outputs this <see cref="Block"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Block"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (TheBlock != null)
                {
                    if (TheBlock.ID != null) { TempList.Add("\"block\": \"" + TheBlock.ID + "\""); }
                    if (TheBlock.HasState) { TempList.Add("\"state\":{\"" + TheBlock.GetStateString().Replace("=", "\":\"").Replace(",", "\",\"") + "\"}"); }
                }

                return string.Join(",", TempList);
            }

            internal string ToString(bool loottable)
            {
                if (!loottable)
                {
                    return ToString();
                }

                List<string> TempList = new List<string>();

                if (TheBlock != null)
                {
                    if (TheBlock.ID != null) { TempList.Add("\"block\": \"" + TheBlock.ID + "\""); }
                    if (TheBlock.HasState) { TempList.Add("\"properties\":{\"" + TheBlock.GetStateString().Replace("=", "\":\"").Replace(",", "\",\"") + "\"}"); }
                }

                return string.Join(",", TempList);
            }

            /// <summary>
            /// Converts a normal block into a json block.
            /// </summary>
            /// <param name="block">The block to convert</param>
            public static implicit operator Block(SharpCraft.Block block)
            {
                return new Block(block);
            }
        }
    }
}
