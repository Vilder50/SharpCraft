using System;
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
                throw new ArgumentException("The given property is not an NBT holding data tag");
            }

            return new DataPath();
        }
    }
}
