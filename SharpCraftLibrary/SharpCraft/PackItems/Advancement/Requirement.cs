using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Interface for items which can be in requirements
    /// </summary>
    public interface IRequirementItem
    {
        /// <summary>
        /// Returns the string used for inserting the item into a requirement
        /// </summary>
        /// <param name="requirement">The requirement to get the string for</param>
        string GetRequirementString(Requirement requirement);
    }

    /// <summary>
    /// A requirement for an advancement
    /// </summary>
    public class Requirement : IRequirementItem
    {
        private int nextGeneratedNumber = 0;
        private IRequirementItem[] requirementItems = null!;

        /// <summary>
        /// Intializes a new <see cref="Requirement"/>
        /// </summary>
        /// <param name="requirementItems">The required items for this requirement. If its a trigger then it will use or. If its a requirement it will use and.</param>
        public Requirement(IRequirementItem[] requirementItems)
        {
            RequirementItems = requirementItems;
        }

        /// <summary>
        /// The required items for this requirement. If its a trigger then it will use or. If its a requirement it will use and.
        /// </summary>
        public IRequirementItem[] RequirementItems { get => requirementItems; set => requirementItems = value ?? throw new ArgumentNullException(nameof(RequirementItems), "RequirementItems may not be null"); }

        /// <summary>
        /// Returns the string used for inserting the item into a requirement
        /// </summary>
        /// <param name="requirement">The requirement to get the string for. Null if this is the parent.</param>
        public string GetRequirementString(Requirement? requirement)
        {
            Requirement getNamesFrom = requirement ?? this;

            return "[" + string.Join(",",RequirementItems.Select(r => r.GetRequirementString(getNamesFrom))) + "]";
        }

        /// <summary>
        /// Returns a distinct name for a trigger
        /// </summary>
        /// <returns>A distinct name for a trigger</returns>
        public string GetGeneratedTriggerName()
        {
            string name = "trigger_" + nextGeneratedNumber;
            nextGeneratedNumber++;
            return name;
        }

        /// <summary>
        /// Returns all the triggers in this <see cref="Requirement"/>
        /// </summary>
        /// <returns>All the triggers</returns>
        public IEnumerable<BaseTrigger> GetChildTriggers()
        {
            List<BaseTrigger> returnedTriggers = new List<BaseTrigger>();
            foreach(IRequirementItem item in RequirementItems)
            {
                if (item is BaseTrigger trigger && !returnedTriggers.Contains(trigger))
                {
                    yield return trigger;
                    returnedTriggers.Add(trigger);
                }
                else if (item is Requirement requirement)
                {
                    foreach(BaseTrigger childTrigger in requirement.GetChildTriggers())
                    {
                        if (!returnedTriggers.Contains(childTrigger))
                        {
                            yield return childTrigger;
                            returnedTriggers.Add(childTrigger);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Converts a <see cref="Requirement"/> into an array
        /// </summary>
        /// <param name="requirement">The <see cref="Requirement"/> to convert</param>
        public static implicit operator Requirement[](Requirement requirement)
        {
            return new Requirement[] { requirement };
        }
    }
}
