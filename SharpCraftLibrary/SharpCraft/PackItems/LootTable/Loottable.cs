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
    public class LootTable : BaseFile, ILootTable, IConvertableToDataTag
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

        private List<LootPool> pools;

        /// <summary>
        /// Intializes a new <see cref="LootTable"/> with the given parameters
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="pools">The loot pools in the loot table</param>
        /// <param name="type">The type of loot table</param>
        public LootTable(BasePackNamespace packNamespace, string fileName, LootPool[] pools, TableType? type = null, WriteSetting writeSetting = WriteSetting.OnDispose) : base(packNamespace, fileName, writeSetting)
        {
            Type = type;
            Pools = pools.ToList();
            if (IsAuto())
            {
                WriteFile(GetStream());
                Dispose();
            }
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
        /// Returns the namespace path of this <see cref="LootTable"/>
        /// </summary>
        /// <returns>this <see cref="LootTable"/>'s name</returns>
        public override string ToString()
        {
            return GetNamespacedName();
        }

        /// <summary>
        /// Converts this loot table into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(ToString());
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("loot_tables");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "loot_tables\\" + FileName + ".json");
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
    }

    /// <summary>
    /// Used for calling loot tables outside this program
    /// </summary>
    public class EmptyLoottable : ILootTable
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyLoottable"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the loot table is in</param>
        /// <param name="fileName">The name of the loot table</param>
        public EmptyLoottable(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the loot table
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the loot table is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for evoking this loot table
        /// </summary>
        /// <returns>The string used for evoking this loot table</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }
    }
}