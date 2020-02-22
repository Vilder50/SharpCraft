using System.Linq;
using System;
using System.Collections.Generic;
using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the execute commands
    /// </summary>
    public class ExecuteCommands
    {
        /// <summary>
        /// The function to write onto
        /// </summary>
        public Function Function { get; private set; }

        internal ExecuteCommands(Function function)
        {
            this.Function = function;
        }

        /// <summary>
        /// Marks that the execute command shouldnt have any run command
        /// </summary>
        public void Stop()
        {
            Function.AddCommand(new StopExecuteCommand());   
        }

        /// <summary>
        /// Aligns the execute coordinates on the given axis
        /// </summary>
        /// <param name="alignX">If it should align on the x axis</param>
        /// <param name="alignY">If it should align on the y axis</param>
        /// <param name="alignZ">If it should align on the z axis</param>
        /// <returns>The function running the command</returns>
        public Function Align(bool alignX, bool alignY, bool alignZ)
        {
            Function.AddCommand(new ExecuteAlign(alignX, alignY, alignZ));
            return Function;
        }

        /// <summary>
        /// Auto aligns the execute coordinates to all the axis
        /// </summary>
        /// <param name="center">True if it should center to the block</param>
        /// <returns>The function running the command</returns>
        public Function Align(bool center = false)
        {
            Function.AddCommand(new ExecuteAlign());
            if (center)
            {
                Function.AddCommand(new ExecutePosition(new Coords(0.5, 0.5, 0.5)));
            }
            return Function;
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="atEntity">The <see cref="BaseSelector"/> to execute at</param>
        /// <returns>The function running the command</returns>
        public Function At(BaseSelector atEntity)
        {
            Function.AddCommand(new ExecuteAt(atEntity));
            return Function;
        }

        /// <summary>
        /// Executes at using the @s <see cref="BaseSelector"/>
        /// </summary>
        /// <returns>The function running the command</returns>
        public Function At()
        {
            Function.AddCommand(new ExecuteAt(ID.Selector.s));
            return Function;
        }

        /// <summary>
        /// Executes as the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="asEntity">The <see cref="BaseSelector"/> to execute as</param>
        /// <returns>The function running the command</returns>
        public Function As(BaseSelector asEntity)
        {
            Function.AddCommand(new ExecuteAs(asEntity));
            return Function;
        }

        /// <summary>
        /// Executes if the given <see cref="Block"/> is at the coords
        /// </summary>
        /// <param name="blockCoords">the coords of the block</param>
        /// <param name="findBlock">the <see cref="Block"/> to find</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfBlock(Vector blockCoords, Block findBlock, bool want = true)
        {
            Function.AddCommand(new ExecuteIfBlock(blockCoords, findBlock, want));
            return Function;
        }

        /// <summary>
        /// Executes if the <see cref="Block"/>s between the 2 corners are the same as the <see cref="Block"/>s at the <paramref name="testCoords"/>
        /// </summary>
        /// <param name="corner1">The first corner</param>
        /// <param name="corner2">The second corner</param>
        /// <param name="testCoords">The coordinate to check at</param>
        /// <param name="masked">true if it should ignore air blocks</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfBlocks(Vector corner1, Vector corner2, Vector testCoords, bool masked = false, bool want = true)
        {
            Function.AddCommand(new ExecuteIfBlocks(corner1, corner2, testCoords, masked, want));
            return Function;
        }

        /// <summary>
        /// Executes if the <paramref name="entitySelector"/> finds an <see cref="Entity"/>
        /// </summary>
        /// <param name="entitySelector">The <see cref="BaseSelector"/> used to search for entities</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfEntity(BaseSelector entitySelector, bool want = true)
        {
            Function.AddCommand(new ExecuteIfEntity(entitySelector, want));
            return Function;
        }

        /// <summary>
        /// Executes if the <see cref="Entity"/> selected with <paramref name="dataPath"/> has the given datapath
        /// </summary>
        /// <param name="entitySelector">The <see cref="BaseSelector"/> which selects the entity</param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfData(BaseSelector entitySelector, string dataPath, bool want = true)
        {
            entitySelector.LimitSelector();
            Function.AddCommand(new ExecuteIfData(new EntityDataLocation(entitySelector, dataPath), want));
            return Function;
        }

        /// <summary>
        /// Executes if the <see cref="Block"/> at the coords has the given datapath
        /// </summary>
        /// <param name="block">the coords of the <see cref="Block"/></param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfData(Vector block, string dataPath, bool want = true)
        {
            Function.AddCommand(new ExecuteIfData(new BlockDataLocation(block, dataPath), want));
            return Function;
        }

        /// <summary>
        /// Executes if the <see cref="Storage"/> has the given datapath
        /// </summary>
        /// <param name="storage">the storage to check if datapath exists in</param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfData(Storage storage, string dataPath, bool want = true)
        {
            Function.AddCommand(new ExecuteIfData(new StorageDataLocation(storage, dataPath), want));
            return Function;
        }

        /// <summary>
        /// Executes if the predicate returns true
        /// </summary>
        /// <param name="predicate">The predicate to check</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfPredicate(IPredicate predicate, bool want = true)
        {
            Function.AddCommand(new ExecuteIfPredicate(predicate, want));
            return Function;
        }

        /// <summary>
        /// Executes if the <paramref name="mainSelector"/>'s score value is <paramref name="operation"/> than <paramref name="otherSelector"/>'s score value
        /// </summary>
        /// <param name="mainSelector">The first <see cref="BaseSelector"/></param>
        /// <param name="mainObject">The first <see cref="BaseSelector"/>'s <see cref="Objective"/></param>
        /// <param name="operation">The operation used to check the scores</param>
        /// <param name="otherSelector">The second <see cref="BaseSelector"/></param>
        /// <param name="otherObject">The second <see cref="BaseSelector"/>'s <see cref="Objective"/></param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(BaseSelector mainSelector, Objective mainObject, ID.IfScoreOperation operation, BaseSelector otherSelector, Objective otherObject, bool want = true)
        {
            mainSelector.LimitSelector();
            otherSelector.LimitSelector();
            Function.AddCommand(new ExecuteIfScoreRelative(mainSelector, mainObject, operation, otherSelector, otherObject, want));
            return Function;
        }

        /// <summary>
        /// Executes if the given <see cref="BaseSelector"/>'s score is in the given <see cref="MCRange"/>
        /// </summary>
        /// <param name="selector">the <see cref="BaseSelector"/>'s score to check</param>
        /// <param name="scoreObject">the <see cref="Objective"/> to containing the score</param>
        /// <param name="range">the <see cref="MCRange"/> the score should be in</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(BaseSelector selector, Objective scoreObject, MCRange range, bool want = true)
        {
            selector.LimitSelector();
            Function.AddCommand(new ExecuteIfScoreMatches(selector, scoreObject, range, want));
            return Function;
        }

        /// <summary>
        /// Executes at the given position
        /// </summary>
        /// <param name="position">the coords to execute at</param>
        /// <returns>The function running the command</returns>
        public Function Positioned(Vector position)
        {
            Function.AddCommand(new ExecutePosition(position));
            return Function;
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>'s coords
        /// </summary>
        /// <param name="entity">The <see cref="BaseSelector"/> to execute at</param>
        /// <returns>The function running the command</returns>
        public Function Positioned(BaseSelector entity)
        {
            Function.AddCommand(new ExecutePositionedAs(entity));
            return Function;
        }

        /// <summary>
        /// Stores the command's success output inside the <see cref="Entity"/>
        /// </summary>
        /// <param name="dataLocation">the location to store the result at</param>
        /// <param name="dataType">the path to the place to store the score</param>
        /// <param name="scale">the number the output should be multiplied with before being inserted</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(IDataLocation dataLocation, ID.StoreTypes dataType, double scale = 1, bool storeSucces = false)
        {
            Function.AddCommand(new ExecuteStoreData(dataLocation, dataType, scale, !storeSucces));
            return Function;
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="Entity"/>'s <see cref="Objective"/>
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to store in</param>
        /// <param name="scoreObject">The <see cref="Objective"/> to store in</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(BaseSelector entity, Objective scoreObject, bool storeSucces = false)
        {
            Function.AddCommand(new ExecuteStoreScore(entity, scoreObject, !storeSucces));
            return Function;
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="BossBar"/>
        /// </summary>
        /// <param name="bossBar">The <see cref="BossBar"/> to store the output in</param>
        /// <param name="value">true if it should store the output in the value, false if it should store it as maxvalue</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(BossBar bossBar, bool value = true, bool storeSucces = false)
        {
            Function.AddCommand(new ExecuteStoreBossbar(bossBar, value, !storeSucces));
            return Function;
        }

        /// <summary>
        /// Executes rotated in the direction of the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> it should be rotated at</param>
        /// <param name="facing">the part of the <see cref="Entity"/> to be faced at</param>
        /// <returns>The function running the command</returns>
        public Function Facing(BaseSelector entity, ID.FacingAnchor facing = ID.FacingAnchor.feet)
        {
            Function.AddCommand(new ExecuteFacingEntity(entity, facing));
            return Function;
        }

        /// <summary>
        /// Executes rotated in the direction of the given coords
        /// </summary>
        /// <param name="coords">the coords to be rotated to</param>
        /// <returns>The function running the command</returns>
        public Function Facing(Vector coords)
        {
            Function.AddCommand(new ExecuteFacingCoord(coords));
            return Function;
        }

        /// <summary>
        /// Executes rotated
        /// </summary>
        /// <param name="rotation">the <see cref="Rotation"/> to execute with</param>
        /// <returns>The function running the command</returns>
        public Function Rotated(Rotation rotation)
        {
            Function.AddCommand(new ExecuteRotated(rotation));
            return Function;
        }

        /// <summary>
        /// Executes rotated as the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> to execute rotated as</param>
        /// <returns>The function running the command</returns>
        public Function Rotated(BaseSelector entity)
        {
            Function.AddCommand(new ExecuteRotatedAs(entity));
            return Function;
        }

        /// <summary>
        /// Executes in the given dimension
        /// </summary>
        /// <param name="dimension">The dimension</param>
        /// <returns>The function running the command</returns>
        public Function Dimension(ID.Dimension dimension)
        {
            Function.AddCommand(new ExecuteDimension(dimension));
            return Function;
        }

        /// <summary>
        /// Changes where the origin used by coordinates are at
        /// </summary>
        /// <param name="anchor">The origin</param>
        /// <returns>The function running the command</returns>
        public Function Anchored(ID.FacingAnchor anchor)
        {
            Function.AddCommand(new ExecuteAnchored(anchor));
            return Function;
        }
    }
}

