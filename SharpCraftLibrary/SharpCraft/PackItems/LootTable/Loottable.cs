﻿using System.Collections.Generic;
using System.IO;
using SharpCraft.Data;
using SharpCraft.LootObjects;
using SharpCraft.Conditions;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// Class for loot table files
    /// </summary>
    public class LootTable : BaseFile<TextWriter>, ILootTable
    {
        /// <summary>
        /// Loot table types
        /// </summary>
        public enum TableType
        {
            /// <summary>
            /// For empty loot tables which doesn't drop anything
            /// </summary>
            empty,
            /// <summary>
            /// For entity loot tables
            /// </summary>
            entity,
            /// <summary>
            /// For block loot tables
            /// </summary>
            block,
            /// <summary>
            /// For chest loot tables
            /// </summary>
            chest,
            /// <summary>
            /// For fishing loot tables
            /// </summary>
            fishing,
            /// <summary>
            /// For advancement reward loot tables
            /// </summary>
            advancement_reward,
            /// <summary>
            /// For all other types of loot tables
            /// </summary>
            generic
        }

        private List<LootPool> pools = null!;

        /// <summary>
        /// Intializes a new <see cref="LootTable"/> with the given parameters. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the loot table is in</param>
        /// <param name="fileName">The name of the loot table file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="pools">The loot pools in the loot table</param>
        /// <param name="type">The type of loot table</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected LootTable(bool _, BasePackNamespace packNamespace, string? fileName, LootPool[] pools, TableType? type = null, WriteSetting writeSetting = WriteSetting.OnDispose) : base(packNamespace, fileName, writeSetting, "loot_table")
        {
            Type = type;
            Pools = pools.ToList();
        }

        /// <summary>
        /// Intializes a new <see cref="LootTable"/> with the given parameters
        /// </summary>
        /// <param name="packNamespace">The namespace the loot table is in</param>
        /// <param name="fileName">The name of the loot table file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="pools">The loot pools in the loot table</param>
        /// <param name="type">The type of loot table</param>
        public LootTable(BasePackNamespace packNamespace, string? fileName, LootPool[] pools, TableType? type = null, WriteSetting writeSetting = WriteSetting.OnDispose) : this(true, packNamespace, fileName, pools, type, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The type of loot table
        /// </summary>
        public TableType? Type { get; set; }

        /// <summary>
        /// The loot pools in this loot table
        /// </summary>
        public List<LootPool> Pools { get => pools; set => pools = value ?? throw new System.ArgumentNullException(nameof(Pools), "Pools may not be null"); }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("loot_tables");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "loot_tables/" + WritePath + ".json");
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            //Get pool strings
            string[] StringPools = new string[Pools.Count];
            for (int i = 0; i < Pools.Count; i++)
            {
                StringPools[i] = Pools[i].GetDataString();
            }

            //write file
            stream.Write("{");
            if (!(Type is null))
            {
                stream.Write("\"type\":\""+ Type +"\",");
            }
            stream.Write("\"pools\":[" + string.Join(",", StringPools) + "]}");
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            pools = null!;
        }
    }
}
