using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// A <see cref="Group"/> for <see cref="Function"/>s, <see cref="Item"/>, <see cref="Block"/> or <see cref="Selector.EntityType"/>
    /// </summary>
    public class Group
    {
        readonly string Path;
        internal Group(PackNamespace Namespace, string GroupName, string[] GroupItems, bool Replace, int Type)
        {
            Path = Namespace.Name + ":" + GroupName.Replace("\\", "/");
            string PathType = "";
            if (Type == 0) { PathType = "functions"; }
            if (Type == 1) { PathType = "blocks"; }
            if (Type == 2) { PathType = "items"; }
            if (Type == 3) { PathType = "entity_types"; }
            if (GroupName.Contains("\\"))
            {
                Directory.CreateDirectory(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\tags\\" + PathType + "\\" + GroupName.Substring(0, GroupName.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\tags\\" + PathType + "\\");
            }
            StreamWriter GroupWriter = new StreamWriter(new FileStream(Namespace.Datapack.GetDataPath() + Namespace.Name + "\\tags\\" + PathType + "\\" + GroupName + ".json", FileMode.Create)) { AutoFlush = true };
            GroupWriter.Write("{\"replace\": " + Replace.ToString().ToLower() + ",\"values\": [\"" + string.Join("\",\"",GroupItems) + "\"]}");
            GroupWriter.Dispose();
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object with the given string
        /// Used to use a <see cref="Group"/> which doesnt have an object
        /// use <see cref="PackNamespace.NewGroup(string, Function[], bool, Group[])"/> to create a new <see cref="Group"/>
        /// </summary>
        /// <param name="Group">An string path to and <see cref="Group"/></param>
        public Group(string Group)
        {
            Path = Group.ToLower().Replace("\\", "/");
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Group"/>
        /// </summary>
        /// <returns>this <see cref="Group"/>'s name</returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// Converts a <see cref="Group"/> into a <see cref="Block"/> so it can be used
        /// </summary>
        /// <param name="group">the <see cref="Group"/> to convert</param>
        public static implicit operator Block(Group group)
        {
            return new Block(group);
        }

        /// <summary>
        /// Converts a <see cref="Group"/> into a <see cref="Function"/> so it can be used
        /// </summary>
        /// <param name="group">the <see cref="Group"/> to convert</param>
        public static implicit operator Function(Group group)
        {
            return new Function(group);
        }

        /// <summary>
        /// Converts a <see cref="Group"/> into an <see cref="Item"/> so it can be used
        /// </summary>
        /// <param name="group">the <see cref="Group"/> to convert</param>
        public static implicit operator Item(Group group)
        {
            return new Item(group);
        }

        /// <summary>
        /// Converts a <see cref="Group"/> into a <see cref="Selector.EntityType"/> so it can be used
        /// </summary>
        /// <param name="group">the <see cref="Group"/> to convert</param>
        public static implicit operator Selector.EntityType(Group group)
        {
            return new Selector.EntityType(group);
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="blockGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Blocks.Normal blockGroup)
        {
            return new Group("blocks\\" + blockGroup.ToString());
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="blockGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Blocks.Special blockGroup)
        {
            return new Group("blocks\\" + blockGroup.ToString());
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="itemGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Items.Normal itemGroup)
        {
            return new Group("items\\" + itemGroup.ToString());
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="itemGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Items.Special itemGroup)
        {
            return new Group("items\\" + itemGroup.ToString());
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="functionGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Function functionGroup)
        {
            return new Group("functions\\" + functionGroup.ToString());
        }

        /// <summary>
        /// Creates a <see cref="Group"/> object out of the group string so it can be used
        /// </summary>
        /// <param name="fluidGroup">the group string</param>
        public static implicit operator Group(ID.Files.Groups.Fluid fluidGroup)
        {
            return new Group("fluids\\" + fluidGroup.ToString());
        }
    }
}
