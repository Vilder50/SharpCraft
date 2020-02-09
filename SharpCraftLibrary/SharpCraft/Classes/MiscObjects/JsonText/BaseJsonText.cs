using System;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// Base class for json text
    /// </summary>
    public abstract partial class JsonText
    {
        /// <summary>
        /// The color of the text
        /// </summary>
        public ID.MinecraftColor? Color { get; set; }

        /// <summary>
        /// If the text is obfuscated or not
        /// </summary>
        public bool? Obfuscated { get; set; }

        /// <summary>
        /// If the text is bold or not
        /// </summary>
        public bool? Bold { get; set; }

        /// <summary>
        /// If the text is italic or not
        /// </summary>
        public bool? Italic { get; set; }

        /// <summary>
        /// If the text is strikethroughed or not
        /// </summary>
        public bool? Strikethrough { get; set; }

        /// <summary>
        /// If the text is underlined or not
        /// </summary>
        public bool? Underline { get; set; }

        /// <summary>
        /// If the text should reset the text look defined by its parent
        /// </summary>
        public bool? Reset { get; set; }

        /// <summary>
        /// Text which should be inserted into the players chat when shift clicked
        /// </summary>
        public string ShiftClickInsertion { get; set; }

        /// <summary>
        /// The thing which should happen when the text is clicked
        /// </summary>
        public BaseClickEvent ClickEvent { get; set; }

        /// <summary>
        /// The the which shows up when the text is hovered over
        /// </summary>
        public BaseHoverEvent HoverEvent { get; set; }

        /// <summary>
        /// Gets the raw JSON string
        /// </summary>
        /// <returns>the raw JSON string used by the game</returns>
        public string GetJsonString()
        {
            List<string> outString = new List<string>();

            if (!(Color is null)) { outString.Add("\"color\":\"" + Color + "\""); }
            if (!(Obfuscated is null)) { outString.Add("\"obfuscated\":" + Obfuscated.ToMinecraftBool()); }
            if (!(Bold is null)) { outString.Add("\"bold\":" + Bold.ToMinecraftBool()); }
            if (!(Italic is null)) { outString.Add("\"italic\":" + Italic.ToMinecraftBool()); }
            if (!(Strikethrough is null)) { outString.Add("\"strikethrough\":" + Strikethrough.ToMinecraftBool()); }
            if (!(Underline is null)) { outString.Add("\"underlined\":" + Underline.ToMinecraftBool()); }
            if (!(Reset is null)) { outString.Add("\"reset\":" + Reset.ToMinecraftBool()); }
            if (!(ShiftClickInsertion is null)) { outString.Add("\"insertion\":\"" + ShiftClickInsertion.Escape() + "\""); }
            if (!(ClickEvent is null)) { outString.Add(ClickEvent.GetEventString()); }
            if (!(HoverEvent is null)) { outString.Add(HoverEvent.GetEventString()); }
            outString.Add(GetSpecificJsonString());

            return "{" + string.Join(",", outString) + "}";
        }

        /// <summary>
        /// Should return the part of the json string which is special for the object inheriting this class
        /// </summary>
        /// <returns>The special part of the json string gotten from the inheriting class</returns>
        protected abstract string GetSpecificJsonString();

        /// <summary>
        /// Converts a single <see cref="JsonText"/> into an array of <see cref="JsonText"/> only containing the <see cref="JsonText"/>
        /// </summary>
        /// <param name="jsonText">the <see cref="JsonText"/> to convert</param>
        public static implicit operator JsonText[](JsonText jsonText)
        {
            return new JsonText[] { jsonText };
        }

        /// <summary>
        /// Converts a string into a <see cref="JsonText"/> object
        /// </summary>
        /// <param name="text">the string to convert</param>
        public static implicit operator JsonText(string text)
        {
            return new Text(text);
        }

        /// <summary>
        /// Converts a <see cref="ScoreValue"/> into a <see cref="JsonText"/> object
        /// </summary>
        /// <param name="score">the <see cref="ScoreValue"/> to convert</param>
        public static implicit operator JsonText(ScoreValue score)
        {
            return new Score(score, score);
        }
    }
}
