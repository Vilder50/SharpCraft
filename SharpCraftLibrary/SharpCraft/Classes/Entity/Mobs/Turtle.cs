using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for turtles
        /// </summary>
        public class Turtle : BaseBreedable
        {
            /// <summary>
            /// Creates a new turtle
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Turtle(ID.Entity? type = ID.Entity.turtle) : base(type) { }

            /// <summary>
            /// The location of the turtles home
            /// </summary>
            [Data.CustomDataTag]
            public Coords HomePos { get; set; }
            /// <summary>
            /// The location the turtle is traveling to
            /// </summary>
            [Data.CustomDataTag]
            public Coords TravelPos { get; set; }
            /// <summary>
            /// True if the turtle has eggs
            /// </summary>
            [Data.DataTag]
            public bool? HasEgg { get; set; }
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
                    if (HomePos != null) { TempList.Add("HomePosX:" + HomePos.X + ",HomePosY:" + HomePos.Y + ",HomePosZ:" + HomePos.Z); }
                    if (TravelPos != null) { TempList.Add("TravelPosX:" + TravelPos.X + ",TravelPosY:" + TravelPos.Y + ",TravelPosZ:" + TravelPos.Z); }
                    if (HasEgg != null) { TempList.Add("HasEgg:" + HasEgg.ToMinecraftBool()); }
                    return string.Join(",", TempList);
                }
            }
        }
    }
}
