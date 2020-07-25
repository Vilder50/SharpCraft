using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the block commands
    /// </summary>
    public class BlockCommands : CommandList
    {
        internal BlockCommands(Function function) : base(function)
        {
            Data = new ClassData(function);
            Structure = new ClassStructure(function);
        }

        /// <summary>
        /// Adds a <see cref="SharpCraft.Block"/> at the given coords
        /// </summary>
        /// <param name="addBlock">the <see cref="SharpCraft.Block"/> to add</param>
        /// <param name="blockCoords">the coords to add it at</param>
        /// <param name="type">how to place the block</param>
        public void Add(Vector blockCoords, Block addBlock, ID.BlockAdd type = ID.BlockAdd.replace)
        {
            ForFunction.AddCommand(new SetblockCommand(blockCoords, addBlock, type));
        }

        /// <summary>
        /// Commands for working with structures
        /// </summary>
        public ClassStructure Structure;
        /// <summary>
        /// Commands for working with structures
        /// </summary>
        public class ClassStructure : CommandList
        {
            internal ClassStructure(Function function) : base(function)
            {

            }

            private (Vector? structureOffset, Vector blockLocation, Vector redstoneLocation) GetStructureCoords(Vector structureLocation, Vector? structureBlockLocation, ID.FacingFull redstoneBlockDirection)
            {
                Vector structureBlockRealLocation = structureBlockLocation ?? structureLocation;
                Vector? structureOffset = structureBlockLocation is null ? null : structureLocation - structureBlockLocation;
                if (structureBlockLocation is LocalCoords)
                {
                    throw new System.ArgumentException("StructureBlockLocation may not be of type LocalCoords", nameof(structureBlockLocation));
                }
                if (!(structureBlockLocation is null))
                {
                    if (structureLocation is LocalCoords)
                    {
                        throw new System.ArgumentException("StructureLocation may not be of type LocalCoords if StructureBlockLocation is specified", nameof(structureLocation));
                    }
                    if (structureLocation is Coords ^ structureBlockLocation is Coords)
                    {
                        throw new System.ArgumentException("StructureLocation may not be of a different type than StructureBlockLocation", nameof(structureLocation));
                    }
                }
                if (!(structureOffset is null))
                {
                    if (System.Math.Abs(structureOffset.X) > SharpCraft.Structure.StructureMaxSize || System.Math.Abs(structureOffset.Y) > SharpCraft.Structure.StructureMaxSize || System.Math.Abs(structureOffset.Z) > SharpCraft.Structure.StructureMaxSize)
                    {
                        throw new System.ArgumentException("The difference between structureLocation and structureBlockLocation may not be bigger than " + SharpCraft.Structure.StructureMaxSize, nameof(structureLocation));
                    }
                }

                Vector redstoneBlockRelativeLocation;
                switch (redstoneBlockDirection)
                {
                    case ID.FacingFull.down:
                        redstoneBlockRelativeLocation = Vector.NegativeY;
                        break;
                    case ID.FacingFull.east:
                        redstoneBlockRelativeLocation = Vector.PositiveX;
                        break;
                    case ID.FacingFull.north:
                        redstoneBlockRelativeLocation = Vector.NegativeZ;
                        break;
                    case ID.FacingFull.south:
                        redstoneBlockRelativeLocation = Vector.PositiveZ;
                        break;
                    case ID.FacingFull.west:
                        redstoneBlockRelativeLocation = Vector.NegativeX;
                        break;
                    case ID.FacingFull.up:
                    default:
                        redstoneBlockRelativeLocation = Vector.PositiveY;
                        break;
                }

                return (structureOffset, structureBlockRealLocation, structureBlockRealLocation is Coords ? (Coords)structureBlockRealLocation + redstoneBlockRelativeLocation : structureBlockRealLocation + redstoneBlockRelativeLocation);
            }

            /// <summary>
            /// Places a structure in the world and replaces the placed structure block with the block which was there before the structure block was placed
            /// </summary>
            /// <param name="structureLocation">The location to place the structure at</param>
            /// <param name="structure">The structure to place</param>
            /// <param name="structureBlockLocation">The location of the structure block which places the structure</param>
            /// <param name="redstoneBlockDirection">The side of the structure block the redstoneblock is placed on</param>
            /// <param name="structurePlaceInformation">Extra information on how the structure should be placed</param>
            /// <returns>The placed structure</returns>
            public IStructure Place(Vector structureLocation, IStructure structure, Vector? structureBlockLocation = null, ID.FacingFull redstoneBlockDirection = ID.FacingFull.down, Blocks.StructureBlock? structurePlaceInformation = null)
            {
                var (structureOffset, blockLocation, redstoneLocation) = GetStructureCoords(structureLocation, structureBlockLocation, redstoneBlockDirection);

                ForFunction.Custom.GroupCommands(group =>
                {
                    Vector structureBlockHoldingLocation = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetStructureBlocksLocation();
                    Vector redstoneBlockHoldingLocation = structureBlockHoldingLocation + Vector.PositiveX;

                    group.Block.Clone(blockLocation, blockLocation, structureBlockHoldingLocation);
                    group.Block.Clone(redstoneLocation, redstoneLocation, redstoneBlockHoldingLocation);
                    group.Block.Add(blockLocation, new Blocks.StructureBlock()
                    {
                        DIntegrity = structurePlaceInformation?.DIntegrity,
                        DIgnoreEntities = structurePlaceInformation?.DIgnoreEntities ?? false,
                        DMirror = structurePlaceInformation?.DMirror,
                        DRotation = structurePlaceInformation?.DRotation,
                        DCoords = structureOffset is null ? null : new IntVector((int)structureOffset.X, (int)structureOffset.Y, (int)structureOffset.Z),
                        DStructure = structure,
                        DMode = ID.StructureDataMode.LOAD,
                        SMode = ID.StructureMode.load
                    });
                    group.Block.Add(redstoneLocation, ID.Block.redstone_block);
                    group.Execute.IfBlock(blockLocation, ID.Block.structure_block).Block.Clone(structureBlockHoldingLocation, structureBlockHoldingLocation, blockLocation);
                    group.Execute.IfBlock(redstoneLocation, ID.Block.redstone_block).Block.Clone(redstoneBlockHoldingLocation, redstoneBlockHoldingLocation, redstoneLocation);
                });

                return structure;
            }

            /// <summary>
            /// Places a structure in the world
            /// </summary>
            /// <param name="structureLocation">The location to place the structure at</param>
            /// <param name="structure">The structure to place</param>
            /// <param name="structureBlockLocation">The location of the structure block which places the structure</param>
            /// <param name="redstoneBlockDirection">The side of the structure block the redstoneblock is placed on</param>
            /// <param name="replaceStructureBlock">A block to replace the structure block with once the structure is placed</param>
            /// <param name="replaceRedstoneBlock">A block to replace the redstone block with once the structure is placed</param>
            /// <param name="structurePlaceInformation">Extra information on how the structure should be placed</param>
            /// <returns>The placed structure</returns>
            public IStructure Place(Vector structureLocation, IStructure structure, Vector? structureBlockLocation, Block? replaceStructureBlock, Block? replaceRedstoneBlock, ID.FacingFull redstoneBlockDirection = ID.FacingFull.down, Blocks.StructureBlock? structurePlaceInformation = null)
            {
                (Vector? structureOffset, Vector blockLocation, Vector redstoneLocation) = GetStructureCoords(structureLocation, structureBlockLocation, redstoneBlockDirection);

                ForFunction.Custom.GroupCommands(group =>
                {
                    Vector structureBlockHoldingLocation = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetStructureBlocksLocation();
                    Vector redstoneBlockHoldingLocation = structureBlockHoldingLocation + Vector.PositiveX;
                    group.Block.Add(blockLocation, new Blocks.StructureBlock()
                    {
                        DIntegrity = structurePlaceInformation?.DIntegrity,
                        DIgnoreEntities = structurePlaceInformation?.DIgnoreEntities ?? false,
                        DMirror = structurePlaceInformation?.DMirror,
                        DRotation = structurePlaceInformation?.DRotation,
                        DCoords = structureOffset is null ? null : new IntVector((int)structureOffset.X, (int)structureOffset.Y, (int)structureOffset.Z),
                        DStructure = structure,
                        DMode = ID.StructureDataMode.LOAD,
                        SMode = ID.StructureMode.load
                    });
                    group.Block.Add(redstoneLocation, ID.Block.redstone_block);
                    if (!(replaceRedstoneBlock is null))
                    {
                        group.Block.Add(redstoneLocation, replaceRedstoneBlock);
                    }
                    if (!(replaceStructureBlock is null))
                    {
                        group.Block.Add(blockLocation, replaceStructureBlock);
                    }
                });

                return structure;
            }

            /// <summary>
            /// Saves blocks into a structure. Note that the save will be "deleted" once the world closes
            /// </summary>
            /// <param name="name">The name of the structure</param>
            /// <param name="structureLocation">The location of the blocks to save</param>
            /// <param name="size">The size of the structure to save</param>
            /// <param name="structureBlockLocation">The location of the structure block which saves the structure</param>
            /// <param name="redstoneBlockDirection">The side of the structure block the redstoneblock is placed on</param>
            /// <returns>A reference to the saved structure</returns>
            public IStructure Save(string name, Vector structureLocation, IntVector size, Vector? structureBlockLocation = null, ID.FacingFull redstoneBlockDirection = ID.FacingFull.down)
            {
                var (structureOffset, blockLocation, redstoneLocation) = GetStructureCoords(structureLocation, structureBlockLocation, redstoneBlockDirection);
                FileMocks.MockStructure structure = "minecraft:temp/" + ForFunction.PackNamespace.Name + "/" + name;

                ForFunction.Custom.GroupCommands(group =>
                {
                    Vector structureBlockHoldingLocation = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetStructureBlocksLocation();
                    Vector redstoneBlockHoldingLocation = structureBlockHoldingLocation + Vector.PositiveX;

                    group.Block.Clone(blockLocation, blockLocation, structureBlockHoldingLocation);
                    group.Block.Clone(redstoneLocation, redstoneLocation, redstoneBlockHoldingLocation);
                    group.Block.Add(blockLocation, new Blocks.StructureBlock()
                    {
                        DCoords = structureOffset is null ? null : new IntVector((int)structureOffset.X, (int)structureOffset.Y, (int)structureOffset.Z),
                        DSize = size,
                        DStructure = structure,
                        DMode = ID.StructureDataMode.SAVE,
                        SMode = ID.StructureMode.save
                    });
                    group.Block.Add(redstoneLocation, ID.Block.redstone_block);
                    group.Execute.IfBlock(blockLocation, ID.Block.structure_block).Block.Clone(structureBlockHoldingLocation, structureBlockHoldingLocation, blockLocation);
                    group.Execute.IfBlock(redstoneLocation, ID.Block.redstone_block).Block.Clone(redstoneBlockHoldingLocation, redstoneBlockHoldingLocation, redstoneLocation);
                });

                return structure;
            }

            /// <summary>
            /// Saves blocks into a structure. Note that the save will be "deleted" once the world closes
            /// </summary>
            /// <param name="name">The name of the structure</param>
            /// <param name="structureLocation">The location of the blocks to save</param>
            /// <param name="size">The size of the structure to save</param>
            /// <param name="structureBlockLocation">The location of the structure block which saves the structure</param>
            /// <param name="redstoneBlockDirection">The side of the structure block the redstoneblock is placed on</param>
            /// <param name="replaceStructureBlock">A block to replace the structure block with once the structure is placed</param>
            /// <param name="replaceRedstoneBlock">A block to replace the redstone block with once the structure is placed</param>
            /// <returns>A reference to the saved structure</returns>
            public IStructure Save(string name, Vector structureLocation, IntVector size, Vector? structureBlockLocation, Block? replaceStructureBlock, Block? replaceRedstoneBlock, ID.FacingFull redstoneBlockDirection = ID.FacingFull.down)
            {
                var (structureOffset, blockLocation, redstoneLocation) = GetStructureCoords(structureLocation, structureBlockLocation, redstoneBlockDirection);
                FileMocks.MockStructure structure = "minecraft:temp/" + ForFunction.PackNamespace.Name + "/" + name;

                ForFunction.Custom.GroupCommands(group =>
                {
                    group.Block.Add(blockLocation, new Blocks.StructureBlock()
                    {
                        DCoords = structureOffset is null ? null : new IntVector((int)structureOffset.X, (int)structureOffset.Y, (int)structureOffset.Z),
                        DSize = size,
                        DStructure = structure,
                        DMode = ID.StructureDataMode.SAVE,
                        SMode = ID.StructureMode.save
                    });
                    group.Block.Add(redstoneLocation, ID.Block.redstone_block);
                    if (!(replaceRedstoneBlock is null))
                    {
                        group.Block.Add(redstoneLocation, replaceRedstoneBlock);
                    }
                    if (!(replaceStructureBlock is null))
                    {
                        group.Block.Add(blockLocation, replaceStructureBlock);
                    }
                });

                return structure;
            }
        }

        /// <summary>
        /// Fills in the <see cref="SharpCraft.Block"/> between the two corner coords
        /// </summary>
        /// <param name="fillBlock">the <see cref="SharpCraft.Block"/> to fill with</param>
        /// <param name="corner1">the first corner coords</param>
        /// <param name="corner2">the second corner coords</param>
        /// <param name="type">how to fill the <see cref="SharpCraft.Block"/>s</param>
        /// <param name="replaceBlock">the <see cref="SharpCraft.Block"/>s to replace</param>
        public void Fill(Vector corner1, Vector corner2, Block fillBlock, ID.BlockFill type = ID.BlockFill.replace, Block? replaceBlock = null)
        {
            if (replaceBlock is null)
            {
                ForFunction.AddCommand(new FillCommand(corner1, corner2, fillBlock, type));
            }
            else
            {
                ForFunction.AddCommand(new FillReplaceCommand(corner1, corner2, fillBlock, replaceBlock));
            }
        }

        /// <summary>
        /// Copies the <see cref="SharpCraft.Block"/>s between the two corner coords to another coords
        /// </summary>
        /// <param name="corner1">the first corner coords</param>
        /// <param name="corner2">the second corner coords</param>
        /// <param name="copyTo">the place to copy to</param>
        /// <param name="type">how to copy the <see cref="SharpCraft.Block"/>s</param>
        /// <param name="way">copy rules</param>
        /// <param name="filteredBlock">the <see cref="SharpCraft.Block"/> to copy</param>
        public void Clone(Vector corner1, Vector corner2, Vector copyTo, ID.BlockClone type = ID.BlockClone.replace, ID.BlockCloneWay way = ID.BlockCloneWay.normal, Block? filteredBlock = null)
        {
            if (filteredBlock is null)
            {
                ForFunction.AddCommand(new CloneCommand(corner1, corner2, copyTo, (type == ID.BlockClone.masked), way));
            }
            else
            {
                ForFunction.AddCommand(new FilteredCloneCommand(corner1, corner2, copyTo, filteredBlock, way));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="Item"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="BlockCoords">The <see cref="SharpCraft.Block"/> to insert into</param>
        /// <param name="AddItem">The <see cref="Item"/> to insert (<see cref="Item.Slot"/> choses the slot)</param>
        public void AddItem(Vector BlockCoords, Item AddItem)
        {
            ForFunction.AddCommand(new ReplaceitemBlockCommand(BlockCoords, new Slots.ContainerSlot(AddItem.Slot ?? 0), AddItem, AddItem.Count ?? 1));
        }

        /// <summary>
        /// Inserts the given <see cref="ILootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="loot">the <see cref="ILootTable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="ILootTable"/> at</param>
        public void Loot(Vector block, ILootTable loot, int? slot = null)
        {
            if (slot is null)
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.InsertTarget(block), new LootSources.LoottableSource(loot)));
            }
            else
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value)), new LootSources.LoottableSource(loot)));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="Entity"/>'s <see cref="LootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="kill">the <see cref="Entity"/>'s <see cref="LootTable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="LootTable"/> at</param>
        public void Loot(Vector block, BaseSelector kill, int? slot = null)
        {
            if (slot is null)
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.InsertTarget(block), new LootSources.KillSource(kill)));
            }
            else
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value)), new LootSources.KillSource(kill)));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="SharpCraft.Block"/>'s <see cref="LootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="breakBlock">the <see cref="SharpCraft.Block"/>'s <see cref="LootTable"/> to input</param>
        /// <param name="breakWith">the tool used to break the <see cref="SharpCraft.Block"/></param>
        /// <param name="slot">the slot to insert the <see cref="LootTable"/> at</param>
        public void Loot(Vector block, Vector breakBlock, Item? breakWith = null, int? slot = null)
        {
            LootTargets.ILootTarget target;
            LootSources.ILootSource source;
            if (slot is null)
            {
                target = new LootTargets.InsertTarget(block);
            }
            else
            {
                target = new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value));
            }
            if (breakWith is null)
            {
                source = new LootSources.MineHandSource(breakBlock, true);
            }
            else
            {
                source = new LootSources.MineItemSource(breakBlock, breakWith);
            }

            ForFunction.AddCommand(new LootCommand(target, source));
        }

        /// <summary>
        /// The data commands
        /// </summary>
        public ClassData Data;

        /// <summary>
        /// The data commands
        /// </summary>
        public class ClassData : CommandList
        {
            internal ClassData(Function function) : base(function)
            {
                
            }

            /// <summary>
            /// Adds the given data to the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="data">The data to give to the <see cref="SharpCraft.Block"/></param>
            /// <param name="place">the coords of the <see cref="SharpCraft.Block"/> to give the data to</param>
            public void Change(Vector place, Data.SimpleDataHolder data)
            {
                ForFunction.AddCommand(new DataMergeBlockCommand(place, data));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the coords of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Vector toBlock, string toDataPath, ID.EntityDataModifierType modifierType, Data.SimpleDataHolder copyData)
            {
                ForFunction.AddCommand(new DataModifyWithDataCommand(new BlockDataLocation(toBlock, toDataPath), modifierType, copyData));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the coords of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Vector toBlock, string toDataPath, int index, Data.SimpleDataHolder copyData)
            {
                ForFunction.AddCommand(new DataModifyInsertDataCommand(new BlockDataLocation(toBlock, toDataPath), index, copyData));
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Block"/> at the given datapath
            /// </summary>
            /// <param name="dataPath">The datapath</param>
            /// <param name="place">the coords of the <see cref="SharpCraft.Block"/> to remove the data from</param>
            public void Remove(Vector place, string dataPath)
            {
                ForFunction.AddCommand(new DataDeleteCommand(new BlockDataLocation(place, dataPath)));
            }

            /// <summary>
            /// Gets the numeric data from the datapath from the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="place">the coords of the <see cref="SharpCraft.Block"/> to get the data from</param>
            /// <param name="dataPath">the datapath to the data</param>
            /// <param name="scale">the number to multiply the numeric value with</param>
            public void Get(Vector place, string dataPath, double scale = 1)
            {
                ForFunction.AddCommand(new DataGetCommand(new BlockDataLocation(place, dataPath), scale));
            }

            /// <summary>
            /// Copies data into the block at given location
            /// </summary>
            /// <param name="toBlock">the coords of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            public void Copy(Vector toBlock, string toDataPath, ID.EntityDataModifierType modifierType, IDataLocation dataLocation)
            {
                ForFunction.AddCommand(new DataModifyWithLocationCommand(new BlockDataLocation(toBlock, toDataPath), modifierType, dataLocation));
            }

            /// <summary>
            /// Copies data into the block at the given location
            /// </summary>
            /// <param name="toBlock">the coords of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            public void Copy(Vector toBlock, string toDataPath, int index, IDataLocation dataLocation)
            {
                ForFunction.AddCommand(new DataModifyInsertLocationCommand(new BlockDataLocation(toBlock, toDataPath), index, dataLocation));
            }
        }
    }
}