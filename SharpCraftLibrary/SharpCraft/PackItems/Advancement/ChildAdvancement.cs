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
    /// Class for child advancement files
    /// </summary>
    public class ChildAdvancement : BaseVisibleAdvancement
    {
        private IAdvancement parent;

        /// <summary>
        /// Intializes a new <see cref="ChildAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <param name="announceInChat">True if when the advancement is unlocked it will be announced in chat. False if not</param>
        /// <param name="description">The description on the advancement</param>
        /// <param name="frame">The frame around the icon</param>
        /// <param name="hidden">True if the advancement can't be seen unless it has been unlocked</param>
        /// <param name="icon">The icon on the advancement</param>
        /// <param name="name">The shown advancement name</param>
        /// <param name="showToast">True if when the advancement is unlocked it will display a toast in the top right corner. False if not</param>
        /// <param name="parent">The parent advancement</param>
        public ChildAdvancement(BasePackNamespace packNamespace, string fileName, IAdvancement parent, Requirement[] requirements, Reward reward, JSON[] name, JSON[] description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, requirements, reward, name, description, icon, frame, announceInChat, showToast, hidden, writeSetting)
        {
            Parent = parent;
            EndConstructor();
        }

        /// <summary>
        /// The parent advancement
        /// </summary>
        public IAdvancement Parent { get => parent; set => parent = value ?? throw new ArgumentNullException(nameof(Parent), "Parent may not be null"); }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteStart(stream);
            WriteDisplayStart(stream);
            WriteDisplayEnd(stream);

            stream.Write(",\"parent\":\"" + Parent.GetNamespacedName() + "\"");

            WriteEnd(stream);
        }

        /// <summary>
        /// Creates a new sibling advancement for this advancement
        /// </summary>
        /// <returns>The new sibling</returns>
        public ChildAdvancement NewSibling(string fileName, Requirement[] requirements, Reward reward, JSON[] name, JSON[] description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            return new ChildAdvancement(PackNamespace, fileName, Parent, requirements, reward, name, description, icon, frame, announceInChat, showToast, hidden, writeSetting);
        }
    }
}
