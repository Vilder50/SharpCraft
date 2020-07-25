using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public enum HorseMarkings
        {
            Normal,
            WhiteLegs,
            WhiteFields,
            BigWhiteDots,
            SmallBlackDots
        }
        public enum HorseColor
        {
            White,
            Creamy,
            Chestnut,
            Brown,
            Black,
            Gray,
            DarkBrown
        }
        public enum FishSize
        {
            Small,
            Large,
            Invisible,
        }
        public enum FishPattern
        {
            Flopper,
            Stripey,
            Flitter,
            Blockfish,
            Betty,
            Clayfish,
            NoPatter,
        }
        public enum DragonPhase
        {
            /// <summary>
            /// The ender dragon will circle around the island
            /// </summary>
            Circling,
            /// <summary>
            /// Flies to a player and fires a fireball
            /// </summary>
            Strafing,
            /// <summary>
            /// Flies to the portal
            /// </summary>
            FlyingToPortal,
            /// <summary>
            /// Lands on the portal
            /// </summary>
            LandingOnPortal,
            /// <summary>
            /// Taking of from the portal
            /// </summary>
            TakingOffFromPortal,
            /// <summary>
            /// Performs a breath attack while standing still
            /// </summary>
            LandedBreathAttack,
            /// <summary>
            /// Gets ready to perform a breath attack on a player
            /// </summary>
            LandedReadyBreathAttack,
            /// <summary>
            /// Roars before going to <see cref="DragonPhase.LandedReadyBreathAttack"/>
            /// </summary>
            LandedRoar,
            /// <summary>
            /// Charges a player
            /// </summary>
            ChargingPlayer,
            /// <summary>
            /// Flies to the portal to die there
            /// </summary>
            FlyingToPortalToDie,
            /// <summary>
            /// Enderdragon will not have any AI
            /// </summary>
            NoAI
        }
        public enum GossipType
        {
            major_negative,
            minor_negative,
            major_positive,
            minor_positive,
            trading,
            golem
        }
        public enum Panda { Lazy, Worried, Playful, Aggresive, Weak, Brown, Normal }
        public enum Cat { Tabby, Tuxedo, Red, Siamese, BritishShorthair, Calico, Persian, Ragdoll, White, Jellie, Black }
        public enum Fox { red, snow }
        public enum Parrot { red, blue, green, cyan, silver }
        public enum Rabbit { Brown, White, Black, Gray, Yellow, Light_Brown, Killer = 99 }
        public enum ShulkerDirection { down, up, north, south, west, east }
        public enum Boat { oak, spruce, birch, jungle, acacia, dark_oak }
        public enum Painting
        {
            Kekab,
            Aztec,
            Alban,
            Aztec2,
            Bomb,
            Plant,
            Wasteland,
            Wanderer,
            Graham,
            Courbet,
            Sunset,
            Sea,
            Creebet,
            Match,
            Bust,
            Stage,
            Void,
            SkullAndRoses,
            Wither,
            Fighters,
            Skeleton,
            DonkeyKong,
            Pointer,
            Pigscene,
            BurningSkull
        }
        public enum ArrowPickup
        {
            /// <summary>
            /// Players can't pick up the arrow
            /// </summary>
            CantPickUp,
            /// <summary>
            /// Players can pick up the arrow
            /// </summary>
            CanPickUp,
            /// <summary>
            /// Players in creative mode can pick the arrow
            /// </summary>
            CreativePickUp
        }
        public enum AttributeSlot
        {
            mainhand,
            offhand,
            feet,
            legs,
            chest,
            head
        }
        public enum AttributeOperation
        {
            /// <summary>
            /// Adds the number to the base
            /// </summary>
            addition,
            /// <summary>
            /// Multiplies the base number with all the given multiply_base modifiers at once
            /// </summary>
            multiply_base,
            /// <summary>
            /// Multiplies the total attribute value with the given number
            /// </summary>
            multiply_total
        }
#pragma warning restore 1591
    }
}
