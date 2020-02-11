using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    public abstract partial class JsonText
    {
        /// <summary>
        /// Base class for click events in json text
        /// </summary>
        public abstract class BaseClickEvent
        {
            private readonly string eventType;

            /// <summary>
            /// Intializes <see cref="BaseClickEvent"/>
            /// </summary>
            /// <param name="eventType">The type of click event</param>
            protected BaseClickEvent(string eventType)
            {
                this.eventType = eventType;
            }

            /// <summary>
            /// Should return the value of the click event
            /// </summary>
            /// <returns>The value of the click event</returns>
            public abstract string GetEventValue();

            /// <summary>
            /// Returns the string for this click event used in <see cref="JsonText"/>
            /// </summary>
            /// <returns>The string used in <see cref="JsonText"/></returns>
            public string GetEventString()
            {
                return $"\"clickEvent\":{{\"action\":\"{eventType}\",\"value\":{GetEventValue()}}}";
            }

            /// <summary>
            /// Converts a <see cref="Commands.BaseCommand"/> into a <see cref="BaseClickEvent"/> object
            /// </summary>
            /// <param name="command">the <see cref="Commands.BaseCommand"/> to convert</param>
            public static implicit operator BaseClickEvent(Commands.BaseCommand command)
            {
                return new RunCommandClickEvent(command);
            }


            /// <summary>
            /// Converts a number into a <see cref="BaseClickEvent"/> object used for changing page in a book.
            /// </summary>
            /// <param name="page">the number to convert</param>
            public static implicit operator BaseClickEvent(int page)
            {
                return new ChangePageClickEvent(page);
            }
        }

        /// <summary>
        /// Opens an url when clicked
        /// </summary>
        public class OpenUrlClickEvent : BaseClickEvent
        {
            private string url;

            /// <summary>
            /// Intializes a new <see cref="OpenUrlClickEvent"/>
            /// </summary>
            /// <param name="url">The url to open</param>
            public OpenUrlClickEvent(string url) : base("open_url")
            {
                Url = url;
            }

            /// <summary>
            /// The url to open
            /// </summary>
            public string Url 
            { 
                get => url; 
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Click event url may not be null or whitespace", nameof(Url));
                    }
                    url = value;
                }
            }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return "\"" + Url + "\"";
            }
        }

        /// <summary>
        /// Runs a command when clicked. Note that the player has to have the right permission to run the command.
        /// </summary>
        public class RunCommandClickEvent : BaseClickEvent
        {
            private Commands.BaseCommand command;

            /// <summary>
            /// Intializes a new <see cref="RunCommandClickEvent"/>
            /// </summary>
            /// <param name="command">The command to run</param>
            public RunCommandClickEvent(Commands.BaseCommand command) : base("run_command")
            {
                Command = command;
            }

            /// <summary>
            /// The command to run
            /// </summary>
            public Commands.BaseCommand Command { get => command; set => command = value ?? throw new ArgumentNullException(nameof(Command), "Click event command may not be null"); }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return "\"/" + Command.GetCommandString() + "\"";
            }
        }

        /// <summary>
        /// Changes the page the player is on in a book
        /// </summary>
        public class ChangePageClickEvent : BaseClickEvent
        {
            /// <summary>
            /// Intializes a new <see cref="ChangePageClickEvent"/>
            /// </summary>
            /// <param name="page">The page to switch to</param>
            public ChangePageClickEvent(int page) : base("change_page")
            {
                Page = page;
            }

            /// <summary>
            /// The page to switch to
            /// </summary>
            public int Page { get; set; }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return Page.ToString();
            }
        }

        /// <summary>
        /// Sets the given text into the players chat when clicked
        /// </summary>
        public class SuggestTextClickEvent : BaseClickEvent
        {
            private string text;

            /// <summary>
            /// Intializes a new <see cref="SuggestTextClickEvent"/>
            /// </summary>
            /// <param name="text">the text to suggest</param>
            public SuggestTextClickEvent(string text) : base("suggest_command")
            {
                Text = text;
            }

            /// <summary>
            /// the text to suggest
            /// </summary>
            public string Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Click event text suggestion may not be null"); }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return "\"" + Text + "\"";
            }
        }

        /// <summary>
        /// Inserts the given text into the players click board when clicked
        /// </summary>
        public class SetClipboardClickEvent : BaseClickEvent
        {
            private string text;

            /// <summary>
            /// Intializes a new <see cref="SetClipboardClickEvent"/>
            /// </summary>
            /// <param name="text">The text to insert into clip board</param>
            public SetClipboardClickEvent(string text) : base("copy_to_clipboard")
            {
                Text = text;
            }

            /// <summary>
            /// The text to insert into clip board
            /// </summary>
            public string Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Click event text suggestion may not be null"); }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return "\"" + Text + "\"";
            }
        }
    }
}
