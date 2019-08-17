using System;

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
            /// This entity's data without its type
            /// </summary>
            public abstract string DataString { get; }

            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseEntity(ID.Entity? type)
            {
                EntityType = type;
            }

            /// <summary>
            /// This entity's data with the entity's type
            /// </summary>
            public string DataWithID
            {
                get
                {
                    string TempString = "";
                    if (EntityType != null) { TempString += "id:" + EntityType.Value; }
                    if (DataString != "") { TempString += "," + DataString; }
                    return TempString.TrimStart(',');
                }
            }

            /// <summary>
            /// The type of the entity
            /// </summary>
            public ID.Entity? EntityType { get; set; }
        }
    }
}
