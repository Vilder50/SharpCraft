using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// Interface for datapack settings
    /// </summary>
    public interface IDatapackSetting
    {
        
    }

    /// <summary>
    /// Class holding a list of datapack settings
    /// </summary>
    public class DatapackSettings
    {
        /// <summary>
        /// Setting which choses what chunk sharpcraft can use for random things
        /// </summary>
        /// <param name="chunkLocation">The chunk coordinate (Note that this is the coordinate of the chunk. So divide world coordinates with 16)</param>
        /// <returns>The setting</returns>
        public IDatapackSetting LoadedChunkSetting(IntVector chunkLocation)
        {
            return new LoadedChunkSetting(chunkLocation);
        }

        /// <summary>
        /// The name to use for the SharpCraft generated namespace. (Please note that changing this can result in problems when used with other datapacks)
        /// </summary>
        /// <param name="name">The name the namespace should have</param>
        /// <returns>The setting</returns>
        public IDatapackSetting SharpCraftNamespaceNameSetting(string name)
        {
            return new SharpCraftNamespaceNameSetting(name);
        }

        /// <summary>
        /// If the SharpCraft namespace should generate it's basic files for making custom SharpCraft things run.
        /// </summary>
        /// <param name="isChild">True if the datapack is a child of another datapack which generates all the SharpCraft items</param>
        /// <returns>The setting</returns>
        public IDatapackSetting SharpDatapackChildSetting(bool isChild)
        {
            return new SharpDatapackChildSetting(isChild);
        }
    }

    internal class LoadedChunkSetting : IDatapackSetting
    {
        private readonly List<IntVector> claimedCoords = new List<IntVector>();
        private int lastLoadedCoordsX = -1;
        private int lastLoadedCoordsY = 0;
        private int lastLoadedCoordsZ = 0;

        public LoadedChunkSetting(IntVector chunkLocation)
        {
            ChunkLocation = new IntVector((int)chunkLocation.X, 0, (int)chunkLocation.Z);
            CornerBlock = new IntVector((int)chunkLocation.X * 16, 0, (int)chunkLocation.Z * 16);
        }

        public IntVector ChunkLocation { get; private set; }

        public IntVector CornerBlock { get; private set; }

        public IntVector GetNextLoadedCoords()
        {
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

                blockCoords = new IntVector((int)(CornerBlock.X + lastLoadedCoordsX), (int)(CornerBlock.Y + lastLoadedCoordsY), (int)(CornerBlock.Z + lastLoadedCoordsZ));
            } while (claimedCoords.Any(c => c.X == lastLoadedCoordsX && c.Y == lastLoadedCoordsY && c.Z == lastLoadedCoordsZ));

            return blockCoords;
        }

        private bool IsLoadedCoordsClaimed(IntVector check)
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

        public IntVector? ClaimLoadedCoordsSquare(IntVector size)
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
                            return new IntVector((int)(x + CornerBlock.X), (int)(y + CornerBlock.Y), (int)(z + CornerBlock.Z));
                        }
                    }
                }
            }

            return null;
        }
    }

    internal class SharpCraftNamespaceNameSetting : IDatapackSetting
    {
        public SharpCraftNamespaceNameSetting(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    internal class SharpDatapackChildSetting : IDatapackSetting
    {
        public SharpDatapackChildSetting(bool isChild)
        {
            IsChild = isChild;
        }

        public bool IsChild { get; private set; }
    }
}
