using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// An object used for Dimensions
    /// </summary>
    public class Dimension : Data.IConvertableToDataTag
    {
        /// <summary>
        /// The vanilla overworld dimension
        /// </summary>
        public static readonly Dimension Overworld = new Dimension(EmptyNamespace.GetMinecraftNamespace(), "overworld");

        /// <summary>
        /// The vanilla nether dimension
        /// </summary>
        public static readonly Dimension Nether = new Dimension(EmptyNamespace.GetMinecraftNamespace(), "the_nether");

        /// <summary>
        /// The vanilla end dimension
        /// </summary>
        public static readonly Dimension End = new Dimension(EmptyNamespace.GetMinecraftNamespace(), "the_end");

        private string name = null!;

        /// <summary>
        /// Creates a new <see cref="Dimension"/> object.
        /// </summary>
        /// <param name="dimensionName">The name of the dimension</param>
        /// <param name="namespace">The namespace the dimension is in.</param>
        public Dimension(BasePackNamespace @namespace, string dimensionName)
        {
            Namespace = @namespace;
            Name = dimensionName;
        }

        /// <summary>
        /// The name of the dimension
        /// </summary>
        public string Name 
        { 
            get => name;
            protected set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new System.ArgumentException("Dimension name may not be null or whitespace", nameof(Name));
                }
                string loweredString = value.ToLower();
                if (!Utils.ValidateName(loweredString,false,true, null))
                {
                    throw new System.ArgumentException("Dimension name is invalid. Name only accepts letters, numbers and /-._");
                }
                name = loweredString;
            }
        }

        /// <summary>
        /// The namespace the dimension is in.
        /// </summary>
        public BasePackNamespace Namespace { get; private set; }

        /// <summary>
        /// Get string used for refering this dimension
        /// </summary>
        /// <returns>String used for refering this dimension</returns>
        public string GetFullName()
        {
            return Namespace.Name + ":" + Name;
        }

        /// <summary>
        /// Converts this dimension into a <see cref="Data.DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="Data.DataPartTag"/></returns>
        public Data.DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new Data.DataPartTag(GetFullName());
        }
    }
}
