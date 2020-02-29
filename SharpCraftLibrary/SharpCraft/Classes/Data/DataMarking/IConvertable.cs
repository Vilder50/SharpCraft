﻿namespace SharpCraft.Data
{
    /// <summary>
    /// Makes a class able to convert itself into a <see cref="DataPartTag"/>
    /// </summary>
    public interface IConvertableToDataTag
    {
        /// <summary>
        /// Converts the object into a <see cref="DataPartTag"/> of the given type
        /// </summary>
        /// <param name="asType">The type of <see cref="DataPartTag"/></param>
        /// <param name="extraConversionData">Extra parameters for specific conversion</param>
        /// <returns>The object as a <see cref="DataPartTag"/></returns>
        DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData);
    }

    /// <summary>
    /// Makes a class able to convert itself into a <see cref="DataPartArray"/>
    /// </summary>
    public interface IConvertableToDataArray
    {
        /// <summary>
        /// Converts the object into a <see cref="DataPartArray"/> of the given type
        /// </summary>
        /// <param name="asType">The type of <see cref="DataPartArray"/></param>
        /// <param name="extraConversionData">Extra parameters for specific conversion</param>
        /// <returns>The object as a <see cref="DataPartArray"/></returns>
        DataPartArray GetAsArray(ID.NBTTagType? asType, object?[]? extraConversionData);
    }

    /// <summary>
    /// Makes a class able to convert itself into a <see cref="DataPartObject"/>
    /// </summary>
    public interface IConvertableToDataObject
    {
        /// <summary>
        /// Converts the object into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">parameters for specific conversion</param>
        /// <returns>The object as a <see cref="DataPartObject"/></returns>
        DataPartObject GetAsDataObject(object?[]? conversionData);
    }
}
