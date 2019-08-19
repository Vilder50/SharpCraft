using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpCraft;
using System.Reflection;

namespace SharpCraft.Data
{
    /// <summary>
    /// The base class for all classes which can hold NBT data tags
    /// </summary>
    public abstract class DataHolderBase
    {
        /// <summary>
        /// Gets a list of all the data tag properties for this object
        /// </summary>
        /// <returns>A list of all the data tag properties for this object</returns>
        public IEnumerable<PropertyInfo> GetDataProperties()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                DataTagAttribute attribute = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute));
                if (attribute != null)
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// Checks if this object has any data tags defined
        /// </summary>
        public bool HasData
        {
            get
            {
                return GetDataProperties().Any(p => !(p.GetValue(this) is null));
            }
        }

        /// <summary>
        /// Clears the object's data tags
        /// </summary>
        public void ClearData()
        {
            IEnumerable<PropertyInfo> properties = GetDataProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, null);
            }
        }

        /// <summary>
        /// Clones all properties with a <see cref="DataTagAttribute"/> from this object onto a newly created object
        /// </summary>
        /// <returns>the cloned object</returns>
        public DataHolderBase Clone()
        {
            DataHolderBase clonedObject = null;
            ConstructorInfo[] constructors = GetType().GetConstructors();
            Type nullAbleType = typeof(Nullable);
            foreach(ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    clonedObject = (DataHolderBase)Activator.CreateInstance(GetType(), true);
                    break;
                }
                if(parameters.Length == 1 && !(Nullable.GetUnderlyingType(parameters[0].ParameterType) is null))
                {
                    clonedObject = (DataHolderBase)constructor.Invoke(new object[] { null });
                    break;
                }
            }

            if (clonedObject is null)
            {
                throw new NotSupportedException("The object to clone doesn't have any parameterless constructors.");
            }

            IEnumerable<PropertyInfo> properties = GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                DataTagAttribute attribute = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute));
                if (!(attribute is null))
                {
                    object value = property.GetValue(this);
                    if (typeof(DataHolderBase).IsInstanceOfType(value))
                    {
                        property.SetValue(clonedObject, ((DataHolderBase)value).Clone());
                    }
                    else
                    {
                        property.SetValue(clonedObject, value);
                    }
                }
            }

            return clonedObject;
        }
    }
}
