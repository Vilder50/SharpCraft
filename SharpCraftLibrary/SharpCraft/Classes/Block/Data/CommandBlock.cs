using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for chest blocks
        /// </summary>
        public class CommandBlock : Block, IBlock.IFacingFull
        {
            /// <summary>
            /// Creates a new chest block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CommandBlock(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CommandBlock(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.command_block || block == SharpCraft.ID.Block.chain_command_block || block == SharpCraft.ID.Block.repeating_command_block;
            }

            /// <summary>
            /// The direction the command block is facing
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// If the command block is conditional
            /// </summary>
            [BlockData("conditional")]
            public bool? SConditional { get; set; }

            /// <summary>
            /// The name of the command block
            /// </summary>
            [BlockData]
            public JSON[] DCustomName { get; set; }

            /// <summary>
            /// The command in the command block
            /// </summary>
            [BlockData]
            public string DCommand { get; set; }

            /// <summary>
            /// The last command's string output
            /// </summary>
            [BlockData]
            public JSON[] DLastOutput { get; set; }

            /// <summary>
            /// The last command's success output
            /// </summary>
            [BlockData]
            public int? DSuccessCount { get; set; }

            /// <summary>
            /// The point in time the last command was ran
            /// </summary>
            [BlockData]
            public long? DLastExecution { get; set; }

            /// <summary>
            /// If the command block should store <see cref="DLastOutput"/>
            /// </summary>
            [BlockData]
            public bool? DTrackOutput { get; set; }

            /// <summary>
            /// If the command block is powered
            /// </summary>
            [BlockData]
            public bool? DPowered { get; set; }

            /// <summary>
            /// If the command block doesnt haeve to be powered to run the command
            /// </summary>
            [BlockData]
            public bool? DAuto { get; set; }

            /// <summary>
            /// If the command block ran last time
            /// </summary>
            [BlockData]
            public bool? DConditionMet { get; set; }

            /// <summary>
            /// If the command block should be able to run multiple times in the same tick.
            /// </summary>
            [BlockData]
            public bool? DUpdateLastExecution { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DCustomName != null) { TempList.Add("CustomName:\"" + DCustomName.GetString(false).Escape() + "\""); }
                if (DCommand != null) { TempList.Add("Command:\"" + DCommand.Escape() + "\""); }
                if (DLastOutput != null) { TempList.Add("LastOutput:\"" + DLastOutput.GetString(false).Escape() + "\""); }
                if (DSuccessCount != null) { TempList.Add("SuccessCount:" + DSuccessCount); }
                if (DLastExecution != null) { TempList.Add("LastExecution:" + DLastExecution); }
                if (DTrackOutput != null) { TempList.Add("TrackOutput:" + DTrackOutput); }
                if (DPowered != null) { TempList.Add("powered:" + DPowered); }
                if (DConditionMet != null) { TempList.Add("conditionMet:" + DConditionMet); }
                if (DUpdateLastExecution != null) { TempList.Add("UpdateLastExecution:" + DUpdateLastExecution); }

                return string.Join(",", TempList);
            }
        }
    }
}
