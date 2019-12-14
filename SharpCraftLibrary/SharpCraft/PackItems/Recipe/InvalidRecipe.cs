using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for invalid recipe files. Used for removing default recipes
    /// </summary>
    public class InvalidRecipe : BaseRecipe
    {
        /// <summary>
        /// Intializes a new <see cref="InvalidRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the recipe is in</param>
        /// <param name="fileName">The name of the recipe file to invalidate</param>
        public InvalidRecipe(BasePackNamespace packNamespace, string fileName) : base(packNamespace, fileName, null, WriteSetting.LockedAuto, "invalid")
        {
            EndConstructor();
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write("{\"type\":\"minecraft:invalid\"}");
        }
    }
}
