using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which disables a datapack
    /// </summary>
    public class DatapackDisableCommand : ICommand
    {
        private BaseDatapack datapack;

        /// <summary>
        /// Intializes a new <see cref="DatapackDisableCommand"/>
        /// </summary>
        /// <param name="datapack">The datapack to disable</param>
        public DatapackDisableCommand(BaseDatapack datapack)
        {
            Datapack = datapack;
        }

        /// <summary>
        /// The datapack to disable
        /// </summary>
        public BaseDatapack Datapack 
        { 
            get => datapack; 
            set => datapack = value ?? throw new ArgumentNullException(nameof(Datapack), "Datapack may not be null"); 
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>datapack disable [Datapack]</returns>
        public string GetCommandString()
        {
            return $"datapack disable {Datapack.IngameName}";
        }
    }

    /// <summary>
    /// Command which enables a datapack
    /// </summary>
    public class DatapackEnableCommand : ICommand
    {
        private BaseDatapack datapack;

        /// <summary>
        /// Intializes a new <see cref="DatapackEnableCommand"/>
        /// </summary>
        /// <param name="datapack">The datapack to disable</param>
        /// <param name="loadFirst">True if the datapack should be loaded before others. False if it should be loaded last</param>
        public DatapackEnableCommand(BaseDatapack datapack, bool loadFirst)
        {
            Datapack = datapack;
            LoadFirst = loadFirst;
        }

        /// <summary>
        /// The datapack to enable
        /// </summary>
        public BaseDatapack Datapack
        {
            get => datapack;
            set => datapack = value ?? throw new ArgumentNullException(nameof(Datapack), "Datapack may not be null");
        }

        /// <summary>
        /// True if the datapack should be loaded before others. False if it should be loaded last
        /// </summary>
        public bool LoadFirst { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>datapack enable [Datapack] [LoadFirst]</returns>
        public string GetCommandString()
        {
            return $"datapack enable {Datapack.IngameName} {(LoadFirst ? "first" : "last")}";
        }
    }

    /// <summary>
    /// Command which enables a datapack before or after another datapack
    /// </summary>
    public class DatapackEnableAtCommand : ICommand
    {
        private BaseDatapack datapack;
        private BaseDatapack otherDatapack;

        /// <summary>
        /// Intializes a new <see cref="DatapackEnableAtCommand"/>
        /// </summary>
        /// <param name="datapack">The datapack to disable</param>
        /// <param name="loadAfter">True if the datapack should be loaded before others. False if it should be loaded last</param>
        /// <param name="otherDatapack">The datapack to enable relative to</param>
        public DatapackEnableAtCommand(BaseDatapack datapack, bool loadAfter, BaseDatapack otherDatapack)
        {
            Datapack = datapack;
            LoadAfter = loadAfter;
            OtherDatapack = otherDatapack;
        }

        /// <summary>
        /// The datapack to enable
        /// </summary>
        public BaseDatapack Datapack
        {
            get => datapack;
            set => datapack = value ?? throw new ArgumentNullException(nameof(Datapack), "Datapack may not be null");
        }

        /// <summary>
        /// True if the datapack should be loaded before others. False if it should be loaded last
        /// </summary>
        public bool LoadAfter { get; set; }

        /// <summary>
        /// The datapack to enable relative to
        /// </summary>
        public BaseDatapack OtherDatapack
        {
            get => otherDatapack;
            set => otherDatapack = value ?? throw new ArgumentNullException(nameof(OtherDatapack), "OtherDatapack may not be null");
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>datapack enable [Datapack] [LoadAfter] [OtherDatapack]</returns>
        public string GetCommandString()
        {
            return $"datapack enable {Datapack.IngameName} {(LoadAfter ? "after" : "before")} {OtherDatapack.IngameName}";
        }
    }

    /// <summary>
    /// Command which returns a list of datapacks
    /// </summary>
    public class DatapackListCommand : ICommand
    {
        /// <summary>
        /// Intializes a new <see cref="DatapackListCommand"/>
        /// </summary>
        /// <param name="list">The list to get</param>
        public DatapackListCommand(ID.DatapackList list)
        {
            List = list;
        }

        /// <summary>
        /// The list to get
        /// </summary>
        public ID.DatapackList List { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>datapack list [List]</returns>
        public string GetCommandString()
        {
            if (List == ID.DatapackList.all)
            {
                return $"datapack list";
            }
            else
            {
                return $"datapack list {List}";
            }
        }
    }
}
