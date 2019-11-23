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
        readonly Function function;

        internal CustomCommands(Function parentFunction)
        {
            function = parentFunction;
        }

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
        public Function TreeSearch(TreeSearchMethod method, TreeSearchCommand command, int minimum, int maximum)
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

            return WriteTreeSearch(function, method, command, minimum, maximum, true);
        }

        private Function WriteTreeSearch(Function function, TreeSearchMethod method, TreeSearchCommand command, int minimum, int maximum, bool first = false)
        {
            int halfSize = ((maximum - minimum) / 2) + minimum;
            int numbersLeft = maximum - minimum;

            BaseCommand executeCommand = null;
            if (first && function.Commands.Count != 0 && function.Commands.Last() is BaseExecuteCommand execute && !execute.DoneChanging)
            {
                executeCommand = function.Commands.Last().ShallowClone();
            }
            function.AddCommand(method(minimum, halfSize));
            if (minimum != halfSize)
            {
                function.World.Function(WriteTreeSearch(first ? function.NewChild(minimum + "-" + halfSize) : function.NewSibling(minimum + "-" + halfSize), method, command, minimum, halfSize));
            }
            else
            {
                function.AddCommand(command(halfSize));
            }

            if (first && !(executeCommand is null))
            {
                function.AddCommand(executeCommand);
            }
            function.AddCommand(method(halfSize + 1, maximum));
            if (halfSize + 1 != maximum)
            {
                function.World.Function(WriteTreeSearch(first ? function.NewChild((halfSize + 1) + "-" + maximum) : function.NewSibling((halfSize + 1) + "-" + maximum), method, command, halfSize + 1, maximum));
            }
            else
            {
                function.AddCommand(command(halfSize + 1));
            }

            function.Dispose();
            return function;
        }

        /// <summary>
        /// Summons a new entity and runs the given commands as the entity
        /// </summary>
        /// <param name="entity">The entity to summon</param>
        /// <param name="functionName">The name of the function it should run</param>
        /// <param name="runCommands">the commands the entity should run</param>
        /// <param name="executeAt">True if it should run the commands at the entity's location</param>
        /// <param name="writeSetting">The setting for writing the function file</param>
        /// <returns>The function the entity runs</returns>
        public Function SummonExecute(Entity.EntityBasic entity, string functionName, Function.FunctionCreater runCommands, bool executeAt = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
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
        public Function SummonExecute(Entity.EntityBasic entity, Coords spawnCoords, string functionName, Function.FunctionCreater runCommands, bool executeAt = true, BaseFile.WriteSetting writeSetting = BaseFile.WriteSetting.LockedAuto)
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
            BaseCommand executeCommand = null;
            if (function.Commands.Count != 0 && function.Commands.Last() is BaseExecuteCommand execute && !execute.DoneChanging)
            {
                executeCommand = function.Commands.Last().ShallowClone();
            }
            function.Entity.Add(createEntity, spawnCoords);
            if (!(executeCommand is null))
            {
                function.AddCommand(executeCommand);
            }
            function.Execute.As(new Selector(ID.Selector.e, findTag));
            if (executeAt)
            {
                function.Execute.At();
            }
            Function executeAs = (Function)function.World.Function(function.NewSibling(functionName, writeSetting));
            executeAs.Entity.Tag.Remove(new Selector(), findTag);
            runCommands(executeAs);

            return executeAs;
        }
    }
}