using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.LootObjects;
using SharpCraft.AdvancementObjects;

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
            Datapack.FileCreator.CreateDirectory(Datapack.GetDataPath() + Name);
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
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function Function(string functionName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                return new Function(this, null, setting);
            }

            Function returnFunction = (Function)GetFile("function", functionName);
            if (returnFunction is null)
            {
                return new Function(this, functionName, setting);
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
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function Function(string functionName, Function.FunctionCreater creater, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function function = Function(functionName, setting);
            creater(function);

            return function;
        }

        /// <summary>
        /// Creates a new randomly named function with the commands to the function
        /// </summary>
        /// <param name="creater">a method creating the function</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function Function(Function.FunctionCreater creater, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function function = Function((string)null, setting);
            creater(function);

            return function;
        }

        /// <summary>
        /// Creates a new crafting table <see cref="SharpCraft.Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="SharpCraft.Recipe"/>'s name</param>
        /// <param name="recipe">A multidimensional array describing how the <see cref="Item"/>s should be layed out in the crafting table</param>
        /// <param name="output">The output <see cref="Item"/></param>
        /// <param name="group">The string id of the group this <see cref="SharpCraft.Recipe"/> is in</param>
        /// <returns>The newly created recipe</returns>
        public Recipe Recipe(string name, Item[,] recipe, Item output, string group = null)
        {
            Recipe returnRecipe = (Recipe)GetFile("recipe", name);

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
        /// Creates a new crafting table shapeless <see cref="SharpCraft.Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="SharpCraft.Recipe"/>'s name</param>
        /// <param name="recipe">The <see cref="Item"/>s needed to craft the <see cref="SharpCraft.Recipe"/></param>
        /// <param name="output">The output <see cref="Item"/></param>
        /// <param name="group">The string id of the group this <see cref="SharpCraft.Recipe"/> is in</param>
        /// <returns>The newly created recipe</returns>
        public Recipe Recipe(string name, Item[] recipe, Item output, string group = null)
        {
            Recipe returnRecipe = (Recipe)GetFile("recipe", name);

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
        /// Creates a new furnace/a type of furnace <see cref="SharpCraft.Recipe"/> with the given parameters
        /// </summary>
        /// <param name="name">The <see cref="SharpCraft.Recipe"/>'s name</param>
        /// <param name="input">The input <see cref="Item"/></param>
        /// <param name="output">the output <see cref="Item"/></param>
        /// <param name="xpDrop">the amount of xp the <see cref="SharpCraft.Recipe"/> should output</param>
        /// <param name="cookTime">the amount of time the <see cref="SharpCraft.Recipe"/> takes</param>
        /// <param name="recipeType">The type of smelt recipe</param>
        /// <returns>The newly created recipe</returns>
        public Recipe Recipe(string name, Item input, ID.Item output, double xpDrop, ID.SmeltType recipeType, int cookTime = 200)
        {
            Recipe returnRecipe = (Recipe)GetFile("recipe", name);

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
        /// Creates a new stonecutter <see cref="SharpCraft.Recipe"/>
        /// </summary>
        /// <param name="name">The <see cref="SharpCraft.Recipe"/>'s name</param>
        /// <param name="input">The input <see cref="Item"/></param>
        /// <param name="output">the output <see cref="Item"/></param>
        /// <returns>The newly created <see cref="SharpCraft.Recipe"/></returns>
        public Recipe Recipe(string name, Item input, Item output)
        {
            Recipe returnRecipe = (Recipe)GetFile("recipe", name);

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
        /// Overwrites the <see cref="SharpCraft.Recipe"/> with that name with an invalid <see cref="SharpCraft.Recipe"/>
        /// </summary>
        /// <param name="name">The <see cref="SharpCraft.Recipe"/>'s name</param>
        public void HideRecipe(string name)
        {
            Recipe returnRecipe = (Recipe)GetFile("recipe", name);

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
        /// Creates a new <see cref="LootTable"/> with the <paramref name="lootPools"/>
        /// </summary>
        /// <param name="tableName">The <see cref="LootTable"/>'s name</param>
        /// <param name="lootPools">The <see cref="LootPool"/>s in the <see cref="LootTable"/></param>
        /// <param name="type">The type of loot table</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The newly created <see cref="SharpCraft.Recipe"/></returns>
        public LootTable Loottable(string tableName, LootPool[] lootPools, LootTable.TableType? type = null, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            LootTable existingFile = (LootTable)GetFile("loot_table", tableName);

            if (!(existingFile is null))
            {
                if (existingFile.IsAuto())
                {
                    throw new ArgumentException("There already exists a table using the given name. That table is an auto table and cannot be changed.");
                }
                if (existingFile.Setting != writeSetting)
                {
                    throw new ArgumentException("There already exists a table using the given name. That table uses the write setting " + existingFile.Setting);
                }
                if (existingFile.Type != type)
                {
                    throw new ArgumentException("There already exists a table using the given name. That table is of the type " + existingFile.Type);
                }

                existingFile.Pools.AddRange(lootPools);
                return existingFile;
            }
            else
            {
                return new LootTable(this, tableName, lootPools, type, writeSetting);
            }
        }

        /// <summary>
        /// Creates a new parent advancement with the given parameters
        /// </summary>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <param name="announceInChat">True if when the advancement is unlocked it will be announced in chat. False if not</param>
        /// <param name="description">The description on the advancement</param>
        /// <param name="frame">The frame around the icon</param>
        /// <param name="hidden">True if the advancement can't be seen unless it has been unlocked</param>
        /// <param name="icon">The icon on the advancement</param>
        /// <param name="name">The shown advancement name</param>
        /// <param name="showToast">True if when the advancement is unlocked it will display a toast in the top right corner. False if not</param>
        /// <param name="background">The background in the advancement gui. Example: minecraft:textures/gui/advancements/backgrounds/end.png.</param>
        /// <returns>The advancement</returns>
        public ParentAdvancement Advancement(string fileName, Requirement[] requirements, Reward reward, JSON[] name, JSON[] description, Item icon, string background, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement existingFile = (BaseAdvancement)GetFile("advancement", fileName);
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileName + ". Use GetFile(\"advancement\",\""+ existingFile.FileName + "\") to get it.");
            }

            return new ParentAdvancement(this, fileName, requirements, reward, name, description, icon, background, frame, announceInChat, showToast, hidden, writeSetting);
        }

        /// <summary>
        /// Creates a new child advancement with the given parameters
        /// </summary>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <param name="announceInChat">True if when the advancement is unlocked it will be announced in chat. False if not</param>
        /// <param name="description">The description on the advancement</param>
        /// <param name="frame">The frame around the icon</param>
        /// <param name="hidden">True if the advancement can't be seen unless it has been unlocked</param>
        /// <param name="icon">The icon on the advancement</param>
        /// <param name="name">The shown advancement name</param>
        /// <param name="showToast">True if when the advancement is unlocked it will display a toast in the top right corner. False if not</param>
        /// <param name="parent">The parent advancement</param>
        /// <returns>The advancement</returns>
        public ChildAdvancement Advancement(string fileName, IAdvancement parent, Requirement[] requirements, Reward reward, JSON[] name, JSON[] description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement existingFile = (BaseAdvancement)GetFile("advancement", fileName);
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileName + ". Use GetFile(\"advancement\",\"" + existingFile.FileName + "\") to get it.");
            }

            return new ChildAdvancement(this, fileName, parent, requirements, reward, name, description, icon, frame, announceInChat, showToast, hidden, writeSetting);
        }

        /// <summary>
        /// Creates a new hidden advancement (Advancement is not visible in the advancement gui)
        /// </summary>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <returns>The advancement</returns>
        public HiddenAdvancement Advancement(string fileName, Requirement[] requirements, Reward reward, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement existingFile = (BaseAdvancement)GetFile("advancement", fileName);
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileName + ". Use GetFile(\"advancement\",\"" + existingFile.FileName + "\") to get it.");
            }

            return new HiddenAdvancement(this, fileName, requirements, reward, writeSetting);
        }

        /// <summary>
        /// Makes the advancement with the file name invalid... which also makes all its children invalid
        /// </summary>
        /// <param name="fileName">The name of the file to make invalid</param>
        public void Advancement(string fileName)
        {
            BaseAdvancement existingFile = (BaseAdvancement)GetFile("advancement", fileName);
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileName + ". Use GetFile(\"advancement\",\"" + existingFile.FileName + "\") to get it.");
            }

            new InvalidAdvancement(this, fileName).Dispose();
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
            FunctionGroup existingFile = (FunctionGroup)GetFile("group_function",name);
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
            BlockGroup existingFile = (BlockGroup)GetFile("group_block",name);
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
            ItemGroup existingFile = (ItemGroup)GetFile("group_item", name);
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
            EntityGroup existingFile = (EntityGroup)GetFile("group_entity",name);
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
            LiquidGroup existingFile = (LiquidGroup)GetFile("group_liquid", name);
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

    /// <summary>
    /// A namespace used for refering functions and such which isn't in a datapack made with SharpCraft
    /// </summary>
    public class EmptyNamespace : BasePackNamespace
    {
        /// <summary>
        /// Intializes a new empty namespace. Make sure to call <see cref="BasePackNamespace.Setup(BaseDatapack, string)"/> after using this
        /// </summary>
        public EmptyNamespace() : base()
        {

        }

        /// <summary>
        /// Creates a new namespace in a datapack
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public EmptyNamespace(BaseDatapack datapack, string namespaceName) : base(datapack, namespaceName)
        {
            
        }
    }
}
