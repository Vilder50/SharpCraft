using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// A object used to create <see cref="Advancement"/>s
    /// </summary>
    public class Advancement
    {
        string Path;
        /// <summary>
        /// Creates an <see cref="Advancement"/> object with the given string
        /// Used to run <see cref="Advancement"/> which doesnt have an object
        /// use <see cref="PackNamespace.NewAdvancement(string, JSON[], JSON[], JSONObjects.Item, string, Requirement, Reward, ID.AdvancementFrame, bool, bool, bool)"/> to create a new <see cref="Advancement"/>
        /// </summary>
        /// <param name="advancement">An string path to and <see cref="Advancement"/></param>
        public Advancement(string advancement)
        {
            Path = advancement.ToLower().Replace("\\", "/");
        }
        internal Advancement(PackNamespace Namespace, string AdvancementName, JSON[] IngameName, JSON[] Description, JSONObjects.Item Icon, Advancement Parent, Requirement Requirement, Reward Reward, ID.AdvancementFrame Frame, bool ShowToast, bool ChatAnnounce, bool Hidden)
        {
            MakeAdvancementPath(Namespace, AdvancementName);
            WriteFile(Namespace, AdvancementName, IngameName, Description, Icon, Parent, null, Requirement, Reward, Frame, ShowToast, ChatAnnounce, Hidden);
        }
        internal Advancement(PackNamespace Namespace, string AdvancementName, JSON[] IngameName, JSON[] Description, JSONObjects.Item Icon, string Background, Requirement Requirement, Reward Reward, ID.AdvancementFrame Frame, bool ShowToast, bool ChatAnnounce, bool Hidden)
        {
            MakeAdvancementPath(Namespace, AdvancementName);
            WriteFile(Namespace, AdvancementName, IngameName, Description, Icon, null, Background, Requirement, Reward, Frame, ShowToast, ChatAnnounce, Hidden);
        }
        internal Advancement(PackNamespace Namespace, string AdvancementName)
        {
            MakeAdvancementPath(Namespace, AdvancementName);
            Path = Namespace.Name + ":" + AdvancementName.Replace("\\", "/");
            StreamWriter AdvancementWriter = new StreamWriter(new FileStream(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\advancements\\" + AdvancementName + ".json", FileMode.Create)) { AutoFlush = true };
            AdvancementWriter.Write("{\"parent\":\"notoast:notoast\",\"criteria\":{\"impossible\":{\"trigger\":\"minecraft:imp0ssible\"}}}");
        }
        private void WriteFile(PackNamespace Namespace, string AdvancementName, JSON[] IngameName, JSON[] Description, JSONObjects.Item Icon, Advancement Parent, string Background, Requirement Requirement, Reward Reward, ID.AdvancementFrame Frame, bool ShowToast, bool ChatAnnounce, bool Hidden)
        {
            Path = Namespace.Name + ":" + AdvancementName.Replace("\\", "/");
            List<Trigger> UsedTriggers = new List<Trigger>();
            if (Requirement != null)
            {
                for (int i = 0; i < Requirement.UsedTriggers.Count; i++)
                {
                    bool Sorted = false;
                    for (int j = 0; j < UsedTriggers.Count; j++)
                    {
                        if (UsedTriggers[j].ToString() == Requirement.UsedTriggers[i].ToString()) { Sorted = true; }
                    }
                    if (!Sorted)
                    {
                        UsedTriggers.Add(Requirement.UsedTriggers[i]);
                    }
                }
            }

            StreamWriter AdvancementWriter = new StreamWriter(new FileStream(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\advancements\\" + AdvancementName + ".json", FileMode.Create)) { AutoFlush = true };

            AdvancementWriter.WriteLine("{");
            if (Icon != null)
            {
                AdvancementWriter.WriteLine("\"display\": {\"icon\":" + Icon + ",\"title\":" + IngameName.GetString() + ",\"description\":" + Description.GetString());
                if (Frame != ID.AdvancementFrame.task) { AdvancementWriter.WriteLine(",\"frame\": \"" + Frame + "\""); }
                if (Background != null) { AdvancementWriter.WriteLine(",\"background\": \"" + Background + "\""); }
                if (!ShowToast) { AdvancementWriter.WriteLine(",\"show_toast\":false"); }
                if (!ChatAnnounce) { AdvancementWriter.WriteLine(",\"announce_to_chat\":false"); }
                if (Hidden) { AdvancementWriter.WriteLine(",\"hidden\":true"); }
                AdvancementWriter.WriteLine("},");
            }
            if (Parent != null) { AdvancementWriter.WriteLine("\"parent\": \"" + Parent + "\","); }
            if (Reward != null) { AdvancementWriter.WriteLine(Reward + ","); }
            AdvancementWriter.WriteLine("\"criteria\":{");
            for (int i = 0; i < UsedTriggers.Count; i++)
            {
                if (i != UsedTriggers.Count - 1) { AdvancementWriter.WriteLine(UsedTriggers[i].Data() + ","); }
                else { AdvancementWriter.WriteLine(UsedTriggers[i].Data()); }
            }
            AdvancementWriter.WriteLine("},\"requirements\":[" + Requirement + "]");
            AdvancementWriter.WriteLine("}");

            AdvancementWriter.Dispose();
        }

        private static void MakeAdvancementPath(PackNamespace Namespace, string advancementName)
        {
            if (advancementName.Contains("\\"))
            {
                Directory.CreateDirectory(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\advancements\\" + advancementName.ToLower().Substring(0, advancementName.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\advancements\\");
            }
        }

        /*
        Thanks to Skylinerw for making an advancement guide:
        https://github.com/skylinerw/guides/blob/master/java/advancements.md
         */

        /// <summary>
        /// An <see cref="Object"/> which triggers if the given thing is true
        /// </summary>
        public class Trigger
        {
            enum AdvancementTrigger
            {
                bred_animals,
                brewed_potion,
                changed_dimension,
                construct_beacon,
                consume_item,
                cured_zombie_villager,
                effects_changed,
                enchanted_item,
                enter_block,
                entity_hurt_player,
                entity_killed_player,
                impossible,
                inventory_changed,
                item_durability_changed,
                levitation,
                location,
                nether_travel,
                placed_block,
                player_hurt_entity,
                player_killed_entity,
                recipe_unlocked,
                slept_in_bed,
                summoned_entity,
                tame_animal,
                tick,
                used_ender_eye,
                used_totem,
                villager_trade,
                channeled_lightning,
                filled_bucket,
                fishing_rod_hooked,
                killed_by_crossbow
            };

            /// <summary>
            /// Creates a new <see cref="Trigger"/>
            /// </summary>
            /// <param name="setName">The <see cref="Trigger"/>'s name. If empty its auto generated</param>
            public Trigger(string setName = null)
            {
                Name = setName;
            }

            /// <summary>
            /// Triggered when a player has bred 2 <see cref="Entity"/>s
            /// </summary>
            /// <param name="parent">The parent <see cref="Entity"/></param>
            /// <param name="partner">The partner <see cref="Entity"/></param>
            /// <param name="child">The child <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger BredAnimals(JSONObjects.Entity parent = null, JSONObjects.Entity partner = null, JSONObjects.Entity child = null)
            {
                TriggerType = AdvancementTrigger.bred_animals;
                this.Parent = parent;
                this.Partner = partner;
                this.Child = child;

                return this;
            }

            /// <summary>
            /// Triggered when a player takes an item out of a brewing stand
            /// </summary>
            /// <param name="potion">The potion effect of the taken potion</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger BrewedPotion(ID.Potion? potion = null)
            {
                TriggerType = AdvancementTrigger.brewed_potion;
                this.Potion = potion;

                return this;
            }

            /// <summary>
            /// Triggered when a player travels from one dimension into another
            /// </summary>
            /// <param name="tpTo">The start dimension</param>
            /// <param name="tpFrom">The go to dimension</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger ChangedDimension(ID.Dimension? tpTo, ID.Dimension? tpFrom)
            {
                TriggerType = AdvancementTrigger.changed_dimension;
                this.TPTo = tpTo;
                this.TPFrom = tpFrom;

                return this;
            }

            /// <summary>
            /// Triggerd when a beacon close to the player activates
            /// </summary>
            /// <param name="layers">The layers the beacon has</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger ConstructBeacon(Range layers)
            {
                TriggerType = AdvancementTrigger.construct_beacon;
                this.Level = layers;

                return this;
            }

            /// <summary>
            /// Triggered when the player consumes an <see cref="Item"/>
            /// </summary>
            /// <param name="consumedItem">the <see cref="Item"/> the player consumed</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger ConsumeItem(JSONObjects.Item consumedItem)
            {
                TriggerType = AdvancementTrigger.consume_item;
                Item = consumedItem;

                return this;
            }

            /// <summary>
            /// Triggered when the player cures a zombie villager
            /// </summary>
            /// <param name="zombie">the zombie before the convertion</param>
            /// <param name="villager">the villager after the convertion</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger CuredZombieVillager(JSONObjects.Entity zombie = null, JSONObjects.Entity villager = null)
            {
                TriggerType = AdvancementTrigger.cured_zombie_villager;
                this.Zombie = zombie;
                this.Villager = villager;

                return this;
            }

            /// <summary>
            /// Triggered when the player gets an effect
            /// </summary>
            /// <param name="effects">the effect(s) the player has</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EffectsChanged(JSONObjects.Effect[] effects)
            {
                TriggerType = AdvancementTrigger.effects_changed;
                this.Effects = effects;

                return this;
            }

            /// <summary>
            /// Triggered when the player enchants an <see cref="Item"/>
            /// </summary>
            /// <param name="enchantedItem">the new enchanted <see cref="Item"/></param>
            /// <param name="level">the amount of levels the player used to enchant</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EnchantedItem(JSONObjects.Item enchantedItem = null, Range level = null)
            {
                TriggerType = AdvancementTrigger.enchanted_item;
                Item = enchantedItem;
                this.Level = level;

                return this;
            }

            /// <summary>
            /// Triggered every tick and for each block the player is in
            /// </summary>
            /// <param name="enterBlock">The <see cref="Block"/> the player is in</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EnterBlock(JSONObjects.Block enterBlock = null)
            {
                TriggerType = AdvancementTrigger.enter_block;
                Block = enterBlock;

                return this;
            }

            /// <summary>
            /// Triggered when an <see cref="Entity"/> hurts the player
            /// </summary>
            /// <param name="damage">the type of damage</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EntityHurtPlayer(JSONObjects.Damage damage = null)
            {
                TriggerType = AdvancementTrigger.entity_hurt_player;
                this.Damage = damage;

                return this;
            }

            /// <summary>
            /// Triggered when an entity kills the player
            /// </summary>
            /// <param name="damageFlags">the type of damage</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EntityKilledPlayer(JSONObjects.DamageFlags damageFlags = null)
            {
                TriggerType = AdvancementTrigger.entity_killed_player;
                this.DamageFlags = damageFlags;

                return this;
            }
            /// <summary>
            /// Never triggers
            /// </summary>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger Impossible()
            {
                TriggerType = AdvancementTrigger.impossible;

                return this;
            }

            /// <summary>
            /// Triggers when the player's inventory changes
            /// </summary>
            /// <param name="occupiedSlots">The amount of slots which are in use</param>
            /// <param name="fullSlots">The amount of slots which are full</param>
            /// <param name="emptySlots">The amount of slots which are empty</param>
            /// <param name="items">Checks if the player's inventory has the <see cref="Item"/>s</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger InventoryChanged(Range occupiedSlots = null, Range fullSlots = null, Range emptySlots = null, JSONObjects.Item[] items = null)
            {
                TriggerType = AdvancementTrigger.inventory_changed;
                this.OccupiedSlots = occupiedSlots;
                this.FullSlots = fullSlots;
                this.EmptySlots = emptySlots;
                this.Items = items;

                return this;
            }

            /// <summary>
            /// Triggered when an item in the player's inventory has been damaged
            /// </summary>
            /// <param name="item">The damaged <see cref="Item"/></param>
            /// <param name="durability">The durability of the item</param>
            /// <param name="durabilityChange">the amount of durability change</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger ItemDurabilityChanged(JSONObjects.Item item = null, Range durability = null, Range durabilityChange = null)
            {
                TriggerType = AdvancementTrigger.item_durability_changed;
                this.Item = item;
                this.Durability = durability;
                this.DurabilityChange = durabilityChange;

                return this;
            }

            /// <summary>
            /// Triggered every tick while the player has levitation
            /// </summary>
            /// <param name="distance">the distance from the start position</param>
            /// <param name="duration">the duration of the effect</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger Levitation(JSONObjects.Distance distance = null, Range duration = null)
            {
                TriggerType = AdvancementTrigger.levitation;
                this.Distance = distance;
                this.Duration = duration;

                return this;
            }

            /// <summary>
            /// Triggered every second for each player to check their location
            /// </summary>
            /// <param name="location">The location the player is in</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger AtLocation(JSONObjects.Location location = null)
            {
                TriggerType = AdvancementTrigger.location;
                this.Location = location;

                return this;
            }

            /// <summary>
            /// Triggered if a player goes into the nether and back
            /// </summary>
            /// <param name="enterLocation">the start location in the overworld</param>
            /// <param name="exitLocation">the end location in the overworld</param>
            /// <param name="distance">the distance between the 2 locations</param>
            /// <returns></returns>
            public Trigger NetherTravel(JSONObjects.Location enterLocation = null, JSONObjects.Location exitLocation = null, JSONObjects.Distance distance = null)
            {
                TriggerType = AdvancementTrigger.nether_travel;
                StartLocation = enterLocation;
                Location = exitLocation;
                this.Distance = distance;

                return this;
            }

            /// <summary>
            /// Triggered when the player places a <see cref="Block"/>
            /// </summary>
            /// <param name="block">the newly placed <see cref="Block"/></param>
            /// <param name="item">the used <see cref="Item"/></param>
            /// <param name="location">the location of the block</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger PlacedBlock(JSONObjects.Block block = null, JSONObjects.Item item = null, JSONObjects.Location location = null)
            {
                TriggerType = AdvancementTrigger.placed_block;
                this.Block = block;
                this.Item = item;
                this.Location = location;

                return this;
            }

            /// <summary>
            /// triggered when the player hurts an <see cref="Entity"/>
            /// </summary>
            /// <param name="damage">the damage type</param>
            /// <param name="entity">the damaged <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger PlayerHurtEntity(JSONObjects.Damage damage = null, JSONObjects.Entity entity = null)
            {
                TriggerType = AdvancementTrigger.player_hurt_entity;
                this.Damage = damage;
                this.Entity = entity;

                return this;
            }

            /// <summary>
            /// triggered when the player hurts an <see cref="Entity"/>
            /// </summary>
            /// <param name="damage">the damage type</param>
            /// <param name="entity">the killed <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger PlayerKilledEntity(JSONObjects.DamageFlags damage = null, JSONObjects.Entity entity = null)
            {
                TriggerType = AdvancementTrigger.player_killed_entity;
                DamageFlags = damage;
                this.Entity = entity;

                return this;
            }

            /// <summary>
            /// Triggered when the player unlocks a new <see cref="Recipe"/>
            /// </summary>
            /// <param name="unlockedRecipe">the unlocked <see cref="Recipe"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger RecipeUnlocked(Recipe unlockedRecipe)
            {
                TriggerType = AdvancementTrigger.recipe_unlocked;
                Recipe = unlockedRecipe;

                return this;
            }

            /// <summary>
            /// triggered when the player starts to sleep
            /// </summary>
            /// <param name="location">the location it happens at</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger EnterBed(JSONObjects.Location location = null)
            {
                TriggerType = AdvancementTrigger.slept_in_bed;
                this.Location = location;

                return this;
            }

            /// <summary>
            /// triggered when the player summons an <see cref="Entity"/>
            /// </summary>
            /// <param name="entity">the newly summoned <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger SummonedEntity(JSONObjects.Entity entity = null)
            {
                TriggerType = AdvancementTrigger.summoned_entity;
                this.Entity = entity;

                return this;
            }

            /// <summary>
            /// triggered when the player tames an <see cref="Entity"/>
            /// </summary>
            /// <param name="entity">the tamed <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger TameAnimal(JSONObjects.Entity entity = null)
            {
                TriggerType = AdvancementTrigger.tame_animal;
                this.Entity = entity;

                return this;
            }

            /// <summary>
            /// Triggers a player every tick
            /// </summary>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger Tick()
            {
                TriggerType = AdvancementTrigger.tick;

                return this;
            }

            /// <summary>
            /// triggered when a player uses an eye of ender
            /// </summary>
            /// <param name="distance">the distance to the stronghold</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger UsedEnderEye(Range distance = null)
            {
                TriggerType = AdvancementTrigger.used_ender_eye;
                EyeDistance = distance;

                return this;
            }

            /// <summary>
            /// triggered when a player activates a totem
            /// </summary>
            /// <param name="item">the activated totem</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger UsedTotem(JSONObjects.Item item = null)
            {
                TriggerType = AdvancementTrigger.used_totem;
                this.Item = item;

                return this;
            }

            /// <summary>
            /// Triggered when the player trades with a villager
            /// </summary>
            /// <param name="villager">the villager traded with</param>
            /// <param name="item">the purchased <see cref="Item"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger VillagerTrade(JSONObjects.Entity villager = null, JSONObjects.Item item = null)
            {
                TriggerType = AdvancementTrigger.villager_trade;
                this.Villager = villager;
                this.Item = item;

                return this;
            }

            /// <summary>
            /// Triggered when the player fills a bucket
            /// </summary>
            /// <param name="item">the new bucket</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger FilledBucket(JSONObjects.Item item = null)
            {
                TriggerType = AdvancementTrigger.filled_bucket;
                this.Item = item;

                return this;
            }

            /// <summary>
            /// Triggered when the player hits/catches something with a fishing rod
            /// </summary>
            /// <param name="item">The catched <see cref="Item"/></param>
            /// <param name="fishingRod">the used fishing rod</param>
            /// <param name="entity">the hit <see cref="Entity"/></param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger CaughtWithRod(JSONObjects.Item item = null, JSONObjects.Item fishingRod = null, JSONObjects.Entity entity = null)
            {
                this.Items = new JSONObjects.Item[2];
                TriggerType = AdvancementTrigger.fishing_rod_hooked;
                this.Items[0] = item;
                this.Items[1] = fishingRod;
                this.Entity = entity;

                return this;
            }

            /// <summary>
            /// triggered when a player makes thunder with a trident
            /// </summary>
            /// <param name="hitEntities">the <see cref="Entity"/>s hit by the lightning</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger TridentChanneling(JSONObjects.Entity[] hitEntities = null)
            {
                TriggerType = AdvancementTrigger.channeled_lightning;
                Entities = hitEntities;

                return this;
            }

            /// <summary>
            /// triggered when a player kills an entity with a crossbow
            /// </summary>
            /// <param name="killEntities">the <see cref="Entity"/>s killed by the crossbow</param>
            /// <returns>this <see cref="Trigger"/></returns>
            public Trigger CrossbowKill(JSONObjects.Entity[] killEntities = null)
            {
                TriggerType = AdvancementTrigger.killed_by_crossbow;
                Entities = killEntities;

                return this;
            }


            internal string Name;
            AdvancementTrigger TriggerType;
            Range Durability;
            Range DurabilityChange;
            Range OccupiedSlots;
            Range FullSlots;
            Range EmptySlots;
            Range Level;
            Range Duration;
            Range EyeDistance;
            JSONObjects.Entity Entity;
            JSONObjects.Entity Parent;
            JSONObjects.Entity Partner;
            JSONObjects.Entity Child;
            JSONObjects.Entity Zombie;
            JSONObjects.Entity Villager;
            JSONObjects.Entity[] Entities;
            JSONObjects.Item[] Items;
            JSONObjects.Item Item;
            Recipe Recipe;
            JSONObjects.Location StartLocation;
            JSONObjects.Location Location;
            ID.Dimension? TPTo;
            ID.Dimension? TPFrom;
            JSONObjects.Block Block;
            ID.Potion? Potion;
            JSONObjects.Damage Damage;
            JSONObjects.Effect[] Effects;
            JSONObjects.DamageFlags DamageFlags;
            JSONObjects.Distance Distance;

            internal string Data()
            {
                List<string> TempList = new List<string>();

                switch (TriggerType)
                {
                    case AdvancementTrigger.bred_animals:
                        if (Partner != null) { TempList.Add("\"partner\":" + Partner); }
                        if (Parent != null) { TempList.Add("\"parent\":" + Parent); }
                        if (Child != null) { TempList.Add("\"child\":" + Child); }
                        break;

                    case AdvancementTrigger.brewed_potion:
                        if (Potion != null) { TempList.Add("\"potion\":\"" + Potion + "\""); }
                        break;

                    case AdvancementTrigger.changed_dimension:
                        if (TPTo != null) { TempList.Add("\"to\":\"" + TPTo + "\""); }
                        if (TPFrom != null) { TempList.Add("\"from\":\"" + TPFrom + "\""); }
                        break;

                    case AdvancementTrigger.construct_beacon:
                        if (Level != null) { TempList.Add(Level.JSONString("level"));}
                        break;

                    case AdvancementTrigger.consume_item:
                    case AdvancementTrigger.used_totem:
                    case AdvancementTrigger.filled_bucket:
                        if (Item != null) { TempList.Add("\"item\":" + Item); }
                        break;

                    case AdvancementTrigger.fishing_rod_hooked:
                        if (Items[0] != null) { TempList.Add("\"item\":" + Items[0]); }
                        if (Items[1] != null) { TempList.Add("\"rod\":" + Items[1]); }
                        if (Entity != null) { TempList.Add("\"entity\":" + Entity); }
                        break;

                    case AdvancementTrigger.cured_zombie_villager:
                        if (Villager != null) { TempList.Add("\"villager\":" + Villager); }
                        if (Zombie != null) { TempList.Add("\"zombie\":" + Zombie); }
                        break;

                    case AdvancementTrigger.effects_changed:
                        if (Effects != null)
                        {
                            List<string> TempEffectList = new List<string>();
                            for (int i = 0; i < Effects.Length; i++)
                            {
                                TempEffectList.Add(Effects[i].ToString());
                            }
                            TempList.Add("\"effects\": {" + string.Join(",", TempEffectList) +"}");
                        }
                        break;

                    case AdvancementTrigger.enchanted_item:
                        if (Item != null) { TempList.Add("\"item\":" + Item); }
                        if (Level != null) { TempList.Add("\"level\":" + Level); }
                        break;

                    case AdvancementTrigger.enter_block:
                        if (Block != null) { TempList.Add("\"block\":" + Block); }
                        break;

                    case AdvancementTrigger.entity_hurt_player:
                        if (Damage != null) { TempList.Add("\"damage\":" + Damage); }
                        break;

                    case AdvancementTrigger.entity_killed_player:
                        if (DamageFlags != null) { TempList.Add("\"killing_blow\":" + DamageFlags); }
                        break;

                    case AdvancementTrigger.inventory_changed:
                        if (EmptySlots != null || FullSlots != null || OccupiedSlots != null)
                        {
                            List<string> TempSlotList = new List<string>();
                            if (EmptySlots != null) { TempSlotList.Add(EmptySlots.JSONString("empty")); }
                            if (FullSlots != null) { TempSlotList.Add(FullSlots.JSONString("full")); }
                            if (OccupiedSlots != null) { TempSlotList.Add(OccupiedSlots.JSONString("occupied")); }
                            TempList.Add("\"slots\": {" + string.Join(",", TempSlotList) + "}");
                        }
                        if (Items != null)
                        {
                            List<string> TempItemList = new List<string>();
                            for (int i = 0; i < Items.Length; i++)
                            {
                                TempItemList.Add(Items[i].ToString());
                            }
                            TempList.Add("\"items\": [" + string.Join(",", TempItemList) + "]");
                        }
                        break;

                    case AdvancementTrigger.item_durability_changed:
                        if (Item != null) { TempList.Add("\"item\":" + Item); }
                        if (Durability != null) { TempList.Add(Durability.JSONString("durability")); }
                        if (DurabilityChange != null) { TempList.Add(DurabilityChange.JSONString("delta")); }
                        break;

                    case AdvancementTrigger.levitation:
                        if (Distance != null) { TempList.Add("\"distance\":" + Distance); }
                        if (Duration != null) { TempList.Add(Duration.JSONString("duration")); }
                        break;

                    case AdvancementTrigger.location:
                        if (Location != null) { TempList.Add(Location.ToString()); }
                        break;

                    case AdvancementTrigger.nether_travel:
                        if (Location != null) { TempList.Add("\"exited\":{" + Location + "}"); }
                        if (StartLocation != null) { TempList.Add("\"entered\":{" + StartLocation + "}"); }
                        if (Distance != null) { TempList.Add("\"distance\":" + Distance); }
                        break;

                    case AdvancementTrigger.placed_block:
                        if (Block != null) { TempList.Add(Block.ToString()); }
                        if (Item != null) { TempList.Add("\"item\":" + Item); }
                        if (Location != null) { TempList.Add("\"location\":{" + Location + "}"); }
                        break;

                    case AdvancementTrigger.player_hurt_entity:
                        if (Entity != null) { TempList.Add("\"entity\":" + Entity); }
                        if (DamageFlags != null) { TempList.Add("\"killing_blow\":" + DamageFlags); }
                        break;

                    case AdvancementTrigger.player_killed_entity:
                        if (Entity != null) { TempList.Add("\"entity\":" + Entity); }
                        break;

                    case AdvancementTrigger.recipe_unlocked:
                        TempList.Add("\"recipe\":\"" + Recipe + "\"");
                        break;

                    case AdvancementTrigger.slept_in_bed:
                        if (Location != null) { TempList.Add(Location.ToString()); }
                        break;

                    case AdvancementTrigger.summoned_entity:
                    case AdvancementTrigger.tame_animal:
                        if (Entity != null) { TempList.Add("\"entity\":" + Entity); }
                        break;

                    case AdvancementTrigger.used_ender_eye:
                        if (EyeDistance != null) { TempList.Add(EyeDistance.JSONString("distance"));}
                        break;

                    case AdvancementTrigger.villager_trade:
                        if (Villager != null) { TempList.Add("\"villager\":" + Villager); }
                        if (Item != null) { TempList.Add("\"item\":" + Item); }
                        break;

                    case AdvancementTrigger.channeled_lightning:
                    case AdvancementTrigger.killed_by_crossbow:
                        if (Entities != null)
                        {
                            List<string> TempEntityStringList = new List<string>();
                            for (int i = 0; i < Entities.Length; i++)
                            {
                                TempEntityStringList.Add(Entities[i].ToString());
                            }
                            TempList.Add("\"victims\":[" + string.Join(",", TempEntityStringList) + "]");
                        }
                        break;
                }

                string TempString = "\"" + Name + "\":{\"trigger\": \"" + TriggerType + "\"";
                if (TempList.Count != 0)
                {
                    TempString += ",\"conditions\": {" + string.Join(",", TempList) + "}"; 
                }
                return TempString + "}";
            }

            /// <summary>
            /// Returns this <see cref="Trigger"/> name
            /// </summary>
            /// <returns>this <see cref="Trigger"/>'s name</returns>
            public override string ToString()
            {
                return Name;
            }
        }

        /// <summary>
        /// An <see cref="Object"/> defining the needed triggers to get the <see cref="Advancement"/>
        /// </summary>
        public class Requirement
        {
            internal string RequirementString = "";
            internal List<Trigger> UsedTriggers = new List<Trigger>();

            /// <summary>
            /// Creates a <see cref="Requirement"/> needing one <see cref="Trigger"/>
            /// </summary>
            /// <param name="requiredTrigger">the <see cref="Trigger"/></param>
            public Requirement(Trigger requiredTrigger)
            {
                if (string.IsNullOrWhiteSpace(requiredTrigger.ToString()))
                {
                    requiredTrigger.Name = "gen_0";
                }

                RequirementString = "[";
                RequirementString += "\"" + requiredTrigger + "\"]";
                UsedTriggers.Add(requiredTrigger);
            }

            /// <summary>
            /// Creates a new <see cref="Requirement"/> with the given <see cref="Trigger"/>s
            /// </summary>
            /// <param name="requiredTriggers">the <see cref="Trigger"/>s</param>
            /// <param name="and">true if the player should trigger all <see cref="Trigger"/>s for this <see cref="Requirement"/>. False if only one <see cref="Trigger"/> is needed</param>
            public Requirement(Trigger[] requiredTriggers, bool and = true)
            {
                RequirementString = "[";
                string[] TempArray = new string[requiredTriggers.Length];
                for (int i = 0; i < requiredTriggers.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(requiredTriggers[i].ToString()))
                    {
                        requiredTriggers[i].Name = "Gen_" + i;
                    }

                    TempArray[i] = "\"" + requiredTriggers[i].ToString() + "\"";
                    UsedTriggers.Add(requiredTriggers[i]);
                }
                if (and) { RequirementString += string.Join("],[", TempArray) + "]"; }
                else {RequirementString += string.Join(",", TempArray) + "]";}
            }

            /// <summary>
            /// Creates a new <see cref="Requirement"/> with the given <see cref="Requirement"/>s
            /// </summary>
            /// <param name="requiredRequirements">the <see cref="Requirement"/>s</param>
            /// <param name="and">true if the player should trigger all <see cref="Requirement"/>s for this <see cref="Requirement"/>. False if only one <see cref="Requirement"/> is needed</param>
            public Requirement(Requirement[] requiredRequirements, bool and = true)
            {
                string[] TempArray = new string[requiredRequirements.Length];
                for (int i = 0; i < requiredRequirements.Length; i++)
                {
                    TempArray[i] = requiredRequirements[i].ToString().Replace("gen" ,"gen_" + i);
                    for (int j = 0; j < requiredRequirements[i].UsedTriggers.Count; j++)
                    {
                        requiredRequirements[i].UsedTriggers[j].Name = requiredRequirements[i].UsedTriggers[j].Name.Replace("gen", "gen_" + i);
                    }
                    UsedTriggers.AddRange(requiredRequirements[i].UsedTriggers);
                }
                if (and) { RequirementString += string.Join(",", TempArray); }
                else { RequirementString += string.Join(",", TempArray); }
            }

            /// <summary>
            /// Outputs this <see cref="Requirement"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Requirement"/>'s data</returns>
            public override string ToString()
            {
                return RequirementString;
            }
        }

        /// <summary>
        /// an <see cref="Object"/> defining what rewards an <see cref="Advancement"/> gives
        /// </summary>
        public class Reward
        {
            private readonly Function[] FunctionRewards;
            private readonly Loottable[] LoottableRewards;
            private readonly Recipe[] RecipeRewards;
            private readonly int? XPReward;

            /// <summary>
            /// Creates a new <see cref="Reward"/> which runs a <see cref="Function"/>
            /// </summary>
            /// <param name="RunFunction">the <see cref="Function"/> to run</param>
            public Reward(Function RunFunction)
            {
                FunctionRewards = new Function[] { RunFunction };
            }

            /// <summary>
            /// Creates a new <see cref="Reward"/> which gives the player xp
            /// </summary>
            /// <param name="xp">the amount of xp to give</param>
            public Reward(int xp)
            {
                XPReward = xp;
            }

            /// <summary>
            /// Creates a new <see cref="Reward"/> which gives a <see cref="Loottable"/>
            /// </summary>
            /// <param name="GiveLoottable">the <see cref="Loottable"/> to give</param>
            public Reward(Loottable GiveLoottable)
            {
                LoottableRewards = new Loottable[] { GiveLoottable };
            }

            /// <summary>
            /// Creates a new <see cref="Reward"/> which gives the player a <see cref="Recipe"/>
            /// </summary>
            /// <param name="GiveRecipe">the <see cref="Recipe"/> to give</param>
            public Reward(Recipe GiveRecipe)
            {
                RecipeRewards = new Recipe[] { GiveRecipe };
            }

            /// <summary>
            /// Outputs this <see cref="Reward"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Reward"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (XPReward != null) { TempList.Add("\"experience\":" + XPReward); }
                if (FunctionRewards != null)
                {
                    List<string> FuncTempList = new List<string>();
                    for (int i = 0; i < FunctionRewards.Length; i++)
                    {
                        FuncTempList.Add(FunctionRewards[i].ToString());
                    }
                    TempList.Add("\"function\": \"" + string.Join("\",\"", FuncTempList) + "\"");
                }
                if (LoottableRewards != null)
                {
                    List<string> LootTempList = new List<string>();
                    for (int i = 0; i < LoottableRewards.Length; i++)
                    {
                        LootTempList.Add(LoottableRewards[i].ToString());
                    }
                    TempList.Add("\"loot\": [\"" + string.Join("\",\"", LootTempList) + "\"]");
                }
                if (RecipeRewards != null)
                {
                    List<string> ReciTempList = new List<string>();
                    for (int i = 0; i < RecipeRewards.Length; i++)
                    {
                        ReciTempList.Add(RecipeRewards[i].ToString());
                    }
                    TempList.Add("\"recipes\": [\"" + string.Join("\",\"", ReciTempList) + "\"]");
                }

                return "\"rewards\":{" + string.Join(",", TempList) + "}";
            }

            /// <summary>
            /// Converts a <see cref="Function"/> into a reward
            /// </summary>
            /// <param name="function">the <see cref="Function"/> to convert</param>
            public static implicit operator Reward(Function function)
            {
                return new Reward(function);
            }

            /// <summary>
            /// Converts a <see cref="Loottable"/> into a reward
            /// </summary>
            /// <param name="loottable">the <see cref="Loottable"/> to convert</param>
            public static implicit operator Reward(Loottable loottable)
            {
                return new Reward(loottable);
            }

            /// <summary>
            /// Converts a <see cref="Recipe"/> into a reward
            /// </summary>
            /// <param name="recipe">the <see cref="Recipe"/> to convert</param>
            public static implicit operator Reward(Recipe recipe)
            {
                return new Reward(recipe);
            }
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Advancement"/>
        /// </summary>
        /// <returns>this <see cref="Advancement"/>'s name</returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
