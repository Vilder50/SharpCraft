using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which teleports one or more entities to another location
    /// </summary>
    public class TeleportToCommand : BaseCommand
    {
        private Vector coordinates;
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="TeleportToCommand"/>
        /// </summary>
        /// <param name="coordinates">The place to teleport the entities to</param>
        /// <param name="selector">Selector selecting the entities to teleport</param>
        public TeleportToCommand(Vector coordinates, BaseSelector selector)
        {
            Coordinates = coordinates;
            Selector = selector;
        }

        /// <summary>
        /// The place to teleport the entities to
        /// </summary>
        public Vector Coordinates { get => coordinates; set => coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null"); }

        /// <summary>
        /// Selector selecting the entities to teleport
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tp [Selector] [Coordinates]</returns>
        public override string GetCommandString()
        {
            return $"tp {Selector.GetSelectorString()} {Coordinates.GetVectorString()}";
        }
    }

    /// <summary>
    /// Command which teleports one or more entities to another entity
    /// </summary>
    public class TeleportToEntityCommand : BaseCommand
    {
        private BaseSelector selector;
        private BaseSelector toSelector;

        /// <summary>
        /// Intializes a new <see cref="TeleportToEntityCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the entities to teleport</param>
        /// <param name="toSelector">The entity to teleport to</param>
        public TeleportToEntityCommand(BaseSelector selector, BaseSelector toSelector)
        {
            Selector = selector;
            ToSelector = toSelector;
        }

        /// <summary>
        /// Selector selecting the entities to teleport
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// The entity to teleport to
        /// </summary>
        public BaseSelector ToSelector
        {
            get => toSelector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(ToSelector), "ToSelector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(ToSelector));
                }
                toSelector = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tp [Selector] [ToSelector]</returns>
        public override string GetCommandString()
        {
            return $"tp {Selector.GetSelectorString()} {ToSelector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which teleports one or more entities to a location facing another location
    /// </summary>
    public class TeleportToFacingCommand : TeleportToCommand
    {
        private Vector facingCoordinates;

        /// <summary>
        /// Intializes a new <see cref="TeleportToFacingCommand"/>
        /// </summary>
        /// <param name="coordinates">The place to teleport the entities to</param>
        /// <param name="selector">Selector selecting the entities to teleport</param>
        /// <param name="facingCoordinates">The coordinates the entities will be facing after teleporting</param>
        public TeleportToFacingCommand(Vector coordinates, BaseSelector selector, Vector facingCoordinates) : base(coordinates, selector)
        {
            Coordinates = coordinates;
            Selector = selector;
            FacingCoordinates = facingCoordinates;
        }

        /// <summary>
        /// The coordinates the entities will be facing after teleporting
        /// </summary>
        public Vector FacingCoordinates { get => facingCoordinates; set => facingCoordinates = value ?? throw new ArgumentNullException(nameof(FacingCoordinates), "FacingCoordinates may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tp [Selector] [Coordinates] facing [FacingCoordinates]</returns>
        public override string GetCommandString()
        {
            return $"{base.GetCommandString()} facing {FacingCoordinates.GetVectorString()}";
        }
    }

    /// <summary>
    /// Command which teleports one or more entities to a location facing another entity
    /// </summary>
    public class TeleportToFacingEntityCommand : TeleportToCommand
    {
        private BaseSelector facingSelector;

        /// <summary>
        /// Intializes a new <see cref="TeleportToFacingEntityCommand"/>
        /// </summary>
        /// <param name="coordinates">The place to teleport the entities to</param>
        /// <param name="selector">Selector selecting the entities to teleport</param>
        /// <param name="facingSelector">The entity the other entities should face when teleported</param>
        /// <param name="anchor">The part of the entity to face</param>
        public TeleportToFacingEntityCommand(Vector coordinates, BaseSelector selector, BaseSelector facingSelector, ID.FacingAnchor anchor) : base(coordinates, selector)
        {
            Coordinates = coordinates;
            Selector = selector;
            FacingSelector = facingSelector;
            Anchor = anchor;
        }

        /// <summary>
        /// The entity the other entities should face when teleported
        /// </summary>
        public BaseSelector FacingSelector
        {
            get => facingSelector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(FacingSelector), "FacingSelector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(FacingSelector));
                }
                facingSelector = value;
            }
        }

        /// <summary>
        /// The part of the entity to face
        /// </summary>
        public ID.FacingAnchor Anchor { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tp [Selector] [Coordinates] facing entity [FacingSelector] [Anchor]</returns>
        public override string GetCommandString()
        {
            return $"{base.GetCommandString()} facing entity {FacingSelector.GetSelectorString()} {Anchor}";
        }
    }

    /// <summary>
    /// Command which teleports one or more entities to a location and change their rotation
    /// </summary>
    public class TeleportToRotationCommand : TeleportToCommand
    {
        private Rotation rotation;

        /// <summary>
        /// Intializes a new <see cref="TeleportToRotationCommand"/>
        /// </summary>
        /// <param name="coordinates">The place to teleport the entities to</param>
        /// <param name="selector">Selector selecting the entities to teleport</param>
        /// <param name="rotation">The rotation the entites gets after being teleported</param>
        public TeleportToRotationCommand(Vector coordinates, BaseSelector selector, Rotation rotation) : base(coordinates, selector)
        {
            Coordinates = coordinates;
            Selector = selector;
            Rotation = rotation;
        }

        /// <summary>
        /// The rotation the entites gets after being teleported
        /// </summary>
        public Rotation Rotation { get => rotation; set => rotation = value ?? throw new ArgumentNullException(nameof(Rotation), "Rotation may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>tp [Selector] [Coordinates] [Rotation]</returns>
        public override string GetCommandString()
        {
            return $"{base.GetCommandString()} {Rotation.GetRotationString()}";
        }
    }
}
