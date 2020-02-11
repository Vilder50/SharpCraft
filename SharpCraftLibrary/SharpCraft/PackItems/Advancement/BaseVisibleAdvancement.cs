using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.AdvancementObjects;
using System.IO;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Base class for visible advancement files
    /// </summary>
    public abstract class BaseVisibleAdvancement : BaseAdvancement
    {
        private Item icon;
        private JsonText description;
        private JsonText name;

        /// <summary>
        /// Intializes a new <see cref="BaseVisibleAdvancement"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the advancement is in</param>
        /// <param name="fileName">The name of the advancement file</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="requirements">The requirements for getting the advancement</param>
        /// <param name="reward">The rewards to get for getting the advancement</param>
        /// <param name="announceInChat">True if when the advancement is unlocked it will be announced in chat. False if not</param>
        /// <param name="description">The description on the advancement</param>
        /// <param name="frame">The frame around the <see cref="Icon"/></param>
        /// <param name="hidden">True if the advancement can't be seen unless it has been unlocked</param>
        /// <param name="icon">The icon on the advancement</param>
        /// <param name="name">The shown advancement name</param>
        /// <param name="showToast">True if when the advancement is unlocked it will display a toast in the top right corner. False if not</param>
        protected BaseVisibleAdvancement(BasePackNamespace packNamespace, string fileName, Requirement[] requirements, Reward reward, JsonText name, JsonText description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, requirements, reward, writeSetting)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Frame = frame;
            AnnounceInChat = announceInChat;
            ShowToast = showToast;
            Hidden = hidden;
        }

        /// <summary>
        /// The shown advancement name
        /// </summary>
        public JsonText Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(Name), "Name may not be null"); }

        /// <summary>
        /// The description on the advancement
        /// </summary>
        public JsonText Description { get => description; set => description = value ?? throw new ArgumentNullException(nameof(Description), "Description may not be null"); }

        /// <summary>
        /// The icon on the advancement
        /// </summary>
        public Item Icon { get => icon; set => icon = value ?? throw new ArgumentNullException(nameof(Icon), "Icon may not be null"); }

        /// <summary>
        /// The frame around the <see cref="Icon"/>
        /// </summary>
        public ID.AdvancementFrame Frame { get; set; }

        /// <summary>
        /// True if when the advancement is unlocked it will be announced in chat. False if not
        /// </summary>
        public bool AnnounceInChat { get; set; }

        /// <summary>
        /// True if when the advancement is unlocked it will display a toast in the top right corner. False if not
        /// </summary>
        public bool ShowToast { get; set; }

        /// <summary>
        /// True if the advancement can't be seen unless it has been unlocked
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Writes the start of the advancement display
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected void WriteDisplayStart(TextWriter stream)
        {
            stream.Write(",\"display\":{");

            //icon
            stream.Write("\"icon\":{\"item\":\""+Icon.ID + "\"");
            string data = Icon.GetItemTagString();
            if (!(data is null))
            {
                stream.Write(",\"nbt\":\""+data.Escape()+"\"");
            }
            stream.Write("}");

            //others
            stream.Write(",\"title\":" + Name.GetJsonString());
            stream.Write(",\"description\":" + Description.GetJsonString());
            stream.Write(",\"frame\":\"" + Frame + "\"");
            stream.Write(",\"show_toast\":" + ShowToast.ToMinecraftBool());
            stream.Write(",\"announce_to_chat\":" + AnnounceInChat.ToMinecraftBool());
            stream.Write(",\"hidden\":" + Hidden.ToMinecraftBool());
        }

        /// <summary>
        /// Writes the end of the advancement display
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected void WriteDisplayEnd(TextWriter stream)
        {
            stream.Write("}");
        }

        /// <summary>
        /// Creates a new child advancement for this advancement
        /// </summary>
        /// <returns>The new child</returns>
        public ChildAdvancement NewChild(string fileName, Requirement[] requirements, Reward reward, JsonText name, JsonText description, Item icon, ID.AdvancementFrame frame = ID.AdvancementFrame.task, bool announceInChat = false, bool showToast = true, bool hidden = false, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            return new ChildAdvancement(PackNamespace, fileName, this, requirements, reward, name, description, icon, frame, announceInChat, showToast, hidden, writeSetting);
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            base.AfterDispose();
            name = null;
            description = null;
            icon = null;
        }
    }
}
