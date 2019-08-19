using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for firework parts
    /// </summary>
    public class Firework : Data.DataHolderBase
    {
        /// <summary>
        /// If the firework should flicker
        /// </summary>
        [Data.DataTag]
        public bool? Flicker { get; set; }

        /// <summary>
        /// if the firework explosion should have trails
        /// </summary>
        [Data.DataTag]
        public bool? Trail { get; set; }

        /// <summary>
        /// the type of the firework explosion
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
        public ID.Firework? Type { get; set; }

        /// <summary>
        /// the colors of the explosion
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagIntArray)]
        public HexColor[] Colors { get; set; }

        /// <summary>
        /// the colors the explosion fades into
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagIntArray)]
        public HexColor[] FadeColors { get; set; }

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
