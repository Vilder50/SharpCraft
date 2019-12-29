﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Storage for holding NBT
    /// </summary>
    public class Storage
    {
        private BasePackNamespace packNamespace;
        private string name;

        /// <summary>
        /// Intializes a new <see cref="Storage"/>
        /// </summary>
        /// <param name="packNamespace">The namespace for the storage</param>
        /// <param name="name">The name of the storage</param>
        public Storage(BasePackNamespace packNamespace, string name)
        {
            PackNamespace = packNamespace;
            Name = name;
        }

        /// <summary>
        /// The namespace for the storage
        /// </summary>
        public BasePackNamespace PackNamespace { get => packNamespace; set => packNamespace = value ?? throw new ArgumentNullException(nameof(PackNamespace), "PackNamespace may not be null"); }

        /// <summary>
        /// The name of the storage
        /// </summary>
        public string Name 
        { 
            get => name; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Name), "Name may not be null or whitespace");
                }
                name = value;
            }
        }

        /// <summary>
        /// Returns the namespaced name of this storage
        /// </summary>
        /// <returns>The namespaced name of this storage</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + Name.Replace("\\", "/");
        }

        /// <summary>
        /// Returns the namespaced name of this storage
        /// </summary>
        /// <returns>The namespaced name of this storage</returns>
        public override string ToString()
        {
            return GetNamespacedName();
        }
    }
}