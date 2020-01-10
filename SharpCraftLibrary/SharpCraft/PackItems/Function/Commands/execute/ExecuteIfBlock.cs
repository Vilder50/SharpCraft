using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the block at the location is the correct block
    /// </summary>
    public class ExecuteIfBlock : BaseExecuteIfCommand
    {
        private Coords coordinates;
        private Block block;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfBlock"/> command
        /// </summary>
        /// <param name="coordinates">The coordinates of the block to check for</param>
        /// <param name="block">The block to check for</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfBlock(Coords coordinates, Block block, bool executeIf = true) : base(executeIf)
        {
            Coordinates = coordinates;
            Block = block;
        }

        /// <summary>
        /// The coordinates of the block to check for
        /// </summary>
        public Coords Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// The block to check for
        /// </summary>
        public Block Block
        {
            get => block;
            set
            {
                block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null.");
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>block [Coordinates] [Block]</returns>
        protected override string GetCheckPart()
        {
            return "block " + Coordinates.GetCoordString() + " " + Block.GetBlockPlacementString();
        }
    }
}
