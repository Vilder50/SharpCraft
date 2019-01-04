using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for horses, llamas, donkeys and mules
        /// </summary>
        public class Horse : BaseBreedable
        {
            /// <summary>
            /// Creates a new horse, llama, donkeys or mule
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Horse(ID.Entity? type) : base(type) { }

            /// <summary>
            /// Makes the mob stay close to other mobs of the same time with this tag being true
            /// </summary>
            [DataTag]
            public bool? Bred { get; set; }
            /// <summary>
            /// Makes the horse graze
            /// </summary>
            [DataTag]
            public bool? EatingHayStack { get; set; }
            /// <summary>
            /// Makes the horse easier to tame.
            /// (Goes up when the horse is fed.)
            /// (A number between 0-100. the higher the easier to tame)
            /// </summary>
            [DataTag]
            public int? Temper { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the owner of the horse
            /// </summary>
            [DataTag]
            public UUID OwnerUUID { get; set; }
            /// <summary>
            /// The item the horse has as it's saddle
            /// </summary>
            [DataTag]
            public Item HorseSaddle { get; set; }
            /// <summary>
            /// The armor the horse has on
            /// </summary>
            [DataTag]
            public Item HorseArmor { get; set; }
            /// <summary>
            /// If the donkey has a chest
            /// </summary>
            [DataTag]
            public bool? DonkeyChested { get; set; }
            /// <summary>
            /// The items inside the donkeys inventory
            /// </summary>
            [DataTag]
            public Item[] DonkeyItems { get; set; }
            /// <summary>
            /// The horse variant
            /// </summary>
            [DataTag]
            public Variant HorseVariant { get; set; }
            /// <summary>
            /// A object used to defina a horse variant
            /// </summary>
            public class Variant
            {
                /// <summary>
                /// The color of the horse
                /// </summary>
                public ID.HorseColor Color { get; set; }
                /// <summary>
                /// The markings
                /// </summary>
                public ID.HorseMarkings Markings { get; set; }

                /// <summary>
                /// Gets the value Minecraft uses to define horse variants
                /// </summary>
                /// <returns>Raw data used by Minecraft</returns>
                public int GetValue()
                {
                    return (int)Color + (int)Markings * 256;
                }
            }
            /// <summary>
            /// How many items the llama can hold
            /// (1-5. Slots = x * 3)
            /// </summary>
            [DataTag]
            public int? LlamaStrenght { get; set; }
            /// <summary>
            /// The item the llama has on. (Normally carpet)
            /// </summary>
            [DataTag]
            public Item LlamaDecorItem { get; set; }
            /// <summary>
            /// If the skeleton horse is a trap
            /// </summary>
            [DataTag]
            public bool? SkeletonTrap { get; set; }
            /// <summary>
            /// The time the skeleton trap has existed. When at 18000 ticks it despawns
            /// </summary>
            [DataTag]
            public Time SkeletonTrapTime { get; set; }
            /// <summary>
            /// If the mob is tame
            /// </summary>
            [DataTag]
            public bool? Tame { get; set; }

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
                    if (Bred != null) { TempList.Add("Bred:" + Bred.ToMinecraftBool()); }
                    if (Tame != null) { TempList.Add("Tame:" + Tame.ToMinecraftBool()); }
                    if (EatingHayStack != null) { TempList.Add("EatingHayStack:" + EatingHayStack.ToMinecraftBool()); }
                    if (Temper != null) { TempList.Add("Temper:" + Temper); }
                    if (OwnerUUID != null) { TempList.Add("OwnerUUID:" + OwnerUUID); }
                    if (HorseSaddle != null) { TempList.Add("SaddleItem:{" + HorseSaddle.DataString + "}"); }
                    if (HorseArmor != null) { TempList.Add("SaddleItem:{" + HorseSaddle.DataString + "}"); }
                    if (DonkeyChested != null) { TempList.Add("ChestedHorse:" + DonkeyChested.ToMinecraftBool()); }
                    if (HorseVariant != null) { TempList.Add("Variant:" + HorseVariant.GetValue()); }
                    if (LlamaStrenght != null) { TempList.Add("Strenght:" + LlamaStrenght); }
                    if (LlamaDecorItem != null) { TempList.Add("DecorItem:{" + LlamaDecorItem.DataString + "}"); }
                    if (SkeletonTrap != null) { TempList.Add("SkeletonTrap:" + SkeletonTrap.ToMinecraftBool()); }
                    if (SkeletonTrapTime != null) { TempList.Add("SkeletonTrapTime:" + SkeletonTrapTime.AsTicks()); }
                    if (DonkeyItems != null)
                    {
                        string TempString = "Items:[";
                        for (int a = 0; a < DonkeyItems.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + DonkeyItems[a].DataString + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
