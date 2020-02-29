using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    static class SharpCraftFiles
    {
        private const string setupFunctionName = "setup";
        //private const string tickFunctionName = "tick";

        public static BaseDatapack? Datapack { get; private set; }
        public static PackNamespace? MinecraftNamespace { get; private set; }
        public static PackNamespace? SharpCraftNamespace { get; private set; }
        public static Function? SetupFunction { get; private set; }
        //public static Function TickFunction { get; private set; }

        static SharpCraftFiles()
        {
            BaseDatapack.AddDatapackListener(dp =>
            {
                if (Datapack is null)
                {
                    Datapack = dp;
                    try
                    {
                        SetupFiles(dp);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to create and/or add sharpcraft files to the datapack. See inner exception.", ex);
                    }
                }
            });
        }

        private static void SetupFiles(BaseDatapack pack)
        {
            SharpCraftSettings.LockSettings();
            MinecraftNamespace = pack.Namespace<PackNamespace>("minecraft");
            SharpCraftNamespace = pack.Namespace<PackNamespace>("sharpcraft");
            #pragma warning disable IDE0067
            FunctionGroup loadGroup = MinecraftNamespace.Group(ID.Files.Groups.Function.load.ToString(), new List<IFunction>(), true);
            //FunctionGroup tickGroup = MinecraftNamespace.Group(ID.Files.Groups.Function.tick.ToString(), new List<IFunction>(), true);
            #pragma warning restore IDE0067

            SetupFunction = SharpCraftNamespace.Function(setupFunctionName, BaseFile.WriteSetting.OnDispose);
            SetupFunction.World.LoadSquare.ForceLoad(SharpCraftSettings.OwnedChunk * 16);
            loadGroup.Items.Insert(0, SetupFunction);

            //TickFunction = SharpCraftNamespace.Function(tickFunctionName, BaseFile.WriteSetting.OnDispose);
            //tickGroup.Items.Insert(0, TickFunction);
        }

        #region constants
        private const string constantFileName = "constants";
        public static Objective? ConstantObjective { get; private set; }
        private static Function? constantNumberFile;
        private static readonly List<Commands.ScoreboardValueChangeCommand> addedNumbers = new List<Commands.ScoreboardValueChangeCommand>();
        public static ScoreValue AddConstantNumber(int number)
        {
            //Get constant making function file
            if (constantNumberFile is null)
            {
                constantNumberFile = SharpCraftNamespace!.Function(constantFileName, BaseFile.WriteSetting.OnDispose);
                ConstantObjective = new Objective("constants");
                constantNumberFile.AddCommand(new Commands.ScoreboardObjectiveAddCommand(ConstantObjective, "dummy", null));
                SetupFunction!.AddCommand(new Commands.RunFunctionCommand(constantNumberFile));
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
        private static Objective? mathObjective;
        private const string mathObjectiveName = "math";
        public static Objective GetMathScoreObject()
        {
            if (mathObjective is null)
            {
                mathObjective = new Objective(mathObjectiveName);
                SetupFunction!.AddCommand(new Commands.ScoreboardObjectiveAddCommand(mathObjective!, "dummy", null));
            }

            return mathObjective;
        }
        #endregion

        #region ray cast
        private const string rotationObjectiveString = "_rotation";
        private static (Function? raySetup, Objective? xRotation, Objective? yRotation, ScoreValue? rayState, Predicate[]? predicates) rayFiles;
        public static (Function raySetup, Objective xRotation, Objective yRotation, ScoreValue rayState, Predicate[] predicates) GetRayFiles()
        {
            if (!(rayFiles.raySetup is null))
            {
                return rayFiles!;
            }

            Objective xRotation;
            Objective yRotation;
            List<Predicate> predicates = new List<Predicate>();
            ScoreValue rayState = new ScoreValue(new NameSelector("#rayState"), GetMathScoreObject());

            //ray casting files hasn't been set up
            xRotation = SetupFunction!.World.Objective.Add("x" + rotationObjectiveString);
            yRotation = SetupFunction.World.Objective.Add("y" + rotationObjectiveString);

            Function raySetup = SharpCraftNamespace!.Function("raycast\\block\\setup", setup =>
            {
                setup.Entity.Teleport(new Selector(), new Coords(), new Rotation(true, 0, 0));
                setup.Execute.Store(new Selector(), yRotation);
                setup.Entity.Data.Get(new Selector(), "Rotation[0]");
                setup.Execute.Store(new Selector(), xRotation);
                setup.Entity.Data.Get(new Selector(), "Rotation[1]");
            });

            MCRange checkRange = new MCRange(-0.3, 0.3);
            #pragma warning disable IDE0067
            Predicate checkXBlock = SharpCraftNamespace.Predicate("raycast\\block\\x", new Conditions.EntityCondition(ID.LootTarget.This, new JSONObjects.Entity() { Distance = new JSONObjects.Distance() { X = checkRange } }));
            Predicate checkYBlock = SharpCraftNamespace.Predicate("raycast\\block\\y", new Conditions.EntityCondition(ID.LootTarget.This, new JSONObjects.Entity() { Distance = new JSONObjects.Distance() { Y = checkRange } }));
            Predicate checkZBlock = SharpCraftNamespace.Predicate("raycast\\block\\z", new Conditions.EntityCondition(ID.LootTarget.This, new JSONObjects.Entity() { Distance = new JSONObjects.Distance() { Z = checkRange } }));
            Predicate lookNegativeY = SharpCraftNamespace.Predicate("raycast\\block\\py", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(xRotation, new MCRange(-90, 0))));
            Predicate lookPositiveY = SharpCraftNamespace.Predicate("raycast\\block\\ny", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(xRotation, new MCRange(0, 90))));
            Predicate lookPositiveZ = SharpCraftNamespace.Predicate("raycast\\block\\pz", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-90, 90))));
            Predicate lookNegativeX = SharpCraftNamespace.Predicate("raycast\\block\\nx", new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(0, 180))));
            #pragma warning restore IDE0067
            predicates.Add(lookNegativeY);
            predicates.Add(lookPositiveY);

            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cnz", checkZBlock.GetCondition() | !lookPositiveZ.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cpx", checkXBlock.GetCondition() | !lookNegativeX.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cpz", checkZBlock.GetCondition() | lookPositiveZ.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cnx", checkXBlock.GetCondition() | lookNegativeX.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cny", checkYBlock.GetCondition() | !lookNegativeY.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\cpy", checkYBlock.GetCondition() | !lookPositiveY.GetCondition()));

            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\xyz", checkXBlock.GetCondition() | checkYBlock.GetCondition() | checkZBlock.GetCondition()));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\d0", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-90, 0)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\d1", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(0, 90)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\d2", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(90, 180)))));
            predicates.Add(SharpCraftNamespace.Predicate("raycast\\block\\d3", !new Conditions.EntityScoresCondition(ID.LootTarget.This, new Conditions.EntityScoresCondition.Scores.Score(yRotation, new MCRange(-180, -90)))));

            rayFiles = (raySetup, xRotation, yRotation, rayState, predicates.ToArray());
            return rayFiles!;
        }
        #endregion

        #region dummy entity
        private const string dummyEntityTag = "SharpDEntity";
        private static Selector? dummyEntitySelector;
        public static Selector GetDummySelector()
        {
            if(!(dummyEntitySelector is null))
            {
                return dummyEntitySelector;
            }

            dummyEntitySelector = new Selector(ID.Selector.e, dummyEntityTag) { Limit = 1 };
            SetupFunction!.Entity.Kill(dummyEntitySelector);
            Entity.EntityBasic dummyEntity = new Entity.AreaCloud(ID.Entity.area_effect_cloud) { Unspawnable = true, Tags = new Tag[] { dummyEntitySelector.Tags![0].Tag } };
            SetupFunction.Entity.Add(SharpCraftSettings.OwnedChunk * 16, dummyEntity);

            return dummyEntitySelector;
        }
        #endregion

        #region random
        private static readonly Dictionary<double, Predicate> randomnessPredicates = new Dictionary<double, Predicate>();
        public static Predicate GetRandomPredicate(double chance)
        {
            if (randomnessPredicates.ContainsKey(chance))
            {
                return randomnessPredicates[chance];
            }
            else
            {
                //create randomness predicate
                Predicate newPredicate = SharpCraftNamespace!.Predicate("random\\chances\\" + chance.ToMinecraftDouble().Replace(".","_"), new Conditions.RandomCondition(chance));
                randomnessPredicates.Add(chance, newPredicate);
                return newPredicate;
            }
        }

        private static ScoreValue? randomNumberHolder;
        public static ScoreValue GetRandomHolder()
        {
            randomNumberHolder ??= new ScoreValue("#random", GetMathScoreObject());
            return randomNumberHolder;
        }

        private static Function? randomNumberFunction;
        public static Function GetRandomNumberFunction()
        {
            if (!(randomNumberFunction is null))
            {
                return randomNumberFunction;
            }

            randomNumberFunction = SharpCraftNamespace!.Function("random\\generate", (f) => 
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

        private static LootTable? hashLoottable;
        public static LootTable GetHashLoottable()
        {
            if (!(hashLoottable is null))
            {
                return hashLoottable;
            }

            hashLoottable = SharpCraftNamespace!.Loottable("random\\hashing", new LootObjects.LootPool(new LootObjects.ItemEntry(ID.Item.dirt)
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

        private static (Function?, Vector?) hashFunction;
        public static (Function function, Vector location) GetHashFunction()
        {
            if (!(hashFunction.Item1 is null))
            {
                return hashFunction!;
            }
            Vector hashBlockLocation = SharpCraftSettings.OwnedChunk * 16;

            LootTable hashLoottable = SharpCraftNamespace!.Loottable("random\\hashing", new LootObjects.LootPool(new LootObjects.ItemEntry(ID.Item.dirt)
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
            Function hash = SharpCraftNamespace.Function("random\\hashing", h => 
            {
                h.AddCommand(new Commands.LootCommand(new Commands.LootTargets.BlockTarget(hashBlockLocation, new Slots.ContainerSlot(28)), new Commands.LootSources.MineHandSource(hashBlockLocation, true)));

                Data.DataPath slotpath = Data.DataPath.GetDataPath<Blocks.ShulkerBox>(b => b.DItems);
                slotpath.Condition(0);
                Data.DataPath attributePath = Data.DataPath.GetDataPath<Item>(i => i.Attributes);
                attributePath.Condition(0);
                slotpath.SetNextPath(attributePath);

                h.Execute.Store(GetRandomHolder(), GetRandomHolder());
                h.Block.Data.Get(hashBlockLocation, slotpath.ToString() + ".Amount");

                h.Block.Add(hashBlockLocation, ID.Block.air);
                h.Block.Add(hashBlockLocation, new Blocks.ShulkerBox(ID.Block.shulker_box) { DLootTable = hashLoottable });
            });

            hashFunction = (hash, hashBlockLocation);
            return hashFunction!;
        }
        #endregion
    }
}
