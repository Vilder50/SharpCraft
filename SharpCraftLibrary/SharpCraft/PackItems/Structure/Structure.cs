using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SharpCraft.Data;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// Class for structure files
    /// </summary>
    public class Structure : BaseFile<BinaryWriter>, IStructure
    {
        /// <summary>
        /// The maximum size a structure can have
        /// </summary>
        public static readonly int StructureMaxSize = 48;

        /// <summary>
        /// Class for blocks in structures
        /// </summary>
        public class Block
        {
            /// <summary>
            /// Intializes a new block for a structure
            /// </summary>
            /// <param name="blockId">The of the block to use in the palette.</param>
            /// <param name="location">The location to place the block at</param>
            /// <param name="blockData">The data the block has</param>
            public Block(int blockId, IntVector location, SharpCraft.Block? blockData = null)
            {
                BlockId = blockId;
                Location = location;
                BlockData = blockData;
            }

            /// <summary>
            /// The of the block to use in the palette.
            /// </summary>
            public int BlockId { get; set; }

            /// <summary>
            /// The location to place the block at
            /// </summary>
            public IntVector Location { get; set; }

            /// <summary>
            /// The data the block has
            /// </summary>
            public SharpCraft.Block? BlockData { get; set; }
        }

        private Block[] blocksInStructure = null!;
        private SharpCraft.Block[][] blockPalettes = null!;
        private Entities.BasicEntity[] entities = null!;

        /// <summary>
        /// Intializes a new <see cref="Structure"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="blockPalettes">The pallete used for the structure</param>
        /// <param name="blocksInStructure">The blocks in the structure</param>
        /// <param name="entities">Entities in the structure</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected Structure(bool _, BasePackNamespace packNamespace, string? fileName, SharpCraft.Block[][] blockPalettes, Block[] blocksInStructure, Entities.BasicEntity[] entities, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, writeSetting, "structure")
        {
            BlockPalettes = blockPalettes;
            BlocksInStructure = blocksInStructure;
            Entities = entities;
        }

        /// <summary>
        /// Intializes a new <see cref="Structure"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the predicate is in</param>
        /// <param name="fileName">The name of the predicate file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="blockPalettes">The pallete used for the structure</param>
        /// <param name="blocksInStructure">The blocks in the structure</param>
        /// <param name="entities">Entities in the structure</param>
        public Structure(BasePackNamespace packNamespace, string? fileName, SharpCraft.Block[][] blockPalettes, Block[] blocksInStructure, Entities.BasicEntity[] entities, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, blockPalettes, blocksInStructure, entities, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The blocks in the structure
        /// </summary>
        public Block[] BlocksInStructure 
        { 
            get => blocksInStructure;
            set 
            {
                Utils.ValidateNoneNullArray(value, nameof(BlocksInStructure), nameof(Structure));
                foreach (Block block in value)
                {
                    if (block.Location.X < 0 || block.Location.Y < 0 || block.Location.Z < 0 || block.Location.X > StructureMaxSize || block.Location.Y > StructureMaxSize || block.Location.Z > StructureMaxSize)
                    {
                        throw new ArgumentException("One or more of the blocks have coordinates outside of the range 0-" + StructureMaxSize, nameof(Entities));
                    }
                }
                blocksInStructure = value; 
            }
        }

        /// <summary>
        /// The pallete used for the structure
        /// </summary>
        public SharpCraft.Block[][] BlockPalettes { get => blockPalettes; set => blockPalettes = value; }

        /// <summary>
        /// Entities in the structure
        /// </summary>
        public Entities.BasicEntity[] Entities
        {
            get => entities;
            set
            {
                Utils.ValidateNoneNullArray(value, nameof(Entities), nameof(Structure));
                foreach(Entities.BasicEntity entity in value)
                {
                    if (entity.Coords is null)
                    {
                        throw new ArgumentException("One or more of the entities doesn't have any Coordinates set. Please set their coords nbt.", nameof(Entities));
                    }
                    if (entity.Coords.X < 0 || entity.Coords.Y < 0 || entity.Coords.Z < 0 || entity.Coords.X > StructureMaxSize || entity.Coords.Y > StructureMaxSize || entity.Coords.Z > StructureMaxSize)
                    {
                        throw new ArgumentException("One or more of the entities have coordinates outside of the range 0-" + StructureMaxSize, nameof(Entities));
                    }
                }
                entities = value;
            }
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override BinaryWriter GetStream()
        {
            CreateDirectory("structures");
            return PackNamespace.Datapack.FileCreator.CreateBinaryWriter(PackNamespace.GetPath() + "structures/" + WritePath + ".nbt", true);
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(BinaryWriter stream)
        {
            //get structure size
            int highestX = 0;
            int highestY = 0;
            int highestZ = 0;
            foreach(Block block in BlocksInStructure)
            {
                highestX = Math.Max(highestX, (int)block.Location.X);
                highestY = Math.Max(highestY, (int)block.Location.Y);
                highestZ = Math.Max(highestZ, (int)block.Location.Z);
            }

            //create nbt
            DataPartObject baseObject = new DataPartObject();
            baseObject.AddValue(new DataPartPath("DataVersion", new DataPartTag(2567)));

            //palette
            baseObject.AddValue(new DataPartPath("palettes", new DataPartArray(blockPalettes.Select(palette => 
            {
                return palette.ToList().Select(block => 
                {
                    return block.GetAsDataObject(new object[] { "Name", "Properties", false });
                }).ToArray();
            }).ToArray(), ID.NBTTagType.TagArrayArray, null, new object[] { })));

            //blocks
            baseObject.AddValue(new DataPartPath("blocks", new DataPartArray(BlocksInStructure.Select(block =>
            {
                DataPartObject blockObject = new DataPartObject();

                blockObject.AddValue(new DataPartPath("state", new DataPartTag(block.BlockId)));
                var location = block.Location.GetAsArray(null, new object[] { });
                location.ForceAsTagList = true;
                blockObject.AddValue(new DataPartPath("pos", location));
                if (!(block.BlockData is null))
                {
                    blockObject.AddValue(new DataPartPath("nbt", block.BlockData.GetDataTree()));
                }

                return blockObject;
            }).ToArray(), ID.NBTTagType.TagCompoundArray, null, new object[] { })));

            //entities
            baseObject.AddValue(new DataPartPath("entities", new DataPartArray(Entities.Select(entity => 
            {
                DataPartObject entityObject = new DataPartObject();

                Entities.BasicEntity clone = (Entities.BasicEntity)entity.Clone();
                if (clone.Coords is null)
                {
                    throw new ArgumentException("One or more of the entities doesn't have any Coordinates set. Please set their coords nbt.");
                }
                Vector coords = clone.Coords;
                clone.Coords = null;

                highestX = Math.Max(highestX, (int)coords.X);
                highestY = Math.Max(highestY, (int)coords.Y);
                highestZ = Math.Max(highestZ, (int)coords.Z);

                var coordsArray = coords.GetAsArray(null, new object[] { });
                coordsArray.ForceAsTagList = true;
                entityObject.AddValue(new DataPartPath("pos", coordsArray));
                entityObject.AddValue(new DataPartPath("blockPos", new DataPartArray(new int[] { (int)coords.X, (int)coords.Y, (int)coords.Z }, null, new object[] { }) { ForceAsTagList = true }));
                entityObject.AddValue(new DataPartPath("nbt", clone.GetDataTree()));

                return entityObject;
            }).ToArray(), ID.NBTTagType.TagCompoundArray, null, new object[] { })));

            baseObject.AddValue(new DataPartPath("size", new DataPartArray(new int[] { highestX + 1, highestY + 1, highestZ + 1 }, ID.NBTTagType.TagIntArray, new object[] { }) { ForceAsTagList = true }));

            stream.Write((byte)10);
            stream.Write((byte)0);
            stream.Write((byte)0);
            baseObject.WriteNbtToStream(stream);
        }

        /// <summary>
        /// Converts this structure into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Unused</param>
        /// <param name="extraConversionData">Unused</param>
        /// <returns>This structure as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(GetNamespacedName());
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            blocksInStructure = null!;
            blockPalettes = null!;
            entities = null!;
        }

        /// <summary>
        /// Converts the given 3d array of blocks into a block palette and a list of block locations. 0:y, 1: x, 2: z
        /// </summary>
        /// <param name="blocks">The blocks to convert</param>
        /// <returns>The converted blocks</returns>
        public static (Block[] blocksInStructure, SharpCraft.Block[] blockPalette) ConvertBlocksToStructure(SharpCraft.Block?[,,] blocks)
        {
            List<SharpCraft.Block> palette = new List<SharpCraft.Block>();
            List<Block> blocksInStructure = new List<Block>();

            for (int y = 0; y < blocks.GetLength(0); y++)
            {
                for (int x = 0; x < blocks.GetLength(1); x++)
                {
                    for (int z = 0; z < blocks.GetLength(2); z++)
                    {
                        SharpCraft.Block? block = blocks[y, x, z];
                        if (block is null)
                        {
                            continue;
                        }
                        else
                        {
                            if (block.ID is null)
                            {
                                throw new ArgumentNullException("ConvertBlocksToStructure: A given block has an id of null which isn't allowed. (index: " + y + "," + x + "," + z + ")");
                            }
                            if (block.ID.Name.Contains("#"))
                            {
                                throw new ArgumentException("ConvertBlocksToStructure: A given block was of a block group type which isn't allowed. (index: " + y + "," + x + "," + z + ")");
                            }

                            SharpCraft.Block? paletteBlock = palette.Find(b =>
                            {
                                bool sameType = b.ID?.Name == block.ID?.Name;
                                if (sameType)
                                {
                                    bool bothHasStates = b.HasState && block.HasState;
                                    if (!b.HasState && !block.HasState)
                                    {
                                        return true;
                                    }
                                    else if (b.HasState && block.HasState)
                                    {
                                        return b.GetStateString() == block.GetStateString();
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            });
                            
                            if (paletteBlock is null)
                            {
                                paletteBlock = block;
                                palette.Add(block);
                            }
                            int paletteIndex = palette.IndexOf(paletteBlock);
                            blocksInStructure.Add(new Block(paletteIndex, new IntVector(x,y,z), block.HasData ? block : null));
                        }
                    }
                }
            }

            return (blocksInStructure.ToArray(), palette.ToArray());
        }
    }
}
