using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for horses, llamas, donkeys and mules
    /// </summary>
    public class Horse : BreedableMob
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
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }
        /// <summary>
        /// The item the horse has as it's saddle
        /// </summary>
        [DataTag("SaddleItem")]
        public Item? HorseSaddle { get; set; }
        /// <summary>
        /// The armor the horse has on
        /// </summary>
        [DataTag("ArmorItem")]
        public Item? HorseArmor { get; set; }
        /// <summary>
        /// If the donkey has a chest
        /// </summary>
        [DataTag("ChestedHorse")]
        public bool? DonkeyChested { get; set; }
        /// <summary>
        /// The items inside the donkeys inventory
        /// </summary>
        [DataTag("Items")]
        public Item[]? DonkeyItems { get; set; }
        /// <summary>
        /// The horse variant
        /// </summary>
        [DataTag("Variant")]
        public Variant? HorseVariant { get; set; }
        /// <summary>
        /// A object used to defina a horse variant
        /// </summary>
        public class Variant : IConvertableToDataTag
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
            /// Converts this <see cref="Variant"/> object into a <see cref="DataPartTag"/>
            /// </summary>
            /// <param name="asType">Not used</param>
            /// <param name="extraConversionData">Not used</param>
            /// <returns>the made <see cref="DataPartTag"/></returns>
            public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
            {
                return new DataPartTag(GetValue());
            }

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
        [DataTag("Strength")]
        public int? LlamaStrenght { get; set; }
        /// <summary>
        /// The item the llama has on. (Normally carpet)
        /// </summary>
        [DataTag("DecorItem")]
        public Item? LlamaDecorItem { get; set; }
        /// <summary>
        /// If the skeleton horse is a trap
        /// </summary>
        [DataTag]
        public bool? SkeletonTrap { get; set; }
        /// <summary>
        /// The time the skeleton trap has existed. When at 18000 ticks it despawns
        /// </summary>
        [DataTag]
        public Time<int>? SkeletonTrapTime { get; set; }
        /// <summary>
        /// If the mob is tame
        /// </summary>
        [DataTag]
        public bool? Tame { get; set; }
    }
}
