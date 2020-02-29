using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.AdvancementObjects;

namespace SharpCraft
{
    /// <summary>
    /// Class for parent advancement files
    /// </summary>
    public class ParentAdvancement : BaseVisibleAdvancement
    {
        private string background = null!;

        /// <summary>
        /// Intializes a new <see cref="ParentAdvancement"/>. Inherite from this constructor.
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
        /// <param name="background">The background in the advancement gui. Example: minecraft:textures/gui/advancements/backgrounds/end.png.</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        public ParentAdvancement(bool _, BasePackNamespace packNamespace, string? fileName, Requirement[] requirements, Reward? reward, BaseJsonText name, BaseJsonText description, Item icon, string background, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, requirements, reward, name, description, icon, frame, announceInChat, showToast, hidden, writeSetting)
        {
            Background = background;
        }

        /// <summary>
        /// Intializes a new <see cref="ParentAdvancement"/>
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
        /// <param name="background">The background in the advancement gui. Example: minecraft:textures/gui/advancements/backgrounds/end.png.</param>
        public ParentAdvancement(BasePackNamespace packNamespace, string? fileName, Requirement[] requirements, Reward? reward, BaseJsonText name, BaseJsonText description, Item icon, string background, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto) 
            : this(true, packNamespace, fileName, requirements, reward, name, description, icon, background, frame, announceInChat, showToast, hidden, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The background in the advancement gui. Example: minecraft:textures/gui/advancements/backgrounds/end.png.
        /// </summary>
        public string Background { get => background; set => background = value ?? throw new ArgumentNullException(nameof(Background), "Background may not be null"); }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteStart(stream);
            WriteDisplayStart(stream);

            stream.Write(",\"background\":\"" + Background + "\"");

            WriteDisplayEnd(stream);
            WriteEnd(stream);
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            base.AfterDispose();
            background = null!;
        }
    }
}
