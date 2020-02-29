using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which changes the size of the worldborder
    /// </summary>
    public class WorldborderSizeCommand : BaseCommand
    {
        private double size;

        /// <summary>
        /// Intializes a new <see cref="WorldborderSizeCommand"/>
        /// </summary>
        /// <param name="size">The size to modify with</param>
        /// <param name="modifier">The way to modify the size</param>
        /// <param name="time">The amount of time to modification takes. Leave null to make it happen instant</param>
        public WorldborderSizeCommand(double size, ID.AddSetModifier modifier, Time? time)
        {
            Modifier = modifier;
            Size = size;
            Time = time;
        }

        /// <summary>
        /// The size to modify with
        /// </summary>
        public double Size 
        { 
            get => size;
            set
            {
                if (Modifier == ID.AddSetModifier.set && value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Size), "Size may not be less than 0 when setting");
                }
                if (value > 60_000_000)
                {
                    throw new ArgumentOutOfRangeException(nameof(Size), "Size may not be higher than 60000000");
                }
                size = value;
            }
        }


        /// <summary>
        /// The way to modify the size
        /// </summary>
        public ID.AddSetModifier Modifier { get; set; }

        /// <summary>
        /// The amount of time to modification takes. Leave null to make it happen instant
        /// </summary>
        public Time? Time { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder [Modifier] [Size] ([Time])</returns>
        public override string GetCommandString()
        {
            if (Time is null)
            {
                return $"worldborder {Modifier} {Size.ToMinecraftDouble()}";
            }
            else
            {
                return $"worldborder {Modifier} {Size.ToMinecraftDouble()} {Time.AsTicks()}";
            }
        }
    }

    /// <summary>
    /// Command which changes the center of the world border
    /// </summary>
    public class WorldborderCenterCommand : BaseCommand
    {
        private Vector coordinates = null!;

        /// <summary>
        /// Intializes a new <see cref="WorldborderCenterCommand"/>
        /// </summary>
        /// <param name="coordinates">The new center of the world border</param>
        public WorldborderCenterCommand(Vector coordinates)
        {
            Coordinates = coordinates;
        }

        /// <summary>
        /// The new center of the world border
        /// </summary>
        public Vector Coordinates { get => coordinates; set => coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder center [Coordinates]</returns>
        public override string GetCommandString()
        {
            return $"worldborder center {Coordinates.GetXString()} {coordinates.GetZString()}";
        }
    }
    
    /// <summary>
    /// Command which changes the amount of damage the worlder border does
    /// </summary>
    public class WorldborderDamageAmountCommand : BaseCommand
    {
        private double damagePerBlock;

        /// <summary>
        /// Intializes a new <see cref="WorldborderDamageAmountCommand"/>
        /// </summary>
        /// <param name="damagePerBlock">The amount of damage the border should do per block per second</param>
        public WorldborderDamageAmountCommand(double damagePerBlock)
        {
            DamagePerBlock = damagePerBlock;
        }

        /// <summary>
        /// The amount of damage the border should do per block per second
        /// </summary>
        public double DamagePerBlock 
        { 
            get => damagePerBlock;
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(DamagePerBlock), "DamagePerBlock may not be less than 0");
                }
                damagePerBlock = value; 
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder damage amount [DamagePerBlock]</returns>
        public override string GetCommandString()
        {
            return $"worldborder damage amount {DamagePerBlock.ToMinecraftDouble()}";
        }
    }

    /// <summary>
    /// Command which changes the amount of blocks players can be inside the border before taking damage
    /// </summary>
    public class WorldborderDamageBufferCommand : BaseCommand
    {
        private double buffer;

        /// <summary>
        /// Intializes a new <see cref="WorldborderDamageBufferCommand"/>
        /// </summary>
        /// <param name="buffer">The amount of blocks the player has to be inside the border to take damage</param>
        public WorldborderDamageBufferCommand(double buffer)
        {
            Buffer = buffer;
        }

        /// <summary>
        /// The amount of blocks the player has to be inside the border to take damage
        /// </summary>
        public double Buffer 
        { 
            get => buffer;
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Buffer), "Buffer may not be less than 0");
                }
                buffer = value; 
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder damage buffer [Buffer]</returns>
        public override string GetCommandString()
        {
            return $"worldborder damage buffer {Buffer.ToMinecraftDouble()}";
        }
    }

    /// <summary>
    /// Command which returns the size of the world border
    /// </summary>
    public class WorldborderGetCommand : BaseCommand
    {
        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder get</returns>
        public override string GetCommandString()
        {
            return $"worldborder get";
        }
    }

    /// <summary>
    /// Command which changes the distance players have to be away from the border to be warned
    /// </summary>
    public class WorldborderWarningDistanceCommand : BaseCommand
    {
        private int distance;

        /// <summary>
        /// Intializes a new <see cref="WorldborderWarningDistanceCommand"/>
        /// </summary>
        /// <param name="distance">The distance the player has to be away from the border before they are being warned</param>
        public WorldborderWarningDistanceCommand(int distance)
        {
            Distance = distance;
        }

        /// <summary>
        /// The distance the player has to be away from the border before they are being warned
        /// </summary>
        public int Distance
        {
            get => distance;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Distance), "Distance may not be less than 0");
                }
                distance = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder warning distance [Distance]</returns>
        public override string GetCommandString()
        {
            return $"worldborder warning distance {Distance}";
        }
    }

    /// <summary>
    /// Command which changes how much time in advance the border should warn players when the border is shrinking
    /// </summary>
    public class WorldborderWarningTimeCommand : BaseCommand
    {
        private Time time = null!;

        /// <summary>
        /// Intializes an ew <see cref="WorldborderWarningTimeCommand"/>
        /// </summary>
        /// <param name="time">The amount of time in advance the border should warn players when the border is shrinking</param>
        public WorldborderWarningTimeCommand(Time time)
        {
            Time = time;
        }

        /// <summary>
        /// The amount of time in advance the border should warn players when the border is shrinking
        /// </summary>
        public Time Time 
        { 
            get => time;
            set 
            {
                if (value?.IsNegative() ?? throw new ArgumentNullException(nameof(Time), "Time may not be null"))
                {
                    throw new ArgumentOutOfRangeException(nameof(Time), "Time may not be negative");
                }
                time = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>worldborder warning time [Time]</returns>
        public override string GetCommandString()
        {
            return $"worldborder warning time {Time.AsTicks()}";
        }
    }
}
