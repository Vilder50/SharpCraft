using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// Static class containing usefull things
    /// </summary>
    public static class SharpCraftItems
    {
        private static readonly List<IntVector> claimedCoords = new List<IntVector>();
        private static int lastLoadedCoordsX = -1;
        private static int lastLoadedCoordsY = 0;
        private static int lastLoadedCoordsZ = 0;

        /// <summary>
        /// Returns a coordinate in a loaded chunk. The given coordinate is only ever given out once so the coordinate is save to use for whatever you want to.
        /// </summary>
        /// <returns>A coordinate in a loaded chunk</returns>
        public static IntVector GetNextLoadedCoords()
        {
            SharpCraftFiles.TrySetupFiles();

            if (lastLoadedCoordsY > 255)
            {
                throw new InvalidOperationException("Cannot give more coordinates. All coordinates are given out.");
            }
            IntVector blockCoords;
            do
            {
                lastLoadedCoordsX++;
                if (lastLoadedCoordsX > 15)
                {
                    lastLoadedCoordsX = 0;
                    lastLoadedCoordsZ++;
                    if (lastLoadedCoordsZ > 15)
                    {
                        lastLoadedCoordsZ = 0;
                        lastLoadedCoordsY++;
                    }
                }

                blockCoords = new IntVector((int)(SharpCraftSettings.OwnedChunk.X * 16 + lastLoadedCoordsX), (int)(SharpCraftSettings.OwnedChunk.Y * 16 + lastLoadedCoordsY), (int)(SharpCraftSettings.OwnedChunk.Z * 16 + lastLoadedCoordsZ));
            } while (claimedCoords.Any(c => c.X == lastLoadedCoordsX && c.Y == lastLoadedCoordsY && c.Z == lastLoadedCoordsZ));

            return blockCoords;
        }

        private static bool IsLoadedCoordsClaimed(IntVector check)
        {
            if (claimedCoords.Any(c => c.GetVectorString() == check.GetVectorString()))
            {
                return true;
            }
            if (check.Y < lastLoadedCoordsY)
            {
                return true;
            }
            else if (check.Y == lastLoadedCoordsY)
            {
                if (check.Z < lastLoadedCoordsZ)
                {
                    return true;
                }
                else if (check.Z == lastLoadedCoordsZ)
                {
                    if (check.X <= lastLoadedCoordsX)
                    {
                        return true;
                    }
                }
            }

            if (check.X < 0 ||
                check.X > 15 ||
                check.Y < 0 ||
                check.Y > 255 ||
                check.Z < 0 ||
                check.Z > 15)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads a block of coords of the given size
        /// </summary>
        /// <param name="size">The size of the block to load</param>
        /// <returns>The "smallest" corner of the loaded block or null if it failed to claim</returns>
        public static IntVector? ClaimLoadedCoordsSquare(IntVector size)
        {
            for (int y = lastLoadedCoordsY; y < 256 - size.Y; y++)
            {
                for (int z = 0; z < 16 - size.Z; z++)
                {
                    for (int x = 0; x < 16 - size.X; x++)
                    {
                        bool success = true;
                        for (int sizeX = 0; sizeX < size.X; sizeX++)
                        {
                            for (int sizeY = 0; sizeY < size.Y; sizeY++)
                            {
                                for (int sizeZ = 0; sizeZ < size.Z; sizeZ++)
                                {
                                    if (IsLoadedCoordsClaimed(new IntVector(x + sizeX, y + sizeY, z + sizeZ)))
                                    {
                                        success = false;
                                        break;
                                    }
                                }
                                if (!success) { break; }
                            }
                            if (!success) { break; }
                        }
                        if (success)
                        {
                            for (int sizeX = 0; sizeX < size.X; sizeX++)
                            {
                                for (int sizeY = 0; sizeY < size.Y; sizeY++)
                                {
                                    for (int sizeZ = 0; sizeZ < size.Z; sizeZ++)
                                    {
                                        claimedCoords.Add(new IntVector(x + sizeX, y + sizeY, z + sizeZ));
                                    }
                                }
                            }
                            return new IntVector((int)(x + SharpCraftSettings.OwnedChunk.X * 16), (int)(y + SharpCraftSettings.OwnedChunk.Y * 16), (int)(z + SharpCraftSettings.OwnedChunk.Z * 16));
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a part of a loot command used for getting a shulker box's content. Note the shulker box has to be of the type <see cref="ID.Block.shulker_box"/>
        /// </summary>
        /// <returns>A <see cref="Commands.LootSources.MineItemSource"/> for getting loot from a shulker box</returns>
        public static Commands.LootSources.MineItemSource GetShulkerItemSource(Vector shulkerLocation)
        {
            return new Commands.LootSources.MineItemSource(shulkerLocation, SharpCraftFiles.GetShulkerLootItem());
        }
    }
}
