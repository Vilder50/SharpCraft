using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Interface for classes which can be at the end of a <see cref="DataPartPath"/>
    /// </summary>
    public interface IDataPartPathEnding
    {
        /// <summary>
        /// If the tree part's tags are all empty
        /// </summary>
        /// <returns>True if its empty</returns>
        bool IsEmpty();

        /// <summary>
        /// Returns the data inside this object
        /// </summary>
        /// <returns>The data in this object</returns>
        string GetDataString();
    }

    /// <summary>
    /// A part of a data tag tree for holding data objects
    /// </summary>
    public class DataPartObject : SimpleDataHolder, IDataPartPathEnding
    {
        private readonly List<DataPartPath> values;

        /// <summary>
        /// Intializes a new <see cref="DataPartObject"/>
        /// </summary>
        public DataPartObject()
        {
            values = new List<DataPartPath>();
        }

        /// <summary>
        /// Adds a value to this object
        /// </summary>
        public void AddValue(DataPartPath path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), "Path may not be null");
            }
            if (values.Any(d => d.PathName == path.PathName))
            {
                throw new ArgumentException("A path with the name " + path.PathName + " has already been added.", nameof(path));
            }

            values.Add(path);
        }

        /// <summary>
        /// Returns a list of all the values in this object
        /// </summary>
        /// <returns>All the values in this object</returns>
        public List<DataPartPath> GetValues()
        {
            return values.Where(d => true).ToList();
        }

        /// <summary>
        /// Merges this <see cref="DataPartObject"/> together with another <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="mergeWith">The other <see cref="DataPartObject"/> to merge with</param>
        public void MergeDataPartObject(DataPartObject mergeWith)
        {
            if (mergeWith is null)
            {
                throw new ArgumentNullException(nameof(mergeWith), "MergeWith may not be null.");
            }

            foreach(DataPartPath path in mergeWith.values)
            {
                DataPartPath existingPath = values.SingleOrDefault(d => d.PathName == path.PathName);
                if (existingPath is null)
                {
                    AddValue(path);
                }
                else
                {
                    if (existingPath.PathValue is DataPartObject addToObject && path.PathValue is DataPartObject addObject)
                    {
                        addToObject.MergeDataPartObject(addObject);
                    }
                    else
                    {
                        throw new ArgumentException("Failed to merge the objects since one or more paths are of conflicting types.");
                    }
                }
            }
        }

        /// <summary>
        /// Returns this data as a string Minecraft can use
        /// </summary>
        /// <returns>This object as a string</returns>
        public override string GetDataString()
        {
            if (IsEmpty())
            {
                return "{}";
            }

            //Sort path names
            List<DataPartPath> sortedPaths = new List<DataPartPath>();
            foreach(DataPartPath path in values)
            {
                if (!path.PathValue.IsEmpty())
                {
                    bool sorted = false;
                    for (int i = 0; i < sortedPaths.Count; i++)
                    {
                        if (string.Compare(path.PathName, sortedPaths[i].PathName) == -1)
                        {
                            sortedPaths.Insert(i, path);
                            sorted = true;
                            break;
                        }
                    }

                    if (!sorted)
                    {
                        sortedPaths.Add(path);
                    }
                }
            }

            //create return string
            string returnString = "{";
            bool addedOne = false;
            foreach(DataPartPath path in sortedPaths)
            {
                if (!addedOne)
                {
                    addedOne = true;
                }
                else
                {
                    returnString += ",";
                }
                returnString += path.PathName + ":" + path.PathValue.GetDataString();
            }

            return returnString + "}";
        }

        /// <summary>
        /// If the tree part's tags are all empty
        /// </summary>
        /// <returns>True if its empty</returns>
        public bool IsEmpty()
        {
            return values.All(i => i.PathValue.IsEmpty());
        }
    }

    /// <summary>
    /// A part of a data tag tree for holding data arrays
    /// </summary>
    public class DataPartArray : SimpleDataHolder, IDataPartPathEnding
    {
        private ID.NBTTagType? arrayType;
        private readonly List<IDataPartPathEnding> items;
        private readonly bool isEmpty;

        /// <summary>
        /// Intializes a new <see cref="DataPartArray"/> with the given value
        /// </summary>
        /// <param name="data">The thing which is inside the array</param>
        /// <param name="conversionParams">extra parameters used for converting the thing in the array correctly</param>
        /// <param name="forceType">a type used for converting the thing in the array correctly</param>
        /// <param name="isJson">If the datatags in the array should be in json format</param>
        public DataPartArray(object data, ID.NBTTagType? forceType, object[] conversionParams, bool isJson = false)
        {
            IsJson = isJson;
            items = new List<IDataPartPathEnding>();

            if (data is null)
            {
                isEmpty = true;
                return;
            }

            if (!data.GetType().IsArray)
            {
                throw new ArgumentException("Data is not an array and cannot be made into an array", nameof(data));
            }

            Array array = ((Array)data);
            
            for (int i = 0; i < array.Length; i++)
            {
                object value = array.GetValue(i);
                if (value is null)
                {
                    continue;
                }
                if (value.GetType().IsArray)
                {
                    AddItem(new DataPartArray(value, forceType, conversionParams, IsJson));
                }
                else
                {
                    if (value is DataHolderBase dataHolder)
                    {
                        AddItem(dataHolder.GetDataTree());
                    }
                    else if (value is IConvertableToDataObject canBeObject && (forceType is null || forceType == ID.NBTTagType.TagCompoundArray))
                    {
                        try
                        {
                            AddItem(canBeObject.GetAsDataObject(conversionParams));
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidCastException($"Failed to convert object at index {i} into a data tag object (See inner exception)", ex);
                        }
                    }
                    else if (value is IConvertableToDataArray canBeArray && (forceType is null || forceType >= ID.NBTTagType.TagArrayArray))
                    {
                        try
                        {
                            AddItem(canBeArray.GetAsArray(forceType, conversionParams));
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidCastException($"Failed to convert object at index {i} into a data tag array (See inner exception)", ex);
                        }
                    }
                    else if (value is IConvertableToDataTag canBeTag)
                    {
                        try
                        { 
                            if (forceType is null)
                            {
                                AddItem(canBeTag.GetAsTag(null, conversionParams));
                            }
                            else
                            {
                                AddItem(canBeTag.GetAsTag((ID.NBTTagType)(int)forceType - 101, conversionParams));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidCastException($"Failed to convert object at index {i} into a data tag (See inner exception)", ex);
                        }
                    }
                    else if (value is DataPartTag dataTag)
                    {
                        AddItem(dataTag);
                    }
                    else if (value is DataPartObject dataObject)
                    {
                        AddItem(dataObject);
                    }
                    else
                    {
                        if (forceType is null)
                        {
                            AddItem(new DataPartTag(value, null, IsJson));
                        }
                        else
                        {
                            AddItem(new DataPartTag(value, (ID.NBTTagType)(int)forceType - 101, IsJson));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If the datatags in the array should be in json format
        /// </summary>
        public bool IsJson { get; set; }

        /// <summary>
        /// Adds an item to this array
        /// </summary>
        /// <param name="item">The item to add</param>
        public void AddItem(IDataPartPathEnding item)
        {
            if (item is null)
            {
                return;
            }
            else if (item is DataPartArray)
            {
                ArrayType = ID.NBTTagType.TagArrayArray;
            }
            else if (item is DataPartObject)
            {
                ArrayType = ID.NBTTagType.TagCompoundArray;
            }
            else if (item is DataPartTag tag)
            {
                if (tag.TagType is null)
                {
                    ArrayType = null;
                }
                else
                {
                    ArrayType = (ID.NBTTagType)((int)tag.TagType + 101);
                }
            }

            items.Add(item);
        }

        /// <summary>
        /// Returns all items stored in this array
        /// </summary>
        /// <returns>all items stored in this array</returns>
        public List<IDataPartPathEnding> GetItems()
        {
            return items.Where(i => true).ToList();
        }

        /// <summary>
        /// The type of array this is
        /// </summary>
        public ID.NBTTagType? ArrayType
        {
            get
            {
                return arrayType ?? throw new InvalidOperationException("Cannot get array type since it hasn't gotten any type");
            }
            private set
            {
                if (!(arrayType is null) && arrayType != value)
                {
                    throw new ArgumentException("An array cannot store different types of objects.");
                }
                arrayType = value;
            }
        }

        /// <summary>
        /// Returns this data as a string Minecraft can use
        /// </summary>
        /// <returns>This object as a string</returns>
        public override string GetDataString()
        {
            if (items.Count == 0)
            {
                return "[]";
            }

            string returnString = "[";
            if (ArrayType == ID.NBTTagType.TagIntArray && !IsJson)
            {
                returnString += "I;";
            }
            bool addedOne = false;
            for (int i = 0; i < items.Count; i++)
            {
                IDataPartPathEnding item = items[i];
                if (!item.IsEmpty())
                {
                    if (!addedOne)
                    {
                        addedOne = true;
                    }
                    else
                    {
                        returnString += ",";
                    }
                    returnString += item.GetDataString();
                }
                else if (ArrayType == ID.NBTTagType.TagCompoundArray)
                {
                    if (!addedOne)
                    {
                        addedOne = true;
                    }
                    else
                    {
                        returnString += ",";
                    }
                    returnString += "{}";
                }
            }

            return returnString += "]";
        }

        /// <summary>
        /// If the tree part's tags are all empty
        /// </summary>
        /// <returns>True if its empty</returns>
        public bool IsEmpty()
        {
            return isEmpty;
        }
    }

    /// <summary>
    /// A part of a data tag tree for ending branches
    /// </summary>
    public class DataPartTag : IDataPartPathEnding
    {
        private object value;

        /// <summary>
        /// Intializes a new <see cref="DataPartTag"/> with the given value
        /// </summary>
        /// <param name="value">The thing this <see cref="DataPartTag"/> should be</param>
        /// <param name="forceType">The type the object should be forced into. Used for enums and marking strings as objects</param>
        /// <param name="isJson">If the tag is a json value</param>
        public DataPartTag(object value, ID.NBTTagType? forceType = null, bool isJson = false)
        {
            TagType = forceType;
            Value = value;
            IsJson = isJson;
        }

        /// <summary>
        /// The thing this <see cref="DataPartTag"/> is holding
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value is null)
                {
                    TagType = null;
                }
                else if (value.GetType().IsEnum)
                {
                    if (TagType is null)
                    {
                        throw new ArgumentNullException("The enum cannot be converted into a null type.", nameof(TagType));
                    }
                    if (!(TagType == ID.NBTTagType.TagByte || TagType == ID.NBTTagType.TagInt || TagType == ID.NBTTagType.TagShort || TagType == ID.NBTTagType.TagLong || TagType == ID.NBTTagType.TagString || TagType == ID.NBTTagType.TagNamespacedString))
                    {
                        throw new ArgumentException("The enum cannot be converted into the given type.", nameof(TagType));
                    }
                }
                else if (value is int)
                {
                    TagType = ID.NBTTagType.TagInt;
                }
                else if (value is byte || value is sbyte || value is bool)
                {
                    TagType = ID.NBTTagType.TagByte;
                }
                else if (value is short)
                {
                    TagType = ID.NBTTagType.TagShort;
                }
                else if (value is long)
                {
                    TagType = ID.NBTTagType.TagLong;
                }
                else if (value is float)
                {
                    TagType = ID.NBTTagType.TagFloat;
                }
                else if (value is double)
                {
                    TagType = ID.NBTTagType.TagDouble;
                }
                else if (value is string)
                {
                    if (TagType != ID.NBTTagType.TagCompound && TagType != ID.NBTTagType.TagNamespacedString)
                    {
                        TagType = ID.NBTTagType.TagString;
                    }
                }
                else if (value is SimpleDataHolder data && TagType == ID.NBTTagType.TagString)
                {
                    this.value = data.GetDataString();
                    return;
                }
                else if (!(value.GetType().IsEnum))
                {
                    throw new ArgumentException("The object cannot be saved in this tag.", nameof(Value));
                }
                this.value = value;
            }
        }

        /// <summary>
        /// The type of object saved in this tag
        /// </summary>
        public ID.NBTTagType? TagType { get; private set; }

        /// <summary>
        /// If the tag is a json value
        /// </summary>
        public bool IsJson { get; set; }

        /// <summary>
        /// Returns this data as a string Minecraft can use
        /// </summary>
        /// <returns>This object as a string</returns>
        public string GetDataString()
        {
            if (Value is null)
            {
                return string.Empty;
            }
            else if (Value is int)
            {
                return ((int)Value).ToString();
            }
            else if (Value is byte realValue)
            {
                if (realValue >= 128)
                {
                    throw new ArgumentOutOfRangeException("Cannot get the data string since the byte is out of out of the -128 to 127 range.");
                }
                return realValue + GetTypeEnding(ID.NBTTagType.TagByte);
            }
            else if (Value is sbyte)
            {
                return (sbyte)Value + GetTypeEnding(ID.NBTTagType.TagByte);
            }
            else if (Value is bool)
            {
                if (IsJson)
                {
                    return ((bool)Value).ToMinecraftBool();
                }
                else
                {
                    return (((bool)Value) ? "1" : "0") + GetTypeEnding(ID.NBTTagType.TagByte);
                }
            }
            else if (Value is short)
            {
                return (short)Value + GetTypeEnding(ID.NBTTagType.TagShort);
            }
            else if (Value is long)
            {
                return (long)Value + GetTypeEnding(ID.NBTTagType.TagLong);
            }
            else if (Value is float)
            {
                return ((float)Value).ToMinecraftFloat() + GetTypeEnding(ID.NBTTagType.TagFloat);
            }
            else if (Value is double)
            {
                return ((double)Value).ToMinecraftDouble() + GetTypeEnding(ID.NBTTagType.TagDouble);
            }
            else if (Value is string)
            {
                if (TagType == ID.NBTTagType.TagCompound)
                {
                    return (string)Value;
                }
                else if (TagType == ID.NBTTagType.TagNamespacedString)
                {
                    return "\"minecraft:" + ((string)Value).Escape() + "\"";
                }
                else
                {
                    return "\"" + ((string)Value).Escape() + "\"";
                }
            }
            else if (Value.GetType().IsEnum)
            {
                int value = ((int)Value);
                switch (TagType)
                {
                    case ID.NBTTagType.TagInt:
                        return value.ToString();
                    case ID.NBTTagType.TagShort:
                        if (value > short.MaxValue || value < short.MinValue)
                        {
                            throw new InvalidCastException("Cannot convert the given Enum into a short.");
                        }
                        return value + GetTypeEnding(ID.NBTTagType.TagShort);
                    case ID.NBTTagType.TagLong:
                        return value + GetTypeEnding(ID.NBTTagType.TagLong);
                    case ID.NBTTagType.TagByte:
                        if (value > byte.MaxValue || value < byte.MinValue)
                        {
                            throw new InvalidCastException("Cannot convert the given Enum into a byte.");
                        }
                        return value + GetTypeEnding(ID.NBTTagType.TagByte);
                    case ID.NBTTagType.TagString:
                        if (IsJson)
                        {
                            return "\"" + Value.ToString().ToLower() + "\"";
                        }
                        else
                        {
                            return "\"" + Value + "\"";
                        }
                    case ID.NBTTagType.TagNamespacedString:
                        if (IsJson)
                        {
                            return "\"minecraft:" + Value.ToString().ToLower() + "\"";
                        }
                        else
                        {
                            return "\"minecraft:" + Value + "\"";
                        }
                }
            }

            throw new ArgumentException("The object saved in this tag cannot be converted to a string", nameof(Value));
        }

        /// <summary>
        /// If the tree part's tags are all empty
        /// </summary>
        /// <returns>True if its empty</returns>
        public bool IsEmpty()
        {
            return Value is null;
        }

        /// <summary>
        /// Gets the letter there is at the end of the given datatype
        /// </summary>
        /// <param name="type">The type to get the ending letter for</param>
        /// <returns>The ending letter</returns>
        private string GetTypeEnding(ID.NBTTagType type)
        {
            if (IsJson)
            {
                return "";
            }
            else
            {
                return type switch
                {
                    ID.NBTTagType.TagByte => "b",
                    ID.NBTTagType.TagShort => "s",
                    ID.NBTTagType.TagDouble => "d",
                    ID.NBTTagType.TagFloat => "f",
                    ID.NBTTagType.TagLong => "L",
                    _ => throw new ArgumentException("The given type doesn't have a special ending letter"),
                };
            }
        }
    }

    /// <summary>
    /// A part of a data tag tree for given branches names
    /// </summary>
    public class DataPartPath
    {
        private string pathName;

        /// <summary>
        /// Intializes a new <see cref="DataPartPath"/>
        /// </summary>
        /// <param name="pathName">The name of the path of the value</param>
        /// <param name="pathValue">The value with the path</param>
        /// <param name="isJson">If the path is a json path</param>
        public DataPartPath(string pathName, IDataPartPathEnding pathValue, bool isJson = false)
        {
            PathName = pathName;
            PathValue = pathValue;
            IsJson = isJson;
        }

        /// <summary>
        /// The name of the path of the value
        /// </summary>
        public string PathName 
        { 
            get
            {
                if (IsJson)
                {
                    return "\"" + pathName.Escape() + "\"";
                }
                else
                {
                    return pathName;
                }
            }
            set
            {
                pathName = value ?? throw new ArgumentNullException("PathName may not be null");
            }
        }

        /// <summary>
        /// If the path is a json path
        /// </summary>
        public bool IsJson { get; set; }

        /// <summary>
        /// The value with the path
        /// </summary>
        public IDataPartPathEnding PathValue { get; set; }
    }
}
