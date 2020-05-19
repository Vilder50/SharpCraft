namespace SharpCraft
{
    /// <summary>
    /// A datapack used for disabling/enabling datapacks.
    /// </summary>
    public class EmptyDatapack : BaseDatapack
    {
        private static EmptyDatapack? emptyPack;

        /// <summary>
        /// Returns an empty datapack
        /// </summary>
        /// <returns>An empty datapack</returns>
        public static EmptyDatapack GetPack()
        {
            emptyPack ??= new EmptyDatapack("vanilla", false);
            return emptyPack;
        }

        /// <summary>
        /// Intializes a new <see cref="EmptyDatapack"/>
        /// </summary>
        /// <param name="name">The name of the datapack</param>
        /// <param name="fileDatapack">True this <see cref="EmptyDatapack"/> is refering to an installed datapack. False if its an inbuilt datapack</param>
        public EmptyDatapack(string name, bool fileDatapack = true) : base("NoneExistingPath", name)
        {
            FileDatapack = fileDatapack;
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public override string IngameName
        {
            get
            {
                if (FileDatapack)
                {
                    return "\"file/" + Name + "\"";
                }
                else
                {
                    return Name;
                }
            }
        }

        /// <summary>
        /// True if this <see cref="EmptyDatapack"/> is refering to an installed datapack. False if its an inbuilt datapack
        /// </summary>
        public bool FileDatapack { get; set; }

        /// <summary>
        /// Returns a new empty namespace
        /// </summary>
        /// <param name="packNamespace">The name of the namespace</param>
        /// <returns>A new empty namespace</returns>
        public EmptyNamespace Namespace(string packNamespace)
        {
            return Namespace<EmptyNamespace>(packNamespace);
        }
    }
}
