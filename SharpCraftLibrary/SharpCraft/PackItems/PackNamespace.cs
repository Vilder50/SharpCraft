using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// An <see cref="object"/> used to define a namespace in a datapack
    /// </summary>
    public class PackNamespace : BasePackNamespace
    {
        int _nextFileID;

        /// <summary>
        /// Intializes a new namespace. Make sure to call <see cref="BasePackNamespace.Setup(BaseDatapack, string)"/> after using this
        /// </summary>
        public PackNamespace() : base()
        {
            
        }

        /// <summary>
        /// Creates a new namespace in a datapack
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public PackNamespace(BaseDatapack datapack, string namespaceName) : base(datapack, namespaceName)
        {
            Directory.CreateDirectory(datapack.GetDataPath() + Name);
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

            Function returnFunction = GetFile<Function>(functionName);
            if (returnFunction is null)
            {
                return new Function(this, functionName);
            }
            else
            {
                return returnFunction;
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
            Recipe returnRecipe = GetFile<Recipe>(name);

            if (returnRecipe is null)
            {
                return new Recipe(this, name, recipe, output, group);
            }
            else
            {
                return returnRecipe;
            }
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
            Recipe returnRecipe = GetFile<Recipe>(name);

            if (returnRecipe is null)
            {
                return new Recipe(this, name, recipe, output, group);
            }
            else
            {
                return returnRecipe;
            }
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
            Recipe returnRecipe = GetFile<Recipe>(name);

            if (returnRecipe is null)
            {
                return new Recipe(this, name, input, output, xpDrop, cookTime, recipeType);
            }
            else
            {
                return returnRecipe;
            }
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
            Recipe returnRecipe = GetFile<Recipe>(name);

            if (returnRecipe is null)
            {
                return new Recipe(this, name, input, output);
            }
            else
            {
                return returnRecipe;
            }
        }

        /// <summary>
        /// Overwrites the <see cref="Recipe"/> with that name with an invalid <see cref="Recipe"/>
        /// </summary>
        /// <param name="name">The <see cref="Recipe"/>'s name</param>
        public void HideRecipe(string name)
        {
            Recipe returnRecipe = GetFile<Recipe>(name);

            if (returnRecipe is null)
            {
                new Recipe(this, name).Dispose();
            }
            else
            {
                throw new InvalidOperationException("Cannot hide recipe since it is in use in this namespace");
            }
        }

        /// <summary>
        /// Creates a new <see cref="Loottable"/> with the <paramref name="lootPools"/>
        /// </summary>
        /// <param name="tableName">The <see cref="Loottable"/>'s name</param>
        /// <param name="lootPools">The <see cref="Loottable.Pool"/>s in the <see cref="Loottable"/></param>
        /// <returns>The newly created <see cref="Recipe"/></returns>
        public Loottable NewLoottable(string tableName, Loottable.Pool[] lootPools)
        {
            Loottable returnTable = GetFile<Loottable>(tableName);

            if (returnTable is null)
            {
                return new Loottable(this, tableName.ToLower(), lootPools);
            }
            else
            {
                return returnTable;
            }
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
            Advancement returnAdvancement = GetFile<Advancement>(advancementName);

            if (returnAdvancement is null)
            {
                return new Advancement(this, advancementName.ToLower(), ingameName, description, icon, parent, requirement, reward, frame, showToast, chatAnnounce, hidden);
            }
            else
            {
                return returnAdvancement;
            }
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
            Advancement returnAdvancement = GetFile<Advancement>(advancementName);

            if (returnAdvancement is null)
            {
                return new Advancement(this, advancementName.ToLower(), ingameName, description, icon, background, requirement, reward, frame, showToast, chatAnnounce, hidden);
            }
            else
            {
                return returnAdvancement;
            }
        }

        /// <summary>
        /// Overwrites the <see cref="Advancement"/> with that name with an invalid <see cref="Advancement"/>
        /// </summary>
        /// <param name="advancementName">The <see cref="Advancement"/>'s name</param>
        public void HideAdvancement(string advancementName)
        {
            Advancement returnAdvancement = GetFile<Advancement>(advancementName);

            if (returnAdvancement is null)
            {
                new Advancement(this, advancementName).Dispose();
            }
            else
            {
                throw new InvalidOperationException("Cannot hide advancement since it's getting used inside the namespace");
            }
        }

        /// <summary>
        /// Returns a <see cref="FunctionGroup"/> with the given functions
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="functionList">The functions in the group</param>
        /// <param name="append">If the functions should be appended to existing functions from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="FunctionGroup"/></returns>
        public FunctionGroup Group(string name, List<IFunction> functionList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            FunctionGroup existingFile = GetFile<FunctionGroup>(name.Replace("/","\\"));
            if (!(existingFile is null))
            {
                ThrowExceptionOnGroupStacking(existingFile, append, writeSetting);

                existingFile.Items.AddRange(functionList);
                return existingFile;
            }
            else
            {
                return new FunctionGroup(this, name, functionList, append, writeSetting);
            }
        }

        /// <summary>
        /// Returns a <see cref="BlockGroup"/> with the given <see cref="BlockType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="blockList">The <see cref="BlockType"/>s in the group</param>
        /// <param name="append">If the <see cref="BlockType"/>s should be appended to existing <see cref="BlockType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="BlockGroup"/></returns>
        public BlockGroup Group(string name, List<BlockType> blockList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            BlockGroup existingFile = GetFile<BlockGroup>(name.Replace("/", "\\"));
            if (!(existingFile is null))
            {
                ThrowExceptionOnGroupStacking(existingFile, append, writeSetting);

                existingFile.Items.AddRange(blockList);
                return existingFile;
            }
            else
            {
                return new BlockGroup(this, name, blockList, append, writeSetting);
            }
        }

        /// <summary>
        /// Returns a <see cref="ItemGroup"/> with the given <see cref="ItemType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="itemList">The <see cref="ItemType"/>s in the group</param>
        /// <param name="append">If the <see cref="ItemType"/>s should be appended to existing <see cref="ItemType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="ItemGroup"/></returns>
        public ItemGroup Group(string name, List<ItemType> itemList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            ItemGroup existingFile = GetFile<ItemGroup>(name.Replace("/", "\\"));
            if (!(existingFile is null))
            {
                ThrowExceptionOnGroupStacking(existingFile, append, writeSetting);

                existingFile.Items.AddRange(itemList);
                return existingFile;
            }
            else
            {
                return new ItemGroup(this, name, itemList, append, writeSetting);
            }
        }

        /// <summary>
        /// Returns a <see cref="EntityGroup"/> with the given <see cref="EntityType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="entityList">The <see cref="EntityType"/>s in the group</param>
        /// <param name="append">If the <see cref="EntityType"/>s should be appended to existing <see cref="EntityType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="EntityGroup"/></returns>
        public EntityGroup Group(string name, List<EntityType> entityList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            EntityGroup existingFile = GetFile<EntityGroup>(name.Replace("/", "\\"));
            if (!(existingFile is null))
            {
                ThrowExceptionOnGroupStacking(existingFile, append, writeSetting);

                existingFile.Items.AddRange(entityList);
                return existingFile;
            }
            else
            {
                return new EntityGroup(this, name, entityList, append, writeSetting);
            }
        }

        /// <summary>
        /// Returns a <see cref="LiquidGroup"/> with the given <see cref="LiquidType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="liquidList">The <see cref="LiquidType"/>s in the group</param>
        /// <param name="append">If the <see cref="LiquidType"/>s should be appended to existing <see cref="LiquidType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="LiquidGroup"/></returns>
        public LiquidGroup Group(string name, List<LiquidType> liquidList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            LiquidGroup existingFile = GetFile<LiquidGroup>(name.Replace("/", "\\"));
            if (!(existingFile is null))
            {
                ThrowExceptionOnGroupStacking(existingFile, append, writeSetting);

                existingFile.Items.AddRange(liquidList);
                return existingFile;
            }
            else
            {
                return new LiquidGroup(this, name, liquidList, append, writeSetting);
            }
        }

        private void ThrowExceptionOnGroupStacking<TItem>(BaseGroup<TItem> existingFile, bool append, BaseFile.WriteSetting writeSetting) where TItem : IGroupable
        {
            if (existingFile.Disposed)
            {
                throw new ArgumentException("Cannot get file since there already exists a file with the same name which has finished writing.");
            }

            if (existingFile.Setting != writeSetting)
            {
                throw new ArgumentException("Cannot get file since there already exists a file with the same name which has a different write setting. (Setting: " + existingFile.Setting + ")");
            }

            if (existingFile.AppendGroup != append)
            {
                throw new ArgumentException("Cannot get file since there already exists a file with the same name which has a different append setting. (Setting: " + existingFile.AppendGroup + ")");
            }
        }

        /// <summary>
        /// Adds the given setting to this namespace
        /// </summary>
        /// <param name="setting">The setting to add</param>
        public void AddSetting(INamespaceSetting setting)
        {
            settings.Add(setting);
        }
    }
}
