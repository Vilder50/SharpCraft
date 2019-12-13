using System.IO;
using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// A object used to create <see cref="Recipe"/>s
    /// </summary>
    public class Recipe : BaseFile, IRecipe, IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new crafting recipe.
        /// </summary>
        /// <param name="space">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        /// <param name="Recipe">The recipe (max 3x3)</param>
        /// <param name="Output">The output item from the recipe</param>
        /// <param name="Group">The group the recipe is in</param>
        public Recipe(PackNamespace space, string fileName, Item[,] Recipe, Item Output, string Group) : base(space, fileName, WriteSetting.LockedAuto, "recipe")
        {
            MakeRecipePath();
            TextWriter recipeWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json");

            List<string> Keys = new List<string>();
            int[,] RecipeWithKeys = new int[3, 3];
            bool[] NoneEmptyVRow = new bool[3];
            bool[] NoneEmptyHRow = new bool[3];
            for (int i = 0; i < Recipe.Length; i++)
            {
                string SearchFor = Recipe[i / Recipe.GetLength(1), i % Recipe.GetLength(1)].ID.Name;

                if (SearchFor != "" && SearchFor != null)
                {
                    bool Sorted = false;
                    for (int j = 0; j < Keys.Count; j++)
                    {
                        if (Keys[j] == SearchFor)
                        {
                            Sorted = true;
                            RecipeWithKeys[i / Recipe.GetLength(1), i % Recipe.GetLength(1)] = j + 1;
                            NoneEmptyVRow[i / Recipe.GetLength(1)] = true;
                            NoneEmptyHRow[i % Recipe.GetLength(1)] = true;
                        }
                    }
                    if (!Sorted)
                    {
                        Keys.Add(SearchFor);
                        RecipeWithKeys[i / Recipe.GetLength(1), i % Recipe.GetLength(1)] = Keys.Count;
                        NoneEmptyVRow[i / Recipe.GetLength(1)] = true;
                        NoneEmptyHRow[i % Recipe.GetLength(1)] = true;
                    }
                }
            }

            recipeWriter.WriteLine("{\"type\":\"crafting_shaped\",\"pattern\":[");

            List<string> RecipeString = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                if (NoneEmptyVRow[i])
                {
                    string Row = "\"";
                    for (int j = 0; j < 3; j++)
                    {
                        if (RecipeWithKeys[i, j] != 0)
                        {
                            if (Keys[RecipeWithKeys[i, j] - 1] != "air")
                            {
                                Row += RecipeWithKeys[i, j];
                            }
                            else
                            {
                                Row += " ";
                            }
                        }
                        else
                        {
                            if (NoneEmptyHRow[j]) { Row += " "; }
                        }
                    }
                    RecipeString.Add(Row + "\"");
                }
            }
            recipeWriter.WriteLine(string.Join(",",RecipeString) + "],\"key\": {");
            List<string> writtenKeys = new List<string>();
            for (int i = 0; i < Keys.Count; i++)
            {
                if (Keys[i] != "air")
                {
                    string thisKey = "\"" + (i + 1) + "\":{";
                    if (!Keys[i].Contains("#"))
                    {
                        thisKey += "\"item\": \"" + Keys[i] + "\"}";
                    }
                    else
                    {
                        thisKey += "\"tag\": \"" + Keys[i].Replace("#", "") + "\"}";
                    }

                    writtenKeys.Add(thisKey);
                }
            }
            recipeWriter.Write(string.Join(",",writtenKeys));
            recipeWriter.WriteLine("},\"result\":{\"item\": \"" + Output.ID.Name +"\",\"count\": " + (Output.Count != null ? Output.Count : 1) + "}");
            if (Group != null) { recipeWriter.WriteLine(",\"group\": \"" + Group + "\""); }
            

            recipeWriter.WriteLine("}");
            recipeWriter.Dispose();
        }

        /// <summary>
        /// Intializes a new shapeless crafting recipe
        /// </summary>
        /// <param name="space">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        /// <param name="NeededItems">List of item's needed for the recipe</param>
        /// <param name="Output">The output item from the recipe</param>
        /// <param name="Group">The group the recipe is in</param>
        public Recipe(PackNamespace space, string fileName, Item[] NeededItems, Item Output, string Group) : base(space, fileName, WriteSetting.LockedAuto, "recipe")
        {
            MakeRecipePath();
            TextWriter recipeWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json");

            recipeWriter.WriteLine("{\"type\":\"crafting_shapeless\",\"ingredients\":[");
            for (int i = 0; i < NeededItems.Length; i++)
            {
                if (NeededItems[i].ToString().Contains("#"))
                {
                    recipeWriter.WriteLine("{\"tag\":\"" + NeededItems[i].ToString().Replace("#","") + "\"}");
                }
                else
                {
                    recipeWriter.WriteLine("{\"item\":\"" + NeededItems[i].ID.Name + "\"}");
                }
                if (i != NeededItems.Length - 1)
                {
                    recipeWriter.WriteLine(",");
                }
            }
            recipeWriter.WriteLine("],\"result\":{\"item\": \"" + Output.ID.Name + "\",\"count\": " + (Output.Count != null ? Output.Count : 1 ) + "}");
            if (Group != null) { recipeWriter.WriteLine(",\"group\": \"" + Group + "\""); }


            recipeWriter.WriteLine("}");
            recipeWriter.Dispose();
        }

        /// <summary>
        /// Intializes a new smelting recipe
        /// </summary>
        /// <param name="space">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        /// <param name="Input">The item to smelt</param>
        /// <param name="Output">The output item from the recipe</param>
        /// <param name="XpDrop"></param>
        /// <param name="CookTime">The amount of time it takes to smelt the item</param>
        /// <param name="type">The type of smelting recipe</param>
        public Recipe(PackNamespace space, string fileName, Item Input, ID.Item Output, double XpDrop, int CookTime, ID.SmeltType type) : base(space, fileName, WriteSetting.LockedAuto, "recipe")
        {
            MakeRecipePath();
            TextWriter recipeWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json");
            recipeWriter.Write("{\"type\":\"");
            switch(type)
            {
                case ID.SmeltType.BlastFurnace:
                    recipeWriter.Write("minecraft:blasting");
                    break;

                case ID.SmeltType.Furnace:
                    recipeWriter.Write("minecraft:smelting");
                    break;

                case ID.SmeltType.Smoker:
                    recipeWriter.Write("minecraft:smoking");
                    break;

                case ID.SmeltType.Campfire:
                    recipeWriter.Write("minecraft:campfire_cooking");
                    break;
            }
            recipeWriter.WriteLine("\",\"ingredient\": { \"item\": \"" + Input.ID.Name + "\"},\"result\": \"" + Output.MinecraftValue() + "\", \"experience\":" + XpDrop.ToMinecraftDouble() + ",\"cookingtime\":" + CookTime + "}}");
            recipeWriter.Dispose();
        }

        /// <summary>
        /// Intializes a new invalid recipe
        /// </summary>
        /// <param name="space">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        public Recipe(PackNamespace space, string fileName) : base(space, fileName, WriteSetting.LockedAuto, "recipe")
        {
            MakeRecipePath();
            using (TextWriter recipeWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json"))
            {
                recipeWriter.WriteLine("{}");
            }
        }

        /// <summary>
        /// Intializes a new recipe for the stone cutter
        /// </summary>
        /// <param name="space">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        /// <param name="Input">The item to cut</param>
        /// <param name="Output">The output item</param>
        public Recipe(PackNamespace space, string fileName, Item Input, Item Output) : base(space, fileName, WriteSetting.LockedAuto, "recipe")
        {
            MakeRecipePath();
            using (TextWriter recipeWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "recipes\\" + FileName + ".json"))
            {
                recipeWriter.WriteLine("{\"type\":\"minecraft:stonecutting\",\"ingredient\": { \"item\": \"" + Input.ID.Name + "\"},\"result\": \"" + Output.ID.Name + "\",\"count\":" + (Output.Count != null ? Output.Count : 1) + "}");
            }
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Recipe"/>
        /// </summary>
        /// <returns>this <see cref="Recipe"/>'s name</returns>
        public override string ToString()
        {
            return PackNamespace.GetPath();
        }

        private void MakeRecipePath()
        {
            CreateDirectory("recipes");
        }

        /// <summary>
        /// Converts this recipe into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartTag(ToString());
        }

        /// <summary>
        /// Writes this recipe file
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
    }

    /// <summary>
    /// Used for giving recipes outside this program
    /// </summary>
    public class EmptyRecipe : IRecipe
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe</param>
        public EmptyRecipe(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the recipe
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the recipe is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for evoking this recipe
        /// </summary>
        /// <returns>The string used for evoking this recipe</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }
    }
}
