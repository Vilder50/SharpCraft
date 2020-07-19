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
        /// Executes at the location specified by the score values. Note that some precision is lost when doing relative coords. Especially when size is higher than 1.
        /// </summary>
        /// <param name="x">The x coord</param>
        /// <param name="y">The y coord</param>
        /// <param name="z">The z coord</param>
        /// <param name="relativeX">If the x coord should be relative to the executed location</param>
        /// <param name="relativeY">If the y coord should be relative to the executed location</param>
        /// <param name="relativeZ">If the z coord should be relative to the executed location</param>
        /// <param name="sizeX">How many blocks 1 score is equal to in the x direction</param>
        /// <param name="sizeY">How many blocks 1 score is equal to in the y direction</param>
        /// <param name="sizeZ">How many blocks 1 score is equal to in the z direction</param>
        /// <returns>The function running the command</returns>
        public Function Positioned(ValueParameter x, ValueParameter y, ValueParameter z, bool relativeX = false, bool relativeY = false, bool relativeZ = false, double sizeX = 1, double sizeY = 1, double sizeZ = 1)
        {
            if (x.IsInt() && y.IsInt() && z.IsInt())
            {
                throw new ArgumentException("You called positioned command using only ints. You might want to call positioned using a Vector object instead. (This is for executing at score values)");
            }

            Selector positionSelector = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetDummySelector();
            Storage tempStorage = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetTempStorage();
            ScoreValue tempScore = new ScoreValue("temp", ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetMathScoreObject());
            ForFunction.Custom.GroupCommands(group =>
            {
                group.Execute.As(positionSelector).World.Function(setup => 
                {
                    bool hasRelative = relativeX || relativeY || relativeZ;
                    if (hasRelative)
                    {
                        setup.World.Function(ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetDummyTeleportGetCoords());
                    }
                    else
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords), ID.EntityDataModifierType.set, new Vector(x.IsInt() ? sizeX * x.IntValue!.Value : 0, y.IsInt() ? sizeY * y.IntValue!.Value : 0, z.IsInt() ? sizeZ * z.IntValue!.Value : 0).GetAsArray(ID.NBTTagType.TagDoubleArray, new object[] { }).GetDataString());
                    }
                    if (relativeX)
                    {
                        if (!x.IsInt() || x.IntValue!.Value != 0)
                        {
                            setup.Execute.Store(tempScore).World.Data.Get(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.X), 1 / sizeX);
                            if (x.IsInt())
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, x.IntValue!.Value);
                            }
                            else
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, x.ScoreValue!);
                            }
                            setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.X)), ID.StoreTypes.Double, sizeX).Entity.Score.Get(tempScore);
                        }
                    }
                    else if (x.IsScore())
                    {
                        setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.X)), ID.StoreTypes.Double, sizeX).Entity.Score.Get(x.ScoreValue!);
                    }
                    else if (!hasRelative)
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.X), ID.EntityDataModifierType.set, (sizeX * x.IntValue!.Value).ToString());
                    }
                    if (relativeY)
                    {
                        if (!y.IsInt() || y.IntValue!.Value != 0)
                        {
                            setup.Execute.Store(tempScore).World.Data.Get(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Y), 1 / sizeY);
                            if (y.IsInt())
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, y.IntValue!.Value);
                            }
                            else
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, y.ScoreValue!);
                            }
                            setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Y)), ID.StoreTypes.Double, sizeY).Entity.Score.Get(tempScore);
                        }
                    }
                    else if (y.IsScore())
                    {
                        setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Y)), ID.StoreTypes.Double, sizeY).Entity.Score.Get(y.ScoreValue!);
                    }
                    else if (!hasRelative)
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Y), ID.EntityDataModifierType.set, (sizeY * y.IntValue!.Value).ToString());
                    }
                    if (relativeZ)
                    {
                        if (!z.IsInt() || z.IntValue!.Value != 0)
                        {
                            setup.Execute.Store(tempScore).World.Data.Get(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Z), 1 / sizeZ);
                            if (z.IsInt())
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, z.IntValue!.Value);
                            }
                            else
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, z.ScoreValue!);
                            }
                            setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Z)), ID.StoreTypes.Double, sizeZ).Entity.Score.Get(tempScore);
                        }
                    }
                    else if (z.IsScore())
                    {
                        setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Z)), ID.StoreTypes.Double, sizeZ).Entity.Score.Get(z.ScoreValue!);
                    }
                    else if (!hasRelative)
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords!.Z), ID.EntityDataModifierType.set, (sizeZ * z.IntValue!.Value).ToString());
                    }
                    setup.Entity.Data.Copy(ID.Selector.s, Entities.AreaCloud.PathCreator.Make(d => d.Coords), ID.EntityDataModifierType.set, new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Coords)));
                });
                group.Execute.At(positionSelector);
            }, forceExecute: true);
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
        /// Executes at the rotation specified by the score values. Note that some precision is lost when doing relative rotation. Especially when size is higher than 1.
        /// </summary>
        /// <param name="horizontal">The horizontal rotation</param>
        /// <param name="vertical">The vertical rotation</param>
        /// <param name="relativeHorizontal">If the horizontal rotation should be relative to the executed rotation</param>
        /// <param name="relativeVertical">If the vertical rotation should be relative to the executed rotation</param>
        /// <param name="sizeHorizontal">How many degress 1 score is equal to in horizontal rotation</param>
        /// <param name="sizeVertical">How many degress 1 score is equal to in vertical rotation</param>
        /// <returns>The function running the command</returns>
        public Function Rotated(ValueParameter horizontal, ValueParameter vertical, bool relativeHorizontal = false, bool relativeVertical = false, double sizeHorizontal = 1, double sizeVertical = 1)
        {
            if (horizontal.IsInt() && vertical.IsInt())
            {
                throw new ArgumentException("You called rotated command using only ints. You might want to call rotated using a rotation object instead. (This is for executing rotated as score values)");
            }

            Selector positionSelector = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetDummySelector();
            Storage tempStorage = ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetTempStorage();
            ScoreValue tempScore = new ScoreValue("temp", ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetMathScoreObject());
            ForFunction.Custom.GroupCommands(group =>
            {
                group.Execute.As(positionSelector).World.Function(setup =>
                {
                    bool hasRelative = relativeHorizontal || relativeVertical;
                    if (hasRelative)
                    {
                        setup.World.Function(ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetDummyTeleportGetRotation());
                    }
                    else
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation), ID.EntityDataModifierType.set, new Rotation(horizontal.IsInt() ? sizeHorizontal * horizontal.IntValue!.Value : 0, vertical.IsInt() ? sizeVertical * vertical.IntValue!.Value : 0).GetAsArray(ID.NBTTagType.TagFloatArray, new object[] { }).GetDataString());
                    }
                    if (relativeHorizontal)
                    {
                        if (!horizontal.IsInt() || horizontal.IntValue!.Value != 0)
                        {
                            setup.Execute.Store(tempScore).World.Data.Get(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.Y), 1 / sizeHorizontal);
                            if (horizontal.IsInt())
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, horizontal.IntValue!.Value);
                            }
                            else
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, horizontal.ScoreValue!);
                            }
                            setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.Y)), ID.StoreTypes.Float, sizeHorizontal).Entity.Score.Get(tempScore);
                        }
                    }
                    else if (horizontal.IsScore())
                    {
                        setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.Y)), ID.StoreTypes.Float, sizeHorizontal).Entity.Score.Get(horizontal.ScoreValue!);
                    }
                    else if (!hasRelative)
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.Y), ID.EntityDataModifierType.set, (sizeHorizontal * horizontal.IntValue!.Value).ToString());
                    }
                    if (relativeVertical)
                    {
                        if (!vertical.IsInt() || vertical.IntValue!.Value != 0)
                        {
                            setup.Execute.Store(tempScore).World.Data.Get(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.X), 1 / sizeVertical);
                            if (vertical.IsInt())
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, vertical.IntValue!.Value);
                            }
                            else
                            {
                                setup.Entity.Score.Operation(tempScore, tempScore, ID.Operation.Add, vertical.ScoreValue!);
                            }
                            setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.X)), ID.StoreTypes.Float, sizeVertical).Entity.Score.Get(tempScore);
                        }
                    }
                    else if (vertical.IsScore())
                    {
                        setup.Execute.Store(new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.X)), ID.StoreTypes.Float, sizeVertical).Entity.Score.Get(vertical.ScoreValue!);
                    }
                    else if (!hasRelative)
                    {
                        setup.World.Data.Change(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation!.X), ID.EntityDataModifierType.set, (sizeVertical * vertical.IntValue!.Value).ToString());
                    }
                    setup.Entity.Data.Copy(ID.Selector.s, Entities.AreaCloud.PathCreator.Make(d => d.Rotation), ID.EntityDataModifierType.set, new StorageDataLocation(tempStorage, Entities.AreaCloud.PathCreator.Make(d => d.Rotation)));
                });
                group.Execute.Rotated(positionSelector);
            }, forceExecute: true);
            return ForFunction;
        }

        /// <summary>
        /// Executes in the given dimension
        /// </summary>
        /// <param name="dimension">The dimension</param>
        /// <returns>The function running the command</returns>
        public Function Dimension(DimensionObjects.IDimension dimension)
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

        /// <summary>
        /// Generates a random number from 0 to 1 and only executes if the number is less than <paramref name="chance"/>
        /// </summary>
        /// <param name="chance">The chance for the command to execute</param>
        /// <param name="want">false if it should execute when it's false</param>
        /// <returns>The function running the command</returns>
        public Function IfRandom(double chance, bool want = true)
        {
            if (chance < 0 || chance > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(chance), "Random chance has to be between 0 and 1");
            }
            ForFunction.Execute.IfPredicate(ForFunction.PackNamespace.Datapack.GetItems<SharpCraftFiles>().GetRandomPredicate(chance), want);
            return ForFunction;
        }
    }
}

