using System;
using System.Linq;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The base class for all entities
        /// </summary>
        public abstract class BaseEntity : Data.DataHolderBase
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseEntity(ID.Entity? type)
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
                Data.DataPartPath idPath = dataObject.GetValues().SingleOrDefault(p => p.PathName == "id");
                if (!(idPath is null))
                {
                    idPath.PathValue = new Data.DataPartTag(null);
                }

                return dataObject.GetDataString();
            }

            /// <summary>
            /// The type of the entity
            /// </summary>
            [Data.DataTag("id", ForceType = ID.NBTTagType.TagNamespacedString)]
            public ID.Entity? EntityType { get; set; }
        }
    }
}
