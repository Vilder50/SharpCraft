using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Class for special recipe files
    /// </summary>
    public class SpecialRecipe : BaseRecipe
    {
        /// <summary>
        /// Types of special recipes
        /// </summary>
        public enum SpecialType
        {
            /// <summary>
            /// Armor dying recipes
            /// </summary>
            armordye,
            /// <summary>
            /// Banner duplicate recipe
            /// </summary>
            bannerduplicate,
            /// <summary>
            /// Book cloning recipe
            /// </summary>
            bookcloning,
            /// <summary>
            /// Firework rocket recipes
            /// </summary>
            firework_rocket,
            /// <summary>
            /// Firework star recipes
            /// </summary>
            firework_star,
            /// <summary>
            /// Firework star add fade recipes
            /// </summary>
            firework_star_fade,
            /// <summary>
            /// map cloning recipe
            /// </summary>
            mapcloning,
            /// <summary>
            /// repair item recipe
            /// </summary>
            repairitem,
            /// <summary>
            /// shild pattern recipes
            /// </summary>
            shielddecoration,
            /// <summary>
            /// shulker box coloring recipes
            /// </summary>
            shulkerboxcoloring,
            /// <summary>
            /// Tipped arrow recipes
            /// </summary>
            tippedarrow,
            /// <summary>
            /// suspecious stew recipes
            /// </summary>
            suspeciousstew
        }

        /// <summary>
        /// Intializes a new <see cref="SpecialRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>. Inherite from this constructor.
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of special recipe</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected SpecialRecipe(bool _, BasePackNamespace packNamespace, string? fileName, SpecialType recipeType, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, group, writeSetting, "crafting_special_" + recipeType.ToString())
        {
            
        }

        /// <summary>
        /// Intializes a new <see cref="SpecialRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        /// <param name="group">The name of the recipe group the recipe is in. Leave null for no group.</param>
        /// <param name="recipeType">The type of special recipe</param>
        public SpecialRecipe(BasePackNamespace packNamespace, string? fileName, SpecialType recipeType, string? group = null, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, recipeType, group, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteFileStart(stream);
            WriteFileEnd(stream);
        }
    }
}
