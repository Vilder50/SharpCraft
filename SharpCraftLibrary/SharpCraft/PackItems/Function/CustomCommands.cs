using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SharpCraft.Commands;
using SharpCraft.Conditions;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All custom commands
    /// </summary>
    public class CustomCommands : CommandList
    {
        internal CustomCommands(Function parentFunction) : base(parentFunction)
        {
            Random = new ClassRandom(ForFunction);
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
                WriteTreeSearch(ForFunction, method, command, minimum, maximum, branches, true);
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
            if (runCommands is null)
            {
                throw new ArgumentNullException(nameof(runCommands), "value may not be null");
            }

            //add tag to find the summoned entity
            Tag findTag = new Tag("SharpSummon");
            Entity.EntityBasic createEntity = (Entity.EntityBasic)entity.Clone();
            List<Tag> tags = createEntity.Tags?.ToList() ?? new List<Tag>();
            tags.Add(findTag);
            createEntity.Tags = tags.ToArray();

            //summon entity and execute as it
            Function executeAs = null;
            GroupCommands(f =>
            {
                f.Entity.Add(spawnCoords, createEntity);
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
                operation.WriteCommands(ForFunction, new ScoreValue(selector, objective));
            });
        }
        #endregion

        #region command grouping
        private class ExecutePrefixer : ICommandChanger
        {
            public bool DoneChanging { get; set; }

            public ICommand ChangeCommand(ICommand command)
            {
                return command;
            }

            public string GetCommandString()
            {
                return null;
            }

            public BaseCommand ShallowClone()
            {
                return null;
            }
        }

        /// <summary>
        /// If the last command is an unfinished execute command, every given command will be executed with a clone of the execute command. (All the given commands will only run if the execute command runs)
        /// </summary>
        /// <param name="writer">Writer for writing the commands</param>
        /// <param name="useFunction">Use a function instead of multiple execute commands</param>
        /// <param name="forceExecute">Don't use function no matter what</param>
        public void GroupCommands(Function.FunctionWriter writer, bool useFunction = false, bool forceExecute = false)
        {
            if (ForFunction.Commands.Count != 0 && ForFunction.Commands.Last() is BaseExecuteCommand execute && !execute.DoneChanging)
            {
                if (!forceExecute && (useFunction || ForFunction.PackNamespace.IsSettingSet(new NamespaceSettings().FunctionGroupedCommands())))
                {
                    ForFunction.World.Function(ForFunction.NewSibling(writer, ForFunction.Setting));
                }
                else
                {
                    BaseExecuteCommand executeCommand = (BaseExecuteCommand)execute.ShallowClone();
                    int prefixLocation = ForFunction.Commands.Count - 1;
                    ForFunction.Commands.RemoveAt(prefixLocation);
                    ExecutePrefixer prefixer = new ExecutePrefixer();
                    ForFunction.AddCommand(prefixer);
                    writer(ForFunction);
                    bool foundPrefixer = false;
                    for (int i = 0; i < ForFunction.Commands.Count; i++)
                    {
                        if (foundPrefixer)
                        {
                            BaseExecuteCommand prefixWith = (BaseExecuteCommand)executeCommand.ShallowClone();
                            ForFunction.Commands[i] = prefixWith.AddCommand(ForFunction.Commands[i]);
                        }
                        else if (ForFunction.Commands[i] == prefixer)
                        {
                            foundPrefixer = true;
                        }
                    }
                    prefixer.DoneChanging = true;
                    ForFunction.Commands.RemoveAt(prefixLocation);
                }
            }
            else
            {
                writer(ForFunction);
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
                Function ifFunction = f.World.Function(ForFunction.NewSibling(ifFunctionName, ifWriter)) as Function;
                ifFunction.Commands.Add(new ScoreboardValueChangeCommand(ifElseSelector, math, ID.ScoreChange.set, 1));
                ifFunction.Dispose();

                f.AddCommand(new ExecuteIfScoreMatches(ifElseSelector, math, 0));
                Function elseFunction = f.World.Function(ForFunction.NewSibling(elseFunctionName, elseWriter)) as Function;
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
        /// <param name="nextExecute">Runs the next loop cycle with the given execute command. Leave null for no commands</param>
        public void WhileLoop(BaseExecuteCommand testCommand, Function.FunctionWriter loopWriter, string loopName = null, BaseExecuteCommand nextExecute = null)
        {
            if (testCommand.HasEndCommand())
            {
                throw new ArgumentException("TestCommand may not have an ending command.", nameof(testCommand));
            }
            if (!(nextExecute is null) && nextExecute.HasEndCommand())
            {
                throw new ArgumentException("NextExecute may not have an ending command.", nameof(nextExecute));
            }
            if (loopWriter is null)
            {
                throw new ArgumentNullException(nameof(loopWriter), "LoopWriter may not be null");
            }

            ForFunction.AddCommand(testCommand.ShallowClone());
            Function loopFunction = ForFunction.World.Function(ForFunction.NewSibling(loopName, loopWriter)) as Function;
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
        /// <param name="nextExecute">Runs the next loop cycle with the given execute command. Leave null for no commands</param>
        /// <param name="stopAtTo">If the loop should stop before running with the "to" number</param>
        public void ForLoop(int from, int to, string loopName, ForLoopDelegate writer, BaseExecuteCommand nextExecute = null, bool stopAtTo = false)
        {
            if (from == to)
            {
                throw new ArgumentException("From and To has the same value so a loop isn't needed.");
            }
            CreateForLoop(from, null, to, null, loopName, writer, from < to, nextExecute, stopAtTo);
        }

        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        /// <param name="nextExecute">Runs the next loop cycle with the given execute command. Leave null for no commands</param>
        /// <param name="stopAtTo">If the loop should stop before running with the "to" number</param>
        public void ForLoop(ScoreValue from, int to, string loopName, ForLoopDelegate writer, bool positive = true, BaseExecuteCommand nextExecute = null, bool stopAtTo = false)
        {
            CreateForLoop(0, from, to, null, loopName, writer, positive, nextExecute, stopAtTo);
        }


        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        /// <param name="nextExecute">Runs the next loop cycle with the given execute command. Leave null for no commands</param>
        /// <param name="stopAtTo">If the loop should stop before running with the "to" number</param>
        public void ForLoop(int from, ScoreValue to, string loopName, ForLoopDelegate writer, bool positive = true, BaseExecuteCommand nextExecute = null, bool stopAtTo = false)
        {
            CreateForLoop(from, null, 0, to, loopName, writer, positive, nextExecute, stopAtTo);
        }

        /// <summary>
        /// Loops through every value from "from" to and with "to"
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The ending value</param>
        /// <param name="loopName">The name of the loop (used for function name and score name)</param>
        /// <param name="writer">Writer for writing commands to loop over</param>
        /// <param name="positive">If the loop is going from a small number to a high number. False if it's going from high to small</param>
        /// <param name="nextExecute">Runs the next loop cycle with the given execute command. Leave null for no commands</param>
        /// <param name="stopAtTo">If the loop should stop before running with the "to" number</param>
        public void ForLoop(ScoreValue from, ScoreValue to, string loopName, ForLoopDelegate writer, bool positive = true, BaseExecuteCommand nextExecute = null, bool stopAtTo = false)
        {
            CreateForLoop(0, from, 0, to, loopName, writer, positive, nextExecute, stopAtTo);
        }

        private void CreateForLoop(int from, ScoreValue fromValue, int to, ScoreValue toValue, string loopName, ForLoopDelegate writer, bool positive, BaseExecuteCommand nextExecute, bool stopAtTo)
        {
            if (!(nextExecute is null) && nextExecute.HasEndCommand())
            {
                throw new ArgumentException("NextExecute may not have an ending command.", nameof(nextExecute));
            }
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
                        testCommand = new ExecuteIfScoreMatches(loopSelector, math, new MCRange(null, to - (stopAtTo ? 1 : 0)));
                    }
                    else
                    {
                        testCommand = new ExecuteIfScoreMatches(loopSelector, math, new MCRange(to + (stopAtTo ? 1 : 0), null));
                    }
                }
                else
                {
                    ID.IfScoreOperation useOperator = (positive ? (stopAtTo ? ID.IfScoreOperation.Smaller : ID.IfScoreOperation.SmallerOrEquel) : (stopAtTo ? ID.IfScoreOperation.Higher : ID.IfScoreOperation.HigherOrEquel));
                    testCommand = new ExecuteIfScoreRelative(loopSelector, math, useOperator, toValue, toValue);
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
                    if (!(nextExecute is null))
                    {
                        loop.AddCommand(nextExecute);
                    }
                    loop.World.Function(loop);
                }));
            });
        }
        #endregion

        #region ray cast
        private static void ValidateRayParams(string rayName, double length, Function.FunctionWriter onHit)
        {
            if (string.IsNullOrWhiteSpace(rayName))
            {
                throw new ArgumentException("RayName may not be null or whitespace.", nameof(rayName));
            }
            if (rayName.IndexOf("/") != -1 || rayName.IndexOf("\\") != -1)
            {
                throw new ArgumentException("RayName may not contain / or \\.", nameof(rayName));
            }
            if (onHit is null)
            {
                throw new ArgumentNullException(nameof(onHit), "Ray cast onHit may not be null");
            }
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Ray length may not be smaller or equal to 0");
            }
        }

        /// <summary>
        /// Shots a ray which stops and run commands when it hits a block.
        /// </summary>
        /// <param name="rayName">The name of the ray</param>
        /// <param name="hit">Block type the ray should be able to hit. Leave null to make the ray hit everything.</param>
        /// <param name="ignore">Block type the ray shouldn't be able to hit. Leave null for no ignored blocks.</param>
        /// <param name="length">The amount of blocks the ray should travel</param>
        /// <param name="onHit">The commands to run when the ray hits a block.</param>
        /// <param name="stepCommands">Commands to run for every block the ray moves.</param>
        /// <remarks>
        /// Running ray cast from within a ray cast might lead to unexpected behavior.
        /// </remarks>
        public void RayCast(string rayName, Block hit, Block ignore, int length, Function.FunctionWriter onHit, Function.FunctionWriter stepCommands = null)
        {
            ValidateRayParams(rayName, length, onHit);

            Objective math = SharpCraftFiles.GetMathScoreObject();
            var (raySetup, xRotation, yRotation, rayState, predicates) = SharpCraftFiles.GetRayFiles();

            //execute as ray entity
            ForFunction.Execute.As(SharpCraftFiles.GetDummySelector());
            ForFunction.World.Function(ForFunction.NewSibling(rayName + "\\start", startRay => 
            {
                startRay.World.Function(raySetup);
                startRay.Custom.ForLoop(0, length - 1, "rStep", (step, stepValue) =>
                {
                    //setup step
                    step.Entity.Score.Set(rayState, rayState, 0);
                    step.Execute.Align();
                    step.Execute.Positioned(new Coords(0.1));
                    step.Entity.Teleport(ID.Selector.s, new Coords());

                    if (!(stepCommands is null))
                    {
                        stepCommands(step);
                    }

                    //check if small step should start
                    step.Execute.Positioned(new LocalCoords(0, 0, 1));
                    step.Execute.IfBlocks(new Coords(), new Coords(), new Coords()); //out of world check

                    #region check predicate
                    BaseCondition getIfBlockCondition(IntVector location)
                    {
                        BaseCondition blockCondition = null;
                        if (!(ignore is null))
                        {
                            blockCondition = !new LocationCondition(new JSONObjects.Location() { Block = ignore }) { Offset = location };
                        }
                        if (!(hit is null))
                        {
                            BaseCondition hitCondition = new LocationCondition(new JSONObjects.Location() { Block = hit }) { Offset = location };
                            if (blockCondition is null)
                            {
                                blockCondition = hitCondition;
                            }
                            else
                            {
                                blockCondition &= hitCondition;
                            }
                        }
                        return blockCondition;
                    }
                    BaseCondition checkCondition = getIfBlockCondition(new IntVector(0));
                    checkCondition |= !(predicates[2].GetCondition() | !getIfBlockCondition(new IntVector(0, 0, -1)));
                    checkCondition |= !(predicates[3].GetCondition() | !getIfBlockCondition(new IntVector(1, 0, 0)));
                    checkCondition |= !(predicates[4].GetCondition() | !getIfBlockCondition(new IntVector(0, 0, 1)));
                    checkCondition |= !(predicates[5].GetCondition() | !getIfBlockCondition(new IntVector(-1, 0, 0)));
                    checkCondition |= !(predicates[6].GetCondition() | !getIfBlockCondition(new IntVector(0, -1, 0)));
                    checkCondition |= !(predicates[7].GetCondition() | !getIfBlockCondition(new IntVector(0, 1, 0)));

                    BaseCondition cornerConditions = predicates[8].GetCondition();
                    BaseCondition positiveCorners = !(predicates[9].GetCondition() | !(getIfBlockCondition(new IntVector(-1, 0, -1)) | getIfBlockCondition(new IntVector(0, 1, -1)) | getIfBlockCondition(new IntVector(-1, 1, 0))));
                    positiveCorners |= !(predicates[10].GetCondition() | !(getIfBlockCondition(new IntVector(-1, 0, 1)) | getIfBlockCondition(new IntVector(0, 1, 1)) | getIfBlockCondition(new IntVector(-1, 1, 0))));
                    positiveCorners |= !(predicates[11].GetCondition() | !(getIfBlockCondition(new IntVector(1, 0, 1)) | getIfBlockCondition(new IntVector(0, 1, 1)) | getIfBlockCondition(new IntVector(1, 1, 0))));
                    positiveCorners |= !(predicates[12].GetCondition() | !(getIfBlockCondition(new IntVector(1, 0, -1)) | getIfBlockCondition(new IntVector(0, 1, -1)) | getIfBlockCondition(new IntVector(1, 1, 0))));
                    positiveCorners = !predicates[1].GetCondition() | !positiveCorners;
                    BaseCondition negativeCorners = !(predicates[9].GetCondition() | !(getIfBlockCondition(new IntVector(-1, 0, -1)) | getIfBlockCondition(new IntVector(0, -1, -1)) | getIfBlockCondition(new IntVector(-1, -1, 0))));
                    negativeCorners |= !(predicates[10].GetCondition() | !(getIfBlockCondition(new IntVector(-1, 0, 1)) | getIfBlockCondition(new IntVector(0, -1, 1)) | getIfBlockCondition(new IntVector(-1, -1, 0))));
                    negativeCorners |= !(predicates[11].GetCondition() | !(getIfBlockCondition(new IntVector(1, 0, 1)) | getIfBlockCondition(new IntVector(0, -1, 1)) | getIfBlockCondition(new IntVector(1, -1, 0))));
                    negativeCorners |= !(predicates[12].GetCondition() | !(getIfBlockCondition(new IntVector(1, 0, -1)) | getIfBlockCondition(new IntVector(0, -1, -1)) | getIfBlockCondition(new IntVector(1, -1, 0))));
                    negativeCorners = !predicates[0].GetCondition() | !negativeCorners;
                    cornerConditions |= !(!positiveCorners | !negativeCorners);

                    checkCondition |= !cornerConditions;

                    using (Predicate checkPredicate = new Predicate(step.PackNamespace, "ray\\" + rayName + "\\check", checkCondition))
                    {
                        step.Execute.IfPredicate(checkPredicate);
                    }
                    #endregion

                    step.Execute.Positioned(new LocalCoords(0, 0, -1));
                    step.World.Function(step.NewSibling("rStartSmall", startSmall => 
                    {
                        //begin small steps
                        startSmall.Custom.ForLoop(0, 49, "rsStep", (smallStep, smallValue) => 
                        {
                            //Function to run when block is hit
                            Function onHitBlock = smallStep.NewSibling("hitBlock", hitBlock =>
                            {
                                hitBlock.Entity.Score.Set(rayState, rayState, 1);
                                onHit(hitBlock);
                            });

                            BaseCommand hitCheck = new ExecuteIfScoreMatches(rayState, rayState, 0).AddCommand(new RunFunctionCommand(onHitBlock));
                            if (!(ignore is null))
                            {
                                hitCheck = new ExecuteIfBlock(new Coords(), ignore, false).AddCommand(hitCheck);
                            }
                            if (!(hit is null))
                            {
                                hitCheck = new ExecuteIfBlock(new Coords(), hit).AddCommand(hitCheck);
                            }

                            //check if small step hits block
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, xRotation, new MCRange(0, 90));
                            smallStep.Execute.Positioned(new Coords(0, -0.02, 0));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, xRotation, new MCRange(-90, 0));
                            smallStep.Execute.Positioned(new Coords(0, 0.02, 0));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, yRotation, new MCRange(0, 180));
                            smallStep.Execute.Positioned(new Coords(-0.02, 0, 0));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, yRotation, new MCRange(0, 180), false);
                            smallStep.Execute.Positioned(new Coords(0.02, 0, 0));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, yRotation, new MCRange(-90, 90));
                            smallStep.Execute.Positioned(new Coords(0, 0, 0.02));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                            smallStep.Execute.IfScore(ID.Selector.s, yRotation, new MCRange(-90, 90), false);
                            smallStep.Execute.Positioned(new Coords(0, 0, -0.02));
                            smallStep.AddCommand(hitCheck.ShallowClone());

                        }, new ExecuteIfScoreMatches(rayState, rayState, 0).AddCommand(new ExecutePosition(new LocalCoords(0, 0, 0.02))));
                    }));
                }, new ExecuteIfScoreMatches(rayState, rayState, 0).AddCommand(new ExecutePosition(new LocalCoords(0, 0, 1))));
            }));
            ForFunction.Entity.Teleport(SharpCraftFiles.GetDummySelector(), SharpCraftSettings.OwnedChunk * 16);
        }

        /// <summary>
        /// Shots a ray which runs commands for every entity it hits
        /// </summary>
        /// <param name="rayName">The name of the ray</param>
        /// <param name="hit">Selector selecting entities the ray should be able to hit. Leave null for all entities</param>
        /// <param name="ignore">Selector selecting entities the ray shouldn't be able to hit. Leave null for no ignored entities</param>
        /// <param name="length">The amount of blocks he ray should travel.</param>
        /// <param name="onHit">The commands to run when the ray hits an entity</param>
        /// <param name="hitSelfAble">If the ray should be able to hit the executor</param>
        public void RayCast(string rayName, Selector hit, Selector ignore, double length, Function.FunctionWriter onHit, bool hitSelfAble = false)
        {
            ValidateRayParams(rayName, length, onHit);

            Tag rayShooterTag = "SharpRayShooter";
            Selector.EntityTag ignoreTag = new Selector.EntityTag(rayShooterTag, false);

            int boxes = 2;
            Function getStep(double number, int depth)
            {
                Function outFunction = ForFunction.NewSibling(rayName + "\\r" + depth);

                if (number > boxes)
                {
                    Function nextStep = getStep(number / boxes, depth + 1);

                    double boxSize = number / boxes;
                    double realBoxSize = boxSize - 1;
                    for (int i = 0; i < boxes; i++)
                    {
                        double offset = boxSize * i + boxSize / 2;
                        outFunction.Execute.Positioned(new LocalCoords(0, 0, offset));
                        outFunction.Execute.Positioned(new Coords(boxSize / -2));
                        outFunction.Execute.IfEntity(new Selector(ID.Selector.s) { BoxX = realBoxSize, BoxY = realBoxSize, BoxZ = realBoxSize });
                        outFunction.Execute.Positioned(new Coords(boxSize / 2));
                        outFunction.Execute.Positioned(new LocalCoords(0, 0, boxSize / -2));
                        outFunction.World.Function(nextStep);
                    }
                }
                else if (number > 0.1)
                {
                    Function nextStep = getStep(number / boxes, depth + 1);

                    double boxSize = number / boxes;
                    double realBoxSize = boxSize - 1;
                    for (int i = 0; i < boxes; i++)
                    {
                        double offset = boxSize * i + boxSize / 2;
                        outFunction.Execute.Positioned(new LocalCoords(0, 0, offset));
                        outFunction.Execute.Positioned(new Coords(boxSize / -2));
                        outFunction.Execute.IfEntity(new Selector(ID.Selector.s) { BoxX = 0, BoxY = 0, BoxZ = 0 });
                        outFunction.Execute.Positioned(new Coords(boxSize - 1));
                        outFunction.Execute.IfEntity(new Selector(ID.Selector.s) { BoxX = 0, BoxY = 0, BoxZ = 0 });
                        outFunction.Execute.Positioned(new Coords(boxSize / 2) + new Coords(boxSize - 1) * -1);
                        outFunction.Execute.Positioned(new LocalCoords(0, 0, boxSize / -2));
                        outFunction.World.Function(nextStep);
                    }
                }
                else
                {
                    outFunction.Execute.IfEntity(new Selector(ID.Selector.s) { BoxX = 0, BoxY = 0, BoxZ = 0 });
                    outFunction.Execute.Positioned(new Coords(-1));
                    outFunction.Execute.IfEntity(new Selector(ID.Selector.s) { BoxX = 0, BoxY = 0, BoxZ = 0 });
                    outFunction.Execute.Positioned(new Coords(1));
                    outFunction.World.Function(outFunction.NewSibling("hit", onHit));
                }

                return outFunction;
            }

            if (hitSelfAble)
            {
                if (!(hit is null))
                {
                    ForFunction.Execute.As(hit);
                }
                else
                {
                    ForFunction.Execute.As(ID.Selector.e);
                }
                if (!(ignore is null))
                {
                    Selector selector = ignore.ShallowClone();
                    selector.SelectorType = ID.Selector.s;
                    ForFunction.Execute.IfEntity(selector, false);
                }
                ForFunction.World.Function(getStep(length, 0));
            }
            else
            {
                ForFunction.World.Function(ForFunction.NewSibling(rayName + "\\start", start =>
                {
                    start.Entity.Tag.Add(ID.Selector.s, rayShooterTag);
                    if (!(hit is null))
                    {
                        Selector selector = hit.ShallowClone();
                        if (!(selector.Tags is null))
                        {
                            Selector.EntityTag[] newTags = new Selector.EntityTag[selector.Tags.Length + 1];
                            Array.Copy(selector.Tags, newTags, selector.Tags.Length);
                            newTags[^1] = ignoreTag;
                            selector.Tags = newTags;
                        }
                        else
                        {
                            selector.Tags = ignoreTag;
                        }
                        start.Execute.As(selector);
                    }
                    else
                    {
                        start.Execute.IfEntity(new Selector(ID.Selector.e) { Tags = ignoreTag });
                    }
                    if (!(ignore is null))
                    {
                        Selector selector = ignore.ShallowClone();
                        selector.SelectorType = ID.Selector.s;
                        start.Execute.IfEntity(selector, false);
                    }
                    start.World.Function(getStep(length, 0));
                    start.Entity.Tag.Remove(ID.Selector.s, rayShooterTag);
                }));
            }
        }
        #endregion

        #region random
        /// <summary>
        /// Commands for randomness
        /// </summary>
        public ClassRandom Random { get; set; }

        /// <summary>
        /// Commands for randomness
        /// </summary>
        public class ClassRandom : CommandList
        {
            internal ClassRandom(Function function) : base(function)
            {
                
            }

            /// <summary>
            /// Generates a random number from 0 to 1 and only executes if the number is less than <paramref name="chance"/>
            /// </summary>
            /// <param name="chance">The chance for the command to execute</param>
            /// <param name="want">false if it should execute when it's false</param>
            /// <returns>The function running the command</returns>
            public Function ExecuteIfRandom(double chance, bool want = true)
            {
                if (chance < 0 || chance > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(chance), "Random chance has to be between 0 and 1");
                }
                ForFunction.Execute.IfPredicate(SharpCraftFiles.GetRandomPredicate(chance), want);
                return ForFunction;
            }

            /// <summary>
            /// Generates a random number from <paramref name="from"/> to <paramref name="to"/>
            /// </summary>
            /// <param name="from">The smallest the number can be</param>
            /// <param name="to">The highest the number can be</param>
            /// <returns>A <see cref="ScoreValue"/> holding the random number</returns>
            public ScoreValue NextRandom(int from = 0, int to = int.MaxValue)
            {
                if (from >= to)
                {
                    throw new ArgumentException("From has be smaller than To to generate a random number.");
                }
                long difference = (long)to - from + 1;
                if (difference > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("To generate a number the difference between From and To may not be higher than int max value.");
                }
                ScoreValue randomHolder = null;
                ForFunction.Custom.GroupCommands((g) =>
                {
                    g.World.Function(SharpCraftFiles.GetRandomNumberFunction());
                    randomHolder = SharpCraftFiles.GetRandomHolder();
                    g.Entity.Score.Operation(randomHolder, randomHolder, ID.Operation.Remainder, (int)difference);
                    g.Entity.Score.Add(randomHolder, randomHolder, from);
                });
                return randomHolder;
            }

            /// <summary>
            /// Hashes the given <see cref="ScoreValue"/>
            /// </summary>
            /// <param name="value">The <see cref="ScoreValue"/> to hash</param>
            /// <returns>The <see cref="ScoreValue"/> holding the hash</returns>
            public ScoreValue HashScore(ScoreValue value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value), "Value may not be null");
                }

                var (function, location) = SharpCraftFiles.GetHashFunction();
                ForFunction.Custom.GroupCommands(g =>
                {
                    g.Execute.Store(new BlockDataLocation(location, Data.DataPath.GetDataPath<Block.ShulkerBox>(b => b.DLootTableSeed).ToString()), ID.StoreTypes.Int);
                    g.Entity.Score.Get(value, value);
                    g.World.Function(function);
                    g.Execute.IfScore(value, value, 0);
                    g.Entity.Score.Set(SharpCraftFiles.GetRandomHolder(), SharpCraftFiles.GetRandomHolder(), 3631387);
                });

                return SharpCraftFiles.GetRandomHolder();
            }
        }
        #endregion
    }
}