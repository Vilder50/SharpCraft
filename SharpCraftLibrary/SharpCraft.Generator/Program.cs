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

            Console.WriteLine("Cleaning up...");
            Directory.Delete(directory, true);

            Console.WriteLine("Generating...");
            var registries = JsonDocument.Parse(fileText).RootElement.EnumerateObject().ToList();
            GenerateFile(registries, "Attributes", "IDs/Generated/AttributeIDs.cs", "minecraft:attribute", name => RemoveNamespace(name).Replace(".","_"), RemoveNamespace);

            GenerateFile(registries, "Biomes", "IDs/Generated/BiomeIDs.cs", "minecraft:biome", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Blocks", "IDs/Generated/BlockIDs.cs", "minecraft:block", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Effects", "IDs/Generated/EffectIDs.cs", "minecraft:mob_effect", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Enchants", "IDs/Generated/EnchantIDs.cs", "minecraft:enchantment", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Entities", "IDs/Generated/EntityIDs.cs", "minecraft:entity_type", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Items", "IDs/Generated/ItemIDs.cs", "minecraft:item", name => name.EndsWith("string") ? "String" : RemoveNamespace(name), RemoveNamespace);
            GenerateFile(registries, "Liquid", "IDs/Generated/LiquidIDs.cs", "minecraft:fluid", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Particles", "IDs/Generated/ParticleIDs.cs", "minecraft:particle_type", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "Potions", "IDs/Generated/PotionIDs.cs", "minecraft:potion", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "VillagerProfession", "IDs/Generated/VillagerProfessionIDs.cs", "minecraft:villager_profession", RemoveNamespace, RemoveNamespace);
            GenerateFile(registries, "VillagerType", "IDs/Generated/VillagerTypeIDs.cs", "minecraft:villager_type", RemoveNamespace, RemoveNamespace);
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

        private static void GenerateFile(List<JsonProperty> data, string generateFrom, string generateTo, string useRegistry, Func<string,string> nameFormatter, Func<string,string> valueFormatter)
        {
            
            Console.WriteLine("Generating " + useRegistry + "...");
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
                throw new FormatException("Expected file to contain <% and %>");
            }
            if (startIndex > endIndex)
            {
                throw new FormatException("Expected file to contain <% before %>");
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

            foreach (var name in entryKeys)
            {
                writer.WriteLine(replaceString.Replace("%name%", nameFormatter(name)).Replace("%value%", valueFormatter(name)));
            }

            writer.Write(text.Substring(endIndex));
        }
    }
}
