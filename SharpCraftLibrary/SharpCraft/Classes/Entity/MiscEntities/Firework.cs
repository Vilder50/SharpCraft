using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for firework entities
        /// </summary>
        public class Firework : EntityBasic
        {
            /// <summary>
            /// Creates a new firework rocket
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Firework(ID.Entity? type = ID.Entity.firework_rocket) : base(type) { }

            /// <summary>
            /// Makes the firework stop flying upwards automatically
            /// </summary>
            [DataTag]
            public bool? Angled { get; set; }
            /// <summary>
            /// The amount of time the firework has been flying
            /// </summary>
            [DataTag]
            public Time Life { get; set; }
            /// <summary>
            /// The time before the firework blows up
            /// </summary>
            [DataTag]
            public Time LifeTime { get; set; }
            /// <summary>
            /// The firework displayed when the rocket blows up
            /// </summary>
            [DataTag]
            public Firework[] Fireworks { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Life != null) { TempList.Add("Life:" + Life.AsTicks()); }
                    if (LifeTime != null) { TempList.Add("LifeTime:" + LifeTime.AsTicks()); }
                    if (Fireworks != null)
                    {
                        string TempString = "FireworksItem:{id:firework_rocket,Count:1,tag:{Fireworks:{Explosions:[";
                        for (int a = 0; a < Fireworks.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Fireworks[a] + "}";
                        }
                        TempString += "]}}}";
                        TempList.Add(TempString);
                    }
                    if (Angled != null) { TempList.Add("ShotAtAngle:" + Angled.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
