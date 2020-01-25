using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the entity has the scores
    /// </summary>
    public class EntityScoresCondition : BaseCondition
    {
        private Scores checkScores;

        /// <summary>
        /// Intializes a new <see cref="EntityScoresCondition"/>
        /// </summary>
        /// <param name="checkScores">The scores to check for</param>
        /// <param name="entity">The entity to check the scores for</param>
        public EntityScoresCondition(ID.LootTarget entity, Scores checkScores) : base("minecraft:entity_scores")
        {
            Entity = entity;
            CheckScores = checkScores;
        }

        /// <summary>
        /// The entity to check the scores for
        /// </summary>
        [DataTag("entity", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget Entity { get; set; }

        /// <summary>
        /// The scores to check for
        /// </summary>
        [DataTag("scores", JsonTag = true)]
        public Scores CheckScores { get => checkScores; set => checkScores = value ?? throw new ArgumentNullException(nameof(CheckScores), "CheckScores may not be null"); }

        /// <summary>
        /// Class for defining scores an entity has to have
        /// </summary>
        public class Scores : IConvertableToDataObject
        {
            private List<Score> checkScores;

            /// <summary>
            /// Intializes a new <see cref="Scores"/> object
            /// </summary>
            /// <param name="scores">The scores the entity has to have</param>
            public Scores(List<Score> scores)
            {
                CheckScores = scores;
            }

            /// <summary>
            /// Intializes a new <see cref="Scores"/> object
            /// </summary>
            /// <param name="score">The score the entity has to have</param>
            public Scores(Score score)
            {
                CheckScores = new List<Score>() { score };
            }

            /// <summary>
            /// The scores the entity has to have
            /// </summary>
            public List<Score> CheckScores { get => checkScores; set => checkScores = value ?? throw new ArgumentNullException(nameof(CheckScores), "CheckScores may not be null"); }

            /// <summary>
            /// Converts this object into a <see cref="DataPartObject"/>
            /// </summary>
            /// <param name="conversionData">Not in use</param>
            /// <returns>This object as a <see cref="DataPartObject"/></returns>
            public DataPartObject GetAsDataObject(object[] conversionData)
            {
                DataPartObject dataObject = new DataPartObject();
                for (int i = 0; i < CheckScores.Count; i++)
                {
                    if (CheckScores[i].Range.Min == CheckScores[i].Range.Max)
                    {
                        dataObject.AddValue(new DataPartPath(CheckScores[i].ScoreObject.Name, new DataPartTag((int)CheckScores[i].Range.Min, isJson: true), true));
                    }
                    else
                    {
                        DataPartObject score = new DataPartObject();
                        score.AddValue(new DataPartPath("min", new DataPartTag((int)(CheckScores[i].Range.Min ?? int.MinValue), isJson: true), true));
                        score.AddValue(new DataPartPath("max", new DataPartTag((int)(CheckScores[i].Range.Max ?? int.MaxValue), isJson: true), true));
                        dataObject.AddValue(new DataPartPath(CheckScores[i].ScoreObject.Name, score, true));
                    }
                }

                return dataObject;
            }

            /// <summary>
            /// Class for defining a score an entity has to have
            /// </summary>
            public class Score
            {
                Objective scoreObject;
                Range range;

                /// <summary>
                /// Intializes a new <see cref="Score"/>
                /// </summary>
                /// <param name="scoreObject">The objective to get the score from</param>
                /// <param name="range">The range the score has to be inside</param>
                public Score(Objective scoreObject, Range range)
                {
                    ScoreObject = scoreObject;
                    Range = range;
                }

                /// <summary>
                /// The objective to get the score from
                /// </summary>
                public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

                /// <summary>
                /// The range the score has to be inside
                /// </summary>
                public Range Range { get => range; set => range = value ?? throw new ArgumentNullException(nameof(Range), "Range may not be null"); }
            }

            /// <summary>
            /// Converts a single score into a list of scores
            /// </summary>
            /// <param name="score">The score to convert</param>
            public static implicit operator Scores (Score score)
            {
                return new Scores(score);
            }
        }
    }
}
