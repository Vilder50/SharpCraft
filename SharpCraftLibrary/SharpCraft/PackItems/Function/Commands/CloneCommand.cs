using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which clones blocks from one place to another
    /// </summary>
    public class CloneCommand : BaseCommand
    {
        private Vector corner1;
        private Vector corner2;
        private Vector location;

        /// <summary>
        /// Intializes a new <see cref="CloneCommand"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the structure to clone</param>
        /// <param name="corner2">The oppesite corners of the structure to clone</param>
        /// <param name="location">The location to clone the structure to</param>
        /// <param name="masked">True if air blocks shouldn't be cloned. False if air blocks should be cloned</param>
        /// <param name="mode">The way to clone the structure</param>
        public CloneCommand(Vector corner1, Vector corner2, Vector location, bool masked, ID.BlockCloneWay mode)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            Location = location;
            Masked = masked;
            Mode = mode;
        }

        /// <summary>
        /// One of the corners of the structure to clone
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
        /// The oppesite corners of the structure to clone
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
        /// The location to clone the structure to
        /// </summary>
        public Vector Location
        {
            get => location;
            set
            {
                location = value ?? throw new ArgumentNullException(nameof(Location), "Location may not be null.");
            }
        }

        /// <summary>
        /// True if air blocks shouldn't be cloned. False if air blocks should be cloned
        /// </summary>
        public bool Masked { get; set; }

        /// <summary>
        /// The way to clone the structure
        /// </summary>
        public ID.BlockCloneWay Mode { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>clone [Corner1] [Corner2] [Location] [Masked] [Mode]</returns>
        public override string GetCommandString()
        {
            return $"clone {Corner1.GetVectorString()} {Corner2.GetVectorString()} {Location.GetVectorString()} {(Masked ? "masked" : "replace")} {Mode}";
        }
    }

    /// <summary>
    /// Command which copies specific blocks from one location to another
    /// </summary>
    public class FilteredCloneCommand : BaseCommand
    {
        private Vector corner1;
        private Vector corner2;
        private Vector location;
        private Block filterBlock;

        /// <summary>
        /// Intializes a new <see cref="FilteredCloneCommand"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the structure to clone</param>
        /// <param name="corner2">The oppesite corners of the structure to clone</param>
        /// <param name="location">The location to clone the structure to</param>
        /// <param name="filterBlock">The block to clone from one place to another</param>
        /// <param name="mode">The way to clone the structure</param>
        public FilteredCloneCommand(Vector corner1, Vector corner2, Vector location, Block filterBlock, ID.BlockCloneWay mode)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            Location = location;
            FilterBlock = filterBlock;
            Mode = mode;
        }

        /// <summary>
        /// One of the corners of the structure to clone
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
        /// The oppesite corners of the structure to clone
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
        /// The location to clone the structure to
        /// </summary>
        public Vector Location
        {
            get => location;
            set
            {
                location = value ?? throw new ArgumentNullException(nameof(Location), "Location may not be null.");
            }
        }

        /// <summary>
        /// The block to clone from one place to another
        /// </summary>
        public Block FilterBlock
        {
            get => filterBlock;
            set
            {
                filterBlock = value ?? throw new ArgumentNullException(nameof(FilterBlock), "FilterBlock may not be null.");
            }
        }

        /// <summary>
        /// The way to clone the structure
        /// </summary>
        public ID.BlockCloneWay Mode { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>clone [Corner1] [Corner2] [Location] filtered [FilterBlock] [Mode]</returns>
        public override string GetCommandString()
        {
            return $"clone {Corner1.GetVectorString()} {Corner2.GetVectorString()} {Location.GetVectorString()} filtered {FilterBlock.GetBlockPlacementString()} {Mode}";
        }
    }
}
