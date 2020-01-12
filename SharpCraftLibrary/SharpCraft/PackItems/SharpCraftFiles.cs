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
    }
}
