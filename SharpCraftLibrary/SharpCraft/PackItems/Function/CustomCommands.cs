using System;

namespace SharpCraft
{
    public partial class Function
    {
        /// <summary>
        /// All custom commands
        /// </summary>
        public class CustomCommands
        {
            readonly Function function;

            internal CustomCommands(Function parentFunction)
            {
                function = parentFunction;
            }

            /// <summary>
            /// Used by binary search. The command to run when the number is found
            /// </summary>
            /// <param name="command">The command to run</param>
            /// <param name="number">The found number</param>
            public delegate void BinarySearchCommand(Function command, int number);

            /// <summary>
            /// Used by binary search. The command used for checking numbers
            /// </summary>
            /// <param name="command">The command to check with</param>
            /// <param name="minNumber">The minimum the number can be</param>
            /// <param name="maxNumber">The maximum the number can be</param>
            public delegate void BinarySearchMethod(Function command, int minNumber, int maxNumber);

            /// <summary>
            /// Uses binary search to run a command based on a number. 
            /// All search function files be placed in a folder with the calling function's name.
            /// </summary>
            /// <param name="method">The command used for finding the number</param>
            /// <param name="command">The command to run when the number has been found</param>
            /// <param name="minimum">The smallest number to search for</param>
            /// <param name="maximum">The highest number to search for</param>
            public Function BinarySearch(BinarySearchMethod method, BinarySearchCommand command, int minimum, int maximum)
            {
                //Check parameters
                if (maximum <= minimum)
                {
                    throw new ArgumentException("maximum cannot be less than or equal to minimum");
                }
                if (function is null)
                {
                    throw new ArgumentNullException(nameof(function), "function cannot be null");
                }
                if (method is null)
                {
                    throw new ArgumentNullException(nameof(method), "method cannot be null");
                }
                if (command is null)
                {
                    throw new ArgumentNullException(nameof(command), "command cannot be null");
                }

                return WriteBinarySearchFile(function, method, command, minimum, maximum, true);
            }

            private Function WriteBinarySearchFile(Function function, BinarySearchMethod method, BinarySearchCommand command, int minimum, int maximum, bool first = false)
            {
                int halfSize = ((maximum - minimum) / 2) + minimum;
                int numbersLeft = maximum - minimum;

                method(function, minimum, halfSize);
                if (minimum != halfSize)
                {
                    function.World.Function(WriteBinarySearchFile(first ? function.NewChild(minimum + "-" + halfSize) : function.NewSibling(minimum + "-" + halfSize), method, command, minimum, halfSize));
                }
                else
                {
                    command(function, halfSize);
                }
                
                method(function, halfSize + 1, maximum);
                if (halfSize + 1 != maximum)
                {
                    function.World.Function(WriteBinarySearchFile(first ? function.NewChild((halfSize + 1) + "-" + maximum) : function.NewSibling((halfSize + 1) + "-" + maximum), method, command, halfSize + 1, maximum));
                }
                else
                {
                    command(function, halfSize + 1);
                }

                return function;
            }
        }
    }
}
