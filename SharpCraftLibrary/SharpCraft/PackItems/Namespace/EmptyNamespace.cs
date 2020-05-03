using System;

namespace SharpCraft
{
    /// <summary>
    /// A namespace used for refering functions and such which isn't in a datapack made with SharpCraft
    /// </summary>
    public class EmptyNamespace : BasePackNamespace
    {
        /// <summary>
        /// Returns a reference to the minecraft namespace
        /// </summary>
        /// <returns>A reference to the minecraft namespace</returns>
        public static EmptyNamespace GetMinecraftNamespace()
        {
            return EmptyDatapack.GetPack().Namespace("minecraft");
        }

        /// <summary>
        /// Returns a reference to the namespace
        /// </summary>
        /// <param name="namespace">The namespace to get</param>
        /// <returns>The namespace</returns>
        public static EmptyNamespace GetNamespace(string @namespace)
        {
            return EmptyDatapack.GetPack().Namespace(@namespace);
        }

        /// <summary>
        /// Intializes a new empty namespace. Make sure to call <see cref="BasePackNamespace.Setup(BaseDatapack, string)"/> after using this. Suggested to use <see cref="EmptyNamespace.GetNamespace(string)"/> instead.
        /// </summary>
        public EmptyNamespace() : base()
        {

        }

        /// <summary>
        /// Creates a new namespace in a datapack. Suggested to use <see cref="EmptyNamespace.GetNamespace(string)"/> instead
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public EmptyNamespace(BaseDatapack datapack, string namespaceName) : base(datapack, namespaceName)
        {
            
        }

        /// <summary>
        /// Not used. Makes no sense to generate names for empty namespace.
        /// </summary>
        /// <param name="getIdFor">Not used</param>
        /// <returns>Not used</returns>
        public override string GetID(object getIdFor)
        {
            throw new InvalidOperationException("Empty namespace shouldn't generate file names");
        }
    }
}
