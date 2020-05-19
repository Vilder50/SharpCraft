using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Base class for advancement files
    /// </summary>
    public abstract class BaseAdvancement : BaseFile, IAdvancement
    {
        private Requirement[] requirements = null!;

        /// <summary>
        /// Intializes a new <see cref="BaseAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        protected BaseAdvancement(BasePackNamespace packNamespace, string? fileName, Requirement[] requirements, Reward? reward, WriteSetting writeSetting) : base(packNamespace, fileName, writeSetting, "advancement")
        {
            Requirements = requirements;
            Reward = reward;
        }

        /// <summary>
        /// The requirements for getting the advancement
        /// </summary>
        public Requirement[] Requirements { get => requirements; set => requirements = value ?? throw new ArgumentNullException(nameof(Requirements), "Requirements may not be null"); }

        /// <summary>
        /// The rewards to get for getting the advancement
        /// </summary>
        public Reward? Reward { get; set; }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected override TextWriter GetStream()
        {
            CreateDirectory("advancements");
            return PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "advancements/" + WritePath + ".json");
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteStart(stream);
            WriteEnd(stream);
        }

        /// <summary>
        /// Writes the beginning of the advancement
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected void WriteStart(TextWriter stream)
        {
            stream.Write("{");

            //requirements and rewards
            stream.Write("\"requirements\":[" + string.Join(",", Requirements.Select(r => r.GetRequirementString(null))) + "]");

            //get and write list of triggers
            List<BaseTrigger> triggers = new List<BaseTrigger>();
            foreach(Requirement requirement in Requirements)
            {
                triggers.AddRange(requirement.GetChildTriggers());
            }
            triggers.Distinct();
            Data.DataPartObject criteria = new Data.DataPartObject();
            foreach(BaseTrigger trigger in triggers)
            {
                criteria.MergeDataPartObject(trigger.GetDataTree());
            }
            stream.Write(",\"criteria\":" + criteria.GetDataString());

            if (!(Reward is null))
            {
                stream.Write(",\"rewards\":" + Reward.GetDataString());
            }
        }

        /// <summary>
        /// Writes the end of the advancement
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected void WriteEnd(TextWriter stream)
        {
            stream.Write("}");
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            requirements = null!;
            Reward = null!;
        }
    }
}
