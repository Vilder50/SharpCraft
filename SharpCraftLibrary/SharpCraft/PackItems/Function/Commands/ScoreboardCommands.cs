using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which adds a new scoreboard objective to the world
    /// </summary>
    public class ScoreboardObjectiveAddCommand : BaseCommand
    {
        private Objective scoreObject = null!;
        private string criterion = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardObjectiveAddCommand"/>
        /// </summary>
        /// <param name="displayName">The displayed name of the objective</param>
        /// <param name="scoreObject">The objective</param>
        /// <param name="criterion">The criterion for the objective</param>
        public ScoreboardObjectiveAddCommand(Objective scoreObject, string criterion, JsonText? displayName)
        {
            DisplayName = displayName;
            ScoreObject = scoreObject;
            Criterion = criterion;
        }

        /// <summary>
        /// The displayed name of the objective
        /// </summary>
        public JsonText? DisplayName { get; set; }

        /// <summary>
        /// The objective
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// The criterion for the objective
        /// </summary>
        public string Criterion { get => criterion; set => criterion = value ?? throw new ArgumentNullException(nameof(Criterion), "Criterion may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives add [ScoreObject] [Criterion] [DisplayName]</returns>
        public override string GetCommandString()
        {
            if (DisplayName is null)
            {
                return $"scoreboard objectives add {ScoreObject.Name} {Criterion}";
            }
            else
            {
                return $"scoreboard objectives add {ScoreObject.Name} {Criterion} {DisplayName.GetJsonString()}";
            }
        }
    }

    /// <summary>
    /// Command which returns a list of all existing scoreboard objectives
    /// </summary>
    public class ScoreboardObjectiveListCommand : BaseCommand
    {
        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives list</returns>
        public override string GetCommandString()
        {
            return "scoreboard objectives list";
        }
    }

    /// <summary>
    /// Command which changes the display name of an objective
    /// </summary>
    public class ScoreboardObjectiveChangeNameCommand : BaseCommand
    {
        private Objective scoreObject = null!;
        private JsonText displayName = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardObjectiveChangeNameCommand"/>
        /// </summary>
        /// <param name="displayName">The new display name for the objective</param>
        /// <param name="scoreObject">The objective to change</param>
        public ScoreboardObjectiveChangeNameCommand(Objective scoreObject, JsonText displayName)
        {
            DisplayName = displayName;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// The new display name for the objective
        /// </summary>
        public JsonText DisplayName { get => displayName; set => displayName = value ?? throw new ArgumentNullException(nameof(DisplayName), "DisplayName may not be null"); }


        /// <summary>
        /// The objective to change
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives modify [ScoreObject] displayname [DisplayName]</returns>
        public override string GetCommandString()
        {
             return $"scoreboard objectives modify {ScoreObject.Name} displayname {DisplayName.GetJsonString()}";
        }
    }

    /// <summary>
    /// Command which changes how an objective is displayed in tab
    /// </summary>
    public class ScoreboardObjectiveChangeRenderCommand : BaseCommand
    {
        private Objective scoreObject = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardObjectiveChangeRenderCommand"/>
        /// </summary>
        /// <param name="scoreObject">The objective to change</param>
        /// <param name="rendering">The new way to render the objective in tab</param>
        public ScoreboardObjectiveChangeRenderCommand(Objective scoreObject, ID.ObjectiveRender rendering)
        {
            ScoreObject = scoreObject;
            Rendering = rendering;
        }

        /// <summary>
        /// The objective to change
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// The new way to render the objective in tab
        /// </summary>
        public ID.ObjectiveRender Rendering { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives modify [ScoreObject] rendertype [Rendering]</returns>
        public override string GetCommandString()
        {
            return $"scoreboard objectives modify {ScoreObject.Name} rendertype {Rendering}";
        }
    }

    /// <summary>
    /// Command which removes an objective
    /// </summary>
    public class ScoreboardObjectiveRemoveCommand : BaseCommand
    {
        private Objective scoreObject = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardObjectiveRemoveCommand"/>
        /// </summary>
        /// <param name="scoreObject">The objective to remove</param>
        public ScoreboardObjectiveRemoveCommand(Objective scoreObject)
        {
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// The objective to remove
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives remove [ScoreObject]</returns>
        public override string GetCommandString()
        {
            return $"scoreboard objectives remove {ScoreObject.Name}";
        }
    }

    /// <summary>
    /// Command which changes a display slot
    /// </summary>
    public class ScoreboardSetDisplayCommand : BaseCommand
    {
        /// <summary>
        /// Intializes a new <see cref="ScoreboardSetDisplayCommand"/>
        /// </summary>
        /// <param name="scoreObject">The objective to display. Null to display nothing</param>
        /// <param name="displaySlot">The slot to change displayed objective</param>
        public ScoreboardSetDisplayCommand(Objective? scoreObject, ID.ScoreDisplay displaySlot)
        {
            ScoreObject = scoreObject;
            DisplaySlot = displaySlot;
        }

        /// <summary>
        /// The objective to display. Null to display nothing
        /// </summary>
        public Objective? ScoreObject { get; set; }

        /// <summary>
        /// The slot to change displayed objective
        /// </summary>
        public ID.ScoreDisplay DisplaySlot { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives setdisplay [DisplaySlot] ([ScoreObject])</returns>
        public override string GetCommandString()
        {
            if (ScoreObject is null)
            {
                return $"scoreboard objectives setdisplay {DisplaySlot}";
            }
            else
            {
                return $"scoreboard objectives setdisplay {DisplaySlot} {ScoreObject.Name}";
            }
        }
    }

    /// <summary>
    /// Command which changes a team sidebar display slot
    /// </summary>
    public class ScoreboardSetTeamDisplayCommand : BaseCommand
    {
        /// <summary>
        /// Intializes a new <see cref="ScoreboardSetTeamDisplayCommand"/>
        /// </summary>
        /// <param name="scoreObject">The objective to display. Null to display nothing</param>
        /// <param name="teamColor">The slot color to change</param>
        public ScoreboardSetTeamDisplayCommand(Objective? scoreObject, ID.MinecraftColor teamColor)
        {
            ScoreObject = scoreObject;
            TeamColor = teamColor;
        }

        /// <summary>
        /// The objective to display. Null to display nothing
        /// </summary>
        public Objective? ScoreObject { get; set; }

        /// <summary>
        /// The slot color to change
        /// </summary>
        public ID.MinecraftColor TeamColor { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard objectives setdisplay sidebar.team.[TeamColor] ([ScoreObject])</returns>
        public override string GetCommandString()
        {
            if (ScoreObject is null)
            {
                return $"scoreboard objectives setdisplay sidebar.team.{TeamColor}";
            }
            else
            {
                return $"scoreboard objectives setdisplay sidebar.team.{TeamColor} {ScoreObject.Name}";
            }
        }
    }

    /// <summary>
    /// Command which changes the value of a score
    /// </summary>
    public class ScoreboardValueChangeCommand : BaseCommand
    {
        private Objective scoreObject = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardValueChangeCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the scores to change</param>
        /// <param name="scoreObject">The objective to change score in</param>
        /// <param name="changeType">The way to change the score</param>
        /// <param name="changeNumber">The number to change the score with</param>
        public ScoreboardValueChangeCommand(BaseSelector selector, Objective scoreObject, ID.ScoreChange changeType, int changeNumber)
        {
            Selector = selector;
            ScoreObject = scoreObject;
            ChangeType = changeType;
            ChangeNumber = changeNumber;
        }

        /// <summary>
        /// Selector for selecting the scores to change
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The objective to change score in
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }

        /// <summary>
        /// The way to change the score
        /// </summary>
        public ID.ScoreChange ChangeType { get; set; }

        /// <summary>
        /// The number to change the score with
        /// </summary>
        public int ChangeNumber { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players [ChangeType] [Selector] [ScoreObject] [ChangeNumber]</returns>
        public override string GetCommandString()
        {
            if (ChangeNumber < 0)
            {
                if (ChangeType == ID.ScoreChange.add)
                {
                    return $"scoreboard players remove {Selector.GetSelectorString()} {ScoreObject.Name} {Math.Abs(ChangeNumber)}";
                }
                else if (ChangeType == ID.ScoreChange.remove)
                {
                    return $"scoreboard players add {Selector.GetSelectorString()} {ScoreObject.Name} {Math.Abs(ChangeNumber)}";
                }
            }
            return $"scoreboard players {ChangeType} {Selector.GetSelectorString()} {ScoreObject.Name} {ChangeNumber}";
        }
    }

    /// <summary>
    /// Command which changes the value of a score
    /// </summary>
    public class ScoreboardValueGetCommand : BaseCommand
    {
        private Objective scoreObject = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardValueGetCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the score to get</param>
        /// <param name="scoreObject">The objective to get the score from</param>
        public ScoreboardValueGetCommand(BaseSelector selector, Objective scoreObject)
        {
            Selector = selector;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// Selector for selecting the score to get
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The objective to get the score from
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }


        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players get [Selector] [ScoreObject]</returns>
        public override string GetCommandString()
        {
            return $"scoreboard players get {Selector.GetSelectorString()} {ScoreObject.Name}";
        }
    }

    /// <summary>
    /// Command which enables a trigger objective for one or more players
    /// </summary>
    public class ScoreboardEnableTriggerCommand : BaseCommand
    {
        private Objective scoreObject = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardEnableTriggerCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the players to enable a trigger for</param>
        /// <param name="scoreObject">The objective to enable triggering</param>
        public ScoreboardEnableTriggerCommand(BaseSelector selector, Objective scoreObject)
        {
            Selector = selector;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// Selector for selecting the players to enable a trigger for
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The objective to enable triggering
        /// </summary>
        public Objective ScoreObject { get => scoreObject; set => scoreObject = value ?? throw new ArgumentNullException(nameof(ScoreObject), "ScoreObject may not be null"); }


        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players enable [Selector] [ScoreObject]</returns>
        public override string GetCommandString()
        {
            return $"scoreboard players enable {Selector.GetSelectorString()} {ScoreObject.Name}";
        }
    }

    /// <summary>
    /// Command which resets a objective for one or more players
    /// </summary>
    public class ScoreboardResetCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardResetCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the scores to reset</param>
        /// <param name="scoreObject">The objective to reset scores in. Null to reset all scores for the selected scores</param>
        public ScoreboardResetCommand(BaseSelector selector, Objective? scoreObject)
        {
            Selector = selector;
            ScoreObject = scoreObject;
        }

        /// <summary>
        /// Selector for selecting the scores to reset
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The objective to reset scores in. Null to reset all scores for the selected scores
        /// </summary>
        public Objective? ScoreObject { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players reset [Selector] [ScoreObject]</returns>
        public override string GetCommandString()
        {
            if (ScoreObject is null)
            {
                return $"scoreboard players reset {Selector.GetSelectorString()}";
            }
            else
            {
                return $"scoreboard players reset {Selector.GetSelectorString()} {ScoreObject.Name}";
            }
        }
    }

    /// <summary>
    /// Commands which returns a list of an entity's scores
    /// </summary>
    public class ScoreboardListCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardObjectiveListCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting the entity to get a list of scores for</param>
        public ScoreboardListCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector for selecting the entity to get a list of scores for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players list [Selector]</returns>
        public string GetCommandString()
        {
             return $"scoreboard players list {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which modfifies one score with another
    /// </summary>
    public class ScoreboardOperationCommand : BaseCommand
    {
        private BaseSelector selector1 = null!;
        private Objective objective1 = null!;
        private BaseSelector selector2 = null!;
        private Objective objective2 = null!;

        /// <summary>
        /// Intializes a new <see cref="ScoreboardOperationCommand"/>
        /// </summary>
        /// <param name="selector1">Selector for selecting the scores to change</param>
        /// <param name="objective1">The objective the scores to change is in</param>
        /// <param name="operator">The operation use on the 2 scores</param>
        /// <param name="selector2">Selector for selecting the scores to change with</param>
        /// <param name="objective2">The objective the scores used for changing is in</param>
        public ScoreboardOperationCommand(BaseSelector selector1, Objective objective1, ID.Operation @operator, BaseSelector selector2, Objective objective2)
        {
            Selector1 = selector1;
            Objective1 = objective1;
            Selector2 = selector2;
            Objective2 = objective2;
            Operator = @operator;
        }

        /// <summary>
        /// Selector for selecting the scores to change
        /// </summary>
        public BaseSelector Selector1 { get => selector1; set => selector1 = value ?? throw new ArgumentNullException(nameof(Selector1), "Selector1 may not be null"); }

        /// <summary>
        /// The objective the scores to change is in
        /// </summary>
        public Objective Objective1
        {
            get => objective1;
            set
            {
                objective1 = value ?? throw new ArgumentNullException(nameof(Objective1), "Objective1 may not be null.");
            }
        }

        /// <summary>
        /// Selector for selecting the scores to change with
        /// </summary>
        public BaseSelector Selector2 { get => selector2; set => selector2 = value ?? throw new ArgumentNullException(nameof(Selector2), "Selector2 may not be null"); }

        /// <summary>
        /// The objective the scores used for changing is in
        /// </summary>
        public Objective Objective2
        {
            get => objective2;
            set
            {
                objective2 = value ?? throw new ArgumentNullException(nameof(Objective2), "Objective2 may not be null.");
            }
        }

        /// <summary>
        /// The operation use on the 2 scores
        /// </summary>
        public ID.Operation Operator { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>scoreboard players operation [Selector1] [Objective1] [Operator] [Selector2] [Objective2]</returns>
        public override string GetCommandString()
        {
            string operatorString = "";
            switch(Operator)
            {
                case ID.Operation.Add:
                    operatorString = "+=";
                    break;
                case ID.Operation.Divide:
                    operatorString = "/=";
                    break;
                case ID.Operation.Equel:
                    operatorString = "=";
                    break;
                case ID.Operation.GetHigher:
                    operatorString = ">";
                    break;
                case ID.Operation.GetLowest:
                    operatorString = "<";
                    break;
                case ID.Operation.Multiply:
                    operatorString = "*=";
                    break;
                case ID.Operation.Remainder:
                    operatorString = "%=";
                    break;
                case ID.Operation.Subtract:
                    operatorString = "-=";
                    break;
                case ID.Operation.Switch:
                    operatorString = "><";
                    break;
            }

            return $"scoreboard players operation {Selector1.GetSelectorString()} {Objective1.Name} {operatorString} {Selector2.GetSelectorString()} {Objective2.Name}";
        }
    }
}
