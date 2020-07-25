using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for command block minecarts
    /// </summary>
    public class MinecartCommand : Minecart
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<MinecartCommand> PathCreator => new Data.DataPathCreator<MinecartCommand>();

        /// <summary>
        /// Creates a new command block minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartCommand(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartCommand() : base(SharpCraft.ID.Entity.command_block_minecart) { }

        /// <summary>
        /// The command to run
        /// </summary>
        [Data.DataTag]
        public string? Command { get; set; }
        /// <summary>
        /// The command's text output
        /// </summary>
        [Data.DataTag]
        public string? LastOutput { get; set; }
        /// <summary>
        /// The command's output
        /// </summary>
        [Data.DataTag]
        public int? SuccessCount { get; set; }
        /// <summary>
        /// Makes it so last output will be stored in <see cref="LastOutput"/>
        /// </summary>
        [Data.DataTag]
        public bool? TrackOutput { get; set; }
    }
}
