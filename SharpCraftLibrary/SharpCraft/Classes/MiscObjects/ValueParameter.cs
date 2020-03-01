using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Struct for holding making parameters which accepts scores and ints
    /// </summary>
    public struct ValueParameter
    {
        /// <summary>
        /// Creates a new <see cref="ValueParameter"/> holding a <see cref="ScoreValue"/>
        /// </summary>
        /// <param name="scoreValue">The <see cref="ScoreValue"/> its holding</param>
        public ValueParameter(ScoreValue scoreValue)
        {
            if (scoreValue is null)
            {
                throw new ArgumentNullException(nameof(scoreValue), "scoreValue may not be null. Paramater needs a value.");
            }
            ScoreValue = scoreValue;
            IntValue = null;
        }

        /// <summary>
        /// Creates a new <see cref="ValueParameter"/> holding an <see cref="int"/>
        /// </summary>
        /// <param name="intValue">The <see cref="int"/> its holding</param>
        public ValueParameter(int intValue)
        {
            ScoreValue = null;
            IntValue = intValue;
        }

        /// <summary>
        /// The <see cref="int"/> its holding
        /// </summary>
        public readonly int? IntValue;

        /// <summary>
        /// The <see cref="ScoreValue"/> its holding
        /// </summary>
        public readonly ScoreValue? ScoreValue;

        /// <summary>
        /// If this <see cref="ValueParameter"/> is holding an <see cref="int"/>
        /// </summary>
        /// <returns>True if it's holding an <see cref="int"/></returns>
        public bool IsInt()
        {
            return !(IntValue is null);
        }

        /// <summary>
        /// If this <see cref="ValueParameter"/> is holding a <see cref="ScoreValue"/>
        /// </summary>
        /// <returns>True if it's holding a <see cref="ScoreValue"/></returns>
        public bool IsScore()
        {
            return !(ScoreValue is null);
        }

        /// <summary>
        /// Converts a <see cref="int"/> into a <see cref="ValueParameter"/>
        /// </summary>
        /// <param name="value">The new <see cref="ValueParameter"/></param>
        public static implicit operator ValueParameter(int value)
        {
            return new ValueParameter(value);
        }

        /// <summary>
        /// Converts a <see cref="ScoreValue"/> into a <see cref="ValueParameter"/>
        /// </summary>
        /// <param name="value">The new <see cref="ValueParameter"/></param>
        public static implicit operator ValueParameter(ScoreValue value)
        {
            return new ValueParameter(value);
        }
    }
}
