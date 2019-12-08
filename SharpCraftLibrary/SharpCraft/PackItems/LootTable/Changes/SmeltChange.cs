using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Smelts the item
    /// </summary>
    public class SmeltChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="SmeltChange"/>
        /// </summary>
        public SmeltChange() : base("furnace_smelt")
        {
            
        }
    }
}
