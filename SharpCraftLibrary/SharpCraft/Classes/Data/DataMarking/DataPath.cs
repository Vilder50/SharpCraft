using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft;
using System.Reflection;
using System.Linq.Expressions;

namespace SharpCraft.Data
{
    /// <summary>
    /// Describes the location of some data
    /// </summary>
    public class DataPath
    {
        private string path;
        private DataPath nextPath;
        private DataPathCondition[] conditions;
        private readonly bool isArray;
        private readonly int arrayCount;

        /// <summary>
        /// Creates a new datapath with the given path
        /// </summary>
        /// <param name="path">The path to the data</param>
        public DataPath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Creates a new datapath with the given path which ends with array(s)
        /// </summary>
        /// <param name="path">The path of the array(s)</param>
        /// <param name="arrayCount">the amount of arrays</param>
        public DataPath(string path, int arrayCount)
        {
            Path = path;
            this.arrayCount = arrayCount;
            isArray = true;
        }

        /// <summary>
        /// The path to the data
        /// </summary>
        public string Path
        {
            get => path;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("The path may not be null or empty");
                }
                if (!value.All(c => char.IsLetter(c) || c == '_' || c == '.'))
                {
                    throw new ArgumentNullException("Paths can only contain letters _ and .");
                }

                path = value;
            }
        }

        /// <summary>
        /// The amount of nested arrays in this path
        /// </summary>
        public int ArrayCount
        {
            get
            {
                return arrayCount;
            }
        }

        /// <summary>
        /// If this datapath is a path to an array or not
        /// </summary>
        public bool IsArray
        {
            get => isArray;
        }

        /// <summary>
        /// Continues the path if the condition(s) are meet
        /// </summary>
        /// <param name="conditions">The condition(s)</param>
        public void Condition(params DataPathCondition[] conditions)
        {
            if (conditions is null)
            {
                throw new ArgumentNullException("Condition may not be null");
            }

            foreach (DataPathCondition condition in conditions)
            {
                if (condition is null)
                {
                    throw new ArgumentNullException("A condition may not be null");
                }
            }

            if (IsArray)
            {
                if (conditions.Length != ArrayCount)
                {
                    throw new ArgumentOutOfRangeException("Not enough conditions has been specified for the array(s). You need " + ArrayCount + " conditions.");
                }
            }
            else
            {
                if (conditions.Length != 1)
                {
                    throw new ArgumentOutOfRangeException("This isn't an array so exacly one condition has to be specified");
                }
                if (conditions[0].Type == DataPathCondition.ConditionType.Index)
                {
                    throw new ArgumentException("The condition cannot be an index condition since this isn't an array");
                }
            }

            this.conditions = conditions;
        }

        /// <summary>
        /// Sets the continuation of the path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>This datapath</returns>
        public DataPath SetNextPath(DataPath path)
        {
            nextPath = path;
            return this;
        }

        /// <summary>
        /// Returns the continuation of this data path
        /// </summary>
        /// <returns>the continuation of this data path</returns>
        public DataPath GetNextpath()
        {
            return nextPath;
        }

        /// <summary>
        /// Converts the datapath into a string
        /// </summary>
        public override string ToString()
        {
            string fullPath = path;
            if (IsArray)
            {
                for (int i = 0; i < arrayCount; i++)
                {
                    fullPath += "[";
                    if (!(conditions is null) && !(conditions[i] is null) && conditions[i].Type != DataPathCondition.ConditionType.Data)
                    {
                        fullPath += conditions[i];
                    }
                    fullPath += "]";
                }
            }
            else
            {
                if (!(conditions is null) && conditions.Length > 0 && !(conditions[0] is null))
                {
                    fullPath += conditions[0];
                }
            }

            if (!(nextPath is null))
            {
                fullPath += "." + nextPath.ToString();
            }

            return fullPath;
        }

        /// <summary>
        /// Converts the datapath into a string
        /// </summary>
        /// <param name="dataPath">The datapath to convert into a string</param>
        public static implicit operator string(DataPath dataPath)
        {
            return dataPath.ToString();
        }

        /// <summary>
        /// Creates an object containing information about how to find the given data
        /// </summary>
        /// <typeparam name="T">The object to get the data path from</typeparam>
        /// <param name="dataHoldingProperty">The property holding the data to get a path to</param>
        /// <returns>A path to some data</returns>
        public static DataPath GetDataPath<T>(Expression<Func<T, object>> dataHoldingProperty) where T : DataHolderBase
        {
            MemberExpression memberExpression = dataHoldingProperty.Body as MemberExpression;
            UnaryExpression unaryExpression = dataHoldingProperty.Body as UnaryExpression;
            PropertyInfo property = (PropertyInfo)(memberExpression ?? unaryExpression.Operand as MemberExpression).Member;

            DataTagAttribute dataTagInformation = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute));
            if (dataTagInformation is null)
            {
                throw new ArgumentException("The given property is not an NBT data tag holding property");
            }
            string pathName = dataTagInformation.DataTagName ?? property.Name;

            if (property.PropertyType.IsArray)
            {
                int arrays = 0;
                Type checkType = property.PropertyType;
                while (checkType.IsArray)
                {
                    arrays++;
                    checkType = checkType.GetElementType();
                }
                return new DataPath(pathName, arrays);
            }
            else
            {
                return new DataPath(pathName);
            }
        }
    }

    /// <summary>
    /// A condition for if the path should be used or not
    /// </summary>
    public class DataPathCondition
    {
        /// <summary>
        /// Types of conditions
        /// </summary>
        public enum ConditionType
        {
            /// <summary>
            /// If its a condition searching for data
            /// </summary>
            Data,
            /// <summary>
            /// If its a condition searching for data in the form of a string
            /// </summary>
            RawData,
            /// <summary>
            /// If its a condition searching for an array index
            /// </summary>
            Index
        }

        readonly DataHolderBase data;
        readonly string rawData;
        readonly int? index;
        readonly ConditionType type;

        /// <summary>
        /// Creates a new condition to check for data in text form
        /// </summary>
        /// <param name="rawData">the data it should have</param>
        public DataPathCondition(string rawData)
        {
            if (string.IsNullOrWhiteSpace(rawData))
            {
                throw new ArgumentException("RawData may not be null or empty", nameof(rawData));
            }
            this.rawData = rawData;
            type = ConditionType.RawData;
        }

        /// <summary>
        /// Creates a new condition to select an index in an array
        /// </summary>
        /// <param name="index">the index to select. Use negative to start from the top. Use null to select every path in the array</param>
        public DataPathCondition(int? index)
        {
            this.index = index;
            type = ConditionType.Index;
        }

        /// <summary>
        /// Creates a new condition to check for the given data at the path
        /// </summary>
        /// <param name="data">the data to check for</param>
        public DataPathCondition(DataHolderBase data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "Data may not be null");
            }
            this.data = data;
            type = ConditionType.Data;
        }

        /// <summary>
        /// The type of condition this condition is
        /// </summary>
        public ConditionType Type { get => type; }

        /// <summary>
        /// Gets the condition as a string used in Minecraft
        /// </summary>
        /// <returns>a string used in Minecraft</returns>
        public override string ToString()
        {
            return rawData ?? data?.GetDataString() ?? index.ToString();
        }

        /// <summary>
        /// Converts a number into an index condition
        /// </summary>
        /// <param name="index">the index to use</param>
        public static implicit operator DataPathCondition(int? index)
        {
            return new DataPathCondition(index);
        }

        /// <summary>
        /// Converts a string into a raw data condition
        /// </summary>
        /// <param name="rawData">the raw data</param>
        public static implicit operator DataPathCondition(string rawData)
        {
            return new DataPathCondition(rawData);
        }

        /// <summary>
        /// Converts a data holder into a data condition
        /// </summary>
        /// <param name="data">the data</param>
        public static implicit operator DataPathCondition(DataHolderBase data)
        {
            return new DataPathCondition(data);
        }
    }
}
