using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace SharpCraft.Generator
{
    class Program
    {
        const string command = @"java -cp %server-file% net.minecraft.data.Main --server --reports";

        static void Main()
        {
            string path;
            while (true) {
                Console.WriteLine("Enter path to server.jar to use for generation:");
                path = Console.ReadLine();
                if (File.Exists(path))
                {
                    if (path.EndsWith(".jar"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Given file is not a .jar file.");
                    }
                } 
                else
                {
                    Console.WriteLine("File doesn't exist.");
                }
            }

            string directory = GetTemporaryDirectory();
            Console.WriteLine("Generating into: " + directory + "...");

            using (Process cmd = new Process())
            {
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("cd " + directory);
                cmd.StandardInput.WriteLine(command.Replace("%server-file%", path));
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            }

            string fileText;
            using (StreamReader reader = new StreamReader(directory + "/generated/reports/registries.json"))
            {
                fileText = reader.ReadToEnd();
            }

            Console.WriteLine("Generating...");
            try
            {
                var registries = JsonDocument.Parse(fileText).RootElement.EnumerateObject().ToList();
                GenerateRegistryFile(registries, "Attributes", "IDs/Generated/AttributeIDs.cs", "minecraft:attribute", name => RemoveNamespace(name).Replace(".", "_"), RemoveNamespace);
                GenerateRegistryFile(registries, "Biomes", "IDs/Generated/BiomeIDs.cs", "minecraft:biome", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Blocks", "IDs/Generated/BlockIDs.cs", "minecraft:block", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Effects", "IDs/Generated/EffectIDs.cs", "minecraft:mob_effect", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Enchants", "IDs/Generated/EnchantIDs.cs", "minecraft:enchantment", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Entities", "IDs/Generated/EntityIDs.cs", "minecraft:entity_type", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Items", "IDs/Generated/ItemIDs.cs", "minecraft:item", name => name.EndsWith("string") ? "String" : RemoveNamespace(name), RemoveNamespace);
                GenerateRegistryFile(registries, "Fluid", "IDs/Generated/FluidIDs.cs", "minecraft:fluid", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Particles", "IDs/Generated/ParticleIDs.cs", "minecraft:particle_type", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "Potions", "IDs/Generated/PotionIDs.cs", "minecraft:potion", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "VillagerProfession", "IDs/Generated/VillagerProfessionIDs.cs", "minecraft:villager_profession", RemoveNamespace, RemoveNamespace);
                GenerateRegistryFile(registries, "VillagerType", "IDs/Generated/VillagerTypeIDs.cs", "minecraft:villager_type", RemoveNamespace, RemoveNamespace);

                string dataFolder = directory + "/generated/data/minecraft/";
                string tagsFolder = dataFolder + "tags/";
                GenerateFileFile(tagsFolder + "blocks", "Files/Groups/Blocks", "IDs/Generated/Files/BlockGroups.cs", t => t, t => t, new List<(string file, string comment)>()
                {
                    ("infiniburn_end", "Blocks which burns for forever in the end"),
                    ("infiniburn_nether", "Blocks which burns for forever in the nether"),
                    ("infiniburn_overworld", "Blocks which burns for forever in the overworld"),
                    ("prevent_mob_spawning_inside","Mobs cannot spawn in these blocks"),
                    ("guarded_by_piglins","Piglins will be angry if any of these blocks are broken"),
                    ("campfires","Can be lit with flint and steel"),
                    ("strider_warm_blocks","Shriders in this block will shake"),
                    ("soul_speed_blocks","Blocks the soul speed enchant works on"),
                    ("piglin_repellents","Blocks piglins tries to stay away from"),
                    ("hoglin_repellents","Blocks hoglins tries to stay away from"),
                    ("wither_summon_base_blocks","Blocks used for making the T part of the wither summon build."),
                    ("wall_post_override","none solid blocks which still makes walls connect upwards."),
                    ("nylium","Blocks nether fungus, roots and sprouts can be placed on. Netherrack can be bonemealed if around of these blocks (netherrack only converts into nylium)."),
                    ("fire","Blocks which can be broken with water bottle water. Falling blocks will try to fall through these blocks. "),
                    ("climbable","Make blocks climbable"),
                    ("beacon_base_blocks","Blocks for building a beacon pyramid"),
                    ("signs","Water doesn't break these blocks"),
                    ("rails","Blocks mobs can't spawn on. Blocks minecarts can be dispenced on to. Blocks tnt minecarts doesn't destroy."),
                    ("jungle_logs","Blocks cocoa beans can be placed on"),
                    ("shulker_boxes","Blocks fences doesn't connect to"),
                    ("flowers","Blocks bees can pollinate and remember"),
                    ("beehives","Blocks bees can fill with pollen and blocks dispensers can use a shear or a glass bottle on"),
                    ("bee_growables","Blocks bees can make grow"),
                    ("dragon_immune","Blocks the enderdragon wont destroy"),
                    ("wither_immune","Blocks the wither wont have easy to break"),
                    ("bamboo_plantable_on","Blocks in this group allows bamboo to be planted ontop"),
                    ("wool","Blocks in this group can be broken using shears. If a block in this group is under a note block it will sound like a guitar"),
                    ("valid_spawn","Blocks in this group allows players to spawn on them"),
                    ("underwater_bonemeals","When one of these blocks are bonemealed under water in a warm water biome the block duplicates"),
                    ("logs","Leaves wont decay around these blocks. Trees can grow into blocks with this tag"),
                    ("fences","Blocks leads can attach to. Blocks which mobs see as fences while pathfinding"),
                    ("impermeable","Blocks in this groups does not allow water and honey to drip through them"),
                    ("beds","Blocks cats can sit on. Blocks which can be slept in"),
                    ("banners","Right clicking these blocks with a map marks it on the map"),
                    ("anvil","This group makes anvils in it show their gui when clicked (Only works for anvil blocks). Changes the death message caused by the block as a falling block landing on and killing a player"),
                    ("enderman_holdable","Endermen can only pick up blocks in this group"),
                });
                GenerateFileFile(tagsFolder + "items", "Files/Groups/Items", "IDs/Generated/Files/ItemGroups.cs", t => t, t => t, new List<(string file, string comment)>()
                {
                    ("piglin_loved","Items piglins pickup."),
                    ("non_flammable_wood","Won't be able to be used as furnace fuel"),
                    ("piglin_repellents","Items piglins wont pickup"),
                    ("beacon_payment_items","Items which can be used for activating beacons."),
                    ("wool","Items which burns in a furnace for 100 ticks"),
                    ("wooden_trapdoors","Items which burns in a furnace for 300 ticks"),
                    ("wooden_stairs","Items which burns in a furnace for 300 ticks"),
                    ("wooden_slabs","Items which burns in a furnace for 150 ticks"),
                    ("wooden_pressure_plates","Items which burns in a furnace for 300 ticks"),
                    ("wooden_doors","Items which burns in a furnace for 200 ticks"),
                    ("wooden_buttons","Items which burns in a furnace for 100 ticks"),
                    ("small_flowers","Items used for crafting suspicious stew. Items which can be fed to brown mushrooms. Items which bees follows"),
                    ("signs","Items which burns in a furnace for 200 ticks"),
                    ("saplings","Items which burns in a furnace for 100 ticks"),
                    ("logs","Items which burns in a furnace for 300 ticks"),
                    ("carpets","Items which can be placed on llamas. Items which burns in a furnace for 67 ticks"),
                    ("boats","Items which burns in a furnace for 1200 ticks"),
                    ("banners","Items which burns in a furnace for 300 ticks"),
                    ("arrows","Items which can be shot from a bow/crossbow"),
                    ("lectern_books","Items which can be placed on a lectern writable_book and written_book"),
                    ("flowers","Items which can be used to breed bees"),
                    ("fishes","Dolphins swims to players with this item. Can be feet to dolphins"),
                    ("planks","Items in this group can be used to repeair wooden tools and shields. Items which burns in a furnace for 300 ticks"),
                });
                GenerateFileFile(tagsFolder + "entity_types", "Files/Groups/Entities", "IDs/Generated/Files/EntityGroups.cs", t => t, t => t, new List<(string file, string comment)>()
                {
                    ("impact_projectiles", "Entities which can break chorus fruit."),
                    ("raiders", "Entities which glows when the bell rings. Entities which don't override ravager AI when riding on one"),
                    ("beehive_inhabitors", "entities which can be in beehives"),
                });
                GenerateFileFile(tagsFolder + "fluids", "Files/Groups/Fluids", "IDs/Generated/Files/FluidGroups.cs", t => t, t => t, new List<(string file, string comment)>()
                {
                    ("lava", "Cactus breaks beside these. Used to make a fluid look like lava. Used to make smoke particles when rain hits these. Items and experience orbs burns inside these. used when creating cobblestone/stone/obsidian."),
                    ("water", "Corals must be beside one of these. Farmland stays hydrated around these. Sugar canes can stay around these. Sponges absorb these. some particles can only survive in these. Entities in these moves like in water. concrete gets solid in these. items float in these. glass bottles can be filled with these."),
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ran into problem: " + ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine("Cleaning up...");
                Directory.Delete(directory, true);
            }
        }

        private static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), "SharpCraft-" + Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private static string RemoveNamespace(string stringWithNamespace)
        {
            return stringWithNamespace.Substring(stringWithNamespace.IndexOf(":") + 1);
        }

        private static void GenerateRegistryFile(List<JsonProperty> data, string generateFrom, string generateTo, string useRegistry, Func<string,string> nameFormatter, Func<string,string> valueFormatter)
        {
            Console.WriteLine("Generating ids " + useRegistry + "...");
            var innerData = data.SingleOrDefault(d => d.Name == useRegistry);
            if (innerData.Equals(default(JsonProperty)))
            {
                throw new ArgumentException("Failed to find registry \""+useRegistry+"\"");
            }

            var entries = innerData.Value.EnumerateObject().SingleOrDefault(d => d.Name == "entries");
            if (innerData.Equals(default(JsonProperty)))
            {
                throw new ArgumentException("Failed to find entries in the registry \"" + useRegistry + "\"");
            }

            var entryKeys = entries.Value.EnumerateObject().Select(e => e.Name);
            WriteFile(entryKeys, generateFrom, generateTo, nameFormatter, valueFormatter, new List<(string value, string comment)>());
        }

        private static void GenerateFileFile(string folder, string generateFrom, string generateTo, Func<string, string> nameFormatter, Func<string, string> valueFormatter, List<(string file, string comment)> comments)
        {
            Console.WriteLine("Generating file ids " + folder + "...");
            if (!Directory.Exists(folder))
            {
                throw new ArgumentException("Failed to find folder \"" + folder + "\"");
            }

            var files = Directory.GetFiles(folder).Select(f => f[(f.Replace("\\","/").LastIndexOf("/") + 1)..(f.LastIndexOf("."))]);

            WriteFile(files, generateFrom, generateTo, nameFormatter, valueFormatter, comments);
        }

        private static void WriteFile(IEnumerable<string> data, string generateFrom, string generateTo, Func<string, string> nameFormatter, Func<string, string> valueFormatter, List<(string value, string comment)> comments)
        {
            string generateFromPath = Path.GetFullPath("./../../../Templates/" + generateFrom + ".template");
            string generateToPath = Path.GetFullPath("./../../../../SharpCraft/" + generateTo);
            if (!File.Exists(generateFromPath))
            {
                throw new ArgumentException("Failed to find the file \"" + generateFromPath + "\"");
            }

            string text;
            using (StreamReader reader = new StreamReader(generateFromPath))
            {
                text = reader.ReadToEnd();
            }

            int startIndex = text.IndexOf("<%");
            int endIndex = text.IndexOf("%>");
            if (startIndex == -1 || endIndex == -1)
            {
                throw new FormatException("Expected \"" + generateFrom + "\" to contain <% and %>");
            }
            if (startIndex > endIndex)
            {
                throw new FormatException("Expected \"" + generateFrom + "\" to contain <% before %>");
            }
            endIndex += 2;
            string replaceString = text[startIndex..endIndex];
            replaceString = replaceString[2..^2];

            //write the actual file
            using StreamWriter writer = new StreamWriter(generateToPath);
            writer.WriteLine("//");
            writer.WriteLine("//This file was generated by SharpCraft.Generator.");
            writer.WriteLine("//Do not make changes directly to this file. Change the template file instead.");
            writer.WriteLine("//\n");
            writer.Write(text.Substring(0, startIndex));

            Dictionary<string, bool> usedComments = new Dictionary<string, bool>();
            comments.ForEach(comment =>
            {
                usedComments.Add(comment.value, false);
            });

            foreach (var name in data)
            {
                string comment = comments.SingleOrDefault(comment => comment.value == name).comment;
                if (!(comment is null))
                {
                    usedComments[name] = true;
                    writer.WriteLine("\n/// <summary>\n///" + comment + "\n/// </summary>");
                }
                writer.WriteLine(replaceString.Replace("%name%", nameFormatter(name)).Replace("%value%", valueFormatter(name)));
            }

            writer.Write(text.Substring(endIndex));
            foreach(var usedInfo in usedComments)
            {
                if (!usedInfo.Value)
                {
                    throw new Exception("Didn't use comment for \"" + usedInfo.Key + "\" in \"" + generateFrom + "\"");
                }
            }
        }
    }
}
