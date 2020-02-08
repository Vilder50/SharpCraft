using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SharpCraft.Writer
{
    /// <summary>
    /// Static class containing methods for running writers
    /// </summary>
    public static class SharpWriter
    {
        /// <summary>
        /// Runs all writers inheriting from the given type
        /// </summary>
        /// <param name="allAssemblies">True if there should be searched for writers in all assemblies</param>
        /// <typeparam name="TWriter">The type the writers should inherite from</typeparam>
        public static void RunNormalWriters<TWriter>(bool allAssemblies = false) where TWriter : ISharpWriterNormal
        {
            foreach(TWriter writer in GetWriters<TWriter>(allAssemblies))
            {
                writer.Write();
            }
        }

        /// <summary>
        /// Runs all writers inheriting from the given type
        /// </summary>
        /// <param name="datapack">Datapack to get namespaces from</param>
        /// <param name="allAssemblies">True if there should be searched for writers in all assemblies</param>
        /// <typeparam name="TWriter">The type the writers should inherite from</typeparam>
        public static void RunNamespaceWriters<TWriter>(Datapack datapack, bool allAssemblies = false) where TWriter : ISharpWriterNamespace
        {
            foreach (TWriter writer in GetWriters<TWriter>(allAssemblies))
            {
                writer.Namespace = datapack.Namespace(writer.NamespaceName);
                writer.Write();
            }
        }

        /// <summary>
        /// Returns a list of all the writers inheriting from the given type
        /// </summary>
        /// <param name="allAssemblies">True if there should be searched for writers in all assemblies</param>
        /// <typeparam name="TWriter">The type the writers should inherite from</typeparam>
        /// <returns>A list of writers</returns>
        public static List<TWriter> GetWriters<TWriter>(bool allAssemblies = false) where TWriter : ISharpWriter
        {
            List<TWriter> writers = new List<TWriter>();
            Type tWriterType = typeof(TWriter);
            Assembly[] assemblies;

            if (allAssemblies)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }
            else
            {
                assemblies = new Assembly[] { tWriterType.Assembly };
            }

            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.IsClass && !type.IsAbstract && tWriterType.IsAssignableFrom(type))
                        {
                            writers.Add((TWriter)Activator.CreateInstance(type));
                        }
                    }
                }
                catch
                {

                }
            }

            writers.Sort((w1, w2) => w1.Index - w2.Index);
            return writers;
        }
    }
}
