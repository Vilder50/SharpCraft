using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.AdvancementObjects;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Class for invalid advancements files
    /// </summary>
    public class InvalidAdvancement : BaseAdvancement
    {
        /// <summary>
        /// Intializes a new <see cref="InvalidAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        public InvalidAdvancement(BasePackNamespace packNamespace, string fileName) : base(packNamespace, fileName, new Requirement[0], null, WriteSetting.LockedAuto)
        {
            EndConstructor();
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            stream.Write("{\"invalid\":true}");
        }
    }
}
