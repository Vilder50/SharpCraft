using SharpCraft.Conditions;
using SharpCraft.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.FileMocks
{
    /// <summary>
    /// Base class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public abstract class BaseMockFile : Data.IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="BaseMockFile"/>
        /// </summary>
        /// <param name="fileName">A string in the format format NAMESPACE:NAME to make into a mock file</param>
        protected BaseMockFile(string fileName)
        {
            var (@namespace, name) = GetNamespaceAndNameFromString(fileName);
            FileId = name;
            PackNamespace = @namespace;
        }

        /// <summary>
        /// Intializes a new <see cref="BaseMockFile"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public BaseMockFile(BasePackNamespace packNamespace, string name)
        {
            FileId = name;
            PackNamespace = packNamespace;
        }

        /// <summary>
        /// The name of the fil
        /// </summary>
        public string FileId { get; private set; }

        /// <summary>
        /// The namespace the file is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for refering the file
        /// </summary>
        /// <returns>The string used for refering the file</returns>
        public virtual string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileId;
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:NAME into a namespace object and a string containing the name
        /// </summary>
        /// <param name="fileName">The string to convert</param>
        /// <returns>The things from the string</returns>
        protected static (BasePackNamespace @namespace, string name) GetNamespaceAndNameFromString(string fileName)
        {
            string[] parts = fileName.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating mock file has to contain a single :");
            }
            return (MockDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }

        /// <summary>
        /// Converts this structure into a <see cref="Data.DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not in use</param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>the made <see cref="Data.DataPartTag"/></returns>
        public virtual Data.DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new Data.DataPartTag(GetNamespacedName());
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockStructure : BaseMockFile, IStructure
    {
        private MockStructure(string fileName) : base(fileName)
        {
           
        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockStructure(BasePackNamespace packNamespace, string name): base(packNamespace, name)
        {
            
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockStructure(string name)
        {
            return new MockStructure(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockRecipe : BaseMockFile, IRecipe
    {
        private MockRecipe(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockRecipe(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockRecipe(string name)
        {
            return new MockRecipe(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockPredicate : BaseMockFile, IPredicate
    {
        private PredicateCondition? thisCondition;
        private MockPredicate(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockPredicate(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockPredicate(string name)
        {
            return new MockPredicate(name);
        }

        /// <summary>
        /// Returns a condition checking this predicate
        /// </summary>
        /// <returns>A condition checking this predicate</returns>
        public PredicateCondition GetCondition()
        {
            thisCondition ??= new PredicateCondition(this);
            return thisCondition;
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockLootTable : BaseMockFile, ILootTable
    {
        private MockLootTable(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockLootTable(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockLootTable(string name)
        {
            return new MockLootTable(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockDimension : BaseMockFile, DimensionObjects.IDimension
    {
        private MockDimension(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockDimension(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockDimension(string name)
        {
            return new MockDimension(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockDimensionType : BaseMockFile, DimensionObjects.IDimensionTypeFile
    {
        private MockDimensionType(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockDimensionType(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Returns the string used for chosen this dimension type
        /// </summary>
        /// <returns>String for chosing this</returns>
        public string GetDimensionTypeString()
        {
            return "\"" + GetNamespacedName() + "\"";
        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockDimensionType(string name)
        {
            return new MockDimensionType(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockAdvancement : BaseMockFile, IAdvancement
    {
        private MockAdvancement(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockAdvancement(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockAdvancement(string name)
        {
            return new MockAdvancement(name);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockFunction : BaseMockFile, IFunction
    {
        /// <summary>
        /// Returns <see cref="BaseFile.GetNamespacedName()"/>
        /// </summary>
        [CompoundPath(0)]
        public string Name
        {
            get => GetNamespacedName();
        }

        /// <summary>
        /// Marks this as not being a group
        /// </summary>
        public bool IsAGroup => false;

        private MockFunction(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockFunction(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockFunction(string name)
        {
            return new MockFunction(name);
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object?[] conversionData)
        {
            return (this as IFunction).GetGroupData(conversionData);
        }
    }

    /// <summary>
    /// Class for empty/none existing files (Usefull for refering files outside of the datapack)
    /// </summary>
    public class MockGroup<TItem> : BaseMockFile, IBlockType, IEntityType, IItemType, IFluidType, IFunction, IGroup<TItem> where TItem : IGroupable
    {
        /// <summary>
        /// Marks this as being a group
        /// </summary>
        public bool IsAGroup => true;

        private MockGroup(string fileName) : base(fileName)
        {

        }

        /// <summary>
        /// Intializes a new mock file
        /// </summary>
        /// <param name="packNamespace">The namespace the file is in</param>
        /// <param name="name">The name of the file</param>
        public MockGroup(BasePackNamespace packNamespace, string name) : base(packNamespace, name)
        {

        }

        /// <summary>
        /// Returns <see cref="GetNamespacedName"/>
        /// </summary>
        [CompoundPath(1)]
        public string Name => GetNamespacedName();

        /// <summary>
        /// Converts a string of the format NAMESPACE:Name into an mock file
        /// </summary>
        /// <param name="name">The string to convert</param>
        public static implicit operator MockGroup<TItem>(string name)
        {
            return new MockGroup<TItem>(name);
        }

        /// <summary>
        /// Returns the string used for refering this group
        /// </summary>
        /// <returns>The string used for refering this group</returns>
        public override string GetNamespacedName()
        {
            return "#" + PackNamespace.Name + ":" + FileId;
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object?[] conversionData)
        {
            return (this as IBlockType).GetGroupData(conversionData);
        }
    }
}
