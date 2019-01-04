using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for firework parts
    /// </summary>
    public class Firework
    {
        /// <summary>
        /// If the firework should flicker
        /// </summary>
        public bool? Flicker;

        /// <summary>
        /// if the firework explosion should have trails
        /// </summary>
        public bool? Trail;

        /// <summary>
        /// the type of the firework explosion
        /// </summary>
        public ID.Firework? Type;

        /// <summary>
        /// the colors of the explosion
        /// </summary>
        public HexColor[] Colors;

        /// <summary>
        /// the colors the explosion fades into
        /// </summary>
        public HexColor[] FadeColors;

        /// <summary>
        /// Gets the raw effect data
        /// </summary>
        /// <returns>the raw effect data used by the game</returns>
        public override string ToString()
        {
            List<string> TempList = new List<string>();

            if (Flicker != null) { TempList.Add("Flicker:" + Flicker); }
            if (Trail != null) { TempList.Add("Trail:" + Trail); }
            if (Type != null) { TempList.Add("Type:" + (int)Type); }
            if (Colors != null)
            {
                string TempString = "Colors:[I;";
                for (int a = 0; a < Colors.Length; a++)
                {
                    if (a != 0) { TempString += ","; }
                    TempString += Colors[a].ColorInt;
                }
                TempString += "]";
                TempList.Add(TempString);
            }
            if (FadeColors != null)
            {
                string TempString = "FadeColors:[I;";
                for (int a = 0; a < FadeColors.Length; a++)
                {
                    if (a != 0) { TempString += ","; }
                    TempString += FadeColors[a].ColorInt;
                }
                TempString += "]";
                TempList.Add(TempString);
            }

            return string.Join(",", TempList);
        }
    }
}
