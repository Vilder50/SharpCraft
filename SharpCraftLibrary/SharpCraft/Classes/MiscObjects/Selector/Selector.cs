using System.Collections.Generic;
using System;

namespace SharpCraft
{

    /// <summary>
    /// An object for selectors
    /// </summary>
    public class Selector : BaseSelector
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
        /// <param name="HasTag">a tag the selected entity must have. Note that this can be overwritten by <see cref="Tags"/></param>
        public Selector(ID.Selector SelectWay, Tag? HasTag = null)
        {
            SelectorType = SelectWay;
            if (HasTag != null) { Tags = new EntityTag[] { new EntityTag(HasTag) }; }
        }

        /// <summary>
        /// The type of this selector
        /// </summary>
        public ID.Selector SelectorType { get; set; }

        /// <summary>
        /// The amount of levels the selected entity must have to be selected
        /// </summary>
        public MCRange? Level { get; set; }

        /// <summary>
        /// The distance there has to be to the selected entity
        /// </summary>
        public MCRange? Distance { get; set; }

        /// <summary>
        /// The x-rotation the selected entity must have (Vertical rotation. 90 = down, -90 = up)
        /// </summary>
        public MCRange? XRotation { get; set; }

        /// <summary>
        /// The y-rotation the selected entity must have (Horizontal rotation. 0 = +z, 90 = -x)
        /// </summary>
        public MCRange? YRotation { get; set; }

        /// <summary>
        /// The maximum amount of entities this selector can select
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The X coords the selector should search from
        /// </summary>
        public double? X { get; set; }

        /// <summary>
        /// The Y coords the selector should search from
        /// </summary>
        public double? Y { get; set; }

        /// <summary>
        /// The Z coords the selector should search from
        /// </summary>
        public double? Z { get; set; }

        /// <summary>
        /// The amount of blocks in the x direction to select in
        /// </summary>
        public double? BoxX { get; set; }

        /// <summary>
        /// The amount of blocks in the y direction to select in
        /// </summary>
        public double? BoxY { get; set; }

        /// <summary>
        /// The amount of blocks in the z direction to select in
        /// </summary>
        public double? BoxZ { get; set; }

        /// <summary>
        /// The names the selected entity must have / must not have to be selected
        /// </summary>
        public EntityName[]? Names { get; set; }

        /// <summary>
        /// The types the selected entity must be / must not be to be selected
        /// </summary>
        public EntityType[]? Types { get; set; }

        /// <summary>
        /// The tags the selected entity must have / must not have to be selected
        /// </summary>
        public EntityTag[]? Tags { get; set; }

        /// <summary>
        /// The scores the selected entity must have
        /// </summary>
        public EntityScore[]? Scores { get; set; }

        /// <summary>
        /// The teams the selected entity must be on / must not be on to be selected
        /// </summary>
        public EntityTeam[]? Teams { get; set; }

        /// <summary>
        /// The gamemode the selected entity must be in / must not be in to be selected
        /// </summary>
        public EntityMode[]? Modes { get; set; }

        /// <summary>
        /// The predicates the selected entity must turn successfull / not successfull
        /// </summary>
        public EntityPredicate[]? Predicates { get; set; }

        /// <summary>
        /// The way the selected entities should be sorted in
        /// </summary>
        public ID.Sort? Sort { get; set; }

        /// <summary>
        /// The NBT the selected entity have to have to be selected
        /// </summary>
        public Entity? NBT { get; set; }

        /// <summary>
        /// If the entity shouldnt have the NBT in <see cref="NBT"/>
        /// </summary>
        public bool NotNBT { get; set; }

        /// <summary>
        /// The type the entity has to be. Note that <see cref="Types"/> overwrites this value
        /// </summary>
        public ID.Entity SingleType
        {
            set
            {
                Types = new EntityType[] { new EntityType(value) };
            }
        }

        /// <summary>
        /// The name the entity has to have. Note that <see cref="Names"/> overwrites this value
        /// </summary>
        public string SingleName
        {
            set
            {
                Names = new EntityName[] { new EntityName(value) };
            }
        }

