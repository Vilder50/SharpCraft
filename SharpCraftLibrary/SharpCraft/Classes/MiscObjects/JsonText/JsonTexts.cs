﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.JsonText
{
    /// <summary>
    /// Class for displaying unformatted json text. Note that this cannot be formatted or contain extra json text.
    /// </summary>
    public class UnformattedText : BaseJsonText
    {
        private string displayText = null!;

        /// <summary>
        /// Intializes a new <see cref="UnformattedText"/>
        /// </summary>
        /// <param name="displayText">The text to display</param>
        public UnformattedText(string displayText)
        {
            DisplayText = displayText;
        }

        /// <summary>
        /// The text to display
        /// </summary>
        public string DisplayText { get => displayText; set => displayText = value ?? throw new ArgumentNullException(nameof(displayText), "Json text to display may not be null"); }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            return DisplayText;
        }

        /// <summary>
        /// Gets the raw JSON string
        /// </summary>
        /// <returns>the raw JSON string used by the game</returns>
        public override string GetJsonString()
        {
            return "\"" + GetSpecificJsonString() + "\"";
        }
    }

    /// <summary>
    /// Class for displaying json text
    /// </summary>
    public class Text : BaseJsonText
    {
        private string displayText = null!;

        /// <summary>
        /// Intializes a new <see cref="Text"/>
        /// </summary>
        /// <param name="displayText">The text to display</param>
        public Text(string displayText)
        {
            DisplayText = displayText;
        }

        /// <summary>
        /// Intializes a new <see cref="Text"/>
        /// </summary>
        /// <param name="displayText">The text to display</param>
        /// <param name="color">The color of the text</param>
        public Text(string displayText, ID.MinecraftColor color)
        {
            DisplayText = displayText;
            Color = color;
        }

        /// <summary>
        /// The text to display
        /// </summary>
        public string DisplayText { get => displayText; set => displayText = value ?? throw new ArgumentNullException(nameof(displayText), "Json text to display may not be null"); }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            return $"\"text\":\"{DisplayText.Escape()}\"";
        }
    }

    /// <summary>
    /// Class for displaying translated json text
    /// </summary>
    public class Translate : BaseJsonText
    {
        private string translateString = null!;

        /// <summary>
        /// Intializes a new <see cref="Translate"/>
        /// </summary>
        /// <param name="translateString">The translation string to translate</param>
        /// <param name="insert">Values to insert into the translation string (at places with %s)</param>
        public Translate(string translateString, BaseJsonText[]? insert = null)
        {
            TranslateString = translateString;
            Insert = insert;
        }

        /// <summary>
        /// The translation string to translate
        /// </summary>
        public string TranslateString { get => translateString; set => translateString = value ?? throw new ArgumentNullException(nameof(TranslateString), "Json translate may not be null"); }

        /// <summary>
        /// Values to insert into the translation string (at places with %s)
        /// </summary>
        public BaseJsonText[]? Insert { get; set; }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            string outText = $"\"translate\":\"{TranslateString.Escape()}\"";
            if (!(Insert is null))
            {
                IEnumerable<string> array = Insert.Select(k => k.GetJsonString()).ToArray();
                outText += ",\"with\":[" + string.Join(",", array) + "]";
            }

            return outText;
        }
    }

    /// <summary>
    /// Class for displaying the names of some selected entities as json text
    /// </summary>
    public class Names : BaseJsonText
    {
        private Selector namesSelector = null!;

        /// <summary>
        /// Intializes a new <see cref="Names"/>
        /// </summary>
        /// <param name="namesSelector">Selector selecting the entities whose names to show</param>
        public Names(Selector namesSelector)
        {
            NamesSelector = namesSelector;
        }

        /// <summary>
        /// Selector selecting the entities whose names to show
        /// </summary>
        public Selector NamesSelector { get => namesSelector; set => namesSelector = value ?? throw new ArgumentNullException(nameof(NameSelector), "JsonText name selector may not be null"); }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            return $"\"selector\":\"{NamesSelector.GetSelectorString().Escape()}\"";
        }
    }

    /// <summary>
    /// Class for displaying a key bind as json text
    /// </summary>
    public class KeyBind : BaseJsonText
    {
        /// <summary>
        /// Intializes a new <see cref="KeyBind"/>
        /// </summary>
        /// <param name="key">The key to display</param>
        public KeyBind(ID.Key key)
        {
            Key = key;
        }

        /// <summary>
        /// The key to display
        /// </summary>
        public ID.Key Key { get; set; }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            return $"\"keybind\":\"key.{Key}\"";
        }
    }

    /// <summary>
    /// Class for displaying data saved somewhere as json text
    /// </summary>
    public class Data : BaseJsonText
    {
        private IDataLocation dataLocation = null!;

        /// <summary>
        /// Intializes a new <see cref="Data"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to display</param>
        /// <param name="interpret">If the data should be interpreted as json text</param>
        public Data(IDataLocation dataLocation, bool interpret)
        {
            DataLocation = dataLocation;
            Interpret = interpret;
        }

        /// <summary>
        /// The location of the data to display
        /// </summary>
        public IDataLocation DataLocation { get => dataLocation; set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "JsonText data location may not be null"); }

        /// <summary>
        /// If the data should be interpreted as json text
        /// </summary>
        public bool Interpret { get; set; }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            string outString = $"\"nbt\":\"{DataLocation.DataPath.Escape()}\",\"interpret\":{Interpret.ToMinecraftBool()}";

            if (DataLocation is BlockDataLocation block)
            {
                outString += $",\"block\":\"{block.Coordinates.GetVectorString()}\"";
            }
            else if (DataLocation is EntityDataLocation entity)
            {
                outString += $",\"entity\":\"{entity.Selector.GetSelectorString().Escape()}\"";
            }
            else if (DataLocation is StorageDataLocation storage)
            {
                outString += $",\"storage\":\"{storage.Storage.GetNamespacedName()}\"";
            }

            return outString;
        }
    }

    /// <summary>
    /// Class for displaying a scoreboard score as json text
    /// </summary>
    public class Score : BaseJsonText
    {
        private BaseSelector selector = null!;
        private Objective scoreObject = null!;

        /// <summary>
        /// Intializes a new <see cref="Score"/>
        /// </summary>
        /// <param name="selector">Selects the entity to show the score for</param>
        /// <param name="scoreObject">The objective to get the score from</param>
        public Score(BaseSelector selector, Objective scoreObject)
        {
            Selector = selector;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// Intializes a new <see cref="Score"/>
        /// </summary>
        /// <param name="score">The score to display</param>
        public Score(ScoreValue score)
        {
            Selector = score.Selector;
            ScoreObject = score.ScoreObject;
        }

        /// <summary>
        /// Selects the entity to show the score for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (value is AllSelector)
                {
                    selector = value;
                }
                else
                {
                    selector = Validators.ValidateSingleSelectSelector(value, nameof(Selector), nameof(Score));
                }
            }
        }

        /// <summary>
        /// The objective to get the score from
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "JsonText ScoreObject may not be null"); }

        /// <summary>
        /// returns the raw string specific for this class
        /// </summary>
        /// <returns>The raw string specific for this class</returns>
        protected override string GetSpecificJsonString()
        {
            return $"\"score\":{{\"name\":\"{Selector.GetSelectorString().Escape()}\",\"objective\":\"{ScoreObject.Name}\"}}";
        }
    }
}
