using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Settings for SharpCraft
    /// </summary>
    public static class SharpCraftSettings
    {
        private static IntVector ownedChunk = new IntVector(0);
        private static bool isLocked = false;

        internal static void LockSettings()
        {
            isLocked = true;
        }

        private static void ThrowIfLocked()
        {
            if (isLocked)
            {
                throw new InvalidOperationException("Cannot change SharpCraft settings at the given point. Setup settings before generating files.");
            }
        }

        /// <summary>
        /// A chunk SharpCraft freely can use. Note that the value has to be for a chunk and not for a block.
        /// </summary>
        public static IntVector OwnedChunk { get => ownedChunk; set { ThrowIfLocked(); ownedChunk = new IntVector((int)value.X, 0, (int)value.Z); } }
    }
}
