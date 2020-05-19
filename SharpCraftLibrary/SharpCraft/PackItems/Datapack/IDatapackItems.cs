using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Interface for classes holding items for datapacks
    /// </summary>
    public interface IDatapackItems
    {
        /// <summary>
        /// The datapack the items are for
        /// </summary>
        public BaseDatapack Datapack { get; set; }
    }

    /// <summary>
    /// Used for getting locations which a force loaded. (Use a datapack to get this item)
    /// </summary>
    public class LoadedBlockItems : IDatapackItems
    {
        private BaseDatapack datapack = null!;

        /// <summary>
        /// The datapack the items are for
        /// </summary>
        public BaseDatapack Datapack { get => datapack; set => datapack ??= value; }

        /// <summary>
        /// Returns a coordinate in a loaded chunk. The given coordinate is only ever given out once so the coordinate is save to use for whatever you want to.
        /// </summary>
        /// <returns>A coordinate in a loaded chunk</returns>
        public IntVector GetNextLoadedCoords()
        {
            datapack.GetItems<SharpCraftFiles>().SetupFiles();
            LoadedChunkSetting setting = Datapack.GetDatapackSetting<LoadedChunkSetting>()!;
            return setting.GetNextLoadedCoords();
        }

        /// <summary>
        /// Loads a block of coords of the given size
        /// </summary>
        /// <param name="size">The size of the block to load</param>
        /// <returns>The "smallest" corner of the loaded block or null if it failed to claim</returns>
        public IntVector? ClaimLoadedCoordsSquare(IntVector size)
        {
            datapack.GetItems<SharpCraftFiles>().SetupFiles();
            LoadedChunkSetting setting = Datapack.GetDatapackSetting<LoadedChunkSetting>()!;
            return setting.ClaimLoadedCoordsSquare(size);
        }
    }

    /// <summary>
    /// Contains items which are related to loot tables. (Use a datapack to get this item)
    /// </summary>
    public class LoottableItems : IDatapackItems
    {
        private BaseDatapack datapack = null!;

        /// <summary>
        /// The datapack the items are for
        /// </summary>
        public BaseDatapack Datapack { get => datapack; set => datapack ??= value; }

        /// <summary>
        /// Returns a part of a loot command used for getting a shulker box's content. Note the shulker box has to be of the type <see cref="ID.Block.purple_shulker_box"/>
        /// </summary>
        /// <returns>A <see cref="Commands.LootSources.MineItemSource"/> for getting loot from a shulker box</returns>
        public Commands.LootSources.MineItemSource GetShulkerItemSource(Vector shulkerLocation)
        {
            return new Commands.LootSources.MineItemSource(shulkerLocation, datapack.GetItems<SharpCraftFiles>().GetShulkerLootItem());
        }
    }
}
