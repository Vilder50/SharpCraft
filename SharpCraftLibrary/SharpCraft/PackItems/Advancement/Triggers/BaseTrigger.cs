using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// The base class for advancement triggers
    /// </summary>
    public abstract class BaseTrigger : DataHolderBase, IRequirementItem, ITrigger
    {
        /// <summary>
        /// Intializes a new <see cref="BaseTrigger"/>
        /// </summary>
        /// <param name="type"></param>
        protected BaseTrigger(string type)
        {
            Type = type;
        }

        /// <summary>
        /// The distinct name of this trigger. Leaving null will generate a name when its added to an advancement and that advancement is written.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The type of trigger
        /// </summary>
        [DataTag("trigger", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public string Type { get; private set; }

        /// <summary>
        /// Returns a tree structure containing all the data tags for this object
        /// </summary>
        /// <returns>the bottom of the tree</returns>
        public override DataPartObject GetDataTree()
        {
            if (Name is null)
            {
                throw new InvalidOperationException("Name is not set. Cannot get data tree");
            }
            DataPartObject dataObject = new DataPartObject();
            dataObject.AddValue(new DataPartPath(Name, base.GetDataTree(), true));
            return dataObject;
        }

        /// <summary>
        /// Returns the string used for inserting the item into a requirement
        /// </summary>
        /// <param name="requirement">The requirement to get the string for</param>
        public string GetRequirementString(Requirement requirement)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = requirement.GetGeneratedTriggerName();
            }
            return "\""+Name.ToLower()+"\"";
        }

        /// <summary>
        /// Converts a <see cref="BaseTrigger"/> into an array
        /// </summary>
        /// <param name="trigger">The <see cref="BaseTrigger"/> to convert</param>
        public static implicit operator BaseTrigger[](BaseTrigger trigger)
        {
            return new BaseTrigger[] { trigger };
        }

        /// <summary>
        /// Converts a <see cref="BaseTrigger"/> into a <see cref="Requirement"/> array
        /// </summary>
        /// <param name="trigger">The <see cref="BaseTrigger"/> to convert</param>
        public static implicit operator Requirement[](BaseTrigger trigger)
        {
            return new Requirement[] { new Requirement(new BaseTrigger[] { trigger }) };
        }

        /// <summary>
        /// Converts a <see cref="BaseTrigger"/> into a <see cref="Requirement"/>
        /// </summary>
        /// <param name="trigger">The <see cref="BaseTrigger"/> to convert</param>
        public static implicit operator Requirement(BaseTrigger trigger)
        {
            return new Requirement(new BaseTrigger[] { trigger });
        }
    }

    /// <summary>
    /// Interface for advancement triggers
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// The distinct name of this trigger.
        /// </summary>
        string? Name { get; set; }
    }
}
