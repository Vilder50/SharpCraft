using System;
using System.Linq;

namespace SharpCraft
{
    /// <summary>
    /// The base class for all entities
    /// </summary>
    public abstract class Entity : Data.DataHolderBase
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Entity> PathCreator => new Data.DataPathCreator<Entity>();

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Entity(ID.Entity? type)
        {
            EntityType = type;
        }

        /// <summary>
        /// Returns this entity's data without its entity type
        /// </summary>
        /// <returns>This entity's data without its entity type</returns>
        public string GetDataWithoutID()
        {
            Data.DataPartObject dataObject = GetDataTree();
            Data.DataPartPath? idPath = dataObject.GetValues().SingleOrDefault(p => p.PathName == "id");
            if (!(idPath is null))
            {
                idPath.PathValue = new Data.DataPartTag(null);
            }

            return dataObject.GetDataString();
        }

        /// <summary>
        /// The type of the entity
        /// </summary>
        [Data.DataTag("id", ForceType = ID.NBTTagType.TagString)]
        public ID.Entity? EntityType { get; set; }
    }
}
