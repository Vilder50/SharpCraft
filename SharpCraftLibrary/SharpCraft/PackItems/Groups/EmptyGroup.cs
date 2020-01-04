using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{ 
    /// <summary>
    /// Used for calling groups outside this program
    /// </summary>
    public class EmptyGroup<TItem> : IGroup<TItem> where TItem : IGroupable
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyGroup{TItem}"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the group is in</param>
        /// <param name="fileName">The name of the group</param>
        public EmptyGroup(BasePackNamespace packNamespace, string fileName)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
        }

        /// <summary>
        /// The name of the group
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the group is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for refering this group
        /// </summary>
        /// <returns>The string used for refering this group</returns>
        public string GetNamespacedName()
        {
            if (typeof(TItem) == typeof(ItemType))
            {
                return PackNamespace.Name + ":items/" + FileName;
            }
            else if (typeof(TItem) == typeof(BlockType))
            {
                return PackNamespace.Name + ":blocks/" + FileName;
            }
            else if (typeof(TItem) == typeof(EntityType))
            {
                return PackNamespace.Name + ":entity_types/" + FileName;
            }
            else if (typeof(TItem) == typeof(LiquidType))
            {
                return PackNamespace.Name + ":fluids/" + FileName;
            }
            else if (typeof(TItem) == typeof(IFunction))
            {
                return PackNamespace.Name + ":functions/" + FileName;
            }
            else
            {
                return PackNamespace.Name + ":" + FileName;
            }
        }
    }
}
