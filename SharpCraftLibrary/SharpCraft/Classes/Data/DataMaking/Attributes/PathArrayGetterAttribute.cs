using System;

namespace SharpCraft.Data
{
    /// <summary>
    /// Marks a method as a method to get an indexer for an array
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PathArrayGetterAttribute : Attribute
    {

    }
}
