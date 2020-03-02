﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace SharpCraft.Data
{
    /// <summary>
    /// Static class containing a method for getting datapaths
    /// </summary>
    public static class DataPathCreator
    {
        private enum PathPart
        {
            Nothing,
            Path,
            Check,
            Filter
        }

        /// <summary>
        /// Gets the datapath for the given location
        /// </summary>
        /// <typeparam name="T">The type of object to get data for</typeparam>
        /// <param name="pathExpression">The datapath to get</param>
        /// <returns>The datapath string</returns>
        public static string GetPath<T>(Expression<Func<T,object?>> pathExpression) where T : DataHolderBase
        {
            string path = "";
            CreatePath(pathExpression.Body, PathPart.Nothing, ref path);
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Failed to get path for the given location");
            }
            return path.Trim('.');
        }

        /// <summary>
        /// Used for adding a compound check to a datapath. Wrap this method around the path to add a check to it.
        /// </summary>
        /// <typeparam name="T">The return type of path</typeparam>
        /// <param name="addTo">The path to the the compund check to</param>
        /// <param name="check">The thing to check for</param>
        /// <returns>The path the compund check was added to</returns>
        public static T AddCompoundCheck<T>(T addTo, SimpleDataHolder check)
        {
            _ = check;
            return addTo;
        }

        /// <summary>
        /// Used for adding a filter to arrays in datapaths. Put this into an indexer to filter it.
        /// </summary>
        /// <param name="filter">The thing to filter for. Leave null to get all items.</param>
        /// <returns>0 (So it can be used in indexers)</returns>
        public static int AddArrayFilter(SimpleDataHolder filter)
        {
            _ = filter;
            return 0;
        }

        private static void CreatePath(Expression expression, PathPart lastPart, ref string path)
        {
            if (expression is BinaryExpression indexExpression)
            {
                if (lastPart == PathPart.Check)
                {
                    throw new ArgumentException("Cannot have index filter and compound check after each other.");
                }
                path = CreateIndexPathPart(indexExpression.Right) + path;
                CreatePath(indexExpression.Left, PathPart.Filter, ref path);
            }
            else if (expression is MemberExpression propertyExpression)
            {
                path = CreatePropertyPathPart(propertyExpression.Member as PropertyInfo, propertyExpression) + path;
                CreatePath(propertyExpression.Expression, PathPart.Path, ref path);
            }
            else if (expression is MethodCallExpression methodExpression)
            {
                if (!(methodExpression.Method.GetCustomAttribute(typeof(PathArrayGetterAttribute)) is null))
                {
                    CreatePath(methodExpression.Object, lastPart, ref path);
                }
                else
                {
                    if (methodExpression.Arguments.Count != 2)
                    {
                        throw new ArgumentException("Methods are not supported.");
                    }
                    path = CreateParameterPathPart(methodExpression.Method, methodExpression.Arguments[1]) + path;
                    if (lastPart == PathPart.Check)
                    {
                        throw new ArgumentException("Cannot have 2 compound checks after each other.");
                    }
                    CreatePath(methodExpression.Arguments[0], PathPart.Check, ref path);
                }
            }
            else if (expression is UnaryExpression asExpression)
            {
                CreatePath(asExpression.Operand, lastPart, ref path);
            }
        }

        private static string CreatePropertyPathPart(PropertyInfo? property, MemberExpression propertyExpression)
        {
            if (property is null)
            {
                throw new ArgumentException("Fields are not supported");
            }
            DataTagAttribute? attribute = (DataTagAttribute?)property.GetCustomAttribute(typeof(DataTagAttribute));
            ArrayPathAttribute? arrayAttribute = (ArrayPathAttribute?)property.GetCustomAttribute(typeof(ArrayPathAttribute));
            CompoundPathAttribute? compoundAttribute = (CompoundPathAttribute?)property.GetCustomAttribute(typeof(CompoundPathAttribute));
            if (!(attribute is null))
            {
                if (attribute.Merge)
                {
                    return "";
                }
                else
                {
                    return "." + (attribute.DataTagName ?? property.Name);
                }
            }
            if (arrayAttribute is null && compoundAttribute is null)
            {
                throw new ArgumentException("Only allows properties with DataTagAttribute or array/compound path attributes");
            }
            //get conversion attribute
            Expression searchAt = propertyExpression.Expression;
            DataConvertionAttribute? converter;
            do
            {
                if (!(searchAt is MemberExpression memberExpression))
                {
                    if (searchAt is BinaryExpression index)
                    {
                        searchAt = index.Left;
                        continue;
                    }
                    if (searchAt is MethodCallExpression method)
                    {
                        searchAt = method.Object;
                        continue;
                    }
                }
                else
                {
                    if (!(memberExpression.Member is PropertyInfo callerProperty))
                    {
                        throw new ArgumentException("Doesn't support fields. Use properties.");
                    }
                    converter = (DataConvertionAttribute?)callerProperty.GetCustomAttribute(typeof(DataConvertionAttribute));
                    if (converter is null)
                    {
                        throw new ArgumentException("Caller property doesn't have a data convertion attribute.");
                    }
                    break;
                }
            }
            while (true);

            if (!(arrayAttribute is null) && (compoundAttribute is null || converter.ConvertType() == DataConvertionAttribute.TagType.Array))
            {
                return "["+arrayAttribute.Index+"]";
            }
            if (!(compoundAttribute is null))
            {
                if (compoundAttribute.ConversionIndex is null)
                {
                    return "." + compoundAttribute.DataTagName!;
                }
                else
                {
                    if (compoundAttribute.ConversionIndex.Value > converter.ConversionParams.Length)
                    {
                        throw new ArgumentException("Couldnt get conversionparams index. index out of array.");
                    }
                    if (converter.ConversionParams[compoundAttribute.ConversionIndex.Value] is string name)
                    {
                        return "." + name;
                    }
                    throw new ArgumentException("Couldnt get conversionparams index. value is not a string.");
                }
            }
            throw new Exception("Can't get here");
        }

        private static MethodInfo? compoundCheckerInfo;
        private static string CreateParameterPathPart(MethodInfo method, Expression check)
        {
            if (compoundCheckerInfo is null)
            {
                compoundCheckerInfo = typeof(DataPathCreator).GetMethod(nameof(AddCompoundCheck))!;
            }
            if (method.GetType() != compoundCheckerInfo.GetType())
            {
                throw new ArgumentException("Methods are not supported (Only "+nameof(AddCompoundCheck)+" is supported)");
            }
            if (Expression.Lambda(check).Compile().DynamicInvoke() is SimpleDataHolder dataHolder)
            {
                return dataHolder.GetDataString();
            }
            else
            {
                return "{}";
            }
        }

        private static MethodInfo? arrayFilterInfo;
        private static string CreateIndexPathPart(Expression filter)
        {
            if (arrayFilterInfo is null)
            {
                arrayFilterInfo = typeof(DataPathCreator).GetMethod(nameof(AddArrayFilter));
            }
            if (filter is MethodCallExpression methodExpression)
            {
                if (methodExpression.Method == arrayFilterInfo)
                {
                    if (Expression.Lambda(methodExpression.Arguments[0]).Compile().DynamicInvoke() is SimpleDataHolder dataHolder)
                    {
                        return "[" + dataHolder.GetDataString() + "]";
                    }
                    else
                    {
                        return "[]";
                    }
                }
            }
            int value = Expression.Lambda(filter).Compile().DynamicInvoke() as int? ?? throw new ArgumentException("Indexer has to return int value");
            return "["+value+"]";
        }
    }
}