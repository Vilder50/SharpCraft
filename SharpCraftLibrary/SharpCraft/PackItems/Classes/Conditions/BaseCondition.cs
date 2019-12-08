using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Base class for conditions
    /// </summary>
    public abstract class BaseCondition : DataHolderBase
    {
        /// <summary>
        /// Intlaizes a new <see cref="BaseCondition"/>
        /// </summary>
        /// <param name="conditionName">The name of the condition</param>
        public BaseCondition(string conditionName)
        {
            ConditionName = conditionName;
        }

        /// <summary>
        /// The name of the condition
        /// </summary>
        [DataTag("condition", JsonTag = true)]
        public string ConditionName { get; private set; }

        /// <summary>
        /// Inverts the given condition
        /// </summary>
        /// <param name="condition">The condition to invert</param>
        /// <returns>The condition to invert</returns>
        public static BaseCondition operator !(BaseCondition condition)
        {
            if (condition is InvertedCondition invertedCondition)
            {
                return invertedCondition.Condition;
            }
            else
            {
                return new InvertedCondition(condition);
            }
        }

        /// <summary>
        /// Returns an <see cref="AllCondition"/> which is true if the given conditions are true
        /// </summary>
        /// <param name="condition1">one of the conditions to check if true</param>
        /// <param name="condition2">one of the conditions to check if true</param>
        /// <returns>A condition which is true if the given conditions are true</returns>
        public static BaseCondition operator &(BaseCondition condition1, BaseCondition condition2)
        {
            AllCondition condition;

            if (condition1 is AllCondition allCondition1)
            {
                condition = allCondition1;
                List<BaseCondition> addedConditions = condition.Conditions.ToList();
                if (condition2 is AllCondition allCondition2)
                {
                    addedConditions.AddRange(allCondition2.Conditions);
                }
                else
                {
                    addedConditions.Add(condition2);
                }
                condition.Conditions = addedConditions.ToArray();
            }
            else if (condition2 is AllCondition allCondition2)
            {
                condition = allCondition2;
                List<BaseCondition> addedConditions = condition.Conditions.ToList();
                addedConditions.Add(condition1);
                condition.Conditions = addedConditions.ToArray();
            }
            else
            {
                condition = new AllCondition(new BaseCondition[] {condition1, condition2 });
            }

            return condition;
        }

        /// <summary>
        /// Returns an <see cref="AlternativeCondition"/> which is true if one of the given conditions are true
        /// </summary>
        /// <param name="condition1">one of the conditions to check if true</param>
        /// <param name="condition2">one of the conditions to check if true</param>
        /// <returns>A condition which is true if one of the given conditions are true</returns>
        public static BaseCondition operator |(BaseCondition condition1, BaseCondition condition2)
        {
            AlternativeCondition condition;

            if (condition1 is AlternativeCondition allCondition1)
            {
                condition = allCondition1;
                List<BaseCondition> addedConditions = condition.Conditions.ToList();
                if (condition2 is AlternativeCondition allCondition2)
                {
                    addedConditions.AddRange(allCondition2.Conditions);
                }
                else
                {
                    addedConditions.Add(condition2);
                }
                condition.Conditions = addedConditions.ToArray();
            }
            else if (condition2 is AlternativeCondition allCondition2)
            {
                condition = allCondition2;
                List<BaseCondition> addedConditions = condition.Conditions.ToList();
                addedConditions.Add(condition1);
                condition.Conditions = addedConditions.ToArray();
            }
            else
            {
                condition = new AlternativeCondition(new BaseCondition[] { condition1, condition2 });
            }

            return condition;
        }

        /// <summary>
        /// Converts a single condition into an array
        /// </summary>
        /// <param name="condition">The condition to convert</param>
        public static implicit operator BaseCondition[] (BaseCondition condition)
        {
            return new BaseCondition[] { condition };
        }
    }
}