        /// <summary>
        /// The team the entity has to be. Note that <see cref="Teams"/> overwrites this value
        /// </summary>
        public Team SingleTeam
        {
            set
            {
                Teams = new EntityTeam[] { new EntityTeam(value) };
            }
        }

        /// <summary>
        /// The score the entity has to have. Note that <see cref="Scores"/> overwrites this value
        /// </summary>
        public EntityScore SingleScore
        {
            set
            {
                Scores = new EntityScore[] { value };
            }
        }

        /// <summary>
        /// The tag the entity has to be. Note that <see cref="Tags"/> overwrites this value
        /// </summary>
        public Tag SingleTag
        {
            set
            {
                Tags = new EntityTag[] { new EntityTag(value) };
            }
        }

        /// <summary>
        /// The gamemode the entity has to be. Note that <see cref="Modes"/> overwrites this value
        /// </summary>
        public ID.Gamemode SingleMode
        {
            set
            {
                Modes = new EntityMode[] { new EntityMode(value) };
            }
        }

        /// <summary>
        /// A predicate which should be true for the selected entity. Note that <see cref="Predicates"/> overwrites this value
        /// </summary>
        public IPredicate SinglePredicate
        {
            set
            {
                Predicates = new EntityPredicate[] { new EntityPredicate(value) };
            }
        }

        /// <summary>
        /// Interface for selector arguments there can be multiple of
        /// </summary>
        public interface ISelectorArgument
        {
            /// <summary>
            /// Gets the argument string
            /// </summary>
            /// <returns>The argument string</returns>
            string GetSelectionString();
        }

        /// <summary>
        /// An object used to define a type an entity has to be / not to be
        /// </summary>
        public class EntityType : ISelectorArgument
        {
            private SharpCraft.EntityType id = null!;

            /// <summary>
            /// Creates the object with the given parameters
            /// </summary>
            /// <param name="id">The type of entity the entity has / has not to be</param>
            /// <param name="wanted">If the entity should be the type or not</param>
            public EntityType(SharpCraft.EntityType id, bool wanted = true)
            {
                Wanted = wanted;
                ID = id;
            }

            /// <summary>
            /// If the entity should be the type or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The type of entity the entity has / has not to be
            /// </summary>
            public SharpCraft.EntityType ID { get => id; set => id = value ?? throw new ArgumentNullException(nameof(ID), "ID may not be null or empty"); }

            /// <summary>
            /// The <see cref="SelectorType"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "type=";
                if (!Wanted) { TempString += "!"; }
                TempString += ID.Name;
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

            /// <summary>
            /// Converts a <see cref="SharpCraft.EntityType"/> into a <see cref="EntityTag"/>
            /// </summary>
            /// <param name="type">the <see cref="SharpCraft.EntityType"/> to convert</param>
            public static implicit operator EntityType(SharpCraft.EntityType type)
            {
                return new EntityType(type);
            }

            /// <summary>
            /// Converts a <see cref="ID.Entity"/> into a <see cref="EntityTag"/>
            /// </summary>
            /// <param name="type">the <see cref="ID.Entity"/> to convert</param>
            public static implicit operator EntityType(ID.Entity type)
            {
                return new EntityType(type);
            }
        }

        /// <summary>
        /// An object used to define a name an entity has to have / not to have
        /// </summary>
        public class EntityName : ISelectorArgument
        {
            private string name = null!;

            /// <summary>
            /// Creates an object defining a name an entity has to have / not to have
            /// </summary>
            /// <param name="name">The name the entity has to have / not to have</param>
            /// <param name="wanted">If the entity has to have the name</param>
            public EntityName(string name, bool wanted = true)
            {
                Name = name;
                Wanted = wanted;
            }

