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
        /// Base class for hover events in json text
        /// </summary>
        public abstract class BaseHoverEvent
        {
            private readonly string eventType;

            /// <summary>
            /// Intializes <see cref="BaseHoverEvent"/>
            /// </summary>
            /// <param name="eventType">The type of hover event</param>
            protected BaseHoverEvent(string eventType)
            {
                this.eventType = eventType;
            }

            /// <summary>
            /// Should return the value of the hovet event
            /// </summary>
            /// <returns>The value of the hover event</returns>
            public abstract string GetEventValue();

            /// <summary>
            /// Returns the string for this hover event used in <see cref="JsonText"/>
            /// </summary>
            /// <returns>The string used in <see cref="JsonText"/></returns>
            public string GetEventString()
            {
                return $"\"hoverEvent\":{{\"action\":\"{eventType}\",\"contents\":{GetEventValue()}}}";
            }

            /// <summary>
            /// Converts a <see cref="JsonText"/> into a <see cref="BaseHoverEvent"/> object
            /// </summary>
            /// <param name="text">the <see cref="JsonText"/> to convert</param>
            public static implicit operator BaseHoverEvent(JsonText text)
            {
                return new TextHoverEvent(text);
            }
        }

        /// <summary>
        /// Shows text when the text is being hovered
        /// </summary>
        public class TextHoverEvent : BaseHoverEvent
        {
            private JsonText text;

            /// <summary>
            /// Intializes a new <see cref="TextHoverEvent"/>
            /// </summary>
            /// <param name="text">The text to show when the text gets hovered</param>
            public TextHoverEvent(JsonText text) : base("show_text")
            {
                Text = text;
            }

            /// <summary>
            /// The text to show when the text gets hovered
            /// </summary>
            public JsonText Text
            {
                get => text;
                set 
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(Text), "Hover text may not be null");
                    }
                    text = value;
                }
            }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                return Text.GetJsonString();
            }
        }

        /// <summary>
        /// Shows the item hover effect when the text is hovered
        /// </summary>
        public class ItemHoverEvent : BaseHoverEvent
        {
            private Item item;

            /// <summary>
            /// Intializes a new <see cref="ItemHoverEvent"/>
            /// </summary>
            /// <param name="item">The item to show</param>
            public ItemHoverEvent(Item item) : base("show_item")
            {
                Item = item;
            }

            /// <summary>
            /// The item to show
            /// </summary>
            public Item Item 
            { 
                get => item; 
                set 
                { 
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(Item), "Hover item may not be null");
                    }
                    if (value.ID is null)
                    {
                        throw new ArgumentNullException(nameof(Item), "Hover item id may not be null");
                    }
                    item = value; 
                }
            }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                string output = "\"id\":\"" + Item.ID.Name+ "\"";
                string tagString = Item.GetItemTagString();
                if (tagString != "{}")
                {
                    output += ",\"tag\":\"" + tagString.Escape() + "\"";
                }
                if (!(item.Count is null))
                {
                    output += ",\"count\":" + item.Count;
                }
                return "{" + output + "}";
            }
        }

        /// <summary>
        /// Shows some information about an entity when the text is hovered
        /// </summary>
        public class EntityHoverEvent : BaseHoverEvent
        {
            private EntityType type;

            /// <summary>
            /// Intializes a new <see cref="EntityHoverEvent"/>
            /// </summary>
            /// <param name="type">The type of entity</param>
            /// <param name="name">The entity's name (Not really used)</param>
            /// <param name="uuid">The entity's uuid</param>
            public EntityHoverEvent(EntityType type, JsonText name = null, UUID uuid = null) : base("show_entity")
            {
                Type = type;
                Name = name;
                UUID = uuid;
            }

            /// <summary>
            /// The type of entity
            /// </summary>
            public EntityType Type { get => type; set => type = value ?? throw new ArgumentNullException(nameof(Type), "Type may not be null"); }

            /// <summary>
            /// The entity's name (Not really used)
            /// </summary>
            public JsonText Name { get; set; }

            /// <summary>
            /// The entity's uuid
            /// </summary>
            public UUID UUID { get; set; }

            /// <summary>
            /// Returns the value of the event
            /// </summary>
            /// <returns>The value of the event</returns>
            public override string GetEventValue()
            {
                string output = $"\"type\":\"{Type.Name}\"";
                if (!(Name is null))
                {
                    output += ",\"name\":" + Name.GetJsonString();
                }
                if (!(UUID is null))
                {
                    output += ",\"id\":\"" + UUID.UUIDString + "\"";
                }

                return "{" + output + "}";
            }
        }
    }
}
