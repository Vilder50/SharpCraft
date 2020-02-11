using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for recipes
    /// </summary>
    public interface IRecipe : Data.IConvertableToDataTag
    {
        /// <summary>
        /// The file name of the recipe
        /// </summary>
        string FileId { get; }

        /// <summary>
        /// The namespace the recipe is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for giving the recipe
        /// </summary>
        /// <returns>string for giving the recipe</returns>
        string GetNamespacedName();
    }
}
