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
    /// An interface for classes which holds data
    /// </summary>
    public abstract class SimpleDataHolder
    {
        /// <summary>
        /// Returns the data inside this object
        /// </summary>
        /// <returns>The data in this object</returns>
        public abstract string GetDataString();

        /// <summary>
        /// Converts a string into a <see cref="DataHoldingString"/> which is a subclass of <see cref="DataHolderBase"/>
        /// </summary>
        /// <param name="data">The data the <see cref="DataHoldingString"/> object should hold</param>
        public static implicit operator SimpleDataHolder(string data)
        {
            return new DataHoldingString(data);
        }
    }

    /// <summary>
    /// The base class for all classes which can hold NBT data tags
    /// </summary>
    public abstract class DataHolderBase : SimpleDataHolder
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
                DataTagAttribute? attribute = (DataTagAttribute?)property.GetCustomAttribute(typeof(DataTagAttribute));
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
            DataHolderBase? clonedObject = null;
            ConstructorInfo[] constructors = GetType().GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    clonedObject = (DataHolderBase)Activator.CreateInstance(GetType(), true)!;
                    break;
                }
                if (parameters.Length == 1 && (!(Nullable.GetUnderlyingType(parameters[0].ParameterType) is null) || parameters[0].ParameterType is object))
                {
                    clonedObject = (DataHolderBase)constructor.Invoke(new object?[] { null });
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
                DataTagAttribute? attribute = (DataTagAttribute?)property.GetCustomAttribute(typeof(DataTagAttribute));
                if (!(attribute is null))
                {
                    object? value = property.GetValue(this);
                    if (typeof(DataHolderBase).IsInstanceOfType(value))
                    {
                        property.SetValue(clonedObject, ((DataHolderBase)value!).Clone());
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
        public override string GetDataString()
        {
            return GetDataTree().GetDataString();
        }

        /// <summary>
        /// Returns a tree structure containing all the data tags for this object
        /// </summary>
        /// <returns>the bottom of the tree</returns>
        public virtual DataPartObject GetDataTree()
        {
            List<PropertyInfo> dataProperties = GetDataProperties().ToList();
            DataPartObject rootObject = new DataPartObject(false);
            foreach (PropertyInfo property in dataProperties)
            {
                object? data = property.GetValue(this);
                if (data is null)
                {
                    continue;
                }

                //get property full path and go through each part and add it to the end
                DataTagAttribute dataTagInformation = (DataTagAttribute)property.GetCustomAttribute(typeof(DataTagAttribute))!;
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
                            //Path doesn't exist yet
                            //Path can be an object, array or tag
                            ID.NBTTagType? forceType = dataTagInformation.UseForcedType ? dataTagInformation.ForceType : (ID.NBTTagType?)null;
                            object?[] conversionData = dataTagInformation.ConversionParams;
                            IConvertableToDataTag? convertAbleTag = data as IConvertableToDataTag;
                            IConvertableToDataArrayBase? convertAbleArray = data as IConvertableToDataArrayBase;
                            IConvertableToDataObject? convertAbleObject = data as IConvertableToDataObject;

                            if ((property.GetValue(this) is DataHolderBase && forceType is null) || forceType == ID.NBTTagType.TagCompound || (!(convertAbleObject is null) && forceType is null))
                            {
                                //if its an object
                                DataHolderBase? dataObject = property.GetValue(this) as DataHolderBase;
                                if (!(convertAbleObject is null) && ((!(dataObject is null) && conversionData.Length != 0) || dataObject is null))
                                {
                                    try
                                    {
                                        if (dataTagInformation.Merge)
                                        {
                                            pathAtLocation.MergeDataPartObject(convertAbleObject.GetAsDataObject(conversionData));
                                        }
                                        else
                                        {
                                            pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleObject.GetAsDataObject(conversionData), dataTagInformation.JsonTag));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new InvalidCastException($"Failed to convert {property.Name} for {GetType().FullName} into a data tag object (See inner exception)", ex);
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
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], dataObject.GetDataTree(), dataTagInformation.JsonTag));
                                    }
                                }
                                else if(!(convertAbleTag is null))
                                {
                                    try
                                    {
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleTag.GetAsTag(forceType, conversionData), dataTagInformation.JsonTag));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new InvalidCastException($"Failed to convert {property.Name} for {GetType().FullName} into a data tag (See inner exception)", ex);
                                    }
                                }
                                else
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], new DataPartTag(data, forceType, dataTagInformation.JsonTag), dataTagInformation.JsonTag));
                                }
                            }
                            else if ((property.PropertyType.IsArray && forceType is null) || ( forceType != null && (int)forceType >= 100) || (!(convertAbleArray is null) && forceType is null))
                            {
                                //if its an array
                                if (!(convertAbleArray is null))
                                {
                                    try
                                    {
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleArray.GetAsArray(forceType, conversionData), dataTagInformation.JsonTag));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new InvalidCastException($"Failed to convert {property.Name} for {GetType().FullName} into a data tag array (See inner exception)", ex);
                                    }
                                }
                                else if (data.GetType().IsArray)
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], new DataPartArray(data, forceType, conversionData, dataTagInformation.JsonTag), dataTagInformation.JsonTag));
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
                                    try
                                    {
                                        pathAtLocation.AddValue(new DataPartPath(pathParts[i], convertAbleTag.GetAsTag(forceType, conversionData), dataTagInformation.JsonTag));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new InvalidCastException($"Failed to convert {property.Name} for {GetType().FullName} into a data tag (See inner exception)", ex);
                                    }
                                }
                                else
                                {
                                    pathAtLocation.AddValue(new DataPartPath(pathParts[i], new DataPartTag(data, forceType, dataTagInformation.JsonTag), dataTagInformation.JsonTag));
                                }
                            }
                        }
                        else
                        {
                            //Path ends in an object
                            //If this property doesn't hold an object then rip
                            if (addToPath.PathValue is DataPartObject addToObject)
                            {
                                if (property.GetValue(this) is DataHolderBase dataObject)
                                {
                                    addToObject.MergeDataPartObject(dataObject.GetDataTree());
                                }
                                else if (data is IConvertableToDataObject objectable)
                                {
                                    object?[] conversionData = dataTagInformation.ConversionParams;
                                    addToObject.MergeDataPartObject(objectable.GetAsDataObject(conversionData));
                                }
                                else
                                {
                                    throw new ArgumentException("The data path " + addToPath.PathName + " is trying to be an object and not an object at the same time.");
                                }
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
                        DataPartPath addToPath = pathAtLocation.GetValues().SingleOrDefault(p => p.PathName == (dataTagInformation.JsonTag ? "\"" + pathParts[i] + "\"" : pathParts[i]));
                        if (addToPath is null)
                        {
                            //If the path doesn't exist yet
                            DataPartObject continueAt = new DataPartObject(dataTagInformation.ForceWriteEmptyCompoundTag);
                            pathAtLocation.AddValue(new DataPartPath(pathParts[i], continueAt, dataTagInformation.JsonTag));
                            pathAtLocation = continueAt;
                        }
                        else
                        {
                            //If the path already exists
                            if (!(addToPath.PathValue is DataPartObject continueAt))
                            {
                                throw new ArgumentException("The data path " + addToPath.PathName + " is trying to be an object and not an object at the same time.");
                            }

                            if (dataTagInformation.ForceWriteEmptyCompoundTag) 
                            {
                                continueAt.IgnoreEmptiness = true;
                            }
                            pathAtLocation = continueAt;
                        }
                    }
                }
            }

            return rootObject;
        }

        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public static Data.DataPathCreator<DataHolderBase> PathCreator => new Data.DataPathCreator<DataHolderBase>();
    }

    /// <summary>
    /// Used for converting from a data holding string to a data holding object
    /// </summary>
    class DataHoldingString : SimpleDataHolder
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
        /// Returns the data inside this object
        /// </summary>
        /// <returns>The data in this object</returns>
        public override string GetDataString()
        {
            return data;
        }
    }
}
