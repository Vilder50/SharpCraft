using System;
using System.Collections.Generic;
using System.Linq;
using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All custom commands
    /// </summary>
    public class CustomCommands
    {
        /// <summary>
        /// The function to write onto
        /// </summary>
        public Function Function { get; private set; }

        internal CustomCommands(Function parentFunction)
        {
            Function = parentFunction;
        }

        #region tree search
        /// <summary>
        /// Used by binary search. The command to run when the number is found
        /// </summary>
        /// <param name="number">The found number</param>
        /// <returns>The command to run when the number is found</returns>
        public delegate BaseCommand TreeSearchCommand(int number);

        /// <summary>
        /// Used by binary search. The command used for checking numbers
        /// </summary>
        /// <param name="minNumber">The minimum the number can be</param>
        /// <param name="maxNumber">The maximum the number can be</param>
        /// <returns>The command used for searching</returns>
        public delegate BaseExecuteCommand TreeSearchMethod(int minNumber, int maxNumber);

        /// <summary>
        /// Uses binary search to run a command based on a number. 
        /// All search function files be placed in a folder with the calling function's name.
        /// </summary>
        /// <param name="method">The command used for finding the number</param>
        /// <param name="command">The command to run when the number has been found</param>
        /// <param name="minimum">The smallest number to search for</param>
        /// <param name="maximum">The highest number to search for</param>
        /// <param name="branches">The amount of branches to check at a time (Low number = fast, but many files. High number = slow, but less files)</param>
        public void TreeSearch(TreeSearchMethod method, TreeSearchCommand command, int minimum, int maximum, int branches = 2)
        {
            //Check parameters
            if (maximum <= minimum)
            {
                throw new ArgumentException("maximum cannot be less than or equal to minimum");
            }
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method), "method cannot be null");
            }
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), "command cannot be null");
            }
            if (branches < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(branches), "Branches cannot be less than 2");
            }

            GroupCommands(f => 
            { 
                WriteTreeSearch(Function, method, command, minimum, maximum, branches, true);
            });
        }

        private Function WriteTreeSearch(Function function, TreeSearchMethod method, TreeSearchCommand command, int minimum, int maximum, int branches, bool first = false)
        {
            int partSize = Math.Max((maximum - minimum + 1) / branches, 1);
            for (int i = 0; i < branches; i++)
            {
                int start = partSize * i + minimum;
                int end = Math.Min(partSize * (i + 1) + minimum - 1,maximum);
                if (i == branches - 1)
                {
                    end = maximum;
                }

                function.AddCommand(method(start, end));
                if (start == end)
                {
                    function.AddCommand(command(start));
                }
                else
                {
                    function.World.Function(WriteTreeSearch(first ? function.NewChild(start + "-" + end) : function.NewSibling(start + "-" + end), method, command, start, end, branches));
                }

                if (end == maximum)
                {
                    break;
                }
            }
            if (!first)
            {
                function.Dispose();
            }
            return function;
        }
        #endregion

        #region summon execute
        /// <summary>
        /// Summons a new entity and runs the given commands as the entity
        /// </summary>
        /// <param name="entity">The entity to summon</param>
        /// <param name="functionName">The name of the function it should run</param>
        /// <param name="runCommands">the commands the entity should run</param>
        /// <param name="executeAt">True if it should run the commands at the entity's location</param>
        /// <param name="writeSetting">The setting for writing the function file</param>
        /// <returns>The function the entity runs</returns>
        public Function SummonExecute(Entity.EntityBasic entity, string functionName, Function.FunctionWriter runCommands, bool executeAt = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            return SummonExecute(entity, new Coords(), functionName, runCommands, executeAt, writeSetting);
        }

        /// <summary>
        /// Summons a new entity and runs the given commands as the entity
        /// </summary>
        /// <param name="entity">The entity to summon</param>
        /// <param name="functionName">The name of the function it should run (this function is created as a sibling to the function running this)</param>
        /// <param name="runCommands">the commands the entity should run</param>
        /// <param name="executeAt">True if it should run the commands at the entity's location</param>
        /// <param name="spawnCoords">The place to spawn the entity at</param>
        /// <param name="writeSetting">The setting for writing the function file</param>
        /// <returns>The function the entity runs</returns>
        public Function SummonExecute(Entity.EntityBasic entity, Vector spawnCoords, string functionName, Function.FunctionWriter runCommands, bool executeAt = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException("function name cannot be null or empty", nameof(functionName));
            }
            if (runCommands is null)
            {
                throw new ArgumentNullException(nameof(runCommands), "value may not be null");
            }

            //add tag to find the summoned entity
            Tag findTag = new Tag("SharpSummon");
            Entity.EntityBasic createEntity = (Entity.EntityBasic)entity.Clone();
            List<Tag> tags = createEntity.Tags.ToList() ?? new List<Tag>();
            tags.Add(findTag);
            createEntity.Tags = tags.ToArray();

            //summon entity and execute as it
            Function executeAs = null;
            GroupCommands(f =>
            {
                f.Entity.Add(createEntity, spawnCoords);
                f.Execute.As(new Selector(ID.Selector.e, findTag));
                if (executeAt)
                {
                    f.Execute.At();
                }
                executeAs = (Function)f.World.Function(f.NewSibling(functionName, writeSetting));
                executeAs.Entity.Tag.Remove(new Selector(), findTag);
                runCommands(executeAs);
            });
            return executeAs;
        }
        #endregion

        #region math
        /// <summary>
        /// Holds a math operation between two scores
        /// </summary>
        public class ScoreOperation : ScoreValue
        {
            private ScoreValue score1;
            private ScoreValue score2;

            /// <summary>
            /// Intializes a new <see cref="ScoreOperation"/>
            /// </summary>
            /// <param name="score1">First score to do math on</param>
            /// <param name="operation">The math operation to do</param>
            /// <param name="score2">Second score to do math on</param>
            public ScoreOperation(ScoreValue score1, ID.Operation operation, ScoreValue score2) : base(new NameSelector("undefined",true), new Objective("undefined"))
            {
                Score1 = score1;
                Score2 = score2;
                Operation = operation;
            }

            /// <summary>
            /// First score to do math on
            /// </summary>
            public ScoreValue Score1 { get => score1; private set => score1 = value; }

            /// <summary>
            /// The math operation to do
            /// </summary>
            public ScoreValue Score2 { get => score2; private set => score2 = value; }

            /// <summary>
            /// Second score to do math on
            /// </summary>
            public ID.Operation Operation { get; private set; }

            /// <summary>
            /// Writes the commands needed to perform the operation to the given function
            /// </summary>
            /// <param name="function">The function to write the commands to</param>
            /// <param name="endingValue">The <see cref="ScoreValue"/> the result should end up in</param>
            public void WriteCommands(Function function, ScoreValue endingValue)
            {
                if (function is null)
                {
                    throw new ArgumentNullException(nameof(function), "Cannot write operation commands to null");
                }
                if (endingValue is null)
                {
                    throw new ArgumentNullException(nameof(endingValue), "Cannot give the final operation score to null");
                }

                int usedNumbers = -1;
                WriteCommands(function, endingValue, SharpCraftFiles.GetMathScoreObject(), ref usedNumbers);
            }

            private ScoreValue WriteCommands(Function function, ScoreValue endingValue, Objective mathObjective, ref int usedNumbers)
            {
                bool makeLastCommand = false;
                if (usedNumbers == -1)
                {
                    makeLastCommand = true;
                    usedNumbers = 0;
                }

                ScoreObject = mathObjective;
                ScoreValue firstScoreValue;
                ScoreValue secondScoreValue;

                //Calculate value of child operations
                if (Score1 is ScoreOperation operation1)
                {
                    firstScoreValue = operation1.WriteCommands(function, endingValue, mathObjective, ref usedNumbers);
                    Selector = firstScoreValue;
                }
                else
                {
                    firstScoreValue = score1;
                }
                if (Score2 is ScoreOperation operation2)
                {
                    secondScoreValue = operation2.WriteCommands(function, endingValue, mathObjective, ref usedNumbers);
                    if (Operation == ID.Operation.Add || Operation == ID.Operation.Multiply)
                    {
                        Selector = secondScoreValue;
                        (firstScoreValue, secondScoreValue) = (secondScoreValue, firstScoreValue);
                    }
                }
                else
                {
                    secondScoreValue = score2;
                }

                //Calculate value of this operation
                if (makeLastCommand)
                {
                    function.AddCommand(new ScoreboardOperationCommand(endingValue, endingValue, ID.Operation.Equel, firstScoreValue, firstScoreValue));
                    function.AddCommand(new ScoreboardOperationCommand(endingValue, endingValue, Operation, secondScoreValue, secondScoreValue));
                }
                else
                {
                    if (Selector is NameSelector nameSelector && nameSelector.Name == "undefined")
                    {
                        (Selector as NameSelector).Name = "Value" + usedNumbers;
                        usedNumbers++;
                        function.AddCommand(new ScoreboardOperationCommand(Selector, mathObjective, ID.Operation.Equel, firstScoreValue, firstScoreValue));
                    }
                    function.AddCommand(new ScoreboardOperationCommand(Selector, mathObjective, Operation, secondScoreValue, secondScoreValue));
                }
                return this;
            }
        }

        /// <summary>
        /// Sets a score value to the value made by a calculation
        /// </summary>
        /// <param name="selector">The selector for selecting the score</param>
        /// <param name="objective">The objective the score to change is in</param>
        /// <param name="operation">The operation calculating the value the score should be set to</param>
        public void SetToScoreOperation(BaseSelector selector, Objective objective, ScoreOperation operation)
        {
            GroupCommands(f =>
            {
                operation.WriteCommands(Function, new ScoreValue(selector, objective));
            });
        }
        #endregion

        #region command grouping
        private class ExecutePrefixer : ICommandChanger
        {
            private readonly BaseExecuteCommand prefixCommand;
            private readonly Function writeToFunction;

            public ExecutePrefixer(BaseExecuteCommand prefixCommand, Function writeToFunction)
            {
                this.prefixCommand = prefixCommand;
                this.writeToFunction = writeToFunction;
            }

            public bool DoneChanging { get; set; }

            public ICommand ChangeCommand(ICommand command)
            {
                bool foundThis = false;
                foreach (ICommand functionCommand in writeToFunction.Commands)
                {
                    if (functionCommand == this)
                    {
                        foundThis = true;
                    }
                    else if (foundThis && functionCommand is BaseExecuteCommand execute && !execute.DoneChanging)
                    {
                        return command;
                    }
                }
                BaseExecuteCommand prefixer = (BaseExecuteCommand)prefixCommand.ShallowClone();
                prefixer.ExecuteCommand = null;
                return prefixer.AddCommand(command);
            }

            public string GetCommandString()
            {
                return null;
            }

            public BaseCommand ShallowClone()
            {
                return prefixCommand.ShallowClone();
            }
        }

        /// <summary>
        /// If the last command is an unfinished execute command, every given command will be executed with a clone of the execute command. (All the given commands will only run if the execute command runs)
        /// </summary>
        /// <param name="writer">Writer for writing the commands</param>
        /// <param name="forceFunction">Use a function instead of multiple execute commands</param>
        public void GroupCommands(Function.FunctionWriter writer, bool forceFunction = false)
        {
            if (Function.Commands.Count != 0 && Function.Commands.Last() is BaseExecuteCommand execute && !execute.DoneChanging)
            {
                if (forceFunction || Function.PackNamespace.IsSettingSet(new NamespaceSettings().FunctionGroupedCommands()))
                {
                    Function.World.Function(Function.NewSibling(writer, Function.Setting));
                }
                else
                {
                    BaseExecuteCommand executeCommand = (BaseExecuteCommand)execute.ShallowClone();
                    int prefixLocation = Function.Commands.Count - 1;
                    Function.Commands.RemoveAt(prefixLocation);
                    ExecutePrefixer prefixer = new ExecutePrefixer(executeCommand, Function);
                    Function.AddCommand(new ExecutePrefixer(executeCommand, Function));
                    writer(Function);
                    prefixer.DoneChanging = true;
                    Function.Commands.RemoveAt(prefixLocation);
                }
            }
            else
            {
                writer(Function);
            }
        }
        #endregion

        #region if else
        private static readonly NameSelector ifElseSelector = new NameSelector("ifelse",true);

        /// <summary>
        /// Runs the if commands if the testCommand successfully runs. Runs else if it doesn't
        /// </summary>
        /// <param name="testCommand">The command to test</param>
        /// <param name="ifWriter">The commands to run if the test is successfully</param>
        /// <param name="elseWriter">The commands to run if the test isn't successfully</param>
        /// <param name="ifFunctionName">The name of the function for running the if commands</param>
        /// <param name="elseFunctionName">The name of the function for running the else commands</param>
        public void IfElse(BaseExecuteCommand testCommand, Function.FunctionWriter ifWriter, Function.FunctionWriter elseWriter, string ifFunctionName = null, string elseFunctionName = null)
        {
            if (testCommand.HasEndCommand())
            {
                throw new ArgumentException("TestCommand may not have an ending command.", nameof(testCommand));
            }
            if (ifWriter is null || elseWriter is null)
            {
                throw new ArgumentNullException("IfWriter and elseWriter may not be null");
            }

            Objective math = SharpCraftFiles.GetMathScoreObject();
            GroupCommands((f) =>
            {
                f.AddCommand(new ScoreboardValueChangeCommand(ifElseSelector, math, ID.ScoreChange.set, 0));
                f.AddCommand(testCommand);
                Function ifFunction = f.World.Function(Function.NewSibling(ifFunctionName, ifWriter)) as Function;
                ifFunction.Commands.Add(new ScoreboardValueChangeCommand(ifElseSelector, math, ID.ScoreChange.set, 1));
                ifFunction.Dispose();

                f.AddCommand(new ExecuteIfScoreMatches(ifElseSelector, math, 0));
                Function elseFunction = f.World.Function(Function.NewSibling(elseFunctionName, elseWriter)) as Function;
                elseFunction.Dispose();
            });
        }
        #endregion

        #region loops
        /// <summary>
        /// Keeps on running the given commands for as long as the test command runs successfully
        /// </summary>
        /// <param name="testCommand">The command which has to run successfully for the loop to continue</param>
        /// <param name="loopWriter">The commands the loop should run</param>
        /// <param name="loopName">The name of the loop file</param>
        public void WhileLoop(BaseExecuteCommand testCommand, Function.FunctionWriter loopWriter, string loopName = null)
        {
            if (testCommand.HasEndCommand())
            {
                throw new ArgumentException("TestCommand may not have an ending command.", nameof(testCommand));
            }
            if (loopWriter is null)
            {
                throw new ArgumentNullException(nameof(loopWriter), "LoopWriter may not be null");
            }

            Function.AddCommand(testCommand.ShallowClone());
            Function loopFunction = Function.World.Function(Function.NewSibling(loopName, loopWriter)) as Function;
            loopFunction.Commands.Add(testCommand.AddCommand(new RunFunctionCommand(loopFunction)));
        }

        /// <summary>
        /// Delegate for making for loops
        /// </summary>
        /// <param name="writeTo">The function to write the loop commands to</param>
        /// <param name="loopValue">The loop's value</param>
        public delegate void ForLoopDelegate(Function writeTo, ScoreValue loopValue);

        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        public void ForLoop(int from, int to, string loopName, ForLoopDelegate writer)
        {
            if (from == to)
            {
                throw new ArgumentException("From and To has the same value so a loop isn't needed.");
            }
            CreateForLoop(from, null, to, null, loopName, writer, from < to);
        }

        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        public void ForLoop(ScoreValue from, int to, string loopName, ForLoopDelegate writer, bool positive = true)
        {
            CreateForLoop(0, from, to, null, loopName, writer, positive);
        }


        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        public void ForLoop(int from, ScoreValue to, string loopName, ForLoopDelegate writer, bool positive = true)
        {
            CreateForLoop(from, null, 0, to, loopName, writer, positive);
        }

        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        public void ForLoop(ScoreValue from, ScoreValue to, string loopName, ForLoopDelegate writer, bool positive = true)
        {
            CreateForLoop(0, from, 0, to, loopName, writer, positive);
        }

        private void CreateForLoop(int from, ScoreValue fromValue, int to, ScoreValue toValue, string loopName, ForLoopDelegate writer, bool positive)
        {
            Objective math = SharpCraftFiles.GetMathScoreObject();
            NameSelector loopSelector = new NameSelector("l_" + loopName);
            GroupCommands((f) =>
            {
                //start loop
                if (fromValue is null)
                {
                    f.Entity.Score.Set(loopSelector, math, from);
                }
                else
                {
                    f.Entity.Score.Operation(loopSelector, math, ID.Operation.Equel, fromValue, fromValue);
                }
                BaseCommand testCommand;
                if (toValue is null)
                {
                    if (positive)
                    {
                        testCommand = new ExecuteIfScoreMatches(loopSelector, math, new Range(null, to));
                    }
                    else
                    {
                        testCommand = new ExecuteIfScoreMatches(loopSelector, math, new Range(to, null));
                    }
                }
                else
                {
                    if (positive)
                    {
                        testCommand = new ExecuteIfScoreRelative(loopSelector, math, ID.IfScoreOperation.SmallerOrEquel, toValue, toValue);
                    }
                    else
                    {
                        testCommand = new ExecuteIfScoreRelative(loopSelector, math, ID.IfScoreOperation.HigherOrEquel, toValue, toValue);
                    }
                }
                if (!(fromValue is null && toValue is null))
                {
                    f.AddCommand(testCommand.ShallowClone());
                }
                f.World.Function(f.NewSibling(loopName, (loop) =>
                {
                    //run loop
                    writer(loop, new ScoreValue(loopSelector, math));

                    if (positive)
                    {
                        loop.Entity.Score.Add(loopSelector, math, 1);
                    }
                    else
                    {
                        loop.Entity.Score.Add(loopSelector, math, -1);
                    }
                    loop.AddCommand(testCommand);
                    loop.World.Function(loop);
                }));
            });
        }
        #endregion
    }
}