            /// <summary>
            /// If the entity should have the name or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The name the entity has to have / not to have
            /// </summary>
            public string Name 
            { 
                get => name; 
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Name may not be null or whitespace",nameof(Name));
                    }
                    name = value;
                }
            }

            /// <summary>
            /// The <see cref="EntityName"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "name=";
                if (!Wanted) { TempString += "!"; }
                TempString += "\"" + Name + "\"";
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityName"/> into an array containing only that one <see cref="EntityName"/>
            /// </summary>
            /// <param name="name">the <see cref="EntityName"/> to convert into an array</param>
            public static implicit operator EntityName[](EntityName name)
            {
                return new EntityName[] { name };
            }
        }

        /// <summary>
        /// An object used to define a name an entity has to have / not to have
        /// </summary>
        public class EntityTag : ISelectorArgument
        {
            private Tag tag = null!;

            /// <summary>
            /// Creates an object defining a tag an entity has to have / not to have
            /// </summary>
            /// <param name="tag">The tag the entity has to have / not to have</param>
            /// <param name="wanted">If the entity has to have the tag or not</param>
            public EntityTag(Tag tag, bool wanted = true)
            {
                Tag = tag;
                Wanted = wanted;
            }

            /// <summary>
            /// If the entity should have the tag or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The tag the entity has to have / not to have
            /// </summary>
            public Tag Tag { get => tag; set => tag = value ?? throw new ArgumentNullException(nameof(Tag), "Tag may not be null"); }

            /// <summary>
            /// The <see cref="EntityTag"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "tag=";
                if (!Wanted) { TempString += "!"; }
                TempString += Tag.Name;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityTag"/> into an array containing only that one <see cref="EntityTag"/>
            /// </summary>
            /// <param name="tag">the <see cref="EntityTag"/> to convert into an array</param>
            public static implicit operator EntityTag[](EntityTag tag)
            {
                return new EntityTag[] { tag };
            }

            /// <summary>
            /// Converts a <see cref="SharpCraft.Tag"/> into a <see cref="EntityTag"/>
            /// </summary>
            /// <param name="tag">the <see cref="SharpCraft.Tag"/> to convert</param>
            public static implicit operator EntityTag(Tag tag)
            {
                return new EntityTag(tag);
            }
        }

        /// <summary>
        /// An object used to define a score an entity has to have
        /// </summary>
        public class EntityScore : ISelectorArgument
        {
            private Objective objective = null!;
            private MCRange score = null!;

            /// <summary>
            /// Creates an object defining a score an entity has to have
            /// </summary>
            /// <param name="objective">The score objective to look in</param>
            /// <param name="score">The range the score has to be inside</param>
            public EntityScore(Objective objective, MCRange score)
            {
                Objective = objective;
                Score = score;
            }

            /// <summary>
            /// The score objective to look in
            /// </summary>
            public Objective Objective { get => objective; set => objective = value ?? throw new ArgumentNullException(nameof(Objective), "Objective may not be null"); }

            /// <summary>
            /// The range the score has to be inside
            /// </summary>
            public MCRange Score { get => score; set => score = value ?? throw new ArgumentNullException(nameof(Score), "Score may not be null"); }

            /// <summary>
            /// The <see cref="EntityScore"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                return Score.SelectorString(Objective.Name);
            }

            /// <summary>
            /// Converts a single <see cref="EntityScore"/> into an array containing only that one <see cref="EntityScore"/>
            /// </summary>
            /// <param name="score">the <see cref="EntityScore"/> to convert into an array</param>
            public static implicit operator EntityScore[](EntityScore score)
            {
                return new EntityScore[] { score };
            }
        }

        /// <summary>
        /// An object used to define a team an entity has to be on / not to be on
        /// </summary>
        public class EntityTeam : ISelectorArgument
        {
            private Team team = null!;

            /// <summary>
            /// Creates an object defining a team an entity has to be / not to be on
            /// </summary>
            /// <param name="team">The team the entity has to be on / not to be on</param>
            /// <param name="wanted">If the entity has to be on the team or not</param>
            public EntityTeam(Team team, bool wanted = true)
            {
                Team = team;
                Wanted = wanted;
            }

            /// <summary>
            /// If the entity should have the tag or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The team the entity has to be on / not to be on
            /// </summary>
            public Team Team { get => team; set => team = value ?? throw new ArgumentNullException(nameof(Team), "Team may not be null"); }

            /// <summary>
            /// The <see cref="EntityTeam"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "team=";
                if (!Wanted) { TempString += "!"; }
                TempString += Team.Name;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityTeam"/> into an array containing only that one <see cref="EntityTeam"/>
            /// </summary>
            /// <param name="team">the <see cref="EntityTeam"/> to convert into an array</param>
            public static implicit operator EntityTeam[](EntityTeam team)
            {
                return new EntityTeam[] { team };
            }

            /// <summary>
            /// Converts a <see cref="SharpCraft.Team"/> into a <see cref="EntityTag"/>
            /// </summary>
            /// <param name="team">the <see cref="SharpCraft.Team"/> to convert</param>
            public static implicit operator EntityTeam(Team team)
            {
                return new EntityTeam(team);
            }
        }

        /// <summary>
        /// An object used to define a gamemode an entity has to be in / not to be in
        /// </summary>
        public class EntityMode : ISelectorArgument
        {
            /// <summary>
            /// Creates an object defining a gamemode an entity has to be in / not to be in
            /// </summary>
            /// <param name="mode">The gamemode the entity has to be in / not to be in</param>
            /// <param name="wanted">If the entity has to be in the gamemode or not</param>
            public EntityMode(ID.Gamemode mode, bool wanted = true)
            {
                Mode = mode;
                Wanted = wanted;
            }

            /// <summary>
            /// If the entity has to be in the gamemode or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The gamemode the entity has to be in / not to be in
            /// </summary>
            public ID.Gamemode Mode { get; set; }

            /// <summary>
            /// The <see cref="EntityMode"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "gamemode=";
                if (!Wanted) { TempString += "!"; }
                TempString += Mode;
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityMode"/> into an array containing only that one <see cref="EntityMode"/>
            /// </summary>
            /// <param name="mode">the <see cref="EntityMode"/> to convert into an array</param>
            public static implicit operator EntityMode[](EntityMode mode)
            {
                return new EntityMode[] { mode };
            }

            /// <summary>
            /// Converts a <see cref="ID.Gamemode"/> into a <see cref="EntityMode"/>
            /// </summary>
            /// <param name="mode">the <see cref="ID.Gamemode"/> to convert</param>
            public static implicit operator EntityMode(ID.Gamemode mode)
            {
                return new EntityMode(mode);
            }
        }

        /// <summary>
        /// An object used to define a predicate an entity has to have / not have
        /// </summary>
        public class EntityPredicate : ISelectorArgument
        {
            private IPredicate predicate = null!;

            /// <summary>
            /// Intializes a new <see cref="EntityPredicate"/>
            /// </summary>
            /// <param name="predicate">The predicate to check</param>
            /// <param name="wanted">If the predicate should be wanted successfull or not</param>
            public EntityPredicate(IPredicate predicate, bool wanted = true)
            {
                Predicate = predicate;
                Wanted = wanted;
            }

            /// <summary>
            /// The predicate to check
            /// </summary>
            public IPredicate Predicate { get => predicate; set => predicate = value ?? throw new System.ArgumentNullException(nameof(Predicate), "Predicate may not be null"); }

            /// <summary>
            /// If the predicate should be wanted successfull or not
            /// </summary>
            public bool Wanted { get; set; }

            /// <summary>
            /// The <see cref="EntityMode"/>'s raw data
            /// </summary>
            public string GetSelectionString()
            {
                string TempString = "predicate=";
                if (!Wanted) { TempString += "!"; }
                TempString += Predicate.GetNamespacedName();
                return TempString;
            }

            /// <summary>
            /// Converts a single <see cref="EntityPredicate"/> into an array containing only that one <see cref="EntityPredicate"/>
            /// </summary>
            /// <param name="predicate">the <see cref="EntityPredicate"/> to convert into an array</param>
            public static implicit operator EntityPredicate[](EntityPredicate predicate)
            {
                return new EntityPredicate[] { predicate };
            }
        }

        /// <summary>
        /// The selector string used by the game
        /// </summary>
        /// <returns>The selector string used by the game</returns>
        public override string GetSelectorString()
        {
            List<string> tempList = new List<string>();

            if (Level != null) { tempList.Add(Level.SelectorString("level")); }
            if (Distance != null) { tempList.Add(Distance.SelectorString("distance")); }
            if (XRotation != null) { tempList.Add(XRotation.SelectorString("x_rotation")); }
            if (YRotation != null) { tempList.Add(YRotation.SelectorString("y_rotation")); }
            if (Limit != null && SelectorType != ID.Selector.s) { tempList.Add("limit=" + Limit); }
            if (X != null) { tempList.Add("x=" + X.ToString()!.Replace(",", ".")); }
            if (Y != null) { tempList.Add("y=" + Y.ToString()!.Replace(",", ".")); }
            if (Z != null) { tempList.Add("z=" + Z.ToString()!.Replace(",", ".")); }
            if (BoxX != null) { tempList.Add("dx=" + BoxX.ToString()!.Replace(",", ".")); }
            if (BoxY != null) { tempList.Add("dy=" + BoxY.ToString()!.Replace(",", ".")); }
            if (BoxZ != null) { tempList.Add("dz=" + BoxZ.ToString()!.Replace(",", ".")); }
            if (Names != null) { tempList.Add(GetSelectionString(Names)); }
            if (Types != null) { tempList.Add(GetSelectionString(Types)); }
            if (Tags != null) { tempList.Add(GetSelectionString(Tags)); }
            if (Predicates != null) { tempList.Add(GetSelectionString(Predicates)); }
            if (Scores != null)
            {
                List<string> tempScoreList = new List<string>();
                for (int i = 0; i < Scores.Length; i++)
                {
                    tempScoreList.Add(Scores[i].GetSelectionString());
                }
                tempList.Add("scores={" + string.Join(",", tempScoreList) + "}");
            }
            if (Modes != null) { tempList.Add(GetSelectionString(Modes)); }
            if (Teams != null) { tempList.Add(GetSelectionString(Teams)); }
            if (Sort != null) { tempList.Add("sort=" + Sort); }
            if (NBT != null)
            {
                if (NotNBT)
                {
                    tempList.Add("nbt=!" + NBT.GetDataWithoutID());
                }
                else
                {
                    tempList.Add("nbt=" + NBT.GetDataWithoutID());
                }
            }



            if (tempList.Count != 0)
            {
                return "@" + SelectorType.ToString() + "[" + string.Join(",", tempList) + "]";
            }
            else
            {
                return "@" + SelectorType.ToString();
            }
        }

        /// <summary>
        /// Limits the selector to only selecting 1 entity
        /// </summary>
        public override void LimitSelector()
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
        public override bool IsLimited()
        {
            if (SelectorType == ID.Selector.s || SelectorType == ID.Selector.p || SelectorType == ID.Selector.r)
            {
                return true;
            }

            return (Limit == 1);
        }

        /// <summary>
        /// Converts a selector type into a selector
        /// </summary>
        /// <param name="selector">the selector type to convert into a selector</param>
        public static implicit operator Selector(ID.Selector selector)
        {
            return new Selector(selector);
        }

        /// <summary>
        /// Returns the string used for selecting the thing in the array
        /// </summary>
        /// <param name="array">An array of things to select</param>
        /// <returns>The string used for selecting the thing in the array</returns>
        private static string GetSelectionString(ISelectorArgument[] array)
        {
            List<string> tempList = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                tempList.Add(array[i].GetSelectionString());
            }
            return string.Join(",", tempList);
        }

        /// <summary>
        /// Returns a shallow clone of this selector
        /// </summary>
        /// <returns>A shallow clone of this selector</returns>
        public virtual Selector ShallowClone()
        {
            return (Selector)MemberwiseClone();
        }
    }
}
