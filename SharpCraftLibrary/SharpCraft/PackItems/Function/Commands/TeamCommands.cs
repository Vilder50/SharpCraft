using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which adds a team to the world
    /// </summary>
    public class TeamAddCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamAddCommand"/>
        /// </summary>
        /// <param name="team">The team to add</param>
        /// <param name="displayName">The team's displayed name</param>
        public TeamAddCommand(Team team, JsonText? displayName)
        {
            Team = team;
            DisplayName = displayName;
        }

        /// <summary>
        /// The team to add
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The team's displayed name
        /// </summary>
        public JsonText? DisplayName { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team add [Team] ([DisplayName])</returns>
        public override string GetCommandString()
        {
            if (DisplayName is null)
            {
                return $"team add {Team.Name}";
            }
            else
            {
                return $"team add {Team.Name} {DisplayName.GetJsonString()}";
            }
        }
    }

    /// <summary>
    /// Command which empties a team
    /// </summary>
    public class TeamEmptyCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamEmptyCommand"/>
        /// </summary>
        /// <param name="team">The team to empty</param>
        public TeamEmptyCommand(Team team)
        {
            Team = team;
        }

        /// <summary>
        /// The team to empty
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team empty [Team]</returns>
        public override string GetCommandString()
        {
            return $"team empty {Team.Name}";
        }
    }

    /// <summary>
    /// Command which makes entities join a team
    /// </summary>
    public class TeamJoinCommand : BaseCommand
    {
        private Team team = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamJoinCommand"/>
        /// </summary>
        /// <param name="team">The team to join</param>
        /// <param name="selector">Selector selecting the entities to put into the team</param>
        public TeamJoinCommand(Team team, BaseSelector selector)
        {
            Team = team;
            Selector = selector;
        }

        /// <summary>
        /// The team to join
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// Selector selecting the entities to put into the team
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team join [Team] [Selector]</returns>
        public override string GetCommandString()
        {
            return $"team join {Team.Name} {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which makes entities leave their team
    /// </summary>
    public class TeamLeaveCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamLeaveCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting entities to make leave their team</param>
        public TeamLeaveCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector for selecting entities to make leave their team
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team leave [Selector]</returns>
        public override string GetCommandString()
        {
            return $"team leave {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which returns a list of all teams
    /// </summary>
    public class TeamListCommand : BaseCommand
    {
        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team list</returns>
        public override string GetCommandString()
        {
            return $"team list";
        }
    }

    /// <summary>
    /// Command which returns a list of players in a team
    /// </summary>
    public class TeamPlayerListCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamPlayerListCommand"/>
        /// </summary>
        /// <param name="team">The team to get a list of players for</param>
        public TeamPlayerListCommand(Team team)
        {
            Team = team;
        }

        /// <summary>
        /// The team to get a list of players for
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team list [Team]</returns>
        public override string GetCommandString()
        {
            return $"team list {Team.Name}";
        }
    }

    /// <summary>
    /// Command which removes a team from the world
    /// </summary>
    public class TeamRemoveCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamRemoveCommand"/>
        /// </summary>
        /// <param name="team">The team to remove</param>
        public TeamRemoveCommand(Team team)
        {
            Team = team;
        }

        /// <summary>
        /// The team to remove
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team remove [Team]</returns>
        public override string GetCommandString()
        {
            return $"team remove {Team.Name}";
        }
    }

    /// <summary>
    /// Command which changes how a team is displayed
    /// </summary>
    public class TeamModifyDisplayCommand : BaseCommand
    {
        private Team team = null!;
        private JsonText value = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyDisplayCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="displaySlot">The display to change</param>
        /// <param name="value">The value to change it to</param>
        public TeamModifyDisplayCommand(Team team, ID.TeamDisplayName displaySlot, JsonText value)
        {
            Team = team;
            DisplaySlot = displaySlot;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The display to change
        /// </summary>
        public ID.TeamDisplayName DisplaySlot { get; set; }

        /// <summary>
        /// The value to change it to
        /// </summary>
        public JsonText Value { get => value; set => this.value = value ?? throw new ArgumentNullException(nameof(Value), "Value may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] [DisplaySlot] [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} {DisplaySlot} {Value.GetJsonString()}";
        }
    }

    /// <summary>
    /// Command which changes how collision works on a team
    /// </summary>
    public class TeamModifyCollisionCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyCollisionCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">The new collision setting</param>
        public TeamModifyCollisionCommand(Team team, ID.TeamCollision value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The new collision setting
        /// </summary>
        public ID.TeamCollision Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] collisionRule [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} collisionRule {Value}";
        }
    }

    /// <summary>
    /// Command which changes the color of a team
    /// </summary>
    public class TeamModifyColorCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyColorCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">The new color to change to</param>
        public TeamModifyColorCommand(Team team, ID.MinecraftColor value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The new color to change to
        /// </summary>
        public ID.MinecraftColor Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] color [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} color {Value}";
        }
    }

    /// <summary>
    /// Command which changes who can see a team's death messages
    /// </summary>
    public class TeamModifyDeathMessageCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyDeathMessageCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">The new death message visibility setting</param>
        public TeamModifyDeathMessageCommand(Team team, ID.TeamVisibility value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The new death message visibility setting
        /// </summary>
        public ID.TeamVisibility Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] deathMessageVisibility [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} deathMessageVisibility {Value}";
        }
    }

    /// <summary>
    /// A command which changes who can see a players on a team's names
    /// </summary>
    public class TeamModifyNameVisibilityCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyNameVisibilityCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">The new name visibility setting</param>
        public TeamModifyNameVisibilityCommand(Team team, ID.TeamVisibility value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// The new name visibility setting
        /// </summary>
        public ID.TeamVisibility Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] nametagVisibility [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} nametagVisibility {Value}";
        }
    }

    /// <summary>
    /// Command which changes if players on a team can hit each other
    /// </summary>
    public class TeamModifyFriendlyFireCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyFriendlyFireCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">True if friendly fire should be on. False if not</param>
        public TeamModifyFriendlyFireCommand(Team team, bool value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// True if friendly fire should be on. False if not
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] friendlyFire [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} friendlyFire {Value.ToMinecraftBool()}";
        }
    }

    /// <summary>
    /// Command which changes if players on a team can see their invisible team mates
    /// </summary>
    public class TeamModifyInvisibilityCommand : BaseCommand
    {
        private Team team = null!;

        /// <summary>
        /// Intializes a new <see cref="TeamModifyInvisibilityCommand"/>
        /// </summary>
        /// <param name="team">The team to modify</param>
        /// <param name="value">True if players in a team should be able to see invisible players in their own team. False if not</param>
        public TeamModifyInvisibilityCommand(Team team, bool value)
        {
            Team = team;
            Value = value;
        }

        /// <summary>
        /// The team to modify
        /// </summary>
        public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

        /// <summary>
        /// True if players in a team should be able to see invisible players in their own team. False if not
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>team modify [Team] seeFriendlyInvisibles [Value]</returns>
        public override string GetCommandString()
        {
            return $"team modify {Team.Name} seeFriendlyInvisibles {Value.ToMinecraftBool()}";
        }
    }
}
