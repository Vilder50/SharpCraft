using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for <see cref="ID.Entity.egg"/>s, <see cref="ID.Entity.ender_pearl"/>s, <see cref="ID.Entity.experience_bottle"/>s, <see cref="ID.Entity.potion"/>s and <see cref="ID.Entity.snowball"/>s
        /// </summary>
        public class Throwable : BaseProjectile
        {
            /// <summary>
            /// Creates a new <see cref="ID.Entity.egg"/>, <see cref="ID.Entity.ender_pearl"/>, <see cref="ID.Entity.experience_bottle"/>, <see cref="ID.Entity.potion"/> or <see cref="ID.Entity.snowball"/>
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Throwable(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The entity shaking when hitting a block
            /// </summary>
            [DataTag]
            public int? Shake { get; set; }
            /// <summary>
            /// The owner of the projectile
            /// </summary>
            [DataTag]
            public UUID Owner { get; set; }
            /// <summary>
            /// The type of thrown potion
            /// </summary>
            [DataTag]
            public SharpCraft.Item Potion { get; set; }
            /// <summary>
            /// The item the entity is displayed as
            /// (Potions do not support use this)
            /// </summary>
            [DataTag]
            public SharpCraft.Item DisplayItem { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = ProjectileDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Potion != null) { TempList.Add("Potion:{" + Potion.DataString + "}"); }
                    if (Shake != null) { TempList.Add("shake:" + Shake + "b"); }
                    if (Owner != null) { TempList.Add("owner:{L:" + Owner.Least + "M:" + Owner.Most + "}"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
