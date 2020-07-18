using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for chest blocks
    /// </summary>
    public class CommandBlock : BaseBlockEntity, Interfaces.IFacingFull
    {
        /// <summary>
        /// Creates a new chest block
        /// </summary>
        /// <param name="type">The type of block</param>
        public CommandBlock(IBlockType? type) : base(type) { }

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
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }

        /// <summary>
        /// If the command block is conditional
        /// </summary>
        [BlockState("conditional")]
        public bool? SConditional { get; set; }

        /// <summary>
        /// The name of the command block
        /// </summary>
        [Data.DataTag("CustomName", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public BaseJsonText? DCustomName { get; set; }

        /// <summary>
        /// The command in the command block
        /// </summary>
        [Data.DataTag("Command")]
        public string? DCommand { get; set; }

        /// <summary>
        /// The last command's string output
        /// </summary>
        [Data.DataTag("LastOutput", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public BaseJsonText? DLastOutput { get; set; }

        /// <summary>
        /// The last command's success output
        /// </summary>
        [Data.DataTag("SuccessCount")]
        public int? DSuccessCount { get; set; }

        /// <summary>
        /// The point in time the last command was ran
        /// </summary>
        [Data.DataTag("LastExecution")]
        public long? DLastExecution { get; set; }

        /// <summary>
        /// If the command block should store <see cref="DLastOutput"/>
        /// </summary>
        [Data.DataTag("TrackOutput")]
        public bool? DTrackOutput { get; set; }

        /// <summary>
        /// If the command block is powered
        /// </summary>
        [Data.DataTag("powered")]
        public bool? DPowered { get; set; }

        /// <summary>
        /// If the command block doesnt haeve to be powered to run the command
        /// </summary>
        [Data.DataTag("auto")]
        public bool? DAuto { get; set; }

        /// <summary>
        /// If the command block ran last time
        /// </summary>
        [Data.DataTag("conditionMet")]
        public bool? DConditionMet { get; set; }

        /// <summary>
        /// False if the command block should be able to run multiple times in the same tick. True if it should only be able to run once.
        /// </summary>
        [Data.DataTag("UpdateLastExecution", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public bool? DCanOnlyRunOnce { get; set; }
    }
}
