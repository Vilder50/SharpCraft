using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for rabbits
        /// </summary>
        public class Rabbit : BaseBreedable
        {
            /// <summary>
            /// Creates a new rabbit
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Rabbit(ID.Entity? type = ID.Entity.rabbit) : base(type) { }

            /// <summary>
            /// The type of rabbit
            /// </summary>
            [DataTag]
            public ID.Rabbit? RabbitType { get; set; }
            /// <summary>
            /// Set to 40 when the rabbit has eaten a carrot.
            /// Goes down by 0-2 every tick.
            /// </summary>
            [DataTag]
            public int? MoreCarrotTicks { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BreedDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (RabbitType != null) { TempList.Add("RabbitType:" + (int)RabbitType); }
                    if (MoreCarrotTicks != null) { TempList.Add("MoreCarrotTicks:" + (int)MoreCarrotTicks); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
