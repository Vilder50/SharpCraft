using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for zombie (-pigmen and -villagers) and drowneds
        /// </summary>
        public class Zombie : BaseMob
        {
            /// <summary>
            /// Creates a new zombie -pigman or -villager or drowned
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Zombie(ID.Entity? type) : base(type) { }

            /// <summary>
            /// If its a baby
            /// </summary>
            [DataTag]
            public bool? IsBaby { get; set; }
            /// <summary>
            /// If it can break doors
            /// </summary>
            [DataTag]
            public bool? CanBreakDoors { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity the pigman is angry on
            /// </summary>
            [DataTag]
            public UUID PigmanHurtUUID { get; set; }
            /// <summary>
            /// The time till the pigman stops being angry
            /// </summary>
            [DataTag]
            public Time PigmanAnger { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity converting the villager
            /// </summary>
            [DataTag]
            public UUID VillagerConvertUUID { get; set; }
            /// <summary>
            /// Time till it will be converted into a villager
            /// (-1 = not converting)
            /// </summary>
            [DataTag]
            public Time VillagerConvertionTime { get; set; }
            /// <summary>
            /// The type of villager
            /// </summary>
            [DataTag]
            public ID.Villager? VillagerProfession { get; set; }
            /// <summary>
            /// The time it will take for the zombie to convert
            /// </summary>
            [DataTag]
            public Time ConvertionTime { get; set; }
            /// <summary>
            /// The time the zombie has been in water
            /// </summary>
            [DataTag]
            public Time WaterTime { get; set; }

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
                    if (IsBaby != null) { TempList.Add("IsBaby:" + IsBaby); }
                    if (CanBreakDoors != null) { TempList.Add("CanBreakDoors:" + CanBreakDoors); }
                    if (PigmanHurtUUID != null) { TempList.Add("PigmanHurtUUID:" + PigmanHurtUUID); }
                    if (PigmanAnger != null) { TempList.Add("Anger:" + PigmanAnger.AsTicks() + "s"); }
                    if (VillagerConvertUUID != null) { TempList.Add("ConversionPlayerMost:" + VillagerConvertUUID.Most + "L,ConversionPlayerLeast:" + VillagerConvertUUID.Least + "L"); }
                    if (VillagerConvertionTime != null) { TempList.Add("ConversionTime:" + VillagerConvertionTime); }
                    if (VillagerProfession != null)
                    {
                        switch ((int)VillagerProfession)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                TempList.Add("Profession:0");
                                break;
                            case 4:
                            case 5:
                                TempList.Add("Profession:1");
                                break;
                            case 6:
                                TempList.Add("Profession:2");
                                break;
                            case 7:
                            case 8:
                            case 9:
                                TempList.Add("Profession:3");
                                break;
                            case 10:
                            case 11:
                                TempList.Add("Profession:4");
                                break;
                            case 12:
                                TempList.Add("Profession:5");
                                break;
                        }
                    }
                    if (ConvertionTime != null) { TempList.Add("DrownedConversionTime:" + ConvertionTime.AsTicks()); }
                    if (WaterTime != null) { TempList.Add("InWaterTime:" + WaterTime.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
