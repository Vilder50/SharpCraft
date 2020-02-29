using System;
using System.Linq;
using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Base class for json text
    /// </summary>
    public abstract partial class JsonText : IConvertableToDataTag
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
        public string? ShiftClickInsertion { get; set; }

        /// <summary>
        /// The thing which should happen when the text is clicked
        /// </summary>
        public BaseClickEvent? ClickEvent { get; set; }

        /// <summary>
        /// The the which shows up when the text is hovered over
        /// </summary>
        public BaseHoverEvent? HoverEvent { get; set; }

        /// <summary>
        /// Extra text
        /// </summary>
        public JsonText[]? Extra { get; set; }

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
            if (!(Extra is null) && Extra.Length != 0)  { outString.Add("\"extra\":[" + string.Join(",",Extra.Select(j => j.GetJsonString())) + "]"); }
            outString.Add(GetSpecificJsonString());

            return "{" + string.Join(",", outString) + "}";
        }

        /// <summary>
        /// Should return the part of the json string which is special for the object inheriting this class
        /// </summary>
        /// <returns>The special part of the json string gotten from the inheriting class</returns>
        protected abstract string GetSpecificJsonString();

        /// <summary>
        /// Returns a shallow clone of this <see cref="JsonText"/>
        /// </summary>
        /// <returns>A shallow clone of this <see cref="JsonText"/></returns>
        public virtual JsonText ShallowClone()
        {
            return (JsonText)MemberwiseClone();
        }

        /// <summary>
        /// Converts this <see cref="JsonText"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not used</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>This <see cref="JsonText"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData)
        {
            return new DataPartTag(GetJsonString());
        }

        /// <summary>
        /// Converts a single <see cref="JsonText"/> into an array of <see cref="JsonText"/> only containing the <see cref="JsonText"/>
        /// </summary>
        /// <param name="jsonText">the <see cref="JsonText"/> to convert</param>
        public static implicit operator JsonText[](JsonText jsonText)
        {
            return new JsonText[] { jsonText };
        }

        /// <summary>
        /// Converts an array of <see cref="JsonText"/> into a single <see cref="JsonText"/> object
        /// </summary>
        /// <param name="array">the array of <see cref="JsonText"/> to convert</param>
        public static implicit operator JsonText(JsonText[] array)
        {
            JsonText parent = array[0].ShallowClone();

            List<JsonText> extraText = parent.Extra?.ToList() ?? new List<JsonText>();
            extraText.AddRange(array[1..]);
            parent.Extra = extraText.ToArray();

            return parent;
        }

        /// <summary>
        /// Adds 2 <see cref="JsonText"/>s together into one object
        /// </summary>
        /// <param name="text1">The parent <see cref="JsonText"/></param>
        /// <param name="text2">The <see cref="JsonText"/> to add to the parent</param>
        /// <returns>The <see cref="JsonText"/>s added together</returns>
        public static JsonText operator +(JsonText text1, JsonText text2)
        {
            JsonText parent = text1.ShallowClone();

            List<JsonText> extraText = parent.Extra?.ToList() ?? new List<JsonText>();
            extraText.Add(text2);
            parent.Extra = extraText.ToArray();

            return parent;
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

        /// <summary>
        /// Converts a <see cref="Selector"/> into a <see cref="JsonText"/> object
        /// </summary>
        /// <param name="selector">the <see cref="Selector"/> to convert</param>
        public static implicit operator JsonText(Selector selector)
        {
            return new Names(selector);
        }

        /// <summary>
        /// Converts a <see cref="ID.Key"/> into a <see cref="JsonText"/> object
        /// </summary>
        /// <param name="key">the <see cref="ID.Key"/> to convert</param>
        public static implicit operator JsonText(ID.Key key)
        {
            return new KeyBind(key);
        }
    }
}
