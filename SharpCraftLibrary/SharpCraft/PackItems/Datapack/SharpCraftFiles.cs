using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    internal class SharpCraftFiles : IDatapackItems
    {
        private BaseDatapack datapack = null!;

        public BaseDatapack Datapack { get => datapack; set => datapack ??= value; }

        private const string setupFunctionName = "setup";
        private const string tickFunctionName = "tick";

        public PackNamespace? MinecraftNamespace { get; private set; }
        public PackNamespace? SharpCraftNamespace { get; private set; }
        public Function? SetupFunction { get; private set; }
        public Function? TickFunction { get; private set; }

        public bool IsChild()
        {
            return Datapack.GetDatapackSetting<SharpDatapackChildSetting>()?.IsChild ?? false;
        }

        private bool isSetup = false;
        public void SetupFiles()
        {
            if (!isSetup)
            {
                SharpCraftNamespace = Datapack.Namespace<PackNamespace>(Datapack.GetDatapackSetting<SharpCraftNamespaceNameSetting>()?.Name ?? "sharpcraft");
                if (!IsChild())
                {
                    MinecraftNamespace = Datapack.Namespace<PackNamespace>("minecraft");
                    #pragma warning disable IDE0067
                    FunctionGroup loadGroup = MinecraftNamespace.Group(ID.Files.Groups.Function.load.ToString(), new List<IFunction>(), true);
                    FunctionGroup tickGroup = MinecraftNamespace.Group(ID.Files.Groups.Function.tick.ToString(), new List<IFunction>(), true);
                    #pragma warning restore IDE0067

                    SetupFunction = SharpCraftNamespace.Function(setupFunctionName, BaseFile.WriteSetting.OnDispose);
                    SetupFunction.World.LoadSquare.ForceLoad(Datapack.GetDatapackSetting<LoadedChunkSetting>()!.CornerBlock);
                    loadGroup.Items.Insert(0, SetupFunction);

                    TickFunction = SharpCraftNamespace.Function(tickFunctionName, BaseFile.WriteSetting.OnDispose);
                    tickGroup.Items.Insert(0, TickFunction);
                }
                isSetup = true;
            }
        }

        #region constants
        private const string constantFileName = "constants";
        public Objective? ConstantObjective { get; private set; }
        private Function? constantNumberFile;
        private readonly List<Commands.ScoreboardValueChangeCommand> addedNumbers = new List<Commands.ScoreboardValueChangeCommand>();
        public ScoreValue AddConstantNumber(int number)
        {
            SetupFiles();
            //Get constant making function file
            if (constantNumberFile is null)
            {
                constantNumberFile = SharpCraftNamespace!.Function(Datapack.Name + "/" + constantFileName, BaseFile.WriteSetting.OnDispose);
                ConstantObjective = new Objective("constants");

                if (!IsChild())
                {
                    SetupFunction!.AddCommand(new Commands.ScoreboardObjectiveAddCommand(ConstantObjective, "dummy", null));
                    SetupFunction!.AddCommand(new Commands.RunFunctionCommand(SharpCraftNamespace.Group("constants", new List<IFunction>() { constantNumberFile }, true)));
                }
            }

            //Check if constant already is added. If not add it to the constant function file.
            Commands.ScoreboardValueChangeCommand command = addedNumbers.SingleOrDefault(c => c.ChangeNumber == number);
            if (command is null)
            {
                NameSelector selector = new NameSelector(number.ToString(), true);
                Commands.ScoreboardValueChangeCommand addCommand = new Commands.ScoreboardValueChangeCommand(selector, ConstantObjective!, ID.ScoreChange.set, number);
                constantNumberFile.AddCommand(addCommand);
                addedNumbers.Add(addCommand);
                return new ScoreValue(selector, ConstantObjective!);
            }
            else
            {
                return new ScoreValue(command.Selector, ConstantObjective!);
            }
        }
        #endregion

        #region math
        private Objective? mathObjective;
        private const string mathObjectiveName = "math";
        public Objective GetMathScoreObject()
        {
            SetupFiles();
            if (mathObjective is null)
            {
                mathObjective = new Objective(mathObjectiveName);
                if (!IsChild())
                {
                    SetupFunction!.AddCommand(new Commands.ScoreboardObjectiveAddCommand(mathObjective!, "dummy", null));
                }
            }

            return mathObjective;
        }
        #endregion

        #region ray cast
        private const string rotationObjectiveString = "_rotation";
        private (IFunction? raySetup, Objective? xRotation, Objective? yRotation, ScoreValue? rayState, IPredicate[]? predicates) rayFiles;
        public (IFunction raySetup, Objective xRotation, Objective yRotation, ScoreValue rayState, IPredicate[] predicates) GetRayFiles()
        {
            SetupFiles();
            if (!(rayFiles.raySetup is null))
            {
                return rayFiles!;
            }

            ScoreValue rayState = new ScoreValue(new NameSelector("#rayState"), GetMathScoreObject());
            if (IsChild())
            {
                rayFiles = (new EmptyFunction(SharpCraftNamespace!, "raycast/block/setup"), new Objective("x" + rotationObjectiveString), new Objective("y" + rotationObjectiveString), rayState, new IPredicate[] 
                {
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/py"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/ny"),
                                                                         
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cnz"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cpx"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cpz"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cnx"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cny"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/cpy"),
                                                                          
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/xyz"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/d0"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/d1"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/d2"),
                    new EmptyPredicate(SharpCraftNamespace!, "raycast/block/d3"),
                });
                return rayFiles!;
            }

            Objective xRotation;
            Objective yRotation;
            List<Predicate> predicates = new List<Predicate>();

            //ray casting files hasn't been set up
            xRotation = SetupFunction!.World.Objective.Add("x" + rotationObjectiveString);
            yRotation = SetupFunction.World.Objective.Add("y" + rotationObjectiveString);

            Function raySetup = SharpCraftNamespace!.Function("raycast/block/setup", setup =>
            {
                setup.Entity.Teleport(new Selector(), new Coords(), new Rotation(true, 0, 0));
                setup.Execute.Store(new Selector(), yRotation);
                setup.Entity.Data.Get(new Selector(), "Rotation[0]");
                setup.Execute.Store(new Selector(), xRotation);
                setup.Entity.Data.Get(new Selector(), "Rotation[1]");
            });

            MCRange checkRange = new MCRange(-0.3, 0.3);
            #pragma warning disable IDE0067
            Predicate checkXBlock = SharpCraftNamespace.Predicate("raycast/block/x", new Conditions.EntityCondition(ID.LootTarget.This, new JsonObjects.Entity() { Distance = new JsonObjects.Distance() { X = checkRange } }));
            Predicate checkYBlock = SharpCraftNamespace.Predicate("raycast/block/y", new Conditions.EntityCondition(ID.LootTarget.This, new JsonObjects.Entity() { Distance = new JsonObjects.Distance() { Y = checkRange } }));
            Predicate checkZBlock = SharpCraftNamespace.Predicate("raycast/block/z", new Conditions.EntityCondition(ID.LootTarget.This, new JsonObjects.Entity() { Distance = new JsonObjects.Distance() { Z = checkRange } }));
            Predicate lookNegativeY = SharpCraftNamespace.Predicate("raycast/block/py", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(xRotation, new MCRange(-90, 0))));
            Predicate lookPositiveY = SharpCraftNamespace.Predicate("raycast/block/ny", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(xRotation, new MCRange(0, 90))));
            Predicate lookPositiveZ = SharpCraftNamespace.Predicate("raycast/block/pz", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-90, 90))));
            Predicate lookNegativeX = SharpCraftNamespace.Predicate("raycast/block/nx", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(0, 180))));
            #pragma warning restore IDE0067
            predicates.Add(lookNegativeY);
            predicates.Add(lookPositiveY);

            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cnz", checkZBlock.GetCondition() | !lookPositiveZ.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cpx", checkXBlock.GetCondition() | !lookNegativeX.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cpz", checkZBlock.GetCondition() | lookPositiveZ.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cnx", checkXBlock.GetCondition() | lookNegativeX.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cny", checkYBlock.GetCondition() | !lookNegativeY.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/cpy", checkYBlock.GetCondition() | !lookPositiveY.GetCondition()));

            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/xyz", checkXBlock.GetCondition() | checkYBlock.GetCondition() | checkZBlock.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/d0", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-90, 0)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/d1", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(0, 90)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/d2", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(90, 180)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast/block/d3", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-180, -90)))));

            rayFiles = (raySetup, xRotation, yRotation, rayState, predicates.ToArray());
            return rayFiles!;
        }
        #endregion

        #region dummy entity
        private const string dummyEntityTag = "SharpDEntity";
        private Selector? dummyEntitySelector;
        public Selector GetDummySelector()
        {
            SetupFiles();
            if (!(dummyEntitySelector is null))
            {
                return dummyEntitySelector;
            }

            dummyEntitySelector = new Selector(ID.Selector.e, dummyEntityTag) { Limit = 1 };
            if (!IsChild())
            {
                SetupFunction!.Entity.Kill(dummyEntitySelector);
                Entity dummyEntity = new Entities.AreaCloud(ID.Entity.area_effect_cloud) { Unspawnable = true, Tags = new Tag[] { dummyEntitySelector.Tags![0].Tag } };
                SetupFunction.Entity.Add(Datapack.GetDatapackSetting<LoadedChunkSetting>()!.CornerBlock, dummyEntity);
            }

            return dummyEntitySelector;
        }
        #endregion

        #region random
        private readonly Dictionary<double, Predicate> randomnessPredicates = new Dictionary<double, Predicate>();
        public Predicate GetRandomPredicate(double chance)
        {
            SetupFiles();
            if (randomnessPredicates.ContainsKey(chance))
            {
                return randomnessPredicates[chance];
            }
            else
            {
                //create randomness predicate
                Predicate newPredicate = SharpCraftNamespace!.Predicate(datapack.Name + "/random/chances/" + chance.ToMinecraftDouble().Replace(".","_"), new Conditions.RandomCondition(chance));
                randomnessPredicates.Add(chance, newPredicate);
                return newPredicate;
            }
        }

        private ScoreValue? randomNumberHolder;
        public ScoreValue GetRandomHolder()
        {
            randomNumberHolder ??= new ScoreValue("#random", GetMathScoreObject());
            return randomNumberHolder;
        }

        private IFunction? randomNumberFunction;
        public IFunction GetRandomNumberFunction()
        {
            SetupFiles();
            if (!(randomNumberFunction is null))
            {
                return randomNumberFunction;
            }

            if (IsChild())
            {
                randomNumberFunction = new EmptyFunction(SharpCraftNamespace!, "random/generate");
                return randomNumberFunction;
            }

            randomNumberFunction = SharpCraftNamespace!.Function("random/generate", (f) => 
            {
                f.Entity.Score.Set(GetRandomHolder(), GetRandomHolder(), 0);
                Predicate halfChance = GetRandomPredicate(0.5);
                for (int i = 0; i < 31; i++)
                {
                    f.Execute.IfPredicate(halfChance);
                    f.Entity.Score.Add(GetRandomHolder(), GetRandomHolder(), (int)Math.Pow(2,i));
                }
            });
            return randomNumberFunction;
        }

        private ILootTable? hashLoottable;
        public ILootTable GetHashLoottable()
        {
            SetupFiles();
            if (!(hashLoottable is null))
            {
                return hashLoottable;
            }

            if (IsChild())
            {
                hashLoottable = new EmptyLoottable(SharpCraftNamespace!, "random/hashing");
            }

            hashLoottable = SharpCraftNamespace!.Loottable("random/hashing", new LootObjects.LootPool(new LootObjects.ItemEntry(ID.Item.dirt)
            {
                Changes = new LootObjects.BaseChange[]
                    {
                        new LootObjects.AttributeChange(new LootObjects.AttributeChange.Attribute[]
                        {
                            new LootObjects.AttributeChange.Attribute(ID.AttributeType.generic_luck, ID.AttributeOperation.addition, new MCRange(int.MinValue, int.MaxValue), ID.AttributeSlot.head)
                        }),
                        new LootObjects.CountChange(1)
                    }
            }, 1), LootTable.TableType.chest);
            return hashLoottable;
        }

        private (Function?, Vector?) hashFunction;
        public (Function function, Vector location) GetHashFunction()
        {
            SetupFiles();
            if (!(hashFunction.Item1 is null))
            {
                return hashFunction!;
            }
            Vector hashBlockLocation = Datapack.GetItems<LoadedBlockItems>().GetNextLoadedCoords();

            LootTable hashLoottable = SharpCraftNamespace!.Loottable(Datapack.Name + "/random/hashing", new LootObjects.LootPool(new LootObjects.ItemEntry(ID.Item.dirt)
            {
                Changes = new LootObjects.BaseChange[]
                    {
                        new LootObjects.AttributeChange(new LootObjects.AttributeChange.Attribute[]
                        {
                            new LootObjects.AttributeChange.Attribute(ID.AttributeType.generic_luck, ID.AttributeOperation.addition, new MCRange(int.MinValue, int.MaxValue), ID.AttributeSlot.head)
                        }),
                        new LootObjects.CountChange(1)
                    }
            }, 1), LootTable.TableType.chest);

            SetupFunction!.AddCommand(new Commands.SetblockCommand(hashBlockLocation, ID.Block.bedrock, ID.BlockAdd.replace));
            SetupFunction.Block.Add(hashBlockLocation, new Blocks.ShulkerBox(ID.Block.shulker_box) { DLootTable = hashLoottable });
            Function hash = SharpCraftNamespace.Function(Datapack.Name + "/random/hashing", h => 
            {
                h.AddCommand(new Commands.LootCommand(new Commands.LootTargets.BlockTarget(hashBlockLocation, new Slots.ContainerSlot(28)), new Commands.LootSources.MineHandSource(hashBlockLocation, true)));

                h.Execute.Store(GetRandomHolder(), GetRandomHolder());
                h.Block.Data.Get(hashBlockLocation, Data.DataPathCreator.GetPath<Blocks.ShulkerBox>(b => b.DItems![0]!.Attributes![0]!.Amount));

                h.Block.Add(hashBlockLocation, ID.Block.air);
                h.Block.Add(hashBlockLocation, new Blocks.ShulkerBox(ID.Block.shulker_box) { DLootTable = hashLoottable });
            });

            hashFunction = (hash, hashBlockLocation);
            return hashFunction!;
        }
        #endregion

        #region
        class ShulkerItemTag : Data.DataHolderBase
        {
            [Data.DataTag("SharpCraft.ShulkerLooter")]
            public bool IsShulkerItem { get; private set; } = true;
        }
        private Item? shulkerLootItem;
        public Item GetShulkerLootItem()
        {
            SetupFiles();
            if (shulkerLootItem is null)
            {
                shulkerLootItem = new Item(ID.Item.ice);
                shulkerLootItem.AddExtraData(new ShulkerItemTag());
                if (IsChild())
                {
                    return shulkerLootItem;
                }

                Conditions.BaseCondition itemCondition = new Conditions.ToolCondition(new JsonObjects.Item() { NBT = shulkerLootItem.GetItemTagString() });
#pragma warning disable IDE0067
                MinecraftNamespace!.Loottable(ID.Files.LootTables.Block(ID.Block.purple_shulker_box), new LootObjects.LootPool[]
                {
                    new LootObjects.LootPool(new LootObjects.ItemEntry(ID.Item.purple_shulker_box)
                    {
                        Changes = new LootObjects.BaseChange[]
                        {
                            new LootObjects.CopyNameChange(),
                            new LootObjects.CopyNBTChange(ID.LootTarget.block_entity, new LootObjects.CopyNBTChange.CopyOperation[] 
                            {
                                new LootObjects.CopyNBTChange.CopyOperation(Data.DataPathCreator.GetPath<Blocks.ShulkerBox>(s => s.DLock), Data.DataPathCreator.GetPath<Item>(i => (i.BlockData as Blocks.ShulkerBox)!.DLock), ID.LootDataModifierType.replace),
                                new LootObjects.CopyNBTChange.CopyOperation(Data.DataPathCreator.GetPath<Blocks.ShulkerBox>(s => s.DLootTable), Data.DataPathCreator.GetPath<Item>(i => (i.BlockData as Blocks.ShulkerBox)!.DLootTable), ID.LootDataModifierType.replace),
                                new LootObjects.CopyNBTChange.CopyOperation(Data.DataPathCreator.GetPath<Blocks.ShulkerBox>(s => s.DLootTableSeed), Data.DataPathCreator.GetPath<Item>(i => (i.BlockData as Blocks.ShulkerBox)!.DLootTableSeed), ID.LootDataModifierType.replace)
                            }),
                            new LootObjects.ContentChange(new LootObjects.DynamicEntry(LootObjects.DynamicEntry.DynamicType.contents))
                        }
                    }, 1, !itemCondition),
                    new LootObjects.LootPool(new LootObjects.DynamicEntry(LootObjects.DynamicEntry.DynamicType.contents), 1, itemCondition),
                });
                #pragma warning restore IDE0067
            }
            return shulkerLootItem;
        }
        #endregion

        #region objective events
        private FunctionGroup? playerObjectiveEventChecker;
        private Function? setupObjectiveEventsFunction;
        private Dictionary<string,FunctionGroup>? objectiveEventFunctions;
        public void AddObjectiveEventFunction(Function addEventToFunction, string objective, bool executeForeach)
        {
#pragma warning disable IDE0068
            SetupFiles();
            if (playerObjectiveEventChecker is null)
            {
                playerObjectiveEventChecker = SharpCraftNamespace!.Group("objectiveEvents/base", new List<IFunction>());

                if (!IsChild())
                {
                    TickFunction!.Execute.As(ID.Selector.a);
                    TickFunction.Execute.At(ID.Selector.s);
                    TickFunction.World.Function(playerObjectiveEventChecker);
                }
            }
            if (objectiveEventFunctions is null)
            {
                objectiveEventFunctions = new Dictionary<string, FunctionGroup>();
            }

            string dictionaryName = objective + (executeForeach ? "T" : "F");
            if (objectiveEventFunctions.ContainsKey(dictionaryName))
            {
                objectiveEventFunctions[dictionaryName].Items.Add(addEventToFunction);
                return;
            }

            //create event function
            string objectiveName = objective.Replace("minecraft", "").Replace(".", "").Replace(":", "").Replace("_", "");
            if (objectiveName.Length > 14)
            {
                objectiveName = objectiveName.Substring(0,14);
            }
            if (setupObjectiveEventsFunction is null)
            {
                setupObjectiveEventsFunction = SharpCraftNamespace!.Function("objectiveEvents/setup");
                FunctionGroup setupObjectiveFunction = SharpCraftNamespace.Group("objectiveEvents/setup", new List<IFunction>() { setupObjectiveEventsFunction });
                if (!IsChild())
                {
                    SetupFunction!.World.Function(setupObjectiveFunction);
                }
            }
            Objective scoreObjective = setupObjectiveEventsFunction.World.Objective.Add("SE" + objectiveName, objective, "Sharpcraft event objective");
            FunctionGroup eventGroup = SharpCraftNamespace!.Group("objectiveEvents/events/" + objectiveName, new List<IFunction>() { addEventToFunction });
            Function eventFunction = SharpCraftNamespace!.Function("objectiveEvents/events/R" + objectiveName, f => 
            { 
                if (executeForeach)
                {
                    f.Entity.Score.Add(ID.Selector.s, scoreObjective, -1);
                    f.Execute.IfScore(ID.Selector.s, scoreObjective, 1..);
                    f.World.Function(f);
                }
                else
                {
                    f.Entity.Score.Set(ID.Selector.s, scoreObjective, 0);
                }
                f.World.Function(eventGroup);
            });
            Function shouldRunEventCheck = SharpCraftNamespace!.Function("objectiveEvents/events/C" + objectiveName, f =>
            {
                f.Execute.IfScore(ID.Selector.s, scoreObjective, 1..);
                f.World.Function(eventFunction);
            });
            playerObjectiveEventChecker.Items.Add(shouldRunEventCheck);
            objectiveEventFunctions.Add(objective, eventGroup);
#pragma warning restore IDE0068
        }
        #endregion
    }
}
