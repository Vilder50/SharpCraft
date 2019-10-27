using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for selectors
    /// </summary>
    public class Selector
    {
        /// <summary>
        /// Creates a new @s selector
        /// </summary>
        public Selector()
        {
            SelectorType = ID.Selector.s;
        }

        /// <summary>
        /// Creates a new selecter of the given type
        /// </summary>
        /// <param name="SelectWay">the type of selector</param>
        /// <param name="HasTag">a tag the selected entity must have. Note that this can be overwritten by <see cref="Tag"/></param>
        public Selector(ID.Selector SelectWay, Tag HasTag = null)
        {
            SelectorType = SelectWay;
            if (HasTag != null) { Tag = new EntityTag[] { new EntityTag(HasTag) }; }
        }

        /// <summary>
        /// Creates a selector which selects a none existing entity
        /// </summary>
        /// <param name="FakeName"></param>
        public Selector(string FakeName)
        {
            this.FakeName = FakeName;
        }
        internal string FakeName;

        /// <summary>
        /// The type of this selector
        /// </summary>
        public ID.Selector? SelectorType;

        /// <summary>
        /// The amount of levels the selected entity must have to be selected
        /// </summary>
        public Range Level;

        /// <summary>
        /// The distance there has to be to the selected entity
        /// </summary>
        public Range Radius;

        /// <summary>
        /// The x-rotation the selected entity must have
        /// </summary>
        public Range XRotation;

        /// <summary>
        /// The y-rotation the selected entity must have
        /// </summary>
        public Range YRotation;

        /// <summary>
        /// The maximum amount of entities this selector can select
        /// </summary>
        public int? Limit;

        /// <summary>
        /// The coords the selector should search from
        /// </summary>
        public double? X, Y, Z;

        /// <summary>
        /// The square the selector should search in
        /// </summary>
        public double? BoxX, BoxY, BoxZ;

        /// <summary>
        /// The names the selected entity must have / must not have to be selected
        /// </summary>
        public EntityName[] Name;

        /// <summary>
        /// The types the selected entity must be / must not be to be selected
        /// </summary>
        public EntityType[] Type;

        /// <summary>
        /// The tags the selected entity must have / must not have to be selected
        /// </summary>
        public EntityTag[] Tag;

        /// <summary>
        /// The scores the selected entity must have
        /// </summary>
        public EntityScore[] Score;

        /// <summary>
        /// The teams the selected entity must be on / must not be on to be selected
        /// </summary>
        public EntityTeam[] Team;

        /// <summary>
        /// The gamemode the selected entity must be in / must not be in to be selected
        /// </summary>
        public EntityMode[] Mode;

        /// <summary>
        /// The way the selected entities should be sorted in
        /// </summary>
        public ID.Sort? Sort;

        /// <summary>
        /// The NBT the selected entity have to have to be selected
        /// </summary>
        public Entity.BaseEntity NBT;

        /// <summary>
        /// If the entity shouldnt have the NBT in <see cref="NBT"/>
        /// </summary>
        public bool NotNBT;

        /// <summary>
        /// The type the entity has to be. Note that <see cref="Type"/> overwrites this value
        /// </summary>
        public ID.Entity SingleType
        {
            set
            {
                Type = new EntityType[] { new EntityType(value) };
            }
        }

        /// <summary>
        /// The name the entity has to have. Note that <see cref="Name"/> overwrites this value
        /// </summary>
        public string SingleName
        {
            set
            {
                Name = new EntityName[] { new EntityName(value) };
            }
        }

        /// <summary>
        /// The team the entity has to be. Not that <see cref="Team"/> overwrites this value
        /// </summary>
        public Team SingleTeam
        {
            set
            {
                Team = new EntityTeam[] { new EntityTeam(value) };
            }
        }

        /// <summary>
        /// The score the entity has to have. Not that <see cref="Score"/> overwrites this value
        /// </summary>
        public EntityScore SingleScore
        {
            set
            {
                Score = new EntityScore[] { value };
            }
        }

        /// <summary>
        /// The tag the entity has to be. Not that <see cref="Tag"/> overwrites this value
        /// </summary>
        public Tag SingleTag
        {
            set
            {
                Tag = new EntityTag[] { new EntityTag(value) };
            }
        }

        /// <summary>
        /// The gamemode the entity has to be. Not that <see cref="Mode"/> overwrites this value
        /// </summary>
        public ID.Gamemode SingleMode
        {
            set
            {
                Mode = new EntityMode[] { new EntityMode(value) };
            }
        }

        /// <summary>
        /// An object used to define a type an entity has to be / not to be
        /// </summary>
        public class EntityType
        {
            private readonly bool Want;
            private readonly SharpCraft.EntityType EntityID;

            /// <summary>
            /// Creates the object with the given parameters
            /// </summary>
            /// <param name="ID">The type of entity the entity has / has not to be</param>
            /// <param name="Wanted">If the entity should be the type or not</param>
            public EntityType(SharpCraft.EntityType ID, bool Wanted = true)
            {
                Want = Wanted;
                EntityID = ID ?? throw new System.ArgumentNullException(nameof(ID), "ID may not be null");
            }

            /// <summary>
            /// The <see cref="SelectorType"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                string TempString = "type=";
                if (!Want) { TempString += "!"; }
                TempString += EntityID.Name;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityType"/> into an array containing only that one <see cref="EntityType"/>
            /// </summary>
            /// <param name="entity">the <see cref="EntityType"/> to convert into an array</param>
            public static implicit operator EntityType[](EntityType entity)
            {
                return new EntityType[] { entity };
            }
        }

        /// <summary>
        /// An object used to define a name an entity has to have / not to have
        /// </summary>
        public class EntityName
        {
            private readonly bool Want;
            private readonly string Name;
            /// <summary>
            /// Creates an object defining a name an entity has to have / not to have
            /// </summary>
            /// <param name="EntityName">The name the entity has to have / not to have</param>
            /// <param name="Wanted">If the entity has to have the name</param>
            public EntityName(string EntityName, bool Wanted = true)
            {
                Want = Wanted;
                Name = EntityName;
            }
            /// <summary>
            /// The <see cref="EntityName"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                string TempString = "name=";
                if (!Want) { TempString += "!"; }
                TempString += Name;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityName"/> into an array containing only that one <see cref="EntityName"/>
            /// </summary>
            /// <param name="name">the <see cref="EntityName"/> to convert into an array</param>
            public static implicit operator EntityName[] (EntityName name)
            {
                return new EntityName[] { name };
            }
        }

        /// <summary>
        /// An object used to define a name an entity has to have / not to have
        /// </summary>
        public class EntityTag
        {
            private readonly bool Want;
            private readonly Tag Tag;
            /// <summary>
            /// Creates an object defining a tag an entity has to have / not to have
            /// </summary>
            /// <param name="TagName">The tag the entity has to have / not to have</param>
            /// <param name="Wanted">If the entity has to have the tag or not</param>
            public EntityTag(Tag TagName, bool Wanted = true)
            {
                Want = Wanted;
                Tag = TagName;
            }

            /// <summary>
            /// The <see cref="EntityTag"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                string TempString = "tag=";
                if (!Want) { TempString += "!"; }
                TempString += Tag;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityTag"/> into an array containing only that one <see cref="EntityTag"/>
            /// </summary>
            /// <param name="tag">the <see cref="EntityTag"/> to convert into an array</param>
            public static implicit operator EntityTag[] (EntityTag tag)
            {
                return new EntityTag[] { tag };
            }
        }

        /// <summary>
        /// An object used to define a score an entity has to have
        /// </summary>
        public class EntityScore
        {
            private readonly ScoreObject Objective;
            private readonly Range Score;
            /// <summary>
            /// Creates an object defining a score an entity has to have
            /// </summary>
            /// <param name="ObjectiveName">The score objective to look in</param>
            /// <param name="Score">The range the score has to be inside</param>
            public EntityScore(ScoreObject ObjectiveName, Range Score)
            {
                Objective = ObjectiveName;
                this.Score = Score;
            }

            /// <summary>
            /// The <see cref="EntityScore"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                return Score.SelectorString(Objective.ToString());
            }

            /// <summary>
            /// Converts a single <see cref="EntityScore"/> into an array containing only that one <see cref="EntityScore"/>
            /// </summary>
            /// <param name="score">the <see cref="EntityScore"/> to convert into an array</param>
            public static implicit operator EntityScore[] (EntityScore score)
            {
                return new EntityScore[] { score };
            }
        }

        /// <summary>
        /// An object used to define a team an entity has to be on / not to be on
        /// </summary>
        public class EntityTeam
        {
            private readonly bool Want;
            private readonly Team Team;
            /// <summary>
            /// Creates an object defining a team an entity has to be / not to be on
            /// </summary>
            /// <param name="TeamName">The team the entity has to be on / not to be on</param>
            /// <param name="Wanted">If the entity has to be on the team or not</param>
            public EntityTeam(Team TeamName, bool Wanted = true)
            {
                Want = Wanted;
                Team = TeamName;
            }

            /// <summary>
            /// The <see cref="EntityTeam"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                string TempString = "team=";
                if (!Want) { TempString += "!"; }
                TempString += Team;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityTeam"/> into an array containing only that one <see cref="EntityTeam"/>
            /// </summary>
            /// <param name="team">the <see cref="EntityTeam"/> to convert into an array</param>
            public static implicit operator EntityTeam[] (EntityTeam team)
            {
                return new EntityTeam[] { team };
            }
        }

        /// <summary>
        /// An object used to define a gamemode an entity has to be in / not to be in
        /// </summary>
        public class EntityMode
        {
            private readonly bool Want;
            private readonly ID.Gamemode? Gamemode;
            /// <summary>
            /// Creates an object defining a gamemode an entity has to be in / not to be in
            /// </summary>
            /// <param name="Mode">The gamemode the entity has to be in / not to be in</param>
            /// <param name="Wanted">If the entity has to be in the gamemode or not</param>
            public EntityMode(ID.Gamemode Mode, bool Wanted = true)
            {
                Want = Wanted;
                Gamemode = Mode;
            }

            /// <summary>
            /// The <see cref="EntityMode"/>'s raw data
            /// </summary>
            public override string ToString()
            {
                string TempString = "gamemode=";
                if (!Want) { TempString += "!"; }
                TempString += Gamemode;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityMode"/> into an array containing only that one <see cref="EntityMode"/>
            /// </summary>
            /// <param name="mode">the <see cref="EntityMode"/> to convert into an array</param>
            public static implicit operator EntityMode[] (EntityMode mode)
            {
                return new EntityMode[] { mode };
            }
        }

        /// <summary>
        /// The <see cref="Selector"/>'s raw data
        /// </summary>
        public override string ToString()
        {
            if (SelectorType != ID.Selector.All && SelectorType != null)
            {
                List<string> TempList = new List<string>();

                if (Level != null) { TempList.Add(Level.SelectorString("level")); }
                if (Radius != null) { TempList.Add(Radius.SelectorString("distance")); }
                if (XRotation != null) { TempList.Add(XRotation.SelectorString("x_rotation")); }
                if (YRotation != null) { TempList.Add(YRotation.SelectorString("y_rotation")); }
                if (Limit != null && SelectorType != ID.Selector.s) { TempList.Add("limit=" + Limit); }
                if (X != null) { TempList.Add("x=" + X.ToString().Replace(",", ".")); }
                if (Y != null) { TempList.Add("y=" + Y.ToString().Replace(",", ".")); }
                if (Z != null) { TempList.Add("z=" + Z.ToString().Replace(",", ".")); }
                if (BoxX != null) { TempList.Add("dx=" + BoxX.ToString().Replace(",", ".")); }
                if (BoxY != null) { TempList.Add("dy=" + BoxY.ToString().Replace(",", ".")); }
                if (BoxZ != null) { TempList.Add("dz=" + BoxZ.ToString().Replace(",", ".")); }
                if (Name != null)
                {
                    for (int i = 0; i < Name.Length; i++)
                    {
                        TempList.Add(Name[i].ToString());
                    }
                }
                if (Type != null)
                {
                    for (int i = 0; i < Type.Length; i++)
                    {
                        TempList.Add(Type[i].ToString());
                    }
                }
                if (Tag != null)
                {
                    for (int i = 0; i < Tag.Length; i++)
                    {
                        TempList.Add(Tag[i].ToString());
                    }
                }
                if (Score != null)
                {
                    List<string> TempScoreList = new List<string>();
                    for (int i = 0; i < Score.Length; i++)
                    {
                        TempScoreList.Add(Score[i].ToString());
                    }
                    TempList.Add("scores={" + string.Join(",",TempScoreList) + "}");
                }
                if (Mode != null)
                {
                    for (int i = 0; i < Mode.Length; i++)
                    {
                        TempList.Add(Mode[i].ToString());
                    }
                }
                if (Team != null)
                {
                    for (int i = 0; i < Team.Length; i++)
                    {
                        TempList.Add(Team[i].ToString());
                    }
                }
                if (Sort != null) { TempList.Add("sort=" + Sort); }
                if (NBT != null)
                {
                    if (NotNBT)
                    {
                        TempList.Add("nbt=!" + NBT.GetDataWithoutID());
                    }
                    else
                    {
                        TempList.Add("nbt=" + NBT.GetDataWithoutID());
                    }
                }



                if (TempList.Count != 0)
                {
                    return "@" + SelectorType.ToString() + "[" + string.Join(",", TempList) + "]";
                }
                else
                {
                    return "@" + SelectorType.ToString();
                }
            }
            else if (SelectorType == ID.Selector.All)
            {
                return "*";
            }
            else
            {
                return FakeName;
            }
        }

        /// <summary>
        /// Limits the selector to only selecting 1 entity
        /// </summary>
        public void Limited()
        {
            if (!IsLimited())
            {
                Limit = 1;
            }
        }

        /// <summary>
        /// Returns true if the selector is limited to only one entity
        /// </summary>
        /// <returns>True if the selector is limited to only one entity</returns>
        public bool IsLimited()
        {
            if (SelectorType == ID.Selector.s || SelectorType == ID.Selector.p || SelectorType == ID.Selector.r || SelectorType is null)
            {
                return true;
            }

            return (Limit == 1);
        }

        /// <summary>
        /// Converts a string into a selector containing a fakename
        /// </summary>
        /// <param name="fakeName">The fake name to make a selector out of</param>
        public static implicit operator Selector(string fakeName)
        {
            return new Selector(fakeName);
        }

        /// <summary>
        /// Converts a selector type into a selector
        /// </summary>
        /// <param name="selector">the selector type to convert into a selector</param>
        public static implicit operator Selector(ID.Selector selector)
        {
            return new Selector(selector);
        }
    }
}
