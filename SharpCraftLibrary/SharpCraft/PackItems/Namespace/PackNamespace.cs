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
        /// Generates a random id for the given <see cref="object"/>
        /// </summary>
        /// <param name="getIdFor">The object to id</param>
        /// <returns>The id for the object</returns>
        public override string GetID(object getIdFor)
        {
            _nextFileID++;
            return _nextFileID.ToString();
        }

        /// <summary>
        /// Creates a new predicate with the given name and condition
        /// </summary>
        /// <param name="name">The name of the predicate</param>
        /// <param name="condition">The condition</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created predicate</returns>
        public Predicate Predicate(string? name, Conditions.BaseCondition condition, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Predicate? existingFile = null;
            if (!(name is null)) 
            {
                existingFile = (Predicate?)GetFile("predicate", name); 
            }

            if (existingFile is null)
            {
                return new Predicate(this, name, condition, setting);
            }
            else
            {
                throw new ArgumentException("There already exists a predicate with the name: " + existingFile.FileId + ". Use GetFile(\"predicate\",\"" + existingFile.FileId + "\") to get it.");
            }
        }

        /// <summary>
        /// Creates a new function with the given name
        /// </summary>
        /// <param name="functionName">The function's name. If null will get random name</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function Function(string? functionName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                return new Function(this, null, setting);
            }

            Function? returnFunction = (Function?)GetFile("function", functionName);
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
        public Function Function(string? functionName, Function.FunctionWriter creater, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
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
        public Function Function(Function.FunctionWriter creater, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function function = Function((string?)null, setting);
            creater(function);

            return function;
        }

        /// <summary>
        /// Creates a function which runs on a player when all the given triggers gets triggered.
        /// </summary>
        /// <param name="triggers">The triggers to trigger</param>
        /// <param name="functionName">The name of the function</param>
        /// <param name="advancementName">The name of the advancement</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function EventFunction(BaseTrigger[] triggers, string? functionName = null, string? advancementName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function function = Function(functionName, setting);
            HiddenAdvancement trigger = Advancement(advancementName, triggers.Select(t => (Requirement)t).ToArray(), new Reward { Function = function }, setting);
            function.AddCommand(new Commands.AdvancementSingleCommand(ID.Selector.s, trigger, null, false));

            return function;
        }

        /// <summary>
        /// Creates a function which runs on a player when all the given triggers gets triggered.
        /// </summary>
        /// <param name="triggers">The triggers to trigger</param>
        /// <param name="creater">The function writer</param>
        /// <param name="functionName">The name of the function</param>
        /// <param name="advancementName">The name of the advancement</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created function</returns>
        public Function EventFunction(BaseTrigger[] triggers, Function.FunctionWriter creater, string? functionName = null, string? advancementName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function outFunction = EventFunction(triggers, functionName, advancementName, setting);
            creater(outFunction);

            return outFunction;
        }

        /// <summary>
        /// Creates a function which runs on a player if they have they get a score of the given type
        /// </summary>
        /// <param name="objectiveType">The score objective type. (See <see cref="ID.Objective"/> for a list of objectives)</param>
        /// <param name="functionName">The name of the function</param>
        /// <param name="setting">The setting used for writing the function file</param>
        /// <param name="runForEachScore">True if it should run the function for each number in the score. False if it only should run the function once when the score is higher than 1.</param>
        /// <returns>The newly created function</returns>
        public Function EventFunction(string objectiveType, bool runForEachScore = false, string? functionName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function outFunction = Function(functionName, setting);
            Datapack.GetItems<SharpCraftFiles>().AddObjectiveEventFunction(outFunction, objectiveType, runForEachScore);

            return outFunction;
        }

        /// <summary>
        /// Creates a function which runs on a player if they have they get a score of the given type
        /// </summary>
        /// <param name="objectiveType">The score objective type. (See <see cref="ID.Objective"/> for a list of objectives)</param>
        /// <param name="creater">The function writer</param>
        /// <param name="functionName">The name of the function</param>
        /// <param name="setting">The setting used for writing the function file</param>
        /// <param name="runForEachScore">True if it should run the function for each number in the score. False if it only should run the function once when the score is higher than 1.</param>
        /// <returns>The newly created function</returns>
        public Function EventFunction(string objectiveType, Function.FunctionWriter creater, bool runForEachScore = false, string? functionName = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            Function outFunction = EventFunction(objectiveType, runForEachScore, functionName, setting);
            creater(outFunction);

            return outFunction;
        }

        /// <summary>
        /// Creates a new crafting table recipe with the given parameters
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="recipe">A multidimensional array describing how the items should be layed out in the crafting table</param>
        /// <param name="output">The output item</param>
        /// <param name="group">The string id of the group the recipe is in</param>
        /// <param name="outputCount">The amount of items to output</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public CraftingRecipe Recipe(string? name, IItemType?[,] recipe, ID.Item output, int outputCount = 1, string? group = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new CraftingRecipe(this, name, recipe, output, outputCount, group, setting);
            }
        }

        /// <summary>
        /// Creates a new shapeless crafting table recipe with the given parameters
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="recipe">The items needed to craft the recipe</param>
        /// <param name="output">The output item</param>
        /// <param name="group">The string id of the group this recipe is in</param>
        /// <param name="outputCount">The amount of items to output</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public ShapelessRecipe Recipe(string? name, IItemType[] recipe, ID.Item output, int outputCount = 1, string? group = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new ShapelessRecipe(this, name, recipe, output, outputCount, group, setting);
            }
        }

        /// <summary>
        /// Creates a new furnace/a type of furnace recipe with the given parameters
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="ingredients">A list of items which are smeltable into the output item</param>
        /// <param name="output">the output item</param>
        /// <param name="xpDrop">the amount of xp the recipe should output</param>
        /// <param name="cookTime">the amount of time the recipe takes. Use null to use default time</param>
        /// <param name="type">The type of smelt recipe</param>
        /// <param name="group">The string id of the group this recipe is in</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public SmeltRecipe Recipe(string? name, SmeltRecipe.SmeltType type, IItemType[] ingredients, ID.Item output, double xpDrop, NoneNegativeTime<int>? cookTime = null, string? group = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new SmeltRecipe(this, name, type, ingredients, output, xpDrop, cookTime, group, setting);
            }
        }

        /// <summary>
        /// Creates a new furnace/a type of furnace recipe with the given parameters
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="ingredient">The item to smelt</param>
        /// <param name="output">the output item</param>
        /// <param name="xpDrop">the amount of xp the recipe should output</param>
        /// <param name="cookTime">the amount of time the recipe takes. Use null to use default time</param>
        /// <param name="type">The type of smelt recipe</param>
        /// <param name="group">The string id of the group this recipe is in</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public SmeltRecipe Recipe(string? name, SmeltRecipe.SmeltType type, IItemType ingredient, ID.Item output, double xpDrop, NoneNegativeTime<int>? cookTime = null, string? group = null, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new SmeltRecipe(this, name, type, ingredient, output, xpDrop, cookTime, group, setting);
            }
        }

        /// <summary>
        /// Creates a new stonecutter recipe
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="ingredients">A list of items which are cut-able into the output item</param>
        /// <param name="output">the output item</param>
        /// <param name="outputCount">The amount of items to output</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public CuttingRecipe Recipe(string? name, IItemType[] ingredients, ID.Item output, int outputCount = 1, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new CuttingRecipe(this, name, ingredients, output, outputCount, null, setting);
            }
        }

        /// <summary>
        /// Creates a new stonecutter recipe
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="ingredient">The cut-able which outputs the item</param>
        /// <param name="output">the output item</param>
        /// <param name="outputCount">The amount of items to output</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public CuttingRecipe Recipe(string? name, IItemType ingredient, ID.Item output, int outputCount = 1, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new CuttingRecipe(this, name, ingredient, output, outputCount, null, setting);
            }
        }

        /// <summary>
        /// Creates a new smithing recipe
        /// </summary>
        /// <param name="name">The recipe's name</param>
        /// <param name="baseItem">The first item in the recipe (NBT will be copied from this item to the output item)</param>
        /// <param name="modifierItem">The second item in the recipe</param>
        /// <param name="output">The item the recipe outputs (Note that it will copy nbt from <paramref name="baseItem"/>)</param>
        /// <param name="setting">The settings for how to write the file</param>
        /// <returns>The newly created recipe</returns>
        public SmithingRecipe Recipe(string? name, IItemType baseItem, IItemType modifierItem, ID.Item output, BaseFile.WriteSetting setting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BaseRecipe?)GetFile("recipe", name);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Use GetFile(\"recipe\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new SmithingRecipe(this, name, baseItem, modifierItem, output, setting);
            }
        }

        /// <summary>
        /// Overwrites the recipe with the given name with an invalid recipe
        /// </summary>
        /// <param name="name">The recipe's name</param>
        public void Recipe(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name), "Name may not be null");
            }

            BaseRecipe? existingFile = (BaseRecipe?)GetFile("recipe", name);

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a recipe with the name: " + existingFile.FileId + ". Don't generate the file if you don't need it anyways.");
            }
            else
            {
                new InvalidRecipe(this, name).Dispose();
            }
        }

        /// <summary>
        /// Creates a new <see cref="LootTable"/> with the <paramref name="lootPools"/>
        /// </summary>
        /// <param name="tableName">The <see cref="LootTable"/>'s name</param>
        /// <param name="lootPools">The <see cref="LootPool"/>s in the <see cref="LootTable"/></param>
        /// <param name="type">The type of loot table</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The newly created <see cref="LootTable"/></returns>
        public LootTable Loottable(string? tableName, LootPool[] lootPools, LootTable.TableType? type = null, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            LootTable? existingFile = null;
            if (!(tableName is null))
            {
                existingFile = (LootTable?)GetFile("loot_table", tableName);
            }

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
        public ParentAdvancement Advancement(string? fileName, Requirement[] requirements, Reward reward, BaseJsonText name, BaseJsonText description, Item icon, string background, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement? existingFile = null;
            if (!(fileName is null))
            {
                existingFile = (BaseAdvancement?)GetFile("advancement", fileName);
            }
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileId + ". Use GetFile(\"advancement\",\""+ existingFile.FileId + "\") to get it.");
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
        public ChildAdvancement Advancement(string? fileName, IAdvancement parent, Requirement[] requirements, Reward reward, BaseJsonText name, BaseJsonText description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement? existingFile = null;
            if (!(fileName is null))
            {
                existingFile = (BaseAdvancement?)GetFile("advancement", fileName);
            }
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileId + ". Use GetFile(\"advancement\",\"" + existingFile.FileId + "\") to get it.");
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
        public HiddenAdvancement Advancement(string? fileName, Requirement[] requirements, Reward reward, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseAdvancement? existingFile = null;
            if (!(fileName is null))
            {
                existingFile = (BaseAdvancement?)GetFile("advancement", fileName);
            }
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileId + ". Use GetFile(\"advancement\",\"" + existingFile.FileId + "\") to get it.");
            }

            return new HiddenAdvancement(this, fileName, requirements, reward, writeSetting);
        }

        /// <summary>
        /// Makes the advancement with the file name invalid... which also makes all its children invalid
        /// </summary>
        /// <param name="fileName">The name of the file to make invalid</param>
        public void Advancement(string fileName)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName), "FileName may not be null");
            }

            BaseAdvancement? existingFile = (BaseAdvancement?)GetFile("advancement", fileName);
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists an advancement with the name: " + existingFile.FileId + ". Use GetFile(\"advancement\",\"" + existingFile.FileId + "\") to get it.");
            }

            new InvalidAdvancement(this, fileName).Dispose();
        }

        /// <summary>
        /// Creates a new dimension
        /// </summary>
        /// <param name="fileName">The name of the dimension</param>
        /// <param name="generator">The generator used for generating the terrain</param>
        /// <param name="type">Information about the dimension</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new dimension</returns>
        public Dimension Dimension(string? fileName, DimensionObjects.BaseGenerator generator, DimensionObjects.IDimensionType type, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(fileName is null))
            {
                existingFile = (BaseRecipe?)GetFile("dimension", fileName);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a dimension with the name: " + existingFile.FileId + ". Use GetFile(\"dimension\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new Dimension(this, fileName, generator, type, writeSetting);
            }
        }

        /// <summary>
        /// Creates a new dimension type
        /// </summary>
        /// <param name="fileName">The name of the dimension type</param>
        /// <param name="type">Information about the type</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new dimension type</returns>
        public DimensionType DimensionType(string? fileName, DimensionObjects.DimensionTypeObject type, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            BaseRecipe? existingFile = null;
            if (!(fileName is null))
            {
                existingFile = (BaseRecipe?)GetFile("dimension_type", fileName);
            }

            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a dimension type with the name: " + existingFile.FileId + ". Use GetFile(\"dimension_type\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new DimensionType(this, fileName, type, writeSetting);
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
        public FunctionGroup Group(string? name, List<IFunction> functionList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            FunctionGroup? existingFile = null;
            if (!(name is null))
            {
                existingFile = (FunctionGroup?)GetFile("group_function", name);
            }
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
        /// Returns a <see cref="BlockGroup"/> with the given <see cref="IBlockType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="blockList">The <see cref="IBlockType"/>s in the group</param>
        /// <param name="append">If the <see cref="IBlockType"/>s should be appended to existing <see cref="IBlockType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="BlockGroup"/></returns>
        public BlockGroup Group(string? name, List<IBlockType> blockList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            BlockGroup? existingFile = null;
            if (!(name is null))
            {
                existingFile = (BlockGroup?)GetFile("group_block", name);
            }
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
        /// Returns a <see cref="ItemGroup"/> with the given <see cref="IItemType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="itemList">The <see cref="IItemType"/>s in the group</param>
        /// <param name="append">If the <see cref="IItemType"/>s should be appended to existing <see cref="IItemType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="ItemGroup"/></returns>
        public ItemGroup Group(string? name, List<IItemType> itemList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            ItemGroup? existingFile = null;
            if (!(name is null))
            {
                existingFile = (ItemGroup?)GetFile("group_item", name);
            }
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
        /// Returns a <see cref="EntityGroup"/> with the given <see cref="IEntityType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="entityList">The <see cref="IEntityType"/>s in the group</param>
        /// <param name="append">If the <see cref="IEntityType"/>s should be appended to existing <see cref="IEntityType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="EntityGroup"/></returns>
        public EntityGroup Group(string? name, List<IEntityType> entityList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            EntityGroup? existingFile = null;
            if (!(name is null))
            {
                existingFile = (EntityGroup?)GetFile("group_entity", name);
            }
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
        /// Returns a <see cref="LiquidGroup"/> with the given <see cref="ILiquidType"/>s
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="liquidList">The <see cref="ILiquidType"/>s in the group</param>
        /// <param name="append">If the <see cref="ILiquidType"/>s should be appended to existing <see cref="ILiquidType"/>s from another datapack</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The <see cref="LiquidGroup"/></returns>
        public LiquidGroup Group(string? name, List<ILiquidType> liquidList, bool append = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.OnDispose)
        {
            LiquidGroup? existingFile = null;
            if (!(name is null))
            {
                existingFile = (LiquidGroup?)GetFile("group_liquid", name);
            }
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

        /// <summary>
        /// Creates and returns a structure
        /// </summary>
        /// <param name="name">The name of the predicate file</param>
        /// <param name="blockPalettes">The pallete used for the structure</param>
        /// <param name="blocksInStructure">The blocks in the structure</param>
        /// <param name="entities">Entities in the structure</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The created structure</returns>
        public Structure Structure(string? name, Block[][] blockPalettes, Structure.Block[] blocksInStructure, Entities.BasicEntity[] entities, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            Structure? existingFile = null;
            if (!(name is null))
            {
                existingFile = (Structure?)GetFile("structure", name);
            }
            if (!(existingFile is null))
            {
                throw new ArgumentException("There already exists a structure with the name: " + existingFile.FileId + ". Use GetFile(\"structure\",\"" + existingFile.FileId + "\") to get it.");
            }
            else
            {
                return new Structure(this, name, blockPalettes, blocksInStructure, entities, writeSetting);
            }
        }

        /// <summary>
        /// Creates and returns a structure
        /// </summary>
        /// <param name="name">The name of the predicate file</param>
        /// <param name="blocks">The blocks in the structure. 0:y, 1: x, 2: z</param>
        /// <param name="entities">Entities in the structure</param>
        /// <param name="writeSetting">The settings for how to write the file</param>
        /// <returns>The created structure</returns>
        public Structure Structure(string? name, Block?[,,]? blocks, Entities.BasicEntity[]? entities, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            Entities.BasicEntity[] entityArray = entities ?? new Entities.BasicEntity[] { };
            Block?[,,] blockArray = blocks ?? new Block[,,] { };
            var (blocksInStructure, blockPalette) = SharpCraft.Structure.ConvertBlocksToStructure(blockArray);

            return Structure(name, new Block[][] { blockPalette }, blocksInStructure, entityArray, writeSetting);
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
