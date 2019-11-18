using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which clears the title on the selected players' screens
    /// </summary>
    public class TitleClearCommand : ICommand
    {
        private Selector selector;

        /// <summary>
        /// Intializes a new <see cref="TitleClearCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose title to clear</param>
        public TitleClearCommand(Selector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector selecting the players whose title to clear
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] clear</returns>
        public string GetCommandString()
        {
            return $"title {Selector} clear";
        }
    }

    /// <summary>
    /// Command which clears the selected players' title settings
    /// </summary>
    public class TitleResetCommand : ICommand
    {
        private Selector selector;

        /// <summary>
        /// Intializes a new <see cref="TitleResetCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose title settings to reset</param>
        public TitleResetCommand(Selector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector selecting the players whose title settings to reset
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] reset</returns>
        public string GetCommandString()
        {
            return $"title {Selector} reset";
        }
    }

    /// <summary>
    /// Command which displayes a title
    /// </summary>
    public class TitleCommand : ICommand
    {
        private Selector selector;
        private JSON[] text;

        /// <summary>
        /// Intializes a new <see cref="TitleCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting players to show the title for</param>
        /// <param name="text">The text to show</param>
        public TitleCommand(Selector selector, JSON[] text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting players to show the title for
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public JSON[] Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] title [Text]</returns>
        public string GetCommandString()
        {
            return $"title {Selector} title {Text.GetString()}";
        }
    }

    /// <summary>
    /// Command which sets the sub title to display next
    /// </summary>
    public class TitleSubtitleCommand : ICommand
    {
        private Selector selector;
        private JSON[] text;

        /// <summary>
        /// Intializes a new <see cref="TitleSubtitleCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose sub title to change</param>
        /// <param name="text">The text to show</param>
        public TitleSubtitleCommand(Selector selector, JSON[] text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting the players whose sub title to change
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public JSON[] Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] subtitle [Text]</returns>
        public string GetCommandString()
        {
            return $"title {Selector} subtitle {Text.GetString()}";
        }
    }

    /// <summary>
    /// Command which sets the text shown in the selected players' action bars
    /// </summary>
    public class TitleActionbarCommand : ICommand
    {
        private Selector selector;
        private JSON[] text;

        /// <summary>
        /// Intializes a new <see cref="TitleActionbarCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players to show the action bar text for</param>
        /// <param name="text">The text to show</param>
        public TitleActionbarCommand(Selector selector, JSON[] text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting the players to show the action bar text for
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public JSON[] Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] actionbar [Text]</returns>
        public string GetCommandString()
        {
            return $"title {Selector} actionbar {Text.GetString()}";
        }
    }

    /// <summary>
    /// Command which changes how long a title will appear for the selected players
    /// </summary>
    public class TitleTimesCommand : ICommand
    {
        private Selector selector;
        private Time fadeIn;
        private Time stay;
        private Time fadeOut;

        /// <summary>
        /// Intializes a new <see cref="TitleTimesCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players to change the title times for</param>
        /// <param name="fadeIn">How long it takes for the title to fade in</param>
        /// <param name="stay">How long the title stays</param>
        /// <param name="fadeOut">How long it takes for the title to fade out</param>
        public TitleTimesCommand(Selector selector, Time fadeIn, Time stay, Time fadeOut)
        {
            Selector = selector;
            FadeIn = fadeIn;
            Stay = stay;
            FadeOut = fadeOut;
        }

        /// <summary>
        /// Selector selecting the players to change the title times for
        /// </summary>
        public Selector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// How long it takes for the title to fade in
        /// </summary>
        public Time FadeIn { get => fadeIn; set => fadeIn = value ?? throw new ArgumentNullException(nameof(FadeIn), "FadeIn may not be null"); }

        /// <summary>
        /// How long the title stays
        /// </summary>
        public Time Stay { get => stay; set => stay = value ?? throw new ArgumentNullException(nameof(Stay), "Stay may not be null"); }

        /// <summary>
        /// How long it takes for the title to fade out
        /// </summary>
        public Time FadeOut { get => fadeOut; set => fadeOut = value ?? throw new ArgumentNullException(nameof(FadeOut), "FadeOut may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] times [FadeIn] [Stay] [FadeOut]</returns>
        public string GetCommandString()
        {
            return $"title {Selector} times {FadeIn.AsTicks()} {Stay.AsTicks()} {FadeOut.AsTicks()}";
        }
    }
}
