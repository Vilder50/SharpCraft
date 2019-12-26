using SharpCraft.Data;
using System;

namespace SharpCraft
{
    /// <summary>
    /// An object used for teams
    /// </summary>
    public class Team : IConvertableToDataTag
    {
        readonly string Name;
        /// <summary>
        /// Creates a new team object.
        /// Note that this doesnt add the team to the world
        /// </summary>
        /// <param name="TeamName">The team's name</param>
        public Team(string TeamName)
        {
            Name = TeamName;
        }

        /// <summary>
        /// Converts this team into a <see cref="DataPartTag"/>
        /// </summary>
        /// <param name="asType">The type of tag (accepts <see cref="ID.NBTTagType.TagString"/>)</param>
        /// <param name="extraConversionData">Unused</param>
        /// <returns>This team as a <see cref="DataPartTag"/></returns>
        public DataPartTag GetAsTag(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (asType != ID.NBTTagType.TagString)
            {
                throw new InvalidCastException("Cannot convert this team into a none string type");
            }

            return new DataPartTag(Name);
        }

        /// <summary>
        /// Outputs the name of this team
        /// </summary>
        /// <returns>the name of this team</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
