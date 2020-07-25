using SharpCraft.Data;
using System;

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public enum Firework { Small, Large, Star, Creeper, Burst }
        public enum BookGeneration { Original, Copy, CopyCopy, Tattered }
        public enum MapMarker { player, frame, red_marker, blue_marker, target_x, target_point, player_off_map, player_off_limits, mansion, monument, banner_white, banner_orange, banner_magenta, banner_light_blue, banner_yellow, banner_lime, banner_pink, banner_gray, banner_light_gray, banner_cyan, banner_purple, banner_blue, banner_brown, banner_green, banner_red, banner_black, red_x }
#pragma warning restore 1591
    }
}
