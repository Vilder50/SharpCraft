using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Interface for classes which can be converted into <see cref="BlockType"/>
    /// </summary>
    public interface IBlockType
    {
        /// <summary>
        /// The block
        /// </summary>
        object Value { get; }

        /// <summary>
        /// The name of the block
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding a block id
    /// </summary>
    public class BlockType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {
        /// <summary>
        /// Intializes a new <see cref="BlockType"/> from a <see cref="ID.Block"/>
        /// </summary>
        /// <param name="block">The id of this block</param>
        public BlockType(ID.Block block)
        {
            Name = "minecraft:" + block;
            Value = block;
        }

        /// <summary>
        /// Intializes a new <see cref="BlockType"/> from a <see cref="ID.Liquid"/>
        /// </summary>
        /// <param name="liquid">The id of this liquid</param>
        public BlockType(ID.Liquid liquid)
        {
            Name = "minecraft:" + liquid;
            Value = liquid;
        }

        /// <summary>
        /// Intializes a new <see cref="BlockType"/>
        /// </summary>
        /// <param name="block">The id of this block</param>
        public BlockType(IBlockType block)
        {
            Name = block.Name;
            Value = block.Value;
        }

        /// <summary>
        /// The block
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Returns <see cref="Name"/>
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Converts this <see cref="BlockType"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type of tag to get. Set to null or <see cref="ID.NBTTagType.TagString"/></param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>This <see cref="BlockType"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (!(asType is null) && asType != ID.NBTTagType.TagString) 
            {
                throw new InvalidCastException("Cannot convert BlockTypeID into the given type");
            }

            return new DataPartTag(Name);
        }


        /// <summary>
        /// specifies wether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to check if equal</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            return obj is BlockType type &&
                   Name == type.Name;
        }

        /// <summary>
        /// Returns this object's HashCode
        /// </summary>
        /// <returns>this object's HashCode</returns>
        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(conversionData),"Need to get 3 conversion data.");
            }
            bool? json = conversionData[2] as bool?;
            if (json is null)
            {
                throw new ArgumentException(nameof(conversionData), "3rd conversion object has to be a bool");
            }

            DataPartObject returnObject = new DataPartObject();
            if (Value is BaseGroup<BlockType> || Value is EmptyGroup<BlockType>)
            {
                returnObject.AddValue(new DataPartPath(conversionData[1].ToString(), new DataPartTag(Name.Substring(1), isJson: json.Value), json.Value));
            }
            else
            {
                returnObject.AddValue(new DataPartPath(conversionData[0].ToString(), new DataPartTag(Name, isJson: json.Value), json.Value));
            }
            return returnObject;
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Block"/> into a <see cref="BlockType"/> object
        /// </summary>
        /// <param name="block">The <see cref="ID.Block"/> to convert</param>

        public static implicit operator BlockType(ID.Block block)
        {
            return new BlockType(block);
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Liquid"/> into a <see cref="BlockType"/> object
        /// </summary>
        /// <param name="liquid">The <see cref="ID.Liquid"/> to convert</param>

        public static implicit operator BlockType(ID.Liquid liquid)
        {
            return new BlockType(liquid);
        }

        /// <summary>
        /// Operator for checking if a <see cref="BlockType"/> is equal to a <see cref="ID.Block"/>
        /// </summary>
        /// <param name="type">The <see cref="BlockType"/> to check with</param>
        /// <param name="block">The <see cref="ID.Block"/> to check with</param>
        /// <returns>True if they are equal</returns>
        public static bool operator == (BlockType type, ID.Block block)
        {
            return type.Name == "minecraft:" + block;
        }

        /// <summary>
        /// Operator for checking if a <see cref="BlockType"/> is not equal to a <see cref="ID.Block"/>
        /// </summary>
        /// <param name="type">The <see cref="BlockType"/> to check with</param>
        /// <param name="block">The <see cref="ID.Block"/> to check with</param>
        /// <returns>True if they are not equal</returns>
        public static bool operator != (BlockType type, ID.Block block)
        {
            return type.Name != "minecraft:" + block;
        }
    }

    /// <summary>
    /// Interface for classes which can be converted into <see cref="ItemType"/>
    /// </summary>
    public interface IItemType
    {
        /// <summary>
        /// The item
        /// </summary>
        object Value { get; }

        /// <summary>
        /// The name of the item
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an item id
    /// </summary>
    public class ItemType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {
        /// <summary>
        /// Intializes a new <see cref="ItemType"/> from a <see cref="ID.Item"/>
        /// </summary>
        /// <param name="item">The id of this item</param>
        public ItemType(ID.Item item)
        {
            Name = "minecraft:" + item.MinecraftValue();
            Value = item;
        }

        /// <summary>
        /// Intializes a new <see cref="ItemType"/>
        /// </summary>
        /// <param name="item">The id of this item</param>
        public ItemType(IItemType item)
        {
            Name = item.Name;
            Value = item.Value;
        }

        /// <summary>
        /// The item
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Returns <see cref="Name"/>
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Item"/> into a <see cref="ItemType"/> object
        /// </summary>
        /// <param name="item">The <see cref="ID.Item"/> to convert</param>

        public static implicit operator ItemType(ID.Item item)
        {
            return new ItemType(item);
        }

        /// <summary>
        /// Operator for checking if a <see cref="ItemType"/> is equal to a <see cref="ID.Item"/>
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to check with</param>
        /// <param name="item">The <see cref="ID.Item"/> to check with</param>
        /// <returns>True if they are equal</returns>
        public static bool operator == (ItemType type, ID.Item item)
        {
            return type.Name == "minecraft:" + item.MinecraftValue();
        }

        /// <summary>
        /// Operator for checking if a <see cref="ItemType"/> is not equal to a <see cref="ID.Item"/>
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to check with</param>
        /// <param name="item">The <see cref="ID.Item"/> to check with</param>
        /// <returns>True if they are not equal</returns>
        public static bool operator != (ItemType type, ID.Item item)
        {
            return type.Name != "minecraft:" + item.MinecraftValue();
        }

        /// <summary>
        /// specifies wether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to check if equal</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            return obj is ItemType type &&
                   Name == type.Name;
        }

        /// <summary>
        /// Returns this object's HashCode
        /// </summary>
        /// <returns>this object's HashCode</returns>
        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        /// <summary>
        /// Converts this <see cref="ItemType"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type of tag to get. Set to null or <see cref="ID.NBTTagType.TagString"/></param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>This <see cref="ItemType"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (!(asType is null) && asType != ID.NBTTagType.TagString)
            {
                throw new InvalidCastException("Cannot convert ItemType into the given type");
            }

            return new DataPartTag(Name);
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(conversionData), "Need to get 3 conversion data.");
            }
            bool? json = conversionData[2] as bool?;
            if (json is null)
            {
                throw new ArgumentException(nameof(conversionData), "3rd conversion object has to be a bool");
            }

            DataPartObject returnObject = new DataPartObject();
            if (Value is BaseGroup<ItemType> || Value is EmptyGroup<ItemType>)
            {
                returnObject.AddValue(new DataPartPath(conversionData[1].ToString(), new DataPartTag(Name.Substring(1), isJson: json.Value), json.Value));
            }
            else
            {
                returnObject.AddValue(new DataPartPath(conversionData[0].ToString(), new DataPartTag(Name, isJson: json.Value), json.Value));
            }
            return returnObject;
        }
    }

    /// <summary>
    /// Interface for classes which can be converted into <see cref="EntityType"/>
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// The entity
        /// </summary>
        object Value { get; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an entity id
    /// </summary>
    public class EntityType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {
        /// <summary>
        /// Intializes a new <see cref="EntityType"/> from a <see cref="ID.Entity"/>
        /// </summary>
        /// <param name="entity">The id of this entity</param>
        public EntityType(ID.Entity entity)
        {
            Name = "minecraft:" + entity;
            Value = entity;
        }

        /// <summary>
        /// Intializes a new <see cref="EntityType"/>
        /// </summary>
        /// <param name="entity">The id of this entity</param>
        public EntityType(IEntityType entity)
        {
            Name = entity.Name;
            Value = entity.Value;
        }

        /// <summary>
        /// The entity
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Returns <see cref="Name"/>
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Entity"/> into a <see cref="EntityType"/> object
        /// </summary>
        /// <param name="entity">The <see cref="ID.Entity"/> to convert</param>

        public static implicit operator EntityType(ID.Entity entity)
        {
            return new EntityType(entity);
        }

        /// <summary>
        /// Converts this <see cref="EntityType"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type of tag to get. Set to null or <see cref="ID.NBTTagType.TagString"/></param>
        /// <param name="extraConversionData">Not in use</param>
        /// <returns>This <see cref="EntityType"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (!(asType is null) && asType != ID.NBTTagType.TagString)
            {
                throw new InvalidCastException("Cannot convert EntityType into the given type");
            }

            return new DataPartTag(Name);
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(conversionData), "Need to get 3 conversion data.");
            }
            bool? json = conversionData[2] as bool?;
            if (json is null)
            {
                throw new ArgumentException(nameof(conversionData), "3rd conversion object has to be a bool");
            }

            DataPartObject returnObject = new DataPartObject();
            if (Value is BaseGroup<EntityType> || Value is EmptyGroup<EntityType>)
            {
                returnObject.AddValue(new DataPartPath(conversionData[1].ToString(), new DataPartTag(Name.Substring(1), isJson: json.Value), json.Value));
            }
            else
            {
                returnObject.AddValue(new DataPartPath(conversionData[0].ToString(), new DataPartTag(Name, isJson: json.Value), json.Value));
            }
            return returnObject;
        }
    }

    /// <summary>
    /// Interface for classes which can be converted into <see cref="LiquidType"/>
    /// </summary>
    public interface ILiquidType
    {
        /// <summary>
        /// The liquid
        /// </summary>
        object Value { get; }

        /// <summary>
        /// The name of the liquid
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an liquid id
    /// </summary>
    public class LiquidType : IGroupable, IConvertableToDataObject
    {
        /// <summary>
        /// Intializes a new <see cref="LiquidType"/> from a <see cref="ID.Liquid"/>
        /// </summary>
        /// <param name="liquid">The id of this liquid</param>
        public LiquidType(ID.Liquid liquid)
        {
            Name = "minecraft:" + liquid;
            Value = liquid;
        }

        /// <summary>
        /// Intializes a new <see cref="LiquidType"/> from a <see cref="ID.Block"/>
        /// </summary>
        /// <param name="block">The id of this block</param>
        public LiquidType(ID.Block block)
        {
            Name = "minecraft:" + block;
            Value = block;
        }

        /// <summary>
        /// Intializes a new <see cref="LiquidType"/>
        /// </summary>
        /// <param name="liquid">The id of this liquid</param>
        public LiquidType(ILiquidType liquid)
        {
            Name = liquid.Name;
            Value = liquid.Value;
        }

        /// <summary>
        /// The liquid
        /// </summary>
        public object Value { get; protected set; }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Returns <see cref="Name"/>
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Liquid"/> into a <see cref="LiquidType"/> object
        /// </summary>
        /// <param name="liquid">The <see cref="ID.Liquid"/> to convert</param>

        public static implicit operator LiquidType(ID.Liquid liquid)
        {
            return new LiquidType(liquid);
        }

        /// <summary>
        /// Implicit converts a <see cref="ID.Block"/> into a <see cref="LiquidType"/> object
        /// </summary>
        /// <param name="block">The <see cref="ID.Block"/> to convert</param>

        public static implicit operator LiquidType(ID.Block block)
        {
            return new LiquidType(block);
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(conversionData), "Need to get 3 conversion data.");
            }
            bool? json = conversionData[2] as bool?;
            if (json is null)
            {
                throw new ArgumentException(nameof(conversionData), "3rd conversion object has to be a bool");
            }

            DataPartObject returnObject = new DataPartObject();
            if (Value is BaseGroup<LiquidType> || Value is EmptyGroup<LiquidType>)
            {
                returnObject.AddValue(new DataPartPath(conversionData[1].ToString(), new DataPartTag(Name.Substring(1), isJson: json.Value), json.Value));
            }
            else
            {
                returnObject.AddValue(new DataPartPath(conversionData[0].ToString(), new DataPartTag(Name, isJson: json.Value), json.Value));
            }
            return returnObject;
        }
    }
}
