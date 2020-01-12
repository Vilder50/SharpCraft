using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.AdvancementObjects;

namespace SharpCraft
{
    /// <summary>
    /// Class for hidden advancements files
    /// </summary>
    public class HiddenAdvancement : BaseAdvancement
    {
        /// <summary>
        /// Intializes a new <see cref="HiddenAdvancement"/>. Inherite from this constructor.
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected HiddenAdvancement(bool _, BasePackNamespace packNamespace, string fileName, Requirement[] requirements, Reward reward, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, requirements, reward, writeSetting)
        {
            
        }

        /// <summary>
        /// Intializes a new <see cref="HiddenAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        public HiddenAdvancement(BasePackNamespace packNamespace, string fileName, Requirement[] requirements, Reward reward, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true,packNamespace, fileName, requirements, reward, writeSetting)
        {
            FinishedConstructing();
        }
    }
}
