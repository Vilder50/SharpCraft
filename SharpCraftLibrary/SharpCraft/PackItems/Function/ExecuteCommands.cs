using System.Linq;
using System;
using System.Collections.Generic;
using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the execute commands
    /// </summary>
    public class ExecuteCommands : CommandList
    {
        internal ExecuteCommands(Function function) : base(function)
        {
            
        }

        /// <summary>
        /// Marks that the execute command shouldnt have any run command
        /// </summary>
        public void Stop()
        {
            ForFunction.AddCommand(new StopExecuteCommand());   
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
            ForFunction.AddCommand(new ExecuteAlign(alignX, alignY, alignZ));
            return ForFunction;
        }

        /// <summary>
        /// Auto aligns the execute coordinates to all the axis
        /// </summary>
        /// <param name="center">True if it should center to the block</param>
        /// <returns>The function running the command</returns>
        public Function Align(bool center = false)
        {
            ForFunction.AddCommand(new ExecuteAlign());
            if (center)
            {
                ForFunction.AddCommand(new ExecutePosition(new Coords(0.5, 0.5, 0.5)));
            }
            return ForFunction;
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="atEntity">The <see cref="BaseSelector"/> to execute at</param>
        /// <returns>The function running the command</returns>
        public Function At(BaseSelector atEntity)
        {
            ForFunction.AddCommand(new ExecuteAt(atEntity));
            return ForFunction;
        }

        /// <summary>
        /// Executes at using the @s <see cref="BaseSelector"/>
        /// </summary>
        /// <returns>The function running the command</returns>
        public Function At()
        {
            ForFunction.AddCommand(new ExecuteAt(ID.Selector.s));
            return ForFunction;
        }

        /// <summary>
        /// Executes as the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="asEntity">The <see cref="BaseSelector"/> to execute as</param>
        /// <returns>The function running the command</returns>
        public Function As(BaseSelector asEntity)
        {
            ForFunction.AddCommand(new ExecuteAs(asEntity));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfBlock(blockCoords, findBlock, want));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfBlocks(corner1, corner2, testCoords, masked, want));
            return ForFunction;
        }

        /// <summary>
        /// Executes if the <paramref name="entitySelector"/> finds an <see cref="Entity"/>
        /// </summary>
        /// <param name="entitySelector">The <see cref="BaseSelector"/> used to search for entities</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfEntity(BaseSelector entitySelector, bool want = true)
        {
            ForFunction.AddCommand(new ExecuteIfEntity(entitySelector, want));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfData(new EntityDataLocation(entitySelector, dataPath), want));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfData(new BlockDataLocation(block, dataPath), want));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfData(new StorageDataLocation(storage, dataPath), want));
            return ForFunction;
        }

        /// <summary>
        /// Executes if the predicate returns true
        /// </summary>
        /// <param name="predicate">The predicate to check</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfPredicate(IPredicate predicate, bool want = true)
        {
            ForFunction.AddCommand(new ExecuteIfPredicate(predicate, want));
            return ForFunction;
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
            ForFunction.AddCommand(new ExecuteIfScoreRelative(mainSelector, mainObject, operation, otherSelector, otherObject, want));
            return ForFunction;
        }

        /// <summary>
        /// Executes if the score is <paramref name="operation"/> than the other score
        /// </summary>
        /// <param name="score">The first score</param>
        /// <param name="operation">The operation used to check the scores</param>
        /// <param name="otherScore">The second score</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(ScoreValue score, ID.IfScoreOperation operation, ScoreValue otherScore, bool want = true)
        {
            return IfScore(score, score, operation, otherScore, otherScore, want);
        }

        /// <summary>
        /// Executes if the score is <paramref name="operation"/> than the other score
        /// </summary>
        /// <param name="score">The first score</param>
        /// <param name="operation">The operation used to check the scores</param>
        /// <param name="otherSelector">The second <see cref="BaseSelector"/></param>
        /// <param name="otherObject">The second <see cref="BaseSelector"/>'s <see cref="Objective"/></param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(ScoreValue score, ID.IfScoreOperation operation, BaseSelector otherSelector, Objective otherObject, bool want = true)
        {
            return IfScore(score, score, operation, otherSelector, otherObject, want);
        }

        /// <summary>
        /// Executes if the score is <paramref name="operation"/> than the other score
        /// </summary>
        /// <param name="mainSelector">The first <see cref="BaseSelector"/></param>
        /// <param name="mainObject">The first <see cref="BaseSelector"/>'s <see cref="Objective"/></param>
        /// <param name="operation">The operation used to check the scores</param>
        /// <param name="otherScore">The second score</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(BaseSelector mainSelector, Objective mainObject, ID.IfScoreOperation operation, ScoreValue otherScore, bool want = true)
        {
            return IfScore(mainSelector, mainObject, operation, otherScore, otherScore, want);
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
            ForFunction.AddCommand(new ExecuteIfScoreMatches(selector, scoreObject, range, want));
            return ForFunction;
        }

        /// <summary>
        /// Executes if the given score is in the given <see cref="MCRange"/>
        /// </summary>
        /// <param name="score">the score to check</param>
        /// <param name="range">the <see cref="MCRange"/> the score should be in</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfScore(ScoreValue score, MCRange range, bool want = true)
        {
            return IfScore(score, score, range, want);
        }

        /// <summary>
        /// Executes at the given position
        /// </summary>
        /// <param name="position">the coords to execute at</param>
        /// <returns>The function running the command</returns>
        public Function Positioned(Vector position)
        {
            ForFunction.AddCommand(new ExecutePosition(position));
            return ForFunction;
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>'s coords
        /// </summary>
        /// <param name="entity">The <see cref="BaseSelector"/> to execute at</param>
        /// <returns>The function running the command</returns>
        public Function Positioned(BaseSelector entity)
        {
            ForFunction.AddCommand(new ExecutePositionedAs(entity));
            return ForFunction;
        }

        /// <summary>
        /// Stores the command's success output inside the <see cref="Entity"/>
        /// </summary>
        /// <param name="dataLocation">the location to store the result at</param>
        /// <param name="dataType">the path to the place to store the score</param>
        /// <param name="scale">the number the output should be multiplied with before being inserted</param>
        /// <param name="storeSuccess">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(IDataLocation dataLocation, ID.StoreTypes dataType, double scale = 1, bool storeSuccess = false)
        {
            ForFunction.AddCommand(new ExecuteStoreData(dataLocation, dataType, scale, !storeSuccess));
            return ForFunction;
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="Entity"/>'s <see cref="Objective"/>
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to store in</param>
        /// <param name="scoreObject">The <see cref="Objective"/> to store in</param>
        /// <param name="storeSuccess">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(BaseSelector entity, Objective scoreObject, bool storeSuccess = false)
        {
            ForFunction.AddCommand(new ExecuteStoreScore(entity, scoreObject, !storeSuccess));
            return ForFunction;
        }

        /// <summary>
        /// Stores the command's success output inside the given score
        /// </summary>
        /// <param name="score">The score to store the result in</param>
        /// <param name="storeSuccess">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(ScoreValue score, bool storeSuccess = false)
        {
            return Store(score, score, storeSuccess);
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="BossBar"/>
        /// </summary>
        /// <param name="bossBar">The <see cref="BossBar"/> to store the output in</param>
        /// <param name="value">true if it should store the output in the value, false if it should store it as maxvalue</param>
        /// <param name="storeSuccess">true if it only should store if the command was successfull</param>
        /// <returns>The function running the command</returns>
        public Function Store(BossBar bossBar, bool value = true, bool storeSuccess = false)
        {
            ForFunction.AddCommand(new ExecuteStoreBossbar(bossBar, value, !storeSuccess));
            return ForFunction;
        }

        /// <summary>
        /// Executes rotated in the direction of the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> it should be rotated at</param>
        /// <param name="facing">the part of the <see cref="Entity"/> to be faced at</param>
        /// <returns>The function running the command</returns>
        public Function Facing(BaseSelector entity, ID.FacingAnchor facing = ID.FacingAnchor.feet)
        {
            ForFunction.AddCommand(new ExecuteFacingEntity(entity, facing));
            return ForFunction;
        }

        /// <summary>
        /// Executes rotated in the direction of the given coords
        /// </summary>
        /// <param name="coords">the coords to be rotated to</param>
        /// <returns>The function running the command</returns>
        public Function Facing(Vector coords)
        {
            ForFunction.AddCommand(new ExecuteFacingCoord(coords));
            return ForFunction;
        }

        /// <summary>
        /// Executes rotated
        /// </summary>
        /// <param name="rotation">the <see cref="Rotation"/> to execute with</param>
        /// <returns>The function running the command</returns>
        public Function Rotated(Rotation rotation)
        {
            ForFunction.AddCommand(new ExecuteRotated(rotation));
            return ForFunction;
        }

        /// <summary>
        /// Executes rotated as the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> to execute rotated as</param>
        /// <returns>The function running the command</returns>
        public Function Rotated(BaseSelector entity)
        {
            ForFunction.AddCommand(new ExecuteRotatedAs(entity));
            return ForFunction;
        }

        /// <summary>
        /// Executes in the given dimension
        /// </summary>
        /// <param name="dimension">The dimension</param>
        /// <returns>The function running the command</returns>
        public Function Dimension(ID.Dimension dimension)
        {
            ForFunction.AddCommand(new ExecuteDimension(dimension));
            return ForFunction;
        }

        /// <summary>
        /// Changes where the origin used by coordinates are at
        /// </summary>
        /// <param name="anchor">The origin</param>
        /// <returns>The function running the command</returns>
        public Function Anchored(ID.FacingAnchor anchor)
        {
            ForFunction.AddCommand(new ExecuteAnchored(anchor));
            return ForFunction;
        }
    }
}

