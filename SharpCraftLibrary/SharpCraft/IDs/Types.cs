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
        /// The name of the block
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding a block id
    /// </summary>
    public class BlockType : IGroupable, Data.IConvertableToDataTag
    {
        /// <summary>
        /// Intializes a new <see cref="BlockType"/> from a <see cref="ID.Block"/>
        /// </summary>
        /// <param name="block">The id of this block</param>
        public BlockType(ID.Block block)
        {
            Name = "minecraft:" + block;
        }

        /// <summary>
        /// Intializes a new <see cref="BlockType"/>
        /// </summary>
        /// <param name="block">The id of this block</param>
        public BlockType(IBlockType block)
        {
            Name = block.Name;
        }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

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
        /// Implicit converts a <see cref="ID.Block"/> into a <see cref="BlockType"/> object
        /// </summary>
        /// <param name="block">The <see cref="ID.Block"/> to convert</param>

        public static implicit operator BlockType(ID.Block block)
        {
            return new BlockType(block);
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
        /// The name of the item
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an item id
    /// </summary>
    public class ItemType : IGroupable
    {
        /// <summary>
        /// Intializes a new <see cref="ItemType"/> from a <see cref="ID.Item"/>
        /// </summary>
        /// <param name="item">The id of this item</param>
        public ItemType(ID.Item item)
        {
            Name = "minecraft:" + item.ToString().ToLower();
        }

        /// <summary>
        /// Intializes a new <see cref="ItemType"/>
        /// </summary>
        /// <param name="item">The id of this item</param>
        public ItemType(IItemType item)
        {
            Name = item.Name;
        }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

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
    }

    /// <summary>
    /// Interface for classes which can be converted into <see cref="EntityType"/>
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// The name of the entity
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an entity id
    /// </summary>
    public class EntityType : IGroupable
    {
        /// <summary>
        /// Intializes a new <see cref="EntityType"/> from a <see cref="ID.Entity"/>
        /// </summary>
        /// <param name="entity">The id of this entity</param>
        public EntityType(ID.Entity entity)
        {
            Name = "minecraft:" + entity;
        }

        /// <summary>
        /// Intializes a new <see cref="EntityType"/>
        /// </summary>
        /// <param name="entity">The id of this entity</param>
        public EntityType(IEntityType entity)
        {
            Name = entity.Name;
        }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Implicit converts a <see cref="ID.Entity"/> into a <see cref="EntityType"/> object
        /// </summary>
        /// <param name="entity">The <see cref="ID.Entity"/> to convert</param>

        public static implicit operator EntityType(ID.Entity entity)
        {
            return new EntityType(entity);
        }
    }

    /// <summary>
    /// Interface for classes which can be converted into <see cref="LiquidType"/>
    /// </summary>
    public interface ILiquidType
    {
        /// <summary>
        /// The name of the liquid
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Class for holding an liquid id
    /// </summary>
    public class LiquidType : IGroupable
    {
        /// <summary>
        /// Intializes a new <see cref="LiquidType"/> from a <see cref="ID.Liquid"/>
        /// </summary>
        /// <param name="liquid">The id of this liquid</param>
        public LiquidType(ID.Liquid liquid)
        {
            Name = "minecraft:" + liquid;
        }

        /// <summary>
        /// Intializes a new <see cref="LiquidType"/>
        /// </summary>
        /// <param name="liquid">The id of this liquid</param>
        public LiquidType(ILiquidType liquid)
        {
            Name = liquid.Name;
        }

        /// <summary>
        /// The id
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Implicit converts a <see cref="ID.Liquid"/> into a <see cref="LiquidType"/> object
        /// </summary>
        /// <param name="liquid">The <see cref="ID.Liquid"/> to convert</param>

        public static implicit operator LiquidType(ID.Liquid liquid)
        {
            return new LiquidType(liquid);
        }
    }
}
