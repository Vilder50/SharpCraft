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

        public static BaseDatapack Datapack { get; private set; }
        public static PackNamespace MinecraftNamespace { get; private set; }
        public static PackNamespace SharpCraftNamespace { get; private set; }
        public static Function SetupFunction { get; private set; }

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
            MinecraftNamespace = pack.Namespace<PackNamespace>("minecraft");
            SharpCraftNamespace = pack.Namespace<PackNamespace>("sharpcraft");
            #pragma warning disable IDE0067
            FunctionGroup loadGroup = MinecraftNamespace.Group(ID.Files.Groups.Function.load.ToString(), new List<IFunction>(), true);
            #pragma warning restore IDE0067
            SetupFunction = loadGroup.Items.SingleOrDefault(i => i is Function && i.PackNamespace == SharpCraftNamespace && i.Name == setupFunctionName) as Function;
            if (SetupFunction is null)
            {
                SetupFunction = SharpCraftNamespace.Function(setupFunctionName, BaseFile.WriteSetting.OnDispose);
                loadGroup.Items.Insert(0, SetupFunction);
            }
        }

        #region constants
        private const string constantFileName = "constants";
        public static Objective ConstantObjective { get; private set; }
        private static Function constantNumberFile;
        public static ScoreValue AddConstantNumber(int number)
        {
            //Get constant making function file
            if (constantNumberFile is null)
            {
                constantNumberFile = (SetupFunction.Commands.SingleOrDefault(c => c is Commands.RunFunctionCommand functionCommand && functionCommand.Function.Name == constantFileName) as Commands.RunFunctionCommand)?.Function as Function;

                if (constantNumberFile is null)
                {
                    constantNumberFile = SharpCraftNamespace.Function(constantFileName, BaseFile.WriteSetting.OnDispose);
                    ConstantObjective = new Objective("constants");
                    constantNumberFile.AddCommand(new Commands.ScoreboardObjectiveAddCommand(ConstantObjective, "dummy", null));
                    SetupFunction.AddCommand(new Commands.RunFunctionCommand(constantNumberFile));
                }
                else
                {
                    ConstantObjective = (constantNumberFile.Commands[0] as Commands.ScoreboardObjectiveAddCommand).ScoreObject;
                }
            }

            //Check if constant already is added. If not add it to the constant function file.
            if (!(constantNumberFile.Commands.SingleOrDefault(c => c is Commands.ScoreboardValueChangeCommand command && command.ChangeNumber == number) is Commands.ScoreboardValueChangeCommand existingCommand))
            {
                NameSelector selector = new NameSelector(number.ToString(), true);
                constantNumberFile.AddCommand(new Commands.ScoreboardValueChangeCommand(selector, ConstantObjective, ID.ScoreChange.set, number));
                return new ScoreValue(selector, ConstantObjective);
            }
            else
            {
                return new ScoreValue(existingCommand.Selector, ConstantObjective);
            }
        }
        #endregion

        #region math
        private static Objective mathObjective;
        private const string mathObjectiveName = "math";
        public static Objective GetMathScoreObject()
        {
            if (mathObjective is null)
            {
                if (SetupFunction.Commands.SingleOrDefault(c => c is Commands.ScoreboardObjectiveAddCommand scoreCommand && scoreCommand.ScoreObject.Name == mathObjectiveName) is Commands.ScoreboardObjectiveAddCommand addCommand)
                {
                    mathObjective = addCommand.ScoreObject;
                }
                else
                {
                    mathObjective = new Objective(mathObjectiveName);
                    SetupFunction.AddCommand(new Commands.ScoreboardObjectiveAddCommand(mathObjective, "dummy", null));
                }
            }

            return mathObjective;
        }
        #endregion
    }
}
