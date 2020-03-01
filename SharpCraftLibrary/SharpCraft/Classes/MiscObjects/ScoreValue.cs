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
        private static int nextVarName = 0;
        private Objective scoreObject = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreValue"/> for holding a value.
        /// </summary>
        public ScoreValue()
        {
            ScoreObject = SharpCraftFiles.GetMathScoreObject();
            Selector = new NameSelector("Variable_" + nextVarName, true);
            nextVarName++;
        }

        /// <summary>
        /// Intializes a new <see cref="ScoreValue"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the score</param>
        /// <param name="scoreObject">The objective the score is in</param>
        public ScoreValue(BaseSelector selector, Objective scoreObject)
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
            protected set
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
        public Objective ScoreObject { get => scoreObject; protected set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

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
        public static implicit operator Objective(ScoreValue score)
        {
            return score.ScoreObject;
        }

        #region math
        #region adding
        /// <summary>
        /// Adds the two values together into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">One of the numbers to add together</param>
        /// <param name="value2">One of the numbers to add together</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator +(ScoreValue value1, ValueParameter value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Add, value2);
        }

        /// <summary>
        /// Adds the two values together into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">One of the numbers to add together</param>
        /// <param name="value2">One of the numbers to add together</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator +(int value1, ScoreValue value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Add, value2);
        }
        #endregion
        #region subtracting
        /// <summary>
        /// subtracts the two values from each other into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to subtract from</param>
        /// <param name="value2">The number to subtract with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator -(ScoreValue value1, ValueParameter value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Subtract, value2);
        }

        /// <summary>
        /// subtracts the two values from each other into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to subtract from</param>
        /// <param name="value2">The number to subtract with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator -(int value1, ScoreValue value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Subtract, value2);
        }
        #endregion
        #region multiplying
        /// <summary>
        /// multiplies the two values together into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">One of the values to multiply with</param>
        /// <param name="value2">One of the values to multiply with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator *(ScoreValue value1, ValueParameter value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Multiply, value2);
        }

        /// <summary>
        /// multiplies the two values together into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">One of the values to multiply with</param>
        /// <param name="value2">One of the values to multiply with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator *(int value1, ScoreValue value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Multiply, value2);
        }
        #endregion
        #region dividing
        /// <summary>
        /// divides the two values by each other into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to divide</param>
        /// <param name="value2">The number to divide with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator /(ScoreValue value1, ValueParameter value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Divide, value2);
        }

        /// <summary>
        /// divides the two values by each other into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to divide</param>
        /// <param name="value2">The number to divide with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator /(int value1, ScoreValue value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Divide, value2);
        }
        #endregion
        #region moduloing
        /// <summary>
        /// modulos the two values into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to divide</param>
        /// <param name="value2">The number to divide with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator %(ScoreValue value1, ValueParameter value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Remainder, value2);
        }

        /// <summary>
        /// modulos the two values into a <see cref="FunctionWriters.CustomCommands.ScoreOperation"/>
        /// </summary>
        /// <param name="value1">The number to divide</param>
        /// <param name="value2">The number to divide with</param>
        /// <returns>The score operation for doing the math</returns>
        public static FunctionWriters.CustomCommands.ScoreOperation operator %(int value1, ScoreValue value2)
        {
            return new FunctionWriters.CustomCommands.ScoreOperation(value1, ID.Operation.Remainder, value2);
        }
        #endregion
        #endregion
    }
}
