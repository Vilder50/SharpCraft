﻿using System;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// An <see cref="object"/> used to define a namespace in a datapack
    /// </summary>
    public class PackNamespace
    {
        string _name;
        Datapack _datapack;
        int _nextFileID;

        /// <summary>
        /// Creates a new namespace in a datapack
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public PackNamespace(Datapack datapack, string namespaceName)
        {
            Name = namespaceName;
            Datapack = datapack;
            Directory.CreateDirectory(datapack.WorldPath + "\\datapacks\\" + datapack.PackName + "\\data\\" + Name);
        }

        /// <summary>
        /// The name the next unamed file will get
        /// </summary>
        public int NextFileID
        {
            get
            {
                _nextFileID++;
                return _nextFileID;
            }
        }

        /// <summary>
        /// The name of this namespace
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Name) + " may not be null or empty");
                }

                _name = value.ToLower();
            }
        }
        /// <summary>
        /// The datapack this namespace is a part of
        /// </summary>
        public Datapack Datapack
        {
            get
            {
                return _datapack;
            }

            private set
            {
                if (value is null)
                {
                    throw new ArgumentException(nameof(Datapack) + " may not be null");
                }
                _datapack = value;
            }
        }

        /// <summary>
        /// Creates a new function with the given name
        /// </summary>
        /// <param name="functionName">The function's name. If null will get random name</param>
        /// <returns>The newly created function</returns>
        public Function NewFunction(string functionName = null)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                return new Function(this, null);
            }
            else
            {
                return new Function(this, functionName.ToLower().Replace("/", "\\"));
            }
        }

        /// <summary>
        /// Creates a new function with the given name and add commands to the function
        /// </summary>
        /// <param name="functionName">The function's name</param>
        /// <param name="creater">a method creating the function</param>
        /// <returns>The newly created function</returns>
        public Function NewFunction(string functionName, Function.FunctionCreater creater)
        {
            Function function = NewFunction(functionName);
            creater(function);

            return function;
        }

        /// <summary>
        /// Creates a new randomly named function with the commands to the function
        /// </summary>
        /// <param name="creater">a method creating the function</param>
        /// <returns>The newly created function</returns>
        public Function NewFunction(Function.FunctionCreater creater)
        {
            Function function = NewFunction();
            creater(function);

            return function;
        }

        /// <summary>
        /// Creates a new crafting table <see cref="Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        /// <param name="recipe">A multidimensional array describing how the <see cref="Item"/>s should be layed out in the crafting table</param>
        /// <param name="output">The output <see cref="Item"/></param>
        /// <param name="group">The string id of the group this <see cref="Recipe"/> is in</param>
        /// <returns>The newly created recipe</returns>
        public Recipe NewRecipe(string name, Item[,] recipe, Item output, string group = null)
        {
            name = name.Replace("/", "\\");
            return new Recipe(this, name.ToLower(), recipe, output, group);
        }

        /// <summary>
        /// Creates a new crafting table shapeless <see cref="Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        /// <param name="recipe">The <see cref="Item"/>s needed to craft the <see cref="Recipe"/></param>
        /// <param name="output">The output <see cref="Item"/></param>
        /// <param name="group">The string id of the group this <see cref="Recipe"/> is in</param>
        /// <returns>The newly created recipe</returns>
        public Recipe NewRecipe(string name, Item[] recipe, Item output, string group = null)
        {
            name = name.Replace("/", "\\");
            return new Recipe(this, name.ToLower(), recipe, output, group);
        }

        /// <summary>
        /// Creates a new furnace/a type of furnace <see cref="Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        /// <param name="input">The input <see cref="Item"/></param>
        /// <param name="output">the output <see cref="Item"/></param>
        /// <param name="xpDrop">the amount of xp the <see cref="Recipe"/> should output</param>
        /// <param name="cookTime">the amount of time the <see cref="Recipe"/> takes</param>
        /// <param name="recipeType">The type of smelt recipe</param>
        /// <returns>The newly created recipe</returns>
        public Recipe NewRecipe(string name, Item input, ID.Item output, double xpDrop, ID.SmeltType recipeType, int cookTime = 200)
        {
            name = name.Replace("/", "\\");
            return new Recipe(this, name.ToLower(), input, output, xpDrop, cookTime, recipeType);
        }

        /// <summary>
        /// Creates a new stonecutter <see cref="Recipe"/>
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        /// <param name="input">The input <see cref="Item"/></param>
        /// <param name="output">the output <see cref="Item"/></param>
        /// <returns>The newly created <see cref="Recipe"/></returns>
        public Recipe NewRecipe(string name, Item input, Item output)
        {
            name = name.Replace("/", "\\");
            return new Recipe(this, name.ToLower(), input, output);
        }

        /// <summary>
        /// Overwrites the <see cref="Recipe"/> with that name with an invalid <see cref="Recipe"/>
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        public void HideRecipe(string name)
        {
            name = name.Replace("/", "\\");
            new Recipe(this, name);
        }

        /// <summary>
        /// Creates a new <see cref="Loottable"/> with the <paramref name="lootPools"/>
        /// </summary>
        /// <param name="tableName">The <see cref="Loottable"/>'s name</param>
        /// <param name="lootPools">The <see cref="Loottable.Pool"/>s in the <see cref="Loottable"/></param>
        /// <returns>The newly created <see cref="Recipe"/></returns>
        public Loottable NewLoottable(string tableName, Loottable.Pool[] lootPools)
        {
            tableName = tableName.Replace("/", "\\");

            if (tableName.Contains("\\"))
            {
                Directory.CreateDirectory(Datapack.GetDataPath() + Name + "\\loot_tables\\" + tableName.ToLower().Substring(0, tableName.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(Datapack.GetDataPath() + Name + "\\loot_tables\\");
            }
            return new Loottable(this, tableName.ToLower(), lootPools);
        }

        /// <summary>
        /// Creates a new <see cref="Advancement"/> with the given parameters
        /// </summary>
        /// <param name="advancementName">The <see cref="Advancement"/>'s name</param>
        /// <param name="ingameName">the shown ingame name</param>
        /// <param name="description">the shown ingame description</param>
        /// <param name="icon">the icon for the <see cref="Advancement"/> - Leave empty to make advancement invisible</param>
        /// <param name="parent">the <see cref="Advancement"/>'s parent <see cref="Advancement"/></param>
        /// <param name="requirement">the <see cref="Advancement.Requirement"/> needed to get the <see cref="Advancement"/></param>
        /// <param name="reward">the <see cref="Advancement.Reward"/> given by getting the <see cref="Advancement"/></param>
        /// <param name="frame">the frame</param>
        /// <param name="showToast">if a toast should be shown when the player gets the <see cref="Advancement"/></param>
        /// <param name="chatAnnounce">if it should be announced to chat when the player gets the <see cref="Advancement"/></param>
        /// <param name="hidden">if the <see cref="Advancement"/> shouldn't be shown in the advancement menu before you get it</param>
        /// <returns>the newly created <see cref="Advancement"/></returns>
        public Advancement NewAdvancement(string advancementName, JSON[] ingameName, JSON[] description, JSONObjects.Item icon, Advancement parent, Advancement.Requirement requirement, Advancement.Reward reward = null, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool showToast = true, bool chatAnnounce = true, bool hidden = false)
        {
            advancementName = advancementName.Replace("/", "\\");
            return new Advancement(this, advancementName.ToLower(), ingameName, description, icon, parent, requirement, reward, frame, showToast, chatAnnounce, hidden);
        }

        /// <summary>
        /// Creates a new <see cref="Advancement"/> with the given parameters
        /// </summary>
        /// <param name="advancementName">The <see cref="Advancement"/>'s name</param>
        /// <param name="ingameName">the shown ingame name</param>
        /// <param name="description">the shown ingame description</param>
        /// <param name="icon">the icon for the <see cref="Advancement"/> - Leave empty to make advancement invisible</param>
        /// <param name="background">the background shown in the advancement menu</param>
        /// <param name="requirement">the <see cref="Advancement.Requirement"/> needed to get the <see cref="Advancement"/></param>
        /// <param name="reward">the <see cref="Advancement.Reward"/> given by getting the <see cref="Advancement"/></param>
        /// <param name="frame">the frame</param>
        /// <param name="showToast">if a toast should be shown when the player gets the <see cref="Advancement"/></param>
        /// <param name="chatAnnounce">if it should be announced to chat when the player gets the <see cref="Advancement"/></param>
        /// <param name="hidden">if the <see cref="Advancement"/> shouldn't be shown in the advancement menu before you get it</param>
        /// <returns>the newly created <see cref="Advancement"/></returns>
        public Advancement NewAdvancement(string advancementName, JSON[] ingameName, JSON[] description, JSONObjects.Item icon, string background, Advancement.Requirement requirement, Advancement.Reward reward = null, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool showToast = true, bool chatAnnounce = true, bool hidden = false)
        {
            advancementName = advancementName.Replace("/", "\\");
            return new Advancement(this, advancementName.ToLower(), ingameName, description, icon, background, requirement, reward, frame, showToast, chatAnnounce, hidden);
        }

        /// <summary>
        /// Overwrites the <see cref="Advancement"/> with that name with an invalid <see cref="Advancement"/>
        /// </summary>
        /// <param name="advancementName">The <see cref="Advancement"/>'s name</param>
        public void HideAdvancement(string advancementName)
        {
            advancementName = advancementName.Replace("/", "\\");
            new Advancement(this, advancementName);
        }

        /// <summary>
        /// Creates a new <see cref="Function"/> <see cref="Group"/>
        /// </summary>
        /// <param name="GroupName">the <see cref="Group"/> name</param>
        /// <param name="FunctionList">a <see cref="Function"/> array containing the <see cref="Group"/>'s <see cref="Function"/>s</param>
        /// <param name="Replace">true if this <see cref="Group"/> should override other <see cref="Group"/>s in the same namespace with the same name</param>
        /// <param name="InsertGroups">a <see cref="Group"/> array containing <see cref="Group"/>s to add to this <see cref="Group"/></param>
        /// <returns>The newly created <see cref="Group"/></returns>
        public Group NewGroup(string GroupName, Function[] FunctionList, bool Replace = false, Group[] InsertGroups = null)
        {
            return new Group(this, GroupName.ToLower().Replace("/", "\\"), CreateFunctionGroupList(FunctionList, InsertGroups), Replace, 0);
        }

        /// <summary>
        /// Creates a new <see cref="Block"/> <see cref="Group"/>
        /// </summary>
        /// <param name="GroupName">the <see cref="Group"/> name</param>
        /// <param name="BlockList">a <see cref="Block"/> array containing the <see cref="Group"/>'s <see cref="Block"/>s</param>
        /// <param name="Replace">true if this <see cref="Group"/> should override other <see cref="Group"/>s in the same namespace with the same name</param>
        /// <param name="InsertGroups">a <see cref="Group"/> array containing <see cref="Group"/>s to add to this <see cref="Group"/></param>
        /// <returns>The newly created <see cref="Group"/></returns>
        public Group NewGroup(string GroupName, ID.Block[] BlockList, bool Replace = false, Group[] InsertGroups = null)
        {
            int Start = 0;
            string[] ToString;
            if (InsertGroups == null)
            {
                ToString = new string[BlockList.Length];
            }
            else
            {
                ToString = new string[BlockList.Length + InsertGroups.Length];
                for (int i = 0; i < InsertGroups.Length; i++)
                {
                    ToString[i] = "#" + InsertGroups[i].ToString();
                }
                Start = InsertGroups.Length;
            }
            for (int i = Start; i < BlockList.Length; i++)
            {
                ToString[i] = BlockList[i].ToString();
            }
            return new Group(this, GroupName.ToLower().Replace("/", "\\"), ToString, Replace, 1);
        }

        /// <summary>
        /// Creates a new <see cref="Item"/> <see cref="Group"/>
        /// </summary>
        /// <param name="GroupName">the <see cref="Group"/> name</param>
        /// <param name="ItemList">a <see cref="Item"/> array containing the <see cref="Group"/>'s <see cref="Item"/>s</param>
        /// <param name="Replace">true if this <see cref="Group"/> should override other <see cref="Group"/>s in the same namespace with the same name</param>
        /// <param name="InsertGroups">a <see cref="Group"/> array containing <see cref="Group"/>s to add to this <see cref="Group"/></param>
        /// <returns>The newly created <see cref="Group"/></returns>
        public Group NewGroup(string GroupName, ID.Item[] ItemList, bool Replace = false, Group[] InsertGroups = null)
        {
            int Start = 0;
            string[] ToString;
            if (InsertGroups == null)
            {
                ToString = new string[ItemList.Length];
            }
            else
            {
                ToString = new string[ItemList.Length + InsertGroups.Length];
                for (int i = 0; i < InsertGroups.Length; i++)
                {
                    ToString[i] = "#" + InsertGroups[i].ToString();
                }
                Start = InsertGroups.Length;
            }
            for (int i = Start; i < ItemList.Length; i++)
            {
                ToString[i] = ItemList[i].MinecraftValue();
            }
            return new Group(this, GroupName.ToLower().Replace("/", "\\"), ToString, Replace, 2);
        }

        /// <summary>
        /// Creates a new <see cref="Entity"/> <see cref="Group"/>
        /// </summary>
        /// <param name="GroupName">the <see cref="Group"/> name</param>
        /// <param name="EntityList">a <see cref="Entity"/> array containing the <see cref="Group"/>'s <see cref="Entity"/>s</param>
        /// <param name="Replace">true if this <see cref="Group"/> should override other <see cref="Group"/>s in the same namespace with the same name</param>
        /// <param name="InsertGroups">a <see cref="Group"/> array containing <see cref="Group"/>s to add to this <see cref="Group"/></param>
        /// <returns>The newly created <see cref="Group"/></returns>
        public Group NewGroup(string GroupName, ID.Entity[] EntityList, bool Replace = false, Group[] InsertGroups = null)
        {
            int Start = 0;
            string[] ToString;
            if (InsertGroups == null)
            {
                ToString = new string[EntityList.Length];
            }
            else
            {
                ToString = new string[EntityList.Length + InsertGroups.Length];
                for (int i = 0; i < InsertGroups.Length; i++)
                {
                    ToString[i] = "#" + InsertGroups[i].ToString();
                }
                Start = InsertGroups.Length;
            }
            for (int i = Start; i < EntityList.Length; i++)
            {
                ToString[i] = EntityList[i].ToString();
            }
            return new Group(this, GroupName.ToLower().Replace("/", "\\"), ToString, Replace, 3);
        }

        private string[] CreateFunctionGroupList(Function[] FunctionList, Group[] InsertGroups = null)
        {
            int Start = 0;
            string[] ToString;
            if (InsertGroups == null)
            {
                ToString = new string[FunctionList.Length];
            }
            else
            {
                ToString = new string[FunctionList.Length + InsertGroups.Length];
                for (int i = 0; i < InsertGroups.Length; i++)
                {
                    ToString[i] = "#" + InsertGroups[i].ToString();
                }
                Start = InsertGroups.Length;
            }
            for (int i = Start; i < FunctionList.Length; i++)
            {
                ToString[i] = FunctionList[i].ToString();
            }

            return ToString;
        }
    }
}