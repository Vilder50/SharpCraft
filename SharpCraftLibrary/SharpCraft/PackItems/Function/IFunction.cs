﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for functions
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// The file name of the function
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The namespace the function is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for running the function
        /// </summary>
        /// <returns>string for running the function</returns>
        string GetNamespacedName();
    }
}
