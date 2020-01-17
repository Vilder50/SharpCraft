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
        readonly Function function;

        internal ExecuteCommands(Function function)
        {
            this.function = function;
        }

        /// <summary>
        /// Marks that the execute command shouldnt have any run command
        /// </summary>
        public void Stop()
        {
            function.AddCommand(new StopExecuteCommand());   
        }

        /// <summary>
        /// Aligns the execute coordinates on the given axis
        /// </summary>
        /// <param name="alignX">If it should align on the x axis</param>
        /// <param name="alignY">If it should align on the y axis</param>
        /// <param name="alignZ">If it should align on the z axis</param>
        public void Align(bool alignX, bool alignY, bool alignZ)
        {
            function.AddCommand(new ExecuteAlign(alignX, alignY, alignZ));
        }

        /// <summary>
        /// Auto aligns the execute coordinates to all the axis
        /// </summary>
        /// <param name="center">True if it should center to the block</param>
        public void Align(bool center = false)
        {
            function.AddCommand(new ExecuteAlign());
            if (center)
            {
                function.AddCommand(new ExecutePosition(new Coords(0.5, 0.5, 0.5)));
            }
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="atEntity">The <see cref="BaseSelector"/> to execute at</param>
        public void At(BaseSelector atEntity)
        {
            function.AddCommand(new ExecuteAt(atEntity));
        }

        /// <summary>
        /// Executes at using the @s <see cref="BaseSelector"/>
        /// </summary>
        public void At()
        {
            function.AddCommand(new ExecuteAt(ID.Selector.s));
        }

        /// <summary>
        /// Executes as the given <see cref="BaseSelector"/>
        /// </summary>
        /// <param name="asEntity">The <see cref="BaseSelector"/> to execute as</param>
        public void As(BaseSelector asEntity)
        {
            function.AddCommand(new ExecuteAs(asEntity));
        }

        /// <summary>
        /// Executes if the given <see cref="Block"/> is at the <see cref="Coords"/>
        /// </summary>
        /// <param name="blockCoords">the <see cref="Coords"/> of the block</param>
        /// <param name="findBlock">the <see cref="Block"/> to find</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfBlock(Block findBlock, Coords blockCoords, bool want = true)
        {
            function.AddCommand(new ExecuteIfBlock(blockCoords, findBlock, want));
        }

        /// <summary>
        /// Executes if the <see cref="Block"/>s between the 2 corners are the same as the <see cref="Block"/>s at the <paramref name="testCoords"/>
        /// </summary>
        /// <param name="corner1">The first corner</param>
        /// <param name="corner2">The second corner</param>
        /// <param name="testCoords">The coordinate to check at</param>
        /// <param name="masked">true if it should ignore air blocks</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfBlocks(Coords corner1, Coords corner2, Coords testCoords, bool masked = false, bool want = true)
        {
            function.AddCommand(new ExecuteIfBlocks(corner1, corner2, testCoords, masked, want));
        }

        /// <summary>
        /// Executes if the <paramref name="entitySelector"/> finds an <see cref="Entity"/>
        /// </summary>
        /// <param name="entitySelector">The <see cref="BaseSelector"/> used to search for entities</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfEntity(BaseSelector entitySelector, bool want = true)
        {
            function.AddCommand(new ExecuteIfEntity(entitySelector, want));
        }

        /// <summary>
        /// Executes if the <see cref="Entity"/> selected with <paramref name="dataPath"/> has the given datapath
        /// </summary>
        /// <param name="entitySelector">The <see cref="BaseSelector"/> which selects the entity</param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfData(BaseSelector entitySelector, string dataPath, bool want = true)
        {
            entitySelector.LimitSelector();
            function.AddCommand(new ExecuteIfData(new EntityDataLocation(entitySelector, dataPath), want));
        }

        /// <summary>
        /// Executes if the <see cref="Block"/> at the <see cref="Coords"/> has the given datapath
        /// </summary>
        /// <param name="block">the <see cref="Coords"/> of the <see cref="Block"/></param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfData(Coords block, string dataPath, bool want = true)
        {
            function.AddCommand(new ExecuteIfData(new BlockDataLocation(block, dataPath), want));
        }

        /// <summary>
        /// Executes if the <see cref="Storage"/> has the given datapath
        /// </summary>
        /// <param name="storage">the storage to check if datapath exists in</param>
        /// <param name="dataPath">The datapath the entity should contain</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfData(Storage storage, string dataPath, bool want = true)
        {
            function.AddCommand(new ExecuteIfData(new StorageDataLocation(storage, dataPath), want));
        }

        /// <summary>
        /// Executes if the predicate returns true
        /// </summary>
        /// <param name="predicate">The predicate to check</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfPredicate(IPredicate predicate, bool want = true)
        {
            function.AddCommand(new ExecuteIfPredicate(predicate, want));
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
        public void IfScore(BaseSelector mainSelector, Objective mainObject, ID.IfScoreOperation operation, BaseSelector otherSelector, Objective otherObject, bool want = true)
        {
            mainSelector.LimitSelector();
            otherSelector.LimitSelector();
            function.AddCommand(new ExecuteIfScoreRelative(mainSelector, mainObject, operation, otherSelector, otherObject, want));
        }

        /// <summary>
        /// Executes if the given <see cref="BaseSelector"/>'s score is in the given <see cref="Range"/>
        /// </summary>
        /// <param name="selector">the <see cref="BaseSelector"/>'s score to check</param>
        /// <param name="scoreObject">the <see cref="Objective"/> to containing the score</param>
        /// <param name="range">the <see cref="Range"/> the score should be in</param>
        /// <param name="want">false if it should execute when it's false</param>
        public void IfScore(BaseSelector selector, Objective scoreObject, Range range, bool want = true)
        {
            selector.LimitSelector();
            function.AddCommand(new ExecuteIfScoreMatches(selector, scoreObject, range, want));
        }

        /// <summary>
        /// Executes at the given position
        /// </summary>
        /// <param name="position">the <see cref="Coords"/> to execute at</param>
        public void Positioned(Coords position)
        {
            function.AddCommand(new ExecutePosition(position));
        }

        /// <summary>
        /// Executes at the given <see cref="BaseSelector"/>'s <see cref="Coords"/>
        /// </summary>
        /// <param name="entity">The <see cref="BaseSelector"/> to execute at</param>
        public void Positioned(BaseSelector entity)
        {
            function.AddCommand(new ExecutePositionedAs(entity));
        }

        /// <summary>
        /// Stores the command's success output inside the <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="BaseSelector"/> which choses the <see cref="Entity"/></param>
        /// <param name="dataPath">the datapath to store the output in</param>
        /// <param name="dataType">the path to the place to store the score</param>
        /// <param name="scale">the number the output should be multiplied with before being inserted</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        public void Store(BaseSelector entity, string dataPath, ID.StoreTypes dataType, double scale = 1, bool storeSucces = false)
        {
            entity.LimitSelector();
            function.AddCommand(new ExecuteStoreEntity(entity, dataPath, dataType, scale, !storeSucces));
        }

        /// <summary>
        /// Stores the command's success output inside the <see cref="Block"/>
        /// </summary>
        /// <param name="blockCoords">the <see cref="Coords"/> of the <see cref="Block"/> to store the output in</param>
        /// <param name="dataPath">the datapath to store the output in</param>
        /// <param name="dataType">the path to the place to store the score</param>
        /// <param name="scale">the number the output should be multiplied with before being inserted</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        public void Store(Coords blockCoords, string dataPath, ID.StoreTypes dataType, double scale = 1, bool storeSucces = false)
        {
            function.AddCommand(new ExecuteStoreBlock(blockCoords, dataPath, dataType, scale, !storeSucces));
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="Entity"/>'s <see cref="Objective"/>
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/> to store in</param>
        /// <param name="scoreObject">The <see cref="Objective"/> to store in</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        public void Store(BaseSelector entity, Objective scoreObject, bool storeSucces = false)
        {
            function.AddCommand(new ExecuteStoreScore(entity, scoreObject, !storeSucces));
        }

        /// <summary>
        /// Stores the command's success output inside the given <see cref="BossBar"/>
        /// </summary>
        /// <param name="bossBar">The <see cref="BossBar"/> to store the output in</param>
        /// <param name="value">true if it should store the output in the value, false if it should store it as maxvalue</param>
        /// <param name="storeSucces">true if it only should store if the command was successfull</param>
        public void Store(BossBar bossBar, bool value = true, bool storeSucces = false)
        {
            function.AddCommand(new ExecuteStoreBossbar(bossBar, value, !storeSucces));
        }

        /// <summary>
        /// Executes rotated in the direction of the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> it should be rotated at</param>
        /// <param name="facing">the part of the <see cref="Entity"/> to be faced at</param>
        public void Facing(BaseSelector entity, ID.FacingAnchor facing = ID.FacingAnchor.feet)
        {
            function.AddCommand(new ExecuteFacingEntity(entity, facing));
        }

        /// <summary>
        /// Executes rotated in the direction of the given <see cref="Coords"/>
        /// </summary>
        /// <param name="coords">the <see cref="Coords"/> to be rotated at</param>
        public void Facing(Coords coords)
        {
            function.AddCommand(new ExecuteFacingCoord(coords));
        }

        /// <summary>
        /// Executes rotated
        /// </summary>
        /// <param name="rotation">the <see cref="Rotation"/> to execute with</param>
        public void Rotated(Rotation rotation)
        {
            function.AddCommand(new ExecuteRotated(rotation));
        }

        /// <summary>
        /// Executes rotated as the given <see cref="Entity"/>
        /// </summary>
        /// <param name="entity">the <see cref="Entity"/> to execute rotated as</param>
        public void Rotated(BaseSelector entity)
        {
            function.AddCommand(new ExecuteRotatedAs(entity));
        }

        /// <summary>
        /// Executes in the given dimension
        /// </summary>
        /// <param name="dimension">The dimension</param>
        public void Dimension(ID.Dimension dimension)
        {
            function.AddCommand(new ExecuteDimension(dimension));
        }

        /// <summary>
        /// Changes where the origin used by coordinates are at
        /// </summary>
        /// <param name="anchor">The origin</param>
        public void Anchored(ID.FacingAnchor anchor)
        {
            function.AddCommand(new ExecuteAnchored(anchor));
        }
    }
}

