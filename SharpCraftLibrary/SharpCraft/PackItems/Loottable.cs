using System.Collections.Generic;
using System.IO;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// A minecraft Loot Table
    /// </summary>
    public class Loottable : BaseFile, ILoottable, IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="Loottable"/>
        /// </summary>
        /// <param name="space">The namespace the loot table is in</param>
        /// <param name="fileName">The name of the loot table</param>
        /// <param name="LootPools">The pools in the loot table</param>
        public Loottable(PackNamespace space, string fileName, Pool[] LootPools) : base(space, fileName, WriteSetting.LockedAuto)
        {
            if (FileName.Contains("\\"))
            {
                Directory.CreateDirectory(PackNamespace.GetPath() + "loot_tables\\" + FileName.Substring(0, FileName.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(PackNamespace.GetPath() + "loot_tables\\");
            }

            StreamWriter TableWriter = new StreamWriter(new FileStream(PackNamespace.GetPath() + "loot_tables\\" + FileName + ".json", FileMode.Create)) { AutoFlush = true };
            string[] StringPools = new string[LootPools.Length];
            for (int i = 0; i < LootPools.Length; i++)
            {
                StringPools[i] = LootPools[i].ToString();
            }
            TableWriter.Write("{\"pools\": [" + string.Join(",",StringPools) + "]}");
            TableWriter.Dispose();
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Loottable"/>
        /// </summary>
        /// <returns>this <see cref="Loottable"/>'s name</returns>
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
        /// Writes this loot table file
        /// </summary>
        /// <param name="stream">The stream used for writing</param>
        protected override void WriteFile(TextWriter stream)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// A loot table <see cref="Pool"/> of <see cref="Entity"/>s
        /// </summary>
        public class Pool
        {
            /// <summary>
            /// Creates an new <see cref="Pool"/> with the given parameters
            /// </summary>
            /// <param name="Rolls">the amount of times the <see cref="Pool"/> should run</param>
            /// <param name="Entries">the <see cref="Entry"/>s in the <see cref="Pool"/></param>
            /// <param name="conditions"><see cref="Condition"/> chosing if the <see cref="Pool"/> should run or not</param>
            public Pool(Range Rolls, Entry[] Entries, Condition[] conditions = null)
            {
                this.Entries = Entries;
                this.Rolls = Rolls;
                this.conditions = conditions;
            }
            /// <summary>
            /// Creates an new <see cref="Pool"/> with the given parameters
            /// </summary>
            /// <param name="Rolls">the amount of times the <see cref="Pool"/> should run</param>
            /// <param name="LuckRolls">the amount of extra rolls per luck point</param>
            /// <param name="Entries">the <see cref="Entry"/>s in the <see cref="Pool"/></param>
            /// <param name="conditions"><see cref="Condition"/> chosing if the <see cref="Pool"/> should run or not</param>
            public Pool(Range Rolls, Range LuckRolls, Entry[] Entries, Condition[] conditions = null)
            {
                this.Entries = Entries;
                this.Rolls = Rolls;
                this.LuckRolls = LuckRolls;
                this.conditions = conditions;
            }
            readonly private Entry[] Entries;
            readonly private Range Rolls;
            readonly private Range LuckRolls;
            readonly private Condition[] conditions;

            /// <summary>
            /// Outputs this <see cref="Pool"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Pool"/>'s data</returns>
            public override string ToString()
            {
                string[] StringConditions = new string[Entries.Length];
                for (int i = 0; i < StringConditions.Length; i++)
                {
                    StringConditions[i] = Entries[i].ToString();
                }
                string TempString = "{\"entries\": [" + string.Join(",", StringConditions) + "]," + Rolls.JSONString("rolls");
                if (LuckRolls != null)
                {
                    TempString += "," + LuckRolls.JSONString("bonus_rolls");
                }
                if (conditions != null)
                {
                    TempString += ",\"conditions\":[" + string.Join<Condition>(",", conditions) + "]";
                }
                return TempString + "}";
            }
        }

        /// <summary>
        /// A <see cref="Loottable"/> <see cref="Entry"/>
        /// </summary>
        public class Entry
        {
            enum LootType
            {
                item,
                empty,
                loot_table
            }
            enum EntryType
            {
                Condition,
                Change,
                Alternatives,
                Dynamic,
                Group,
                Sequence,
                normal
            }

            /// <summary>
            /// the place to get the items from
            /// </summary>
            public enum DynamicType
            {
                /// <summary>
                /// drops the <see cref="Entry"/>/<see cref="Block"/>'s content
                /// </summary>
                contents,

                /// <summary>
                /// drops itself
                /// </summary>
                self
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops a single <see cref="Item"/>
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="item">the <see cref="Item"/> to drop</param>
            /// <param name="conditions">the <see cref="Condition"/>s for running this entry</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Normal(int weight, ID.Item item, Condition[] conditions = null)
            {
                _lootType = LootType.item;
                _entryType = EntryType.normal;
                _weight = weight;
                _Item = item.MinecraftValue();
                _conditions = conditions;
                if (conditions != null)
                {
                    _entryType = EntryType.Condition;
                }

                return this;
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops a <see cref="Loottable"/>
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="loottable">the <see cref="Loottable"/> to drop</param>
            /// <param name="conditions">the <see cref="Condition"/>s for running this entry</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Normal(int weight, Loottable loottable, Condition[] conditions = null)
            {
                _lootType = LootType.loot_table;
                _entryType = EntryType.normal;
                _weight = weight;
                _Item = loottable.ToString();
                _conditions = conditions;
                if (conditions != null)
                {
                    _entryType = EntryType.Condition;
                }

                return this;
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops nothing
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="conditions">the <see cref="Condition"/>s for running this entry</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Normal(int weight, Condition[] conditions = null)
            {
                _lootType = LootType.empty;
                _entryType = EntryType.normal;
                _weight = weight;
                _conditions = conditions;
                if (conditions != null)
                {
                    _entryType = EntryType.Condition;
                }

                return this;
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops an <see cref="Item"/> with changes
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="item">the <see cref="Item"/> to drop</param>
            /// <param name="changes">the <see cref="Change"/>s to make on the item</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Changes(int weight, Change[] changes, ID.Item item)
            {
                _lootType = LootType.item;
                _entryType = EntryType.Change;
                _weight = weight;
                _Item = item.MinecraftValue();
                _changes = changes;

                return this;
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops <see cref="Loottable"/> with changes
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="loottable">the <see cref="Loottable"/> to drop</param>
            /// <param name="changes">the <see cref="Change"/>s to make on the item</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Changes(int weight, Change[] changes, Loottable loottable)
            {
                _lootType = LootType.loot_table;
                _entryType = EntryType.Change;
                _weight = weight;
                _Item = loottable.ToString();
                _changes = changes;

                return this;
            }

            /// <summary>
            /// creates an <see cref="Entry"/> which drops nothing... with changes! (don't ask)
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="changes">the <see cref="Change"/>s to make on the nothingness... (I still don't know)</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Changes(int weight, Change[] changes)
            {
                _lootType = LootType.item;
                _entryType = EntryType.Change;
                _weight = weight;
                _changes = changes;

                return this;
            }

            /// <summary>
            /// Runs the first entry which can run
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="entries">the <see cref="Entry"/>s to pick from</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Alternatives(int weight, Entry[] entries)
            {
                _entryType = EntryType.Alternatives;
                _weight = weight;
                _entries = entries;

                return this;
            }

            /// <summary>
            /// Gets block specific drops
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="dynamicType">the place to get the drops from</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Dynamic(int weight, DynamicType dynamicType)
            {
                _entryType = EntryType.Dynamic;
                _weight = weight;
                _dynamicType = dynamicType;

                return this;
            }

            /// <summary>
            /// Runs the <paramref name="entries"/> if the <paramref name="conditions"/> are met
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="conditions">the <see cref="Condition"/>s for running this entry</param>
            /// <param name="entries">the <see cref="Entry"/>s to run</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Group(int weight, Condition[] conditions, Entry[] entries)
            {
                _entryType = EntryType.Group;
                _weight = weight;
                _entries = entries;
                _conditions = conditions;

                return this;
            }

            /// <summary>
            /// Runs through the <paramref name="entries"/> untill one doesn't execute
            /// </summary>
            /// <param name="weight">the <see cref="Entry"/>'s weight</param>
            /// <param name="entries">the <see cref="Entry"/>s to run</param>
            /// <returns>this <see cref="Entry"/></returns>
            public Entry Sequence(int weight, Entry[] entries)
            {
                _entryType = EntryType.Sequence;
                _weight = weight;
                _entries = entries;
                _entries = entries;

                return this;
            }

            private LootType _lootType;
            private EntryType _entryType;
            private int _weight;
            private string _Item;
            private Condition[] _conditions;
            private Change[] _changes;
            private Entry[] _entries;
            private DynamicType _dynamicType;

            /// <summary>
            /// Outputs this <see cref="Entry"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Entry"/>'s data</returns>
            public override string ToString()
            {
                switch(_entryType)
                {
                    case EntryType.normal:
                        if (_lootType == LootType.empty)
                        {
                            return "{\"type\":\"" + _lootType + "\",\"weight\":" + _weight + "}";
                        }
                        else
                        {
                            return "{\"type\":\"" + _lootType + "\",\"name\":\"" + _Item + "\",\"weight\":" + _weight + "}";
                        }

                    case EntryType.Condition:
                        if (_lootType == LootType.empty)
                        {
                            return "{\"conditions\":[" + string.Join<Condition>(",", _conditions) + "],\"type\":\"" + _lootType + "\",\"weight\":" + _weight + "}";
                        }
                        else
                        {
                            return "{\"conditions\":[" + string.Join<Condition>(",", _conditions) + "],\"type\":\"" + _lootType + "\",\"name\":\"" + _Item + "\",\"weight\":" + _weight + "}";
                        }

                    case EntryType.Change:
                        if (_lootType == LootType.empty)
                        {
                            return "{\"functions\":[" + string.Join<Change>(",", _changes) + "],\"type\":\"" + _lootType + "\",\"weight\":" + _weight + "}";
                        }
                        else
                        {
                            return "{\"functions\":[" + string.Join<Change>(",", _changes) + "],\"type\":\"" + _lootType + "\",\"name\":\"" + _Item + "\",\"weight\":" + _weight + "}";
                        }

                    case EntryType.Alternatives:
                        return "{\"children\":[" + string.Join<Entry>(",", _entries) + "],\"type\":\"alternatives\",\"weight\":" + _weight + "}";

                    case EntryType.Dynamic:
                        return "{\"name\":\"" + _dynamicType + "\",\"type\":\"dynamic\",\"weight\":" + _weight + "}";

                    case EntryType.Group:
                        return "{\"children\":[" + string.Join<Entry>(",", _entries) + "],\"type\":\"group\",\"weight\":" + _weight + "}";

                    case EntryType.Sequence:
                        return "{\"children\":[" + string.Join<Entry>(",", _entries) + "],\"type\":\"sequence\",\"weight\":" + _weight + "}";
                }
                return "{}";
            }
        }

        /// <summary>
        /// A <see cref="Change"/> which is made on an <see cref="Item"/>
        /// </summary>
        public class Change
        {
            enum ChangeType
            {
                EnchantRandom,
                EnchantLevels,
                Map,
                Smelt,
                Looting,
                SetAttribute,
                SetCount,
                SetDamage,
                SetName,
                SetNbt,
                ApplyBonus,
                CopyName,
                ExplosionDecay,
                LimitStack,
                SetContent,
                SetLootTable,
                FillPlayerHead,
                CopyNBT,
            }

            enum LootBonusFormula
            {
                binomial_with_bonus_count,
                uniform_bonus_count,
                ore_drops
            };

            /// <summary>
            /// Creates a new <see cref="Change"/>
            /// </summary>
            /// <param name="Conditions">The <see cref="Condition"/>s which has to be true to run</param>
            public Change(Condition[] Conditions = null)
            {
                _conditions = Conditions;
            }
            private readonly Condition[] _conditions;

            private CopyOperation[] _operations;
            private ID.LootTarget _target;
            private ChangeType _type;
            private Range _value;
            private Item _itemNBT;
            private ID.Enchant _enchant;
            private ID.Enchant[] _randomEnchants;
            private bool _allow;
            private string _setName;
            private ID.AttributeType _attribute;
            private ID.AttributeSlot[] _attributeSlots;
            private ID.AttributeOperation _operation;
            private ID.Structure _structure;
            private ID.MapMarker _marker;
            private int? _zoom;
            private int? _radius;
            private LootBonusFormula _formula;
            private double _multiplier;
            private Entry[] _entries;
            private long _seed;
            private Loottable _loottable;

            /// <summary>
            /// enchants the <see cref="Item"/> with one of the enchantments
            /// </summary>
            /// <param name="enchants">a list of enchantments</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change EnchantRandom(ID.Enchant[] enchants)
            {
                _type = ChangeType.EnchantRandom;
                _randomEnchants = enchants;

                return this;
            }

            /// <summary>
            /// enchants the <see cref="Item"/> with the levels
            /// </summary>
            /// <param name="levels">the amount of levels to enchant with</param>
            /// <param name="allowTrasure">if treasure enchants are allowed or not</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Enchant(Range levels, bool allowTrasure)
            {
                _type = ChangeType.EnchantLevels;
                _allow = allowTrasure;
                _value = levels;

                return this;
            }

            /// <summary>
            /// adds map information to the <see cref="Item"/>
            /// </summary>
            /// <param name="structure">the structure the map should find</param>
            /// <param name="marker">the marker icon used on the map</param>
            /// <param name="onlyNewChunks">if it only should search in unloaded chunks</param>
            /// <param name="zoom">how much zoomed in the map is</param>
            /// <param name="searchRadius">how far it should search</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Map(ID.Structure structure, ID.MapMarker marker, bool onlyNewChunks = true, int? zoom = null, int? searchRadius = null)
            {
                _type = ChangeType.Map;
                _structure = structure;
                _marker = marker;
                _allow = onlyNewChunks;
                _zoom = zoom;
                _radius = searchRadius;

                return this;
            }

            /// <summary>
            /// runs the <see cref="Item"/> through a furnace
            /// </summary>
            /// <returns>this <see cref="Change"/></returns>
            public Change Smelt()
            {
                _type = ChangeType.Smelt;

                return this;
            }

            /// <summary>
            /// adds extra items per level of looting
            /// </summary>
            /// <param name="countPerLevel">the amount of <see cref="Item"/>s to add</param>
            /// <param name="limit">the maximum amount of <see cref="Item"/>s it can drop</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Looting(Range countPerLevel, int? limit)
            {
                _type = ChangeType.Looting;
                _value = countPerLevel;
                _radius = limit;

                return this;
            }

            /// <summary>
            /// adds an <see cref="MCAttribute"/> to the <see cref="Item"/>
            /// </summary>
            /// <param name="attribute">the <see cref="MCAttribute"/> name</param>
            /// <param name="operation">the operation to use</param>
            /// <param name="value">the value of the <see cref="MCAttribute"/></param>
            /// <param name="slots">the slots the item has to be in to be activated</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Attribute(ID.AttributeType attribute, ID.AttributeOperation operation, Range value, ID.AttributeSlot[] slots)
            {
                _type = ChangeType.SetAttribute;
                _attribute = attribute;
                _operation = operation;
                _value = value;
                _attributeSlots = slots;

                return this;
            }

            /// <summary>
            /// Sets the <see cref="Item"/>'s name
            /// </summary>
            /// <param name="name">the name of the <see cref="Item"/></param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Name(JSON[] name)
            {
                _type = ChangeType.SetName;
                _setName = name.GetString().Escape();

                return this;
            }

            /// <summary>
            /// Sets the <see cref="Item"/>'s count
            /// </summary>
            /// <param name="count">the amount of <see cref="Item"/>s</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Count(Range count)
            {
                _type = ChangeType.SetCount;
                _value = count;

                return this;
            }

            /// <summary>
            /// Sets the <see cref="Item"/>s damage
            /// </summary>
            /// <param name="damage">the damage</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Damage(Range damage)
            {
                _type = ChangeType.SetDamage;
                _value = damage;

                return this;
            }

            /// <summary>
            /// Changes the <see cref="Item"/>'s nbt
            /// </summary>
            /// <param name="item">the nbt to add to the <see cref="Item"/></param>
            /// <returns>this <see cref="Change"/></returns>
            public Change NBT(Item item)
            {
                _type = ChangeType.SetNbt;
                _itemNBT = item;

                return this;
            }

            /// <summary>
            /// change the <see cref="Item"/>'s count to drop <see cref="Item"/>s in the same way ores does with fortune
            /// count*(max(0, random(0, 1)-1)+1)
            /// </summary>
            /// <param name="enchant">the enchantment to look at</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Bonus(ID.Enchant enchant)
            {
                _type = ChangeType.ApplyBonus;
                _formula = LootBonusFormula.ore_drops;
                _enchant = enchant;

                return this;
            }

            /// <summary>
            /// uniform adds to <see cref="Item"/>'s count
            /// multiplier*enchantment_level
            /// </summary>
            /// <param name="enchant">the enchantment to look at</param>
            /// <param name="multiplier">the amount to multiply by</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Bonus(ID.Enchant enchant, int multiplier)
            {
                _type = ChangeType.ApplyBonus;
                _formula = LootBonusFormula.uniform_bonus_count;
                _enchant = enchant;
                _multiplier = multiplier;

                return this;
            }

            /// <summary>
            /// I have no idea what this does...
            /// </summary>
            /// <param name="enchant"></param>
            /// <param name="probability"></param>
            /// <param name="rounds"></param>
            /// <returns>this <see cref="Change"/></returns>
            public Change Bonus(ID.Enchant enchant, double probability, int rounds)
            {
                _type = ChangeType.ApplyBonus;
                _formula = LootBonusFormula.binomial_with_bonus_count;
                _enchant = enchant;
                _multiplier = probability;
                _radius = rounds;

                return this;
            }

            /// <summary>
            /// copies the <see cref="Block"/>'s name onto the <see cref="Item"/>
            /// </summary>
            /// <returns>this <see cref="Change"/></returns>
            public Change CopyName()
            {
                _type = ChangeType.CopyName;

                return this;
            }

            /// <summary>
            /// destroys items based on explosion which happened
            /// </summary>
            /// <returns>this <see cref="Change"/></returns>
            public Change ExplosionDecay()
            {
                _type = ChangeType.ExplosionDecay;

                return this;
            }

            /// <summary>
            /// limits how small/big a stack in a chest might get
            /// </summary>
            /// <param name="count">the </param>
            /// <returns>this <see cref="Change"/></returns>
            public Change LimitStacks(Range count)
            {
                _type = ChangeType.LimitStack;
                _value = count;

                return this;
            }

            /// <summary>
            /// Add items to the item's BlockEntityTag
            /// </summary>
            /// <param name="entries">the entries of the items to drop</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change SetContent(Entry[] entries)
            {
                _type = ChangeType.SetContent;
                _entries = entries;

                return this;
            }

            /// <summary>
            /// Adds a loottable and a seed to the item
            /// </summary>
            /// <param name="loottable">the loottable to put into the item</param>
            /// <param name="seed">the seed to put into the item</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change SetLootTable(Loottable loottable, long seed = 0)
            {
                _type = ChangeType.SetLootTable;
                _seed = seed;
                _loottable = loottable;

                return this;
            }

            /// <summary>
            /// Fills the player head with the specified entity's data
            /// (Only supports player entities)
            /// </summary>
            /// <param name="target">The entity whose data to use</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change FillPlayerHead(ID.LootTarget target)
            {
                _type = ChangeType.FillPlayerHead;
                _target = target;

                return this;
            }

            /// <summary>
            /// Copies NBT from the target onto the item
            /// </summary>
            /// <param name="copyFrom">the target to copy from</param>
            /// <param name="operations">The copy operations</param>
            /// <returns>this <see cref="Change"/></returns>
            public Change CopyNBT(ID.LootTarget copyFrom, CopyOperation[] operations)
            {
                _type = ChangeType.CopyNBT;
                _target = copyFrom;
                _operations = operations;
                
                return this;
            }

            /// <summary>
            /// An object for storing a copy operation for <see cref="CopyNBT(ID.LootTarget, CopyOperation[])"/>
            /// </summary>
            public class CopyOperation
            {
                /// <summary>
                /// The path to copy from (the path to the target's data)
                /// </summary>
                public string CopyFromPath { get; private set; }
                /// <summary>
                /// The path to copy to (the path to the item's data)
                /// </summary>
                public string CopyToPath { get; private set; }
                /// <summary>
                /// The way to copy the data
                /// </summary>
                public ID.DataCopyWay CopyWay { get; private set; }

                /// <summary>
                /// Creates a new <see cref="CopyOperation"/> object
                /// </summary>
                /// <param name="copyFromPath">The path to copy from (the path to the target's data)</param>
                /// <param name="copyToPath">The path to copy to (The path starts at tag like this: Item.tag[This Path])</param>
                /// <param name="copyWay">The way to copy the data</param>
                public CopyOperation(string copyFromPath, string copyToPath, ID.DataCopyWay copyWay)
                {
                    CopyFromPath = copyFromPath;
                    CopyToPath = copyToPath;
                    CopyWay = copyWay;
                }
            }

            /// <summary>
            /// Ourputs raw data used by the game
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string ToString()
            {
                string ReturnString = "";
                switch(_type)
                {
                    case ChangeType.Smelt:
                        ReturnString = "{\"function\": \"minecraft:furnace_smelt\"";
                        break;
                    case ChangeType.SetCount:
                        ReturnString = "{\"function\":\"minecraft:set_count\"," + _value.JSONString("count");
                        break;
                    case ChangeType.SetDamage:
                        ReturnString = "{\"function\":\"minecraft:set_damage\"," + _value.JSONString("damage");
                        break;
                    case ChangeType.SetNbt:
                        ReturnString = "{\"function\":\"minecraft:set_nbt\",\"tag\":\"" + _itemNBT.GetItemTagString().Escape() + "\"";
                        break;
                    case ChangeType.EnchantRandom:
                        ReturnString = "{\"function\":\"minecraft:enchant_randomly\"";
                        if (_randomEnchants != null)
                        {
                            ReturnString += ",\"enchantments\": [\"" + string.Join("\",\"", _randomEnchants) + "\"]";
                        }
                        break;
                    case ChangeType.EnchantLevels:
                        ReturnString = "{\"function\":\"minecraft:enchant_with_levels\"," + _value.JSONString("levels");
                        if (_allow) { ReturnString += ",\"treasure\": true"; }
                        break;
                    case ChangeType.Looting:
                        ReturnString = "{\"function\":\"minecraft:looting_enchant\"," + _value.JSONString("count");
                        if (_radius != null) { ReturnString += ",\"limit\":" + _radius; }
                        break;
                    case ChangeType.SetAttribute:
                        ReturnString = "{\"function\":\"minecraft:set_attributes\",\"modifiers\":[{\"name\":\"" + _attribute + "\",\"attribute\":\"" + _attribute.ToString().Replace("_", ".") + "\"," + _value.JSONString("amount") + ",\"operation\":\"" + _operation.ToString() + "\"";
                        if (_attributeSlots != null)
                        {
                            if (_attributeSlots.Length == 1)
                            {
                                ReturnString += ",\"slot\":\"" + _attributeSlots[0] + "\"";
                            }
                            else
                            {
                                ReturnString += ",\"slot\":[\"" + string.Join("\",\"", _attributeSlots) + "\"]";
                            }
                        }
                        ReturnString += "}]";
                        break;
                    case ChangeType.SetName:
                        ReturnString = "{\"function\":\"minecraft:set_name\",\"name\":\"" + _setName + "\"";
                        break;
                    case ChangeType.ApplyBonus:
                        ReturnString = "{\"function\":\"minecraft:apply_bonus\",\"enchantment\":\"" + _enchant + "\",\"formula\":\"" + _formula + "\"";
                        switch (_formula)
                        {
                            case LootBonusFormula.binomial_with_bonus_count:
                                ReturnString += "\"probability\":" + _multiplier.ToMinecraftDouble() + ",\"extraRounds\":" + _radius;
                                break;
                            case LootBonusFormula.uniform_bonus_count:
                                ReturnString += "\"bonusMultiplier\":" + _multiplier.ToMinecraftDouble();
                                break;
                        }
                        break;
                    case ChangeType.CopyName:
                        ReturnString = "{\"function\":\"minecraft:copy_name\"";
                        break;
                    case ChangeType.ExplosionDecay:
                        ReturnString = "{\"function\":\"minecraft:explosion_decay\"";
                        break;
                    case ChangeType.LimitStack:
                        ReturnString = "{\"function\":\"minecraft:limit_count\"," + _value.JSONString("limit");
                        break;
                    case ChangeType.SetContent:
                        ReturnString = "{\"function\":\"minecraft:set_contents\",\"entries\":[" + string.Join<Entry>(",",_entries) + "]";
                        break;
                    case ChangeType.SetLootTable:
                        ReturnString = "{\"function\":\"minecraft:set_loot_table\",\"name\":\"" + _loottable + "\",\"seed\":\"" + _seed + "\"";
                        break;
                    case ChangeType.FillPlayerHead:
                        ReturnString = "{\"function\":\"minecraft:fill_player_head\",\"entity\":\"" + _target.ToString().ToLower() + "\"";
                        break;
                    case ChangeType.CopyNBT:
                        ReturnString = "{\"function\":\"minecraft:copy_nbt\",\"source\":\"" + _target.ToString().ToLower() + "\",\"ops\":[";
                        for (int i = 0; i < _operations.Length; i++)
                        {
                            if (i != 0)
                            {
                                ReturnString += ",";
                            }
                            ReturnString += "{\"source\":\"" + _operations[i].CopyFromPath + "\",\"target\":\"" + _operations[i].CopyToPath + "\",\"op\":\"" + _operations[i].CopyWay + "\"}";
                        }
                        ReturnString += "]";
                        break;
                    case ChangeType.Map:
                        ReturnString = "{\"function\":\"minecraft:exploration_map\",\"destination\":\"" + _structure + "\",\"decoration\":\"" + _marker + "\",\"skip_existing_chunks\":" + _allow;
                        if (!(_zoom is null))
                        {
                            ReturnString += ",\"zoom\":" + _zoom;
                        }
                        if (!(_radius is null))
                        {
                            ReturnString += ",\"search_radius\":" + _radius;
                        }
                        break;
                }

                if (_conditions != null)
                {
                    ReturnString += ",\"conditions\":[" + string.Join<Condition>(",", _conditions) + "]";
                }
                return ReturnString + "}";
            }
        }

        /// <summary>
        /// A <see cref="Condition"/> which has to be true for the thing to happen
        /// </summary>
        public class Condition
        {
            enum LootCondition
            {
                EntityProperties,
                EntityScore,
                KilledByPlayer,
                RandomChance,
                RandomLootChance,
                BlockState,
                DamageSource,
                Location,
                EntityPresent,
                UsedItem,
                SurvivesExplosion,
                RandomEnchantmentChance,
                Weather,
                Alternative
            }

            /// <summary>
            /// Creates a new <see cref="Loottable"/> condition
            /// </summary>
            /// <param name="inverted">If the condition should be false instead of true</param>
            public Condition(bool inverted = false)
            {
                _inverted = inverted;
            }

            private readonly bool _inverted;

            private ID.Enchant _enchant;
            private double[] _chances;
            private ID.LootTarget _checkEntity;
            private LootCondition _condition;
            private JSONObjects.Entity _entity;
            private Range _range;
            private ScoreObject _scoreObject;
            private double _random;
            private double _Multiplier;
            private JSONObjects.Damage _damage;
            private JSONObjects.Location _location;
            private JSONObjects.Item _item;
            private bool? _rain;
            private bool? _thunder;
            private JSONObjects.Block _block;
            private Condition[] _conditions;

            /// <summary>
            /// Tests if the entity is the specified entity
            /// </summary>
            /// <param name="entity">the entity its supposed to be</param>
            /// <param name="select">the entity to check</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition EntityProperties(JSONObjects.Entity entity, ID.LootTarget select)
            {
                _condition = LootCondition.EntityProperties;
                _entity = entity;
                _checkEntity = select;

                return this;
            }

            /// <summary>
            /// Tests if the entity has the specified score
            /// </summary>
            /// <param name="scoreObject">the <see cref="ScoreObject"/> to look in</param>
            /// <param name="range">the range the score has to be in</param>
            /// <param name="select">the entity to check</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition EntityScore(ScoreObject scoreObject, Range range, ID.LootTarget select)
            {
                _condition = LootCondition.EntityScore;
                _scoreObject = scoreObject;
                _range = range;
                _checkEntity = select;

                return this;
            }

            /// <summary>
            /// Tests if it was a player who killed the entity
            /// </summary>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition KilledByPlayer()
            {
                _condition = LootCondition.KilledByPlayer;

                return this;
            }

            /// <summary>
            /// Tests if a random number is less than the specified number
            /// </summary>
            /// <param name="chance">The number the random number has to be less than</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition RandomChance(double chance)
            {
                _condition = LootCondition.RandomChance;
                _random = chance;

                return this;
            }

            /// <summary>
            /// Tests if a random number is less than the specified number multiplied by the looting multiplier
            /// <paramref name="chance"/> + ( [looting level] + <paramref name="lootMultiplier"/>)
            /// </summary>
            /// <param name="chance">The number the random number has to be less than</param>
            /// <param name="lootMultiplier">the amount to multiply with per looting level</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition RandomLootChance(double chance, double lootMultiplier)
            {
                _condition = LootCondition.RandomLootChance;
                _random = chance;
                _Multiplier = lootMultiplier;

                return this;
            }

            /// <summary>
            /// Tests if the broken block has the specified block state
            /// </summary>
            /// <param name="block">the block the block has to be</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition BlockState(JSONObjects.Block block)
            {
                _condition = LootCondition.BlockState;
                _block = block;

                return this;
            }

            /// <summary>
            /// Tests if the damage is of the specified damage type
            /// </summary>
            /// <param name="damage">the type of damage</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition DamageSource(JSONObjects.Damage damage)
            {
                _condition = LootCondition.DamageSource;
                _damage = damage;

                return this;
            }

            /// <summary>
            /// Tests if the specified entity is loaded and exists in the world
            /// </summary>
            /// <param name="entity">the entity to test for</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition EntityPresent(JSONObjects.Entity entity)
            {
                _condition = LootCondition.EntityPresent;
                _entity = entity;

                return this;
            }
            /// <summary>
            /// Tests if the location of the looted thing
            /// </summary>
            /// <param name="location">the location the looted thing has to be at</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition Location(JSONObjects.Location location)
            {
                _condition = LootCondition.Location;
                _location = location;

                return this;
            }

            /// <summary>
            /// Tests if the player used the specified item to loot the thing
            /// </summary>
            /// <param name="item">the item which has to be used</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition UsedItem(JSONObjects.Item item)
            {
                _condition = LootCondition.UsedItem;
                _item = item;

                return this;
            }

            /// <summary>
            /// Tests if the item would survive the explosion
            /// </summary>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition SurvivesExplosion()
            {
                _condition = LootCondition.SurvivesExplosion;

                return this;
            }

            /// <summary>
            /// Tests if a random number is less than the number in <paramref name="chancesPerLevel"/> indexed at the item's <paramref name="enchant"/>'s level
            /// </summary>
            /// <param name="enchant">the enchantment to get the index from</param>
            /// <param name="chancesPerLevel">the chance each level should give</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition RandomEnchantmentChance(ID.Enchant enchant, double[] chancesPerLevel)
            {
                _condition = LootCondition.RandomEnchantmentChance;
                _enchant = enchant;
                _chances = chancesPerLevel;

                return this;
            }

            /// <summary>
            /// Tests the weather
            /// </summary>
            /// <param name="rain">true if the weather should be raining</param>
            /// <param name="thunder">true if the weather should be thundering</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition Weather(bool? rain, bool? thunder)
            {
                _condition = LootCondition.Weather;
                _rain = rain;
                _thunder = thunder;

                return this;
            }

            /// <summary>
            /// Tests if any of the given <see cref="Condition"/>s are true
            /// </summary>
            /// <param name="conditions">the <see cref="Condition"/> to test</param>
            /// <returns>this <see cref="Condition"/></returns>
            public Condition Or(Condition[] conditions)
            {
                _condition = LootCondition.Alternative;
                _conditions = conditions;

                return this;
            }

            private string JustCondition()
            {
                switch (_condition)
                {
                    case LootCondition.BlockState:
                        return "{\"condition\":\"block_state_property\"," + _block.ToString(true) + "}";

                    case LootCondition.DamageSource:
                        return "{\"condition\":\"damage_source_properties\",\"predicate\":" + _damage + "}";

                    case LootCondition.EntityPresent:
                        return "{\"condition\":\"entity_present\",\"predicate\":" + _entity + "}";

                    case LootCondition.EntityProperties:
                        return "{\"condition\":\"entity_properties\",\"predicate\":" + _entity + ",\"entity\":\"" + _checkEntity.ToString().ToLower() + "\"}";

                    case LootCondition.EntityScore:
                        return "{\"condition\":\"entity_scores\",\"scores\":{" + _range.JSONString(_scoreObject.ToString()) + "},\"entity\":\"" + _checkEntity.ToString().ToLower() + "\"}";

                    case LootCondition.KilledByPlayer:
                        return "{\"condition\":\"killed_by_player\"}";

                    case LootCondition.Location:
                        return "{\"condition\":\"location_check\",\"predicate\":" + _location + "}";

                    case LootCondition.RandomChance:
                        return "{\"condition\":\"random_chance\",\"chance\":" + _random.ToMinecraftDouble() + "}";

                    case LootCondition.RandomEnchantmentChance:
                        string returnString = "{\"condition\":\"table_bonus\",\"enchantment\":\"" + _enchant + "\",\"chances\":[";
                        List<string> values = new List<string>();
                        foreach(double number in _chances)
                        {
                            values.Add(number.ToMinecraftDouble());
                        }
                        return returnString + string.Join("],[",values) + "]}";

                    case LootCondition.RandomLootChance:
                        return "{\"condition\":\"looting_multiplier\",\"chance\":" + _random.ToMinecraftDouble() + ",\"looting_multiplier\":" + _Multiplier.ToMinecraftDouble() + "}";

                    case LootCondition.SurvivesExplosion:
                        return "{\"condition\":\"survives_explosion\"}";

                    case LootCondition.UsedItem:
                        return "{\"condition\":\"match_tool\",\"predicate\":" + _item + "}";

                    case LootCondition.Weather:
                        string weatherString = "{\"condition\":\"match_tool\"";
                        if (_rain != null) { weatherString += ",\"raining\":" + _rain; }
                        if (_thunder != null) { weatherString += ",\"raining\":" + _thunder; }
                        return weatherString + "}";

                    case LootCondition.Alternative:
                        return "{\"condition\":\"alternative\",\"terms\":[" + string.Join<Condition>(",",_conditions) + "]}";
                }
                return "{}";
            }

            /// <summary>
            /// Outputs the raw data used by the game
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string ToString()
            {
                if(_inverted)
                {
                    return "{\"condition\":\"inverted\",\"term\":" + JustCondition() + "}";
                }
                else
                {
                    return JustCondition();
                }
            }

            /// <summary>
            /// Converts a single <see cref="Condition"/> into an array containing only that one <see cref="Condition"/>
            /// </summary>
            /// <param name="condition">the <see cref="Condition"/> to convert into an array</param>
            public static implicit operator Condition[](Condition condition)
            {
                return new Condition[] { condition };
            }
        }
    }

    /// <summary>
    /// Used for calling loot tables outside this program
    /// </summary>
    public class EmptyLoottable : ILoottable
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
