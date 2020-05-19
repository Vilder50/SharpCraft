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
    public class TitleClearCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleClearCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose title to clear</param>
        public TitleClearCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector selecting the players whose title to clear
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] clear</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} clear";
        }
    }

    /// <summary>
    /// Command which clears the selected players' title settings
    /// </summary>
    public class TitleResetCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleResetCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose title settings to reset</param>
        public TitleResetCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector selecting the players whose title settings to reset
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] reset</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} reset";
        }
    }

    /// <summary>
    /// Command which displayes a title
    /// </summary>
    public class TitleCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private BaseJsonText text = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting players to show the title for</param>
        /// <param name="text">The text to show</param>
        public TitleCommand(BaseSelector selector, BaseJsonText text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting players to show the title for
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public BaseJsonText Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] title [Text]</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} title {Text.GetJsonString()}";
        }
    }

    /// <summary>
    /// Command which sets the sub title to display next
    /// </summary>
    public class TitleSubtitleCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private BaseJsonText text = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleSubtitleCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose sub title to change</param>
        /// <param name="text">The text to show</param>
        public TitleSubtitleCommand(BaseSelector selector, BaseJsonText text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting the players whose sub title to change
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public BaseJsonText Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] subtitle [Text]</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} subtitle {Text.GetJsonString()}";
        }
    }

    /// <summary>
    /// Command which sets the text shown in the selected players' action bars
    /// </summary>
    public class TitleActionbarCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private BaseJsonText text = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleActionbarCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players to show the action bar text for</param>
        /// <param name="text">The text to show</param>
        public TitleActionbarCommand(BaseSelector selector, BaseJsonText text)
        {
            Selector = selector;
            Text = text;
        }

        /// <summary>
        /// Selector selecting the players to show the action bar text for
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The text to show
        /// </summary>
        public BaseJsonText Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Text may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] actionbar [Text]</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} actionbar {Text.GetJsonString()}";
        }
    }

    /// <summary>
    /// Command which changes how long a title will appear for the selected players
    /// </summary>
    public class TitleTimesCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private NoneNegativeTime<int> fadeIn = null!;
        private NoneNegativeTime<int> stay = null!;
        private NoneNegativeTime<int> fadeOut = null!;

        /// <summary>
        /// Intializes a new <see cref="TitleTimesCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players to change the title times for</param>
        /// <param name="fadeIn">How long it takes for the title to fade in</param>
        /// <param name="stay">How long the title stays</param>
        /// <param name="fadeOut">How long it takes for the title to fade out</param>
        public TitleTimesCommand(BaseSelector selector, NoneNegativeTime<int> fadeIn, NoneNegativeTime<int> stay, NoneNegativeTime<int> fadeOut)
        {
            Selector = selector;
            FadeIn = fadeIn;
            Stay = stay;
            FadeOut = fadeOut;
        }

        /// <summary>
        /// Selector selecting the players to change the title times for
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// How long it takes for the title to fade in
        /// </summary>
        public NoneNegativeTime<int> FadeIn { get => fadeIn; set => fadeIn = value ?? throw new ArgumentNullException(nameof(FadeIn), "FadeIn may not be null"); }

        /// <summary>
        /// How long the title stays
        /// </summary>
        public NoneNegativeTime<int> Stay { get => stay; set => stay = value ?? throw new ArgumentNullException(nameof(Stay), "Stay may not be null"); }

        /// <summary>
        /// How long it takes for the title to fade out
        /// </summary>
        public NoneNegativeTime<int> FadeOut { get => fadeOut; set => fadeOut = value ?? throw new ArgumentNullException(nameof(FadeOut), "FadeOut may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>title [Selector] times [FadeIn] [Stay] [FadeOut]</returns>
        public override string GetCommandString()
        {
            return $"title {Selector.GetSelectorString()} times {FadeIn.GetAsTicks()} {Stay.GetAsTicks()} {FadeOut.GetAsTicks()}";
        }
    }
}
