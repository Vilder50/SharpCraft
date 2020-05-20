using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which adds/removes a tag from one or more entities
    /// </summary>
    public class TagCommand : BaseCommand
    {
        private BaseSelector selector = null!;
        private Tag tag = null!;

        /// <summary>
        /// Intializes a new <see cref="TagCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the entities to add/remove the tag from</param>
        /// <param name="tag">The tag to add/remove</param>
        /// <param name="addTag">True if the tag should be added. False if removed</param>
        public TagCommand(BaseSelector selector, Tag tag, bool addTag)
        {
            Selector = selector;
            Tag = tag;
            AddTag = addTag;
        }

        /// <summary>
        /// Selector for selecting the entities to add/remove the tag from
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The tag to add/remove
        /// </summary>
        public Tag Tag { get => tag; set => tag = value ?? throw new ArgumentNullException(nameof(Tag), "Tag may not be null"); }

        /// <summary>
        /// True if the tag should be added. False if removed
        /// </summary>
        public bool AddTag { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tag [Selector] [AddTag] [Tag]</returns>
        public override string GetCommandString()
        {
            return $"tag {Selector.GetSelectorString()} {(AddTag ? "add" : "remove")} {Tag.Name}";
        }
    }

    /// <summary>
    /// Command which returns a list of tags from one or more entities
    /// </summary>
    public class TagListCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="TagListCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the entities to get a list of tags from</param>
        public TagListCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector for selecting the entities to get a list of tags from
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tag [Selector] list</returns>
        public override string GetCommandString()
        {
            return $"tag {Selector.GetSelectorString()} list";
        }
    }
}
