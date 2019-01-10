using System.IO;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// A object used to create <see cref="Recipe"/>s
    /// </summary>
    public class Recipe
    {
        readonly string Path;
        /// <summary>
        /// Creates an <see cref="Recipe"/> object with the given string
        /// Used to give <see cref="Recipe"/>s which doesnt have an object
        /// use fx <see cref="Packspace.NewRecipe(string, Item, ID.Item, double, int)"/> to create a new <see cref="Recipe"/>
        /// </summary>
        /// <param name="recipe">An string path to and <see cref="Recipe"/></param>
        public Recipe(string recipe)
        {
            Path = recipe.ToLower().Replace("\\", "/");
        }
        internal Recipe(Packspace Namespace, string Name,Item[,] Recipe, Item Output, string Group)
        {
            MakeRecipePath(Namespace, Name);
            Path = Namespace.Name + ":" + Name.Replace("\\", "/");
            StreamWriter RecipeWriter = new StreamWriter(new FileStream(Namespace.WorldPath + "\\datapacks\\" + Namespace.PackName + "\\data\\" + Namespace.Name + "\\recipes\\" + Name + ".json", FileMode.Create)) { AutoFlush = true };

            List<string> Keys = new List<string>();
            int[,] RecipeWithKeys = new int[3, 3];
            bool[] NoneEmptyVRow = new bool[3];
            bool[] NoneEmptyHRow = new bool[3];
            for (int i = 0; i < Recipe.Length; i++)
            {
                string SearchFor;
                if (Recipe[i / Recipe.GetLength(1), i % Recipe.GetLength(1)].Group == null)
                {
                    SearchFor = Recipe[i / Recipe.GetLength(1), i % Recipe.GetLength(1)].ID.MinecraftValue();
                }
                else
                {
                    SearchFor = "#" + Recipe[i / Recipe.GetLength(1), i % Recipe.GetLength(1)].Group.ToString();
                }

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

            RecipeWriter.WriteLine("{\"type\":\"crafting_shaped\",\"pattern\":[");

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
            RecipeWriter.WriteLine(string.Join(",",RecipeString) + "],\"key\": {");
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
            RecipeWriter.Write(string.Join(",",writtenKeys));
            RecipeWriter.WriteLine("},\"result\":{\"item\": \"" + Output.ID.MinecraftValue() +"\",\"count\": " + (Output.Count != null ? Output.Count : 1) + "}");
            if (Group != null) { RecipeWriter.WriteLine(",\"group\": \"" + Group + "\""); }
            

            RecipeWriter.WriteLine("}");
            RecipeWriter.Dispose();
        }
        internal Recipe(Packspace Namespace, string Name, Item[] NeededItems, Item Output, string Group)
        {
            MakeRecipePath(Namespace, Name);
            Path = Namespace.Name + ":" + Name.Replace("\\", "/");
            StreamWriter RecipeWriter = new StreamWriter(new FileStream(Namespace.WorldPath + "\\datapacks\\" + Namespace.PackName + "\\data\\" + Namespace.Name + "\\recipes\\" + Name + ".json", FileMode.Create)) { AutoFlush = true };

            RecipeWriter.WriteLine("{\"type\":\"crafting_shapeless\",\"ingredients\":[");
            for (int i = 0; i < NeededItems.Length; i++)
            {
                if (NeededItems[i].ToString().Contains("#"))
                {
                    RecipeWriter.WriteLine("{\"tag\":\"" + NeededItems[i].ToString().Replace("#","") + "\"}");
                }
                else
                {
                    RecipeWriter.WriteLine("{\"item\":\"" + NeededItems[i].ID.MinecraftValue() + "\"}");
                }
                if (i != NeededItems.Length - 1)
                {
                    RecipeWriter.WriteLine(",");
                }
            }
            RecipeWriter.WriteLine("],\"result\":{\"item\": \"" + Output.ID.MinecraftValue() + "\",\"count\": " + (Output.Count != null ? Output.Count : 1 ) + "}");
            if (Group != null) { RecipeWriter.WriteLine(",\"group\": \"" + Group + "\""); }


            RecipeWriter.WriteLine("}");
            RecipeWriter.Dispose();
        }
        internal Recipe(Packspace Namespace, string Name, Item Input, ID.Item Output, double XpDrop, int CookTime, ID.SmeltType type)
        {
            MakeRecipePath(Namespace, Name);
            Path = Namespace.Name + ":" + Name.Replace("\\", "/");
            StreamWriter RecipeWriter = new StreamWriter(new FileStream(Namespace.WorldPath + "\\datapacks\\" + Namespace.PackName + "\\data\\" + Namespace.Name + "\\recipes\\" + Name + ".json", FileMode.Create)) { AutoFlush = true };
            RecipeWriter.Write("{\"type\":\"");
            switch(type)
            {
                case ID.SmeltType.BlastFurnace:
                    RecipeWriter.Write("blasting");
                    break;

                case ID.SmeltType.Furnace:
                    RecipeWriter.Write("smelting");
                    break;

                case ID.SmeltType.Smoker:
                    RecipeWriter.Write("smoking");
                    break;

                case ID.SmeltType.Campfire:
                    RecipeWriter.Write("campfire");
                    break;
            }
            RecipeWriter.WriteLine("\",\"ingredient\": { \"item\": \"" + Input + "\"},\"result\": \"" + Output + "\", \"experience\":" + XpDrop.ToMinecraftDouble() + ",\"cookingtime\":" + CookTime + "}}");
            RecipeWriter.Dispose();
        }
        internal Recipe(Packspace Namespace, string Name)
        {
            MakeRecipePath(Namespace, Name);
            Path = Namespace.Name + ":" + Name.Replace("\\", "/");
            StreamWriter RecipeWriter = new StreamWriter(new FileStream(Namespace.WorldPath + "\\datapacks\\" + Namespace.PackName + "\\data\\" + Namespace.Name + "\\recipes\\" + Name + ".json", FileMode.Create)) { AutoFlush = true };
            RecipeWriter.WriteLine("{}");
            RecipeWriter.Dispose();
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Recipe"/>
        /// </summary>
        /// <returns>this <see cref="Recipe"/>'s name</returns>
        public override string ToString()
        {
            return Path;
        }

        private static void MakeRecipePath(Packspace pack, string name)
        {
            if (name.Contains("\\"))
            {
                Directory.CreateDirectory(pack.WorldPath + "\\datapacks\\" + pack.PackName + "\\data\\" + pack.Name + "\\recipes\\" + name.ToLower().Substring(0, name.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(pack.WorldPath + "\\datapacks\\" + pack.PackName + "\\data\\" + pack.Name + "\\recipes\\");
            }
        }
    }
}
