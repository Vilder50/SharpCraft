using System;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object used for JSON text
    /// (fx used in tellraw)
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// Creates the base of a new JSON string
        /// </summary>
        public JSON() { }
        /// <summary>
        /// Creates a JSON string only containing text
        /// Not that this does not support anything else than the text inputted
        /// </summary>
        /// <param name="BasicText">the text the json should show</param>
        public JSON(string BasicText)
        {
            this.BasicText = true;
            Text = BasicText;
        }

        /// <summary>
        /// Creates a JSON string with the specified color
        /// </summary>
        /// <param name="Text">the text to display</param>
        /// <param name="Color">the color of the text</param>
        public JSON(string Text, ID.MinecraftColor Color)
        {
            this.Text = Text;
            this.Color = Color;
        }
        internal bool? BasicText;

        /// <summary>
        /// The text to show
        /// </summary>
        public string Text;

        /// <summary>
        /// The "to be translated" text to show
        /// </summary>
        public string Translate;

        /// <summary>
        /// The JSON strings to insert into the translated text
        /// </summary>
        public JSON[] With;

        /// <summary>
        /// The selector to show
        /// Note if <see cref="Objective"/> is set it will show a score instead. And if <see cref="DataPath"/> is set it will show data for the entity instead.
        /// </summary>
        public Selector Selector;

        /// <summary>
        /// The minecraft keyboard key to show
        /// </summary>
        public ID.Keys? Key;

        /// <summary>
        /// The color of the text
        /// </summary>
        public ID.MinecraftColor? Color;

        /// <summary>
        /// If the text is obfuscated or not
        /// </summary>
        public bool? Obfuscated;

        /// <summary>
        /// If the text is bold or not
        /// </summary>
        public bool? Bold;

        /// <summary>
        /// If the text is italic or not
        /// </summary>
        public bool? Italic;

        /// <summary>
        /// If the text is strikethroughed or not
        /// </summary>
        public bool? Strikethrough;

        /// <summary>
        /// If the text is underlined or not
        /// </summary>
        public bool? Underline;

        /// <summary>
        /// If the text should reset the text look defined by its parent
        /// </summary>
        public bool? Reset;

        /// <summary>
        /// the text to input into a player's chat field if a player shift clicks the text
        /// </summary>
        public string ClickShiftInsertion;

        /// <summary>
        /// the link to open if a player clicks the text
        /// </summary>
        public string ClickURL;

        /// <summary>
        /// the command to run if a player clicks the text
        /// Note that you have to use /trigger if its a text displayed in chat
        /// </summary>
        public string ClickCommand;

        /// <summary>
        /// the text to input into a player's chat field if a player clicks the text
        /// </summary>
        public string ClickInsertion;

        /// <summary>
        /// the page to change to if a player clicks the text
        /// </summary>
        public int? ClickBookPageChange;

        /// <summary>
        /// the text to show when a player hovers over the text
        /// </summary>
        public JSON[] HoverText;

        /// <summary>
        /// the item data the player sees when hovering over the text
        /// </summary>
        public Item HoverItem;

        /// <summary>
        /// the objective to show a score in.
        /// Note that you also have to chose a selector to show a score
        /// </summary>
        public ScoreObject Objective;

        /// <summary>
        /// The coords of a block to show data for
        /// note that <see cref="DataPath"/> also has to be set
        /// </summary>
        public Coords BlockCoords;

        /// <summary>
        /// The datapath to the data to show
        /// note that <see cref="DataPath"/> or <see cref="Selector"/> also has to be set
        /// </summary>
        public string DataPath;

        /// <summary>
        /// if the output from data should be made nice
        /// </summary>
        public bool? interpret;

        /// <summary>
        /// Gets the raw JSON data
        /// </summary>
        /// <returns>the raw JSON data used by the game</returns>
        public override string ToString()
        {
            if (BasicText == true)
            {
                return "\"" + Text.Escape().Replace("[NewLine]", "\\n") + "\"";
            }
            else
            {
                List<string> TempList = new List<string>();

                if (Text != null) { TempList.Add("\"text\":\"" + Text.Escape().Replace("[NewLine]","\\n") + "\""); }
                if (Translate != null) { TempList.Add("\"translate\":\"" + Translate.Escape() + "\""); }
                if (With != null) {TempList.Add("\"with\":" + With.GetString()); }
                if (DataPath != null)
                {
                    if (Selector != null)
                    {
                        TempList.Add("\"entity\":\"" + Selector + "\",\"nbt\":\"" + DataPath + "\"");
                    }
                    else if (BlockCoords != null)
                    {
                        TempList.Add("\"block\":\""+ BlockCoords.X + " " + BlockCoords.Y + " " + BlockCoords.Z + "\",\"nbt\":\"" + DataPath + "\"");
                    }

                    if (interpret != null) { TempList.Add("\"interpret\":" + interpret); }
                }
                else if (Selector != null && Objective == null) { TempList.Add("\"selector\":\"" + Selector.ToString().Escape() + "\""); }
                if (Key != null) { TempList.Add("\"keybind\":\"key." + Convert.ToString(Key).Replace("_", ".") + "\""); }
                if (Color != null) { TempList.Add("\"color\":\"" + Color + "\""); }
                if (Obfuscated != null) { TempList.Add("\"obfuscated\":" + Obfuscated); }
                if (Bold != null) { TempList.Add("\"bold\":" + Bold); }
                if (Italic != null) { TempList.Add("\"italic\":" + Italic); }
                if (Strikethrough != null) { TempList.Add("\"strikethrough\":" + Strikethrough); }
                if (Underline != null) { TempList.Add("\"underline\":" + Underline); }
                if (Reset != null) { TempList.Add("\"Reset\":" + Reset); }
                if (ClickShiftInsertion != null) { TempList.Add("\"insertion\":\"" + ClickShiftInsertion.Escape() + "\""); }
                if (ClickURL != null) { TempList.Add("\"clickEvent\":{ \"action\":\"open_url\",\"value\":\"" + ClickURL.Escape() + "\"}"); }
                if (ClickCommand != null) { TempList.Add("\"clickEvent\":{ \"action\":\"run_command\",\"value\":\"" + ClickCommand.Escape() + "\"}"); }
                if (ClickInsertion != null) { TempList.Add("\"clickEvent\":{ \"action\":\"suggest_command\",\"value\":\"" + ClickInsertion.Escape() + "\"}"); }
                if (ClickBookPageChange != null) { TempList.Add("\"clickEvent\":{ \"action\":\"change_page\",\"value\":\"" + ClickBookPageChange + "\"}"); }
                if (HoverItem != null) { TempList.Add("\"hoverEvent\":{ \"action\":\"show_text\",\"value\":\"{" + HoverItem.DataString.Escape() + "}\"}"); }
                if (HoverText != null)
                {
                    string TempString = "\"hoverEvent\":{ \"action\":\"show_text\",\"value\":[";
                    for (int a = 0; a < HoverText.Length; a++)
                    {
                        if (a != 0) { TempString += ","; }
                        TempString += "{" + HoverText[a] + "}";
                    }
                    TempString += "]}";
                    TempList.Add(TempString);
                }
                if (Objective != null) { TempList.Add("\"score\":{\"name\":\"" + Selector + "\", \"objective\":\"" + Objective + "\"}"); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }

        /// <summary>
        /// Returns the thing used to mark a new line in a JSON text
        /// </summary>
        public static string NewLine
        {
            get
            {
                return "[NewLine]";
            }
        }

        /// <summary>
        /// Converts a string into a simple JSON string
        /// </summary>
        /// <param name="text">the string to convert</param>
        public static implicit operator JSON(string text)
        {
            return new JSON(text);
        }

        /// <summary>
        /// Converts a single JSON string into an array of JSON strings only containing the JSON string
        /// </summary>
        /// <param name="json">the JSON string to convert</param>
        public static implicit operator JSON[](JSON json)
        {
            return new JSON[] { json };
        }
    }
}
