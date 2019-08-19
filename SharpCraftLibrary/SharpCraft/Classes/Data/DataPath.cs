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
        private string[] ifData;
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
                if (!value.All(c => char.IsLetter(c) || c == '_'))
                {
                    throw new ArgumentNullException("Paths can only contain letters and _");
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
        /// continues the datapath from the objects with the given data in the array
        /// </summary>
        /// <param name="indexSelectors">the data the objects should have</param>
        /// <returns>this datapath</returns>
        public DataPath this[params string[] indexSelectors]
        {
            get
            {
                return this;
            }
            set
            {
                if (!IsArray)
                {
                    throw new InvalidOperationException("Cannot get an index from this data path since the path isn't an array");
                }
                if (indexSelectors.Length != arrayCount)
                {
                    throw new ArgumentException("Not enough selectors. (Need " + indexSelectors.Length + " selectors)");
                }

                ifData = indexSelectors;
            }
        }

        /// <summary>
        /// Continues the path if the object at the path has the following data
        /// </summary>
        /// <param name="data">The data the object should have</param>
        public void IfData(string data)
        {
            if (IsArray)
            {
                throw new InvalidOperationException("Cannot test for data inside of an array");
            }
            ifData = new string[] { data };
        }

        /// <summary>
        /// Sets the continuation of the path
        /// </summary>
        /// <param name="path"></param>
        public void SetNextPath(DataPath path)
        {
            nextPath = path;
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
                    path += "[";
                    path += ifData[i];
                    path += "]";
                }
            }
            else
            {
                if (!(ifData is null) && ifData.Length > 0 && !(ifData[0] is null))
                {
                    fullPath += ifData[0];
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
            PropertyInfo property = (PropertyInfo)((MemberExpression)dataHoldingProperty.Body).Member;

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
}
