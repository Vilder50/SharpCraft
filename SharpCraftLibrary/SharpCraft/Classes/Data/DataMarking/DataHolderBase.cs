﻿using System;
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
            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    clonedObject = (DataHolderBase)Activator.CreateInstance(GetType(), true);
                    break;
                }
                if (parameters.Length == 1 && !(Nullable.GetUnderlyingType(parameters[0].ParameterType) is null))
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

        /// <summary>
        /// Returns the data from this object as a string used by Minecraft
        /// </summary>
        /// <returns>the data in raw string form</returns>
        public string GetDataString()
        {
            return GetDataTree().GetDataString();
        }

        /// <summary>
        /// Returns a tree structure containing all the data tags for this object
        /// </summary>
        /// <returns>the bottom of the tree</returns>
        public DataPartObject GetDataTree()
        {
            List<PropertyInfo> dataProperties = GetDataProperties().ToList();
            DataPartObject rootObject = new DataPartObject();
            foreach (PropertyInfo property in dataProperties)
            {
                object data = property.GetValue(this);
                if (data is null)
                {
                    continue;
                }

                //get property full path and go through each part and add it to the end
                DataTagAttribute dataTagInformation = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute));
                string fullPath = dataTagInformation.DataTagName ?? property.Name;

                DataPartObject pathAtLocation = rootObject;
                string[] pathParts = fullPath.Split('.');
                for (int i = 0; i < pathParts.Length; i++)
                {
                    if (i == pathParts.Length - 1)
                    {
                        //path ends
                        DataPartPath addToPath = pathAtLocation.GetValues().SingleOrDefault(p => p.PathName == pathParts[i]);
                        if (addToPath is null)
                        {
                            //Path doesn't exists yet
                            //Path can be an object, array or tag
                            ID.NBTTagType? forceType = dataTagInformation.UseForcedType ? dataTagInformation.ForceType : (ID.NBTTagType?)null;
                            object[] conversionData = dataTagInformation.ConversionParams;
                            IConvertableToDataTag convertAbleTag = data as IConvertableToDataTag;
                            IConvertableToDataArray convertAbleArray = data as IConvertableToDataArray;
                            IConvertableToDataObject convertAbleObject = data as IConvertableToDataObject;

                            if ((property.GetValue(this) is DataHolderBase && forceType is null) || forceType == ID.NBTTagType.TagCompound || (!(convertAbleObject is null) && forceType is null))
                            {
                                //if its an object
                                DataHolderBase dataObject = property.GetValue(this) as DataHolderBase;
                                if (!(convertAbleObject is null) && ((!(dataObject is null) && conversionData.Length != 0) || dataObject is null))
                                {
                                    if (dataTagInformation.Merge)
                                    {
                                        pathAtLocation.MergeDataPartObject(convertAbleObject.GetAsDataObject(conversionData));
                                    }
                                    else
                                    {
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleObject.GetAsDataObject(conversionData)));
                                    }
                                }
                                else if (!(dataObject is null))
                                {
                                    if (dataTagInformation.Merge)
                                    {
                                        pathAtLocation.MergeDataPartObject(dataObject.GetDataTree());
                                    }
                                    else
                                    {
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], dataObject.GetDataTree()));
                                    }
                                }
                                else
                                {
                                    throw new ArgumentException("Cannot convert the given type into an data tag object. (" + pathParts[i] + ")");
                                }
                            }
                            else if ((property.PropertyType.IsArray && forceType is null) || ( forceType != null && (int)forceType >= 100) || (!(convertAbleArray is null) && forceType is null))
                            {
                                //if its an array
                                if (!(convertAbleArray is null))
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleArray.GetAsArray(forceType, conversionData)));
                                }
                                else if (data.GetType().IsArray)
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], new DataPartArray(data, forceType, conversionData)));
                                }
                                else
                                {
                                    throw new ArgumentException("Cannot convert the given type into an data tag array. (" + pathParts[i] + ")");
                                }
                            }
                            else
                            {
                                //if its a data tag
                                if (!(convertAbleTag is null))
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleTag.GetAsTag(forceType, conversionData)));
                                }
                                else
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], new DataPartTag(data)));
                                }
                            }
                        }
                        else
                        {
                            //Path ends in an object
                            //If this property doesn't hold an object then rip
                            if (property.GetValue(this) is DataHolderBase dataObject && addToPath.PathValue is DataPartObject addToObject)
                            {
                                addToObject.MergeDataPartObject(dataObject.GetDataTree());
                            }
                            else
                            {
                                throw new ArgumentException("The data path " + addToPath.PathName + " is trying to be an object and not an object at the same time.");
                            }
                        }
                    }
                    else
                    {
                        //path continues
                        DataPartPath addToPath = pathAtLocation.GetValues().SingleOrDefault(p => p.PathName == pathParts[i]);
                        if (addToPath is null)
                        {
                            //If the path doesn't exist yet
                            DataPartObject continueAt = new DataPartObject();
                            pathAtLocation.AddValue(new DataPartPath(pathParts[i], continueAt));
                            pathAtLocation = continueAt;
                        }
                        else
                        {
                            //If the path already exists
                            if (!(addToPath.PathValue is DataPartObject continueAt))
                            {
                                throw new ArgumentException("The data path " + addToPath.PathName + " is trying to be an object and not an object at the same time.");
                            }

                            pathAtLocation = continueAt;
                        }
                    }
                }
            }

            return rootObject;
        }
    }

    /// <summary>
    /// Used for converting from a data holding string to a data holding object
    /// </summary>
    public class DataHoldingString : DataHolderBase
    {
        readonly string data;

        /// <summary>
        /// Intializes a new <see cref="DataHoldingString"/> holding the given data.
        /// </summary>
        /// <param name="data">The data it holds</param>
        public DataHoldingString(string data)
        {
            this.data = data;
        }

        /// <summary>
        /// Checks if this object has any data tags defined
        /// </summary>
        public new bool HasData
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the string inside of this <see cref="DataHoldingString"/>
        /// </summary>
        /// <returns>The data in this object as a string</returns>
        public new string GetDataString()
        {
            return data;
        }

        /// <summary>
        /// Invalid Method
        /// </summary>
        /// <returns>Invalid Method</returns>
        [Obsolete]
        public new DataPartObject GetDataTree()
        {
            throw new InvalidOperationException("This object does not support the given method");
        }

        /// <summary>
        /// Invalid Method
        /// </summary>
        /// <returns>Invalid Method</returns>
        [Obsolete]
        public new void ClearData()
        {
            throw new InvalidOperationException("This object does not support the given method");
        }

        /// <summary>
        /// Invalid Method
        /// </summary>
        /// <returns>Invalid Method</returns>
        [Obsolete]
        public new IEnumerable<PropertyInfo> GetDataProperties()
        {
            throw new InvalidOperationException("This object does not support the given method");
        }

        /// <summary>
        /// Clones all properties with a <see cref="DataTagAttribute"/> from this object onto a newly created object
        /// </summary>
        /// <returns>the cloned object</returns>
        public new DataHolderBase Clone()
        {
            return new DataHoldingString(data);
        }

        /// <summary>
        /// Converts a string into a <see cref="DataHoldingString"/> which is a subclass of <see cref="DataHolderBase"/>
        /// </summary>
        /// <param name="data">The data the <see cref="DataHoldingString"/> object should hold</param>
        public static implicit operator DataHoldingString(string data)
        {
            return new DataHoldingString(data);
        }
    }
}
