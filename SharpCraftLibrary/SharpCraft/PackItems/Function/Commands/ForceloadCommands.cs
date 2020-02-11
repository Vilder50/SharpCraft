using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which adds/removes a forceloaded chunk
    /// </summary>
    public class ForceloadChunkCommand : BaseCommand
    {
        private Vector coordinates;

        /// <summary>
        /// Intializes a new <see cref="ForceloadChunkCommand"/>
        /// </summary>
        /// <param name="coordinates">A coordinate in the chunk to force load</param>
        /// <param name="addChunk">True if the chunk should be force loaded. False if the chunk shouldn't</param>
        public ForceloadChunkCommand(Vector coordinates, bool addChunk)
        {
            Coordinates = coordinates;
            AddChunk = addChunk;
        }

        /// <summary>
        /// A coordinate in the chunk to force load
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// True if the chunk should be force loaded. False if the chunk shouldn't
        /// </summary>
        public bool AddChunk { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>forceload [AddChunk] [Coordinates]</returns>
        public override string GetCommandString()
        {
            return $"forceload {(AddChunk ? "add" : "remove")} {Coordinates.GetXString()} {Coordinates.GetZString()}";
        }
    }

    /// <summary>
    /// Command which adds/removes a forceloaded chunk
    /// </summary>
    public class ForceloadChunksCommand : BaseCommand
    {
        private Vector corner1;
        private Vector corner2;

        /// <summary>
        /// Intializes a new <see cref="ForceloadChunksCommand"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the square of chunks to forceload</param>
        /// <param name="corner2">The oppesite corner of the square of chunks to forceload</param>
        /// <param name="addChunk">True if the chunks should be force loaded. False if the chunks shouldn't</param>
        public ForceloadChunksCommand(Vector corner1, Vector corner2, bool addChunk)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            AddChunk = addChunk;
        }

        /// <summary>
        /// One of the corners of the square of chunks to forceload
        /// </summary>
        public Vector Corner1
        {
            get => corner1;
            set
            {
                corner1 = value ?? throw new ArgumentNullException(nameof(Corner1), "Corner1 may not be null.");
            }
        }

        /// <summary>
        /// The oppesite corner of the square of chunks to forceload
        /// </summary>
        public Vector Corner2
        {
            get => corner2;
            set
            {
                corner2 = value ?? throw new ArgumentNullException(nameof(Corner2), "Corner2 may not be null.");
            }
        }

        /// <summary>
        /// True if the chunks should be force loaded. False if the chunks shouldn't
        /// </summary>
        public bool AddChunk { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>forceload [AddChunk] [Corner1] [Corner2]</returns>
        public override string GetCommandString()
        {
            return $"forceload {(AddChunk ? "add" : "remove")} {Corner1.GetXString()} {Corner1.GetZString()} {Corner2.GetXString()} {Corner2.GetZString()}";
        }
    }

    /// <summary>
    /// Command which removes all force loaded chunks
    /// </summary>
    public class ForceloadRemoveAllCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>forceload remove all</returns>
        public override string GetCommandString()
        {
            return $"forceload remove all";
        }
    }

    /// <summary>
    /// Command which checks how many chunks are loaded
    /// </summary>
    public class ForceloadQueryCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>forceload query</returns>
        public override string GetCommandString()
        {
            return $"forceload query";
        }
    }

    /// <summary>
    /// Command which checks if a chunk is force loaded
    /// </summary>
    public class ForceloadQueryChunkCommand : BaseCommand
    {
        private Vector coordinates;

        /// <summary>
        /// Intializes a new <see cref="ForceloadQueryChunkCommand"/>
        /// </summary>
        /// <param name="coordinates">A coordinate in the chunk to check if loaded</param>
        public ForceloadQueryChunkCommand(Vector coordinates)
        {
            Coordinates = coordinates;
        }



        /// <summary>
        /// A coordinate in the chunk to check if loaded
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>forceload query [Coordinates]</returns>
        public override string GetCommandString()
        {
            return $"forceload query {Coordinates.GetXString()} {Coordinates.GetZString()}";
        }
    }
}
