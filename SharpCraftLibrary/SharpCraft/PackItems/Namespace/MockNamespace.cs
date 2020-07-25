using System;

namespace SharpCraft
{
    /// <summary>
    /// A namespace used for refering functions and such which isn't in a datapack made with SharpCraft
    /// </summary>
    public class MockNamespace : BasePackNamespace
    {
        /// <summary>
        /// Returns a reference to the minecraft namespace
        /// </summary>
        /// <returns>A reference to the minecraft namespace</returns>
        public static MockNamespace GetMinecraftNamespace()
        {
            return MockDatapack.GetPack().Namespace("minecraft");
        }

        /// <summary>
        /// Returns a reference to the namespace
        /// </summary>
        /// <param name="namespace">The namespace to get</param>
        /// <returns>The namespace</returns>
        public static MockNamespace GetNamespace(string @namespace)
        {
            return MockDatapack.GetPack().Namespace(@namespace);
        }

        /// <summary>
        /// Intializes a new empty namespace. Make sure to call <see cref="BasePackNamespace.Setup(BaseDatapack, string)"/> after using this. Suggested to use <see cref="MockNamespace.GetNamespace(string)"/> instead.
        /// </summary>
        public MockNamespace() : base()
        {

        }

        /// <summary>
        /// Creates a new namespace in a datapack. Suggested to use <see cref="MockNamespace.GetNamespace(string)"/> instead
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public MockNamespace(BaseDatapack datapack, string namespaceName) : base(datapack, namespaceName)
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
