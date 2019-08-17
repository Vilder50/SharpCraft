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
            [Data.DataTag]
            public bool? IsBaby { get; set; }
            /// <summary>
            /// If it can break doors
            /// </summary>
            [Data.DataTag]
            public bool? CanBreakDoors { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity the pigman is angry on
            /// </summary>
            [Data.DataTag("HurtBy", ForceType = ID.NBTTagType.TagString)]
            public UUID PigmanHurtUUID { get; set; }
            /// <summary>
            /// The time till the pigman stops being angry
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time PigmanAnger { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity converting the villager
            /// </summary>
            [Data.CustomDataTag]
            public UUID VillagerConvertUUID { get; set; }
            /// <summary>
            /// Time till it will be converted into a villager
            /// (-1 = not converting)
            /// </summary>
            [Data.DataTag("ConversionTime", ForceType = ID.NBTTagType.TagInt)]
            public Time VillagerConvertionTime { get; set; }

            /// <summary>
            /// The level of the villagers trading options
            /// </summary>
            [Data.CustomDataTag]
            public int? VillagerLevel {get; set;}

            /// <summary>
            /// The villager's proffession
            /// </summary>
            [Data.CustomDataTag]
            public ID.VillagerProffession? VillagerProffession {get; set;}

            /// <summary>
            /// The type of villager
            /// </summary>
            [Data.CustomDataTag]
            public ID.VillagerType? VillagerType {get; set;}

            /// <summary>
            /// The trades the villager has
            /// </summary>
            [Data.DataTag("Offers")]
            public Villager.Trade[] VillagerTrades { get; set; }

            /// <summary>
            /// The time it will take for the zombie to convert
            /// </summary>
            [Data.DataTag("DrownedConversionTime", ForceType = ID.NBTTagType.TagInt)]
            public Time ConvertionTime { get; set; }
            /// <summary>
            /// The time the zombie has been in water
            /// </summary>
            [Data.DataTag("InWaterTime", ForceType = ID.NBTTagType.TagInt)]
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
                    if (ConvertionTime != null) { TempList.Add("DrownedConversionTime:" + ConvertionTime.AsTicks()); }
                    if (WaterTime != null) { TempList.Add("InWaterTime:" + WaterTime.AsTicks()); }
                    if (VillagerLevel != null || VillagerProffession != null || VillagerType != null) 
                    {
                        List<string> typeList = new List<string>();
                        if (VillagerLevel != null) {typeList.Add("level:" + VillagerLevel);}
                        if (VillagerProffession != null) {typeList.Add("proffesion:\"" + VillagerProffession + "\"");}
                        if (VillagerType != null) {typeList.Add("type:\"" + VillagerType + "\"");}
                        TempList.Add("VillagerData:{" + string.Join(",", typeList) + "}");
                    }
                    if (VillagerTrades != null)
                    {
                        string TempString = "Offers:{Recipes:[";
                        for (int a = 0; a < VillagerTrades.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + VillagerTrades[a] + "}";
                        }
                        TempString += "]}";
                        TempList.Add(TempString);
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
