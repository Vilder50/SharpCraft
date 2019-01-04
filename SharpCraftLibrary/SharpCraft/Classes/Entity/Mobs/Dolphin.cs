using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for dolphins
        /// </summary>
        public class Dolphin : BaseMob
        {
            /// <summary>
            /// Creates a new dolphin
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Dolphin(ID.Entity? type = ID.Entity.dolphin) : base(type) { }

            /// <summary>
            /// The location of the treasure the dolphin will lead the player to
            /// </summary>
            [DataTag]
            public Coords TreasureLocation { get; set; }
            /// <summary>
            /// If the dolphin has gotten any fish from the player
            /// </summary>
            [DataTag]
            public bool? GotFish { get; set; }
            /// <summary>
            /// If the dolphin will lead the player to a treasure
            /// </summary>
            [DataTag]
            public bool? CanFindTreasure { get; set; }
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
                    if (TreasureLocation != null) { TempList.Add("TreasurePosX:" + TreasureLocation.X + ",TreasurePosY:" + TreasureLocation.Y + ",TreasurePosZ:" + TreasureLocation.Z); }
                    if (GotFish != null) { TempList.Add("GotFish:" + GotFish + "b"); }
                    if (CanFindTreasure != null) { TempList.Add("CanFindTreasure:" + GotFish + "b"); }
                    return string.Join(",", TempList);
                }
            }
        }
    }
}
