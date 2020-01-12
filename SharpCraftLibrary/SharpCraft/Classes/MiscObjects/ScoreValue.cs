using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Way of storing a scoreboard balue
    /// </summary>
    public class ScoreValue
    {
        private ScoreObject scoreObject;
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="ScoreValue"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the score</param>
        /// <param name="scoreObject">The objective the score is in</param>
        public ScoreValue(BaseSelector selector, ScoreObject scoreObject)
        {
            Selector = selector;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// Selector for selecting the score
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Selector may only select one score.", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The objective the score is in
        /// </summary>
        public ScoreObject ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// returns the <see cref="ScoreValue"/>'s selector
        /// </summary>
        /// <param name="score">The <see cref="ScoreValue"/>'s selector</param>
        public static implicit operator BaseSelector(ScoreValue score)
        {
            return score.Selector;
        }

        /// <summary>
        /// returns the <see cref="ScoreValue"/>'s scoreObject
        /// </summary>
        /// <param name="score">The <see cref="ScoreValue"/>'s scoreObject</param>
        public static implicit operator ScoreObject(ScoreValue score)
        {
            return score.ScoreObject;
        }
    }
}
