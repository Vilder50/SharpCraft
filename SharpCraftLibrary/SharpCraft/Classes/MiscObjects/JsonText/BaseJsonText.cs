using System;
using System.Linq;
using System.Collections.Generic;
using SharpCraft.Data;
using SharpCraft.JsonText;

namespace SharpCraft
{
    /// <summary>
    /// Base class for json text
    /// </summary>
    public abstract class BaseJsonText : IConvertableToDataTag
    {
        /// <summary>
        /// The color of the text. (Cannot exist together with <see cref="RGBColor"/>)
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
        public BaseJsonText[]? Extra { get; set; }

        /// <summary>
        /// The font. (minecraft:default is the normal font)
        /// </summary>
        public string? Font { get; set; }

        /// <summary>
        /// The exact color of the text. (Cannot exist together with <see cref="Color"/>)
        /// </summary>
        public RGBColor? RGBColor { get; set; }

        /// <summary>
        /// Gets the raw JSON string
        /// </summary>
        /// <returns>the raw JSON string used by the game</returns>
        public virtual string GetJsonString()
        {
            List<string> outString = new List<string>();

            if (!(Color is null) && !(RGBColor is null))
            {
                throw new InvalidOperationException("Cannot get json string since both " + nameof(Color) + " and" + nameof(RGBColor) + " is set.");
            }
            if (!(Color is null)) { outString.Add("\"color\":\"" + Color + "\""); }
            if (!(RGBColor is null)) { outString.Add("\"color\":\"" + RGBColor.GetHexColor() + "\""); }
            if (!(Obfuscated is null)) { outString.Add("\"obfuscated\":" + Obfuscated.ToMinecraftBool()); }
            if (!(Bold is null)) { outString.Add("\"bold\":" + Bold.ToMinecraftBool()); }
            if (!(Italic is null)) { outString.Add("\"italic\":" + Italic.ToMinecraftBool()); }
            if (!(Strikethrough is null)) { outString.Add("\"strikethrough\":" + Strikethrough.ToMinecraftBool()); }
            if (!(Underline is null)) { outString.Add("\"underlined\":" + Underline.ToMinecraftBool()); }
            if (!(Reset is null)) { outString.Add("\"reset\":" + Reset.ToMinecraftBool()); }
            if (!(ShiftClickInsertion is null)) { outString.Add("\"insertion\":\"" + ShiftClickInsertion.Escape() + "\""); }
            if (!(Font is null)) { outString.Add("\"font\":\"" + Font.Escape() + "\""); }
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
        /// Returns a shallow clone of this <see cref="BaseJsonText"/>
        /// </summary>
        /// <returns>A shallow clone of this <see cref="BaseJsonText"/></returns>
        public virtual BaseJsonText ShallowClone()
        {
            return (BaseJsonText)MemberwiseClone();
        }

        /// <summary>
        /// Converts this <see cref="BaseJsonText"/> into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">Not used</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>This <see cref="BaseJsonText"/> as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartTag(GetJsonString());
        }

        /// <summary>
        /// Converts a single <see cref="BaseJsonText"/> into an array of <see cref="BaseJsonText"/> only containing the <see cref="BaseJsonText"/>
        /// </summary>
        /// <param name="jsonText">the <see cref="BaseJsonText"/> to convert</param>
        public static implicit operator BaseJsonText[](BaseJsonText jsonText)
        {
            return new BaseJsonText[] { jsonText };
        }

        /// <summary>
        /// Converts an array of <see cref="BaseJsonText"/> into a single <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="array">the array of <see cref="BaseJsonText"/> to convert</param>
        public static implicit operator BaseJsonText(BaseJsonText[] array)
        {
            BaseJsonText parent = array[0].ShallowClone();

            List<BaseJsonText> extraText = parent.Extra?.ToList() ?? new List<BaseJsonText>();
            extraText.AddRange(array[1..]);
            parent.Extra = extraText.ToArray();

            return parent;
        }

        /// <summary>
        /// Adds 2 <see cref="BaseJsonText"/>s together into one object
        /// </summary>
        /// <param name="text1">The parent <see cref="BaseJsonText"/></param>
        /// <param name="text2">The <see cref="BaseJsonText"/> to add to the parent</param>
        /// <returns>The <see cref="BaseJsonText"/>s added together</returns>
        public static BaseJsonText operator +(BaseJsonText text1, BaseJsonText text2)
        {
            BaseJsonText parent = text1.ShallowClone();

            List<BaseJsonText> extraText = parent.Extra?.ToList() ?? new List<BaseJsonText>();
            extraText.Add(text2);
            parent.Extra = extraText.ToArray();

            return parent;
        }

        /// <summary>
        /// Converts a string into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="text">the string to convert</param>
        public static implicit operator BaseJsonText(string text)
        {
            return new Text(text);
        }

        /// <summary>
        /// Converts a <see cref="ScoreValue"/> into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="score">the <see cref="ScoreValue"/> to convert</param>
        public static implicit operator BaseJsonText(ScoreValue score)
        {
            return new Score(score, score);
        }

        /// <summary>
        /// Converts a <see cref="Selector"/> into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="selector">the <see cref="Selector"/> to convert</param>
        public static implicit operator BaseJsonText(Selector selector)
        {
            return new Names(selector);
        }

        /// <summary>
        /// Converts a <see cref="ID.Key"/> into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="key">the <see cref="ID.Key"/> to convert</param>
        public static implicit operator BaseJsonText(ID.Key key)
        {
            return new KeyBind(key);
        }

        /// <summary>
        /// Converts a data location into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="dataLocation">the <see cref="ID.Key"/> to convert</param>
        public static implicit operator BaseJsonText(BlockDataLocation dataLocation)
        {
            return new JsonText.Data(dataLocation, true);
        }

        /// <summary>
        /// Converts a data location into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="dataLocation">the <see cref="ID.Key"/> to convert</param>
        public static implicit operator BaseJsonText(EntityDataLocation dataLocation)
        {
            return new JsonText.Data(dataLocation, true);
        }

        /// <summary>
        /// Converts a data location into a <see cref="BaseJsonText"/> object
        /// </summary>
        /// <param name="dataLocation">the <see cref="ID.Key"/> to convert</param>
        public static implicit operator BaseJsonText(StorageDataLocation dataLocation)
        {
            return new JsonText.Data(dataLocation, true);
        }
    }
}
