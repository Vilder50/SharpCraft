using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command for getting data out of a place
    /// </summary>
    public class DataGetCommand : ICommand
    {
        private IDataLocation dataLocation;
        private double scale;

        /// <summary>
        /// Intializes a new <see cref="DataGetCommand"/>
        /// </summary>
        /// <param name="dataLocation">The place to get the data from</param>
        /// <param name="scale">A number to multiply the data with</param>
        public DataGetCommand(IDataLocation dataLocation, double scale)
        {
            DataLocation = dataLocation;
            Scale = scale;
        }

        /// <summary>
        /// The place to get the data from
        /// </summary>
        public IDataLocation DataLocation 
        { 
            get => dataLocation; 
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null"); 
        }

        /// <summary>
        /// A number to multiply the data with
        /// </summary>
        public double Scale { get => scale; set => scale = value; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data get [DataLocation] [Scale]</returns>
        public string GetCommandString()
        {
            return $"data get {DataLocation.GetLocationString()} {Scale}";
        }
    }

    /// <summary>
    /// Command for merging data
    /// </summary>
    public class DataMergeCommand : ICommand
    {
        private IDataLocation dataLocation;
        private Data.DataHolderBase data;

        /// <summary>
        /// Intializes a new <see cref="DataMergeCommand"/>
        /// </summary>
        /// <param name="dataLocation">The place to get merge the data to</param>
        /// <param name="data">The data to merge</param>
        public DataMergeCommand(IDataLocation dataLocation, Data.DataHolderBase data)
        {
            DataLocation = dataLocation;
            Data = data;
        }

        /// <summary>
        /// The place to get merge the data to
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// The data to merge
        /// </summary>
        public Data.DataHolderBase Data 
        { 
            get => data; 
            set => data = value ?? throw new ArgumentNullException(nameof(Data), "Data may not be null");
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data merge [DataLocation] [Data]</returns>
        public string GetCommandString()
        {
            return $"data merge {DataLocation.GetLocationString()} {data.GetDataString()}";
        }
    }

    /// <summary>
    /// Command used for modifying data at a location with data from another location
    /// </summary>
    public class DataModifyWithLocationCommand : ICommand
    {
        private IDataLocation dataLocation;
        private IDataLocation fromDataLocation;

        /// <summary>
        /// Intializes a new <see cref="DataModifyWithLocationCommand"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to modify</param>
        /// <param name="modifyType">The way to modify the data</param>
        /// <param name="fromDataLocation">The location to get the data used for modifying</param>
        public DataModifyWithLocationCommand(IDataLocation dataLocation, ID.EntityDataModifierType modifyType, IDataLocation fromDataLocation)
        {
            DataLocation = dataLocation;
            FromDataLocation = fromDataLocation;
            ModifyType = modifyType;
        }

        /// <summary>
        /// The location of the data to modify
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// The location to get the data used for modifying
        /// </summary>
        public IDataLocation FromDataLocation
        {
            get => fromDataLocation;
            set => fromDataLocation = value ?? throw new ArgumentNullException(nameof(FromDataLocation), "FromDataLocation may not be null");
        }

        /// <summary>
        /// The way to modify the data
        /// </summary>
        public ID.EntityDataModifierType ModifyType { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data modify [DataLocation] [ModifyType] from [FromDataLocation]</returns>
        public string GetCommandString()
        {
            return $"data modify {DataLocation.GetLocationString()} {ModifyType} from {FromDataLocation.GetLocationString()}";
        }
    }

    /// <summary>
    /// Command used for modifying data at a location with given data
    /// </summary>
    public class DataModifyWithDataCommand : ICommand
    {
        private IDataLocation dataLocation;
        private Data.DataHolderBase data;

        /// <summary>
        /// Intializes a new <see cref="DataModifyWithDataCommand"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to modify</param>
        /// <param name="modifyType">The way to modify the data</param>
        /// <param name="data">The data to modify with</param>
        public DataModifyWithDataCommand(IDataLocation dataLocation, ID.EntityDataModifierType modifyType, DataHolderBase data)
        {
            DataLocation = dataLocation;
            Data = data;
            ModifyType = modifyType;
        }

        /// <summary>
        /// The location of the data to modify
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// The data to modify with
        /// </summary>
        public Data.DataHolderBase Data
        {
            get => data;
            set => data = value ?? throw new ArgumentNullException(nameof(Data), "Data may not be null");
        }

        /// <summary>
        /// The way to modify the data
        /// </summary>
        public ID.EntityDataModifierType ModifyType { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data modify [DataLocation] [ModifyType] value [Data]</returns>
        public string GetCommandString()
        {
            return $"data modify {DataLocation.GetLocationString()} {ModifyType} value {Data.GetDataString()}";
        }
    }

    /// <summary>
    /// Command used for modifying data at a location at an index with data from another location
    /// </summary>
    public class DataModifyInsertLocationCommand : ICommand
    {
        private IDataLocation dataLocation;
        private IDataLocation fromDataLocation;

        /// <summary>
        /// Intializes a new <see cref="DataModifyInsertLocationCommand"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to modify</param>
        /// <param name="index">The index of the data to change</param>
        /// <param name="fromDataLocation">The location to get the data used for modifying</param>
        public DataModifyInsertLocationCommand(IDataLocation dataLocation, int index, IDataLocation fromDataLocation)
        {
            DataLocation = dataLocation;
            FromDataLocation = fromDataLocation;
            Index = index;
        }

        /// <summary>
        /// The location of the data to modify
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// The location to get the data used for modifying
        /// </summary>
        public IDataLocation FromDataLocation
        {
            get => fromDataLocation;
            set => fromDataLocation = value ?? throw new ArgumentNullException(nameof(FromDataLocation), "FromDataLocation may not be null");
        }

        /// <summary>
        /// The index of the data to change
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data modify [DataLocation] insert [Index] from [FromDataLocation]</returns>
        public string GetCommandString()
        {
            return $"data modify {DataLocation.GetLocationString()} insert {Index} from {FromDataLocation.GetLocationString()}";
        }
    }

    /// <summary>
    /// Command used for modifying data at a location at an index with given data
    /// </summary>
    public class DataModifyInsertDataCommand : ICommand
    {
        private IDataLocation dataLocation;
        private Data.DataHolderBase data;

        /// <summary>
        /// Intializes a new <see cref="DataModifyInsertDataCommand"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to modify</param>
        /// <param name="index">The index of the data to change</param>
        /// <param name="data">The data to modify with</param>
        public DataModifyInsertDataCommand(IDataLocation dataLocation, int index, DataHolderBase data)
        {
            DataLocation = dataLocation;
            Data = data;
            Index = index;
        }

        /// <summary>
        /// The location of the data to modify
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// The data to modify with
        /// </summary>
        public DataHolderBase Data
        {
            get => data;
            set => data = value ?? throw new ArgumentNullException(nameof(Data), "Data may not be null");
        }

        /// <summary>
        /// The index of the data to change
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data modify [DataLocation] insert [Index] from [FromDataLocation]</returns>
        public string GetCommandString()
        {
            return $"data modify {DataLocation.GetLocationString()} insert {Index} value {Data.GetDataString()}";
        }
    }

    /// <summary>
    /// Command used for deleting data at a given location
    /// </summary>
    public class DataDeleteCommand : ICommand
    {
        private IDataLocation dataLocation;

        /// <summary>
        /// Intializes a new <see cref="DataDeleteCommand"/>
        /// </summary>
        /// <param name="dataLocation">The location of the data to remove</param>
        public DataDeleteCommand(IDataLocation dataLocation)
        {
            DataLocation = dataLocation;
        }

        /// <summary>
        /// The location of the data to remove
        /// </summary>
        public IDataLocation DataLocation
        {
            get => dataLocation;
            set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null");
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>data remove [DataLocation]</returns>
        public string GetCommandString()
        {
            return $"data remove {DataLocation.GetLocationString()}";
        }
    }
}
