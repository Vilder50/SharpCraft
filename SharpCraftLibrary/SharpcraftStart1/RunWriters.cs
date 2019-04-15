using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;
using SharpCraft;

namespace SharpCraftBeginning
{
    /// <summary>
    /// A class holding the run all writers method
    /// </summary>
    public static class RunWriters
    {
        /// <summary>
        /// Runs all writers in this assambly 
        /// </summary>
        /// <param name="pack">the packspace all the writers should be writing to</param>
        /// <param name="setupWriter">a writer which is used to setup needed things for all other writes</param>
        public static void Run(Packspace pack, Type setupWriter = null)
        {
            Console.WriteLine("Started");

            Type baseType = typeof(BaseDatapackWriter);

            //Get list of all sub classes
            List<Type> unsortedWriters = Assembly.GetAssembly(baseType).GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType)).ToList();
            List<Type> sortedWrites = new List<Type>();

            //make setupWriter be the first writer
            if (!(setupWriter is null))
            {
                if (!(setupWriter.IsClass && !setupWriter.IsAbstract && setupWriter.IsSubclassOf(baseType)))
                {
                    throw new ArgumentException(nameof(setupWriter) + " has to be a subclass of " + baseType.ToString());
                }
                else
                {
                    if (BaseDatapackWriter.RequiredClassesList(setupWriter).Count != 0)
                    {
                        throw new ArgumentException(nameof(setupWriter) + " has requirements, but is the setup writer so it shouldn't have any requirements.");
                    }
                    unsortedWriters.Remove(setupWriter);
                    sortedWrites.Add(setupWriter);
                }
            }

            //Sorts the list of classes so things with requirements are will have their required classes above them
            int lastAmount = unsortedWriters.Count;
            while (unsortedWriters.Count != 0)
            {
                //Cycle unsorted types
                for (int i = 0; i < unsortedWriters.Count; i++)
                {
                    Type type = unsortedWriters[i];
                    bool sorted = false;

                    //get requirement list
                    List<Type> requiredTypes = BaseDatapackWriter.RequiredClassesList(type);

                    //Check if requirement list is empty
                    if (requiredTypes.Count == 0)
                    {
                        sorted = true;
                    }
                    else
                    {
                        //check if all required classes already are in the sorted list
                        sorted = true;
                        foreach (Type requiredType in requiredTypes)
                        {
                            if (!sortedWrites.Contains(requiredType))
                            {
                                sorted = false;
                                break;
                            }
                        }
                    }

                    //Add to list if it should be added
                    if (sorted)
                    {
                        sortedWrites.Add(type);
                        unsortedWriters.Remove(type);
                        i--;
                    }
                }

                //If nothing new has been added to the sorted list throw exception
                if (lastAmount == unsortedWriters.Count)
                {
                    foreach (Type type in unsortedWriters)
                    {
                        List<Type> requiredTypes = BaseDatapackWriter.RequiredClassesList(type);
                        foreach (Type requiredType in requiredTypes)
                        {
                            if (!sortedWrites.Contains(requiredType))
                            {
                                List<Type> requiredRequiredTypes = BaseDatapackWriter.RequiredClassesList(requiredType);
                                if (requiredRequiredTypes.Contains(type))
                                {
                                    throw new ArgumentException(type.ToString() + " makes a requirement loop together with " + requiredType.ToString());
                                }
                            }
                        }
                    }
                    throw new ArgumentException("Something went wrong with sorting the writers based on their requirements.");
                }
                lastAmount = unsortedWriters.Count;
            }

            //run writers
            int amountOfWriters = sortedWrites.Count - 1;
            for (int i = 0; i < sortedWrites.Count; i++)
            {
                BaseDatapackWriter writer = (BaseDatapackWriter)Activator.CreateInstance(sortedWrites[i]);
                string name = sortedWrites[i].ToString();

                Console.WriteLine("\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Blue;
                long startTime = DateTime.Now.Ticks;
                int curserStartLocation = Console.CursorTop;
                Console.WriteLine($">Making {name} ({i}/{amountOfWriters}) (0)");
                Console.ForegroundColor = ConsoleColor.White;

                Thread create = new Thread(() =>
                {
                    writer.Pack = pack;
                    writer.StartWrite();
                });
                create.Start();

                long time;
                int xLocation;
                int yLocation;
                ConsoleColor oldBackground;
                ConsoleColor oldForeground;
                while (create.IsAlive)
                {
                    Thread.Sleep(100);
                    time = (DateTime.Now.Ticks - startTime) / 1000000;
                    xLocation = Console.CursorLeft;
                    yLocation = Console.CursorTop;
                    oldBackground = Console.BackgroundColor;
                    oldForeground = Console.ForegroundColor;

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(0, curserStartLocation);

                    Console.WriteLine($">Making {name} ({i}/{amountOfWriters}) ({time})");

                    Console.SetCursorPosition(xLocation, yLocation);
                    Console.BackgroundColor = oldBackground;
                    Console.ForegroundColor = oldForeground;
                }

                time = (DateTime.Now.Ticks - startTime) / 1000000;
                xLocation = Console.CursorLeft;
                yLocation = Console.CursorTop;

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, curserStartLocation);

                Console.WriteLine($">Done making {name} ({i}/{amountOfWriters}) ({time})");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(xLocation, yLocation);
                if (Console.CursorLeft == 0)
                {
                    Console.WriteLine("------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("\n------------------------------------------------------");
                }
            }

            //finish
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\nDone!");
        }
    }
}
