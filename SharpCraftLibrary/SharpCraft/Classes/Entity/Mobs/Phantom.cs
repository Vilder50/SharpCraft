using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for phantoms
        /// </summary>
        public class Phantom : BaseMob
        {
            /// <summary>
            /// Creates a new phantom
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Phantom(ID.Entity? type = ID.Entity.phantom) : base(type) { }

            /// <summary>
            /// The phantom will circle around this location when not attacking
            /// </summary>
            [DataTag]
            public Coords Area { get; set; }
            /// <summary>
            /// The size of the phantom.
            /// (0-64) Damage = 6+size
            /// </summary>
            [DataTag]
            public int? Size { get; set; }
            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Area != null) { TempList.Add("AX:" + Area.X + ",AY:" + Area.Y + ",AZ:" + Area.Z); }
                    if (Size != null) { TempList.Add("Size:" + Size); }
                    return string.Join(",", TempList);
                }
            }
        }
    }
}
