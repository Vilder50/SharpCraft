namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
        #pragma warning disable 1591
        public enum Color
        {
            white,
            orange,
            magenta,
            light_blue,
            yellow,
            lime,
            pink,
            gray,
            silver,
            cyan,
            purple,
            blue,
            brown,
            green,
            red,
            black
        }
        public enum Key
        {
            forward,
            left,
            back,
            right,
            jump,
            sneak,
            sprint,
            inventory,
            swapHands,
            drop,
            use,
            attack,
            pickItem,
            chat,
            playerlist,
            command,
            screenshot,
            togglePerspective,
            smoothCamera,
            fullscreen,
            spectatorOutlines,
            hotbar_1,
            hotbar_2,
            hotbar_3,
            hotbar_4,
            hotbar_5,
            hotbar_6,
            hotbar_7,
            hotbar_8,
            hotbar_9,
            saveToolbarActivator,
            loadToolbarActivator
        }
        public enum MinecraftColor { black, dark_blue, dark_green, dark_aqua, dark_red, dark_purple, gold, gray, dark_gray, blue, green, aqua, red, light_purple, yellow, white, }
        #pragma warning restore 1591
    }
}
