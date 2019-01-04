namespace SharpCraft
{
    /// <summary>
    /// An object used for teams
    /// </summary>
    public class Team
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
        /// Outputs the name of this team
        /// </summary>
        /// <returns>the name of this team</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
