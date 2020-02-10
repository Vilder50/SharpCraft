using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which makes particles appear
    /// </summary>
    public class ParticleNormalCommand : BaseCommand
    {
        private Vector displayCoords;
        private Vector size;
        private double speed;
        private int count;

        /// <summary>
        /// Intializes a new <see cref="ParticleNormalCommand"/>
        /// </summary>
        /// <param name="particle">The particles to display</param>
        /// <param name="displayCoords">The coordinates to display the particles at</param>
        /// <param name="size">The size to each side the particles can spawn in</param>
        /// <param name="speed">The speed of the particles</param>
        /// <param name="count">The amount of particles</param>
        /// <param name="force">True if particles always should be shown. False if particles shouldn't</param>
        /// <param name="selector">Selector selecting players to show the particles to. Leave null to show particles to everyone</param>
        public ParticleNormalCommand(ID.Particle particle, Vector displayCoords, Vector size, double speed, int count, bool force, BaseSelector selector)
        {
            Particle = particle;
            DisplayCoords = displayCoords;
            Size = size;
            Speed = speed;
            Count = count;
            Force = force;
            Selector = selector;
        }

        /// <summary>
        /// The particles to display
        /// </summary>
        public ID.Particle Particle { get; set; }

        /// <summary>
        /// The coordinates to display the particles at
        /// </summary>
        public Vector DisplayCoords { get => displayCoords; set => displayCoords = value ?? throw new ArgumentNullException(nameof(DisplayCoords), "DisplayCoords may not be null."); }

        /// <summary>
        /// The size to each side the particles can spawn in
        /// </summary>
        public Vector Size { get => size; set => size = value ?? throw new ArgumentNullException(nameof(Size), "Size may not be null."); }

        /// <summary>
        /// The speed of the particles
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Speed), "Speed may not be less than 0");
                }
                speed = value;
            }
        }

        /// <summary>
        /// The amount of particles
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 0");
                }
                count = value;
            }
        }

        /// <summary>
        /// True if particles always should be shown. False if particles shouldn't
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Selector selecting players to show the particles to. Leave null to show particles to everyone
        /// </summary>
        public BaseSelector Selector { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>particle [Particle] [DisplayCoords] [Size] [Speed] [Count] [Force] [Selector]</returns>
        public override string GetCommandString()
        {
            if (Selector is null)
            {
                if (!Force)
                {
                    return $"particle {Particle} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count}";
                }
                else
                {
                    return $"particle {Particle} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")}";
                }
            }
            else
            {
                return $"particle {Particle} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")} {Selector.GetSelectorString()}";
            }
        }
    }

    /// <summary>
    /// Command which makes colored dust particles appear
    /// </summary>
    public class ParticleColoredDustCommand : BaseCommand
    {
        private Vector displayCoords;
        private Vector size;
        private double speed;
        private int count;
        private RGBColor color;
        private double particleSize;

        /// <summary>
        /// Intializes a new <see cref="ParticleColoredDustCommand"/>
        /// </summary>
        /// <param name="color">The color of the particles</param>
        /// <param name="particleSize">The size of the particles</param>
        /// <param name="displayCoords">The coordinates to display the particles at</param>
        /// <param name="size">The size to each side the particles can spawn in</param>
        /// <param name="speed">The speed of the particles</param>
        /// <param name="count">The amount of particles</param>
        /// <param name="force">True if particles always should be shown. False if particles shouldn't</param>
        /// <param name="selector">Selector selecting players to show the particles to. Leave null to show particles to everyone</param>
        public ParticleColoredDustCommand(RGBColor color, double particleSize, Vector displayCoords, Vector size, double speed, int count, bool force, BaseSelector selector)
        {
            Color = color;
            ParticleSize = particleSize;
            DisplayCoords = displayCoords;
            Size = size;
            Speed = speed;
            Count = count;
            Force = force;
            Selector = selector;
        }

        /// <summary>
        /// The color of the particles
        /// </summary>
        public RGBColor Color { get => color; set => color = value ?? throw new ArgumentNullException(nameof(Color), "Color may not be null."); }

        /// <summary>
        /// The size of the particles
        /// </summary>
        public double ParticleSize
        {
            get => particleSize;
            set
            {
                if (value < 0 || value > 4)
                {
                    throw new ArgumentOutOfRangeException(nameof(ParticleSize), "ParticleSize may not be less than 0 or higher than 4");
                }
                particleSize = value;
            }
        }

        /// <summary>
        /// The coordinates to display the particles at
        /// </summary>
        public Vector DisplayCoords { get => displayCoords; set => displayCoords = value ?? throw new ArgumentNullException(nameof(DisplayCoords), "DisplayCoords may not be null."); }

        /// <summary>
        /// The size to each side the particles can spawn in
        /// </summary>
        public Vector Size { get => size; set => size = value ?? throw new ArgumentNullException(nameof(Size), "Size may not be null."); }

        /// <summary>
        /// The speed of the particles
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Speed), "Speed may not be less than 0");
                }
                speed = value;
            }
        }

        /// <summary>
        /// The amount of particles
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 0");
                }
                count = value;
            }
        }

        /// <summary>
        /// True if particles always should be shown. False if particles shouldn't
        /// </summary>
        public bool Force { get; set; }


        /// <summary>
        /// Selector selecting players to show the particles to. Leave null to show particles to everyone
        /// </summary>
        public BaseSelector Selector { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>particle dust [Color] [ParticleSize] [DisplayCoords] [Size] [Speed] [Count] [Force] [Selector]</returns>
        public override string GetCommandString()
        {
            string colorString = (decimal.Divide(color.Red, 255).ToString().Replace(",", ".")) + " " + (decimal.Divide(color.Green, 255).ToString().Replace(",", ".")) + " " + (decimal.Divide(color.Blue, 255).ToString().Replace(",", "."));

            if (Selector is null)
            {
                if (!Force)
                {
                    return $"particle dust {colorString} {ParticleSize.ToMinecraftDouble()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count}";
                }
                else
                {
                    return $"particle dust {colorString} {ParticleSize.ToMinecraftDouble()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")}";
                }
            }
            else
            {
                return $"particle dust {colorString} {ParticleSize.ToMinecraftDouble()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")} {Selector.GetSelectorString()}";
            }
        }
    }

    /// <summary>
    /// Command which makes block particles appear
    /// </summary>
    public class ParticleBlockCommand : BaseCommand
    {
        private Vector displayCoords;
        private Vector size;
        private double speed;
        private int count;
        private Block block;

        /// <summary>
        /// Intializes a new <see cref="ParticleBlockCommand"/>
        /// </summary>
        /// <param name="displayCoords">The coordinates to display the particles at</param>
        /// <param name="size">The size to each side the particles can spawn in</param>
        /// <param name="speed">The speed of the particles</param>
        /// <param name="count">The amount of particles</param>
        /// <param name="force">True if particles always should be shown. False if particles shouldn't</param>
        /// <param name="selector">Selector selecting players to show the particles to. Leave null to show particles to everyone</param>
        /// <param name="asBlockDust">True if the particles should be in dust form. False if they should be squares</param>
        /// <param name="block">The block the particles should look like</param>
        public ParticleBlockCommand(Block block, Vector displayCoords, Vector size, double speed, int count, bool asBlockDust, bool force, BaseSelector selector)
        {
            Block = block;
            AsBlockDust = asBlockDust;
            DisplayCoords = displayCoords;
            Size = size;
            Speed = speed;
            Count = count;
            Force = force;
            Selector = selector;
        }

        /// <summary>
        /// The block the particles should look like
        /// </summary>
        public Block Block { get => block; set => block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null"); }

        /// <summary>
        /// The coordinates to display the particles at
        /// </summary>
        public Vector DisplayCoords { get => displayCoords; set => displayCoords = value ?? throw new ArgumentNullException(nameof(DisplayCoords), "DisplayCoords may not be null."); }

        /// <summary>
        /// The size to each side the particles can spawn in
        /// </summary>
        public Vector Size { get => size; set => size = value ?? throw new ArgumentNullException(nameof(Size), "Size may not be null."); }

        /// <summary>
        /// The speed of the particles
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Speed), "Speed may not be less than 0");
                }
                speed = value;
            }
        }

        /// <summary>
        /// The amount of particles
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 0");
                }
                count = value;
            }
        }

        /// <summary>
        /// True if the particles should be in dust form. False if they should be squares
        /// </summary>
        public bool AsBlockDust { get; set; }

        /// <summary>
        /// True if particles always should be shown. False if particles shouldn't
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Selector selecting players to show the particles to. Leave null to show particles to everyone
        /// </summary>
        public BaseSelector Selector { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>particle [AsBlockDust] [Block] [DisplayCoords] [Size] [Speed] [Count] [Force] [Selector]</returns>
        public override string GetCommandString()
        {
            string particleType = AsBlockDust ? "falling_dust" : "block";
            if (Selector is null)
            {
                if (!Force)
                {
                    return $"particle {particleType} {Block.GetBlockPlacementString()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count}";
                }
                else
                {
                    return $"particle {particleType} {Block.GetBlockPlacementString()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")}";
                }
            }
            else
            {
                return $"particle {particleType} {Block.GetBlockPlacementString()} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")} {Selector.GetSelectorString()}";
            }
        }
    }

    /// <summary>
    /// Command which makes item particles appear
    /// </summary>
    public class ParticleItemCommand : BaseCommand
    {
        private Vector displayCoords;
        private Vector size;
        private double speed;
        private int count;
        private Item item;

        /// <summary>
        /// Intializes a new <see cref="ParticleItemCommand"/>
        /// </summary>
        /// <param name="displayCoords">The coordinates to display the particles at</param>
        /// <param name="size">The size to each side the particles can spawn in</param>
        /// <param name="speed">The speed of the particles</param>
        /// <param name="count">The amount of particles</param>
        /// <param name="force">True if particles always should be shown. False if particles shouldn't</param>
        /// <param name="selector">Selector selecting players to show the particles to. Leave null to show particles to everyone</param>
        /// <param name="item">The item the particles should look like</param>
        public ParticleItemCommand(Item item, Vector displayCoords, Vector size, double speed, int count, bool force, BaseSelector selector)
        {
            Item = item;
            DisplayCoords = displayCoords;
            Size = size;
            Speed = speed;
            Count = count;
            Force = force;
            Selector = selector;
        }

        /// <summary>
        /// The item the particles should look like
        /// </summary>
        public Item Item { get => item; set => item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null"); }

        /// <summary>
        /// The coordinates to display the particles at
        /// </summary>
        public Vector DisplayCoords { get => displayCoords; set => displayCoords = value ?? throw new ArgumentNullException(nameof(DisplayCoords), "DisplayCoords may not be null."); }

        /// <summary>
        /// The size to each side the particles can spawn in
        /// </summary>
        public Vector Size { get => size; set => size = value ?? throw new ArgumentNullException(nameof(Size), "Size may not be null."); }

        /// <summary>
        /// The speed of the particles
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Speed), "Speed may not be less than 0");
                }
                speed = value;
            }
        }

        /// <summary>
        /// The amount of particles
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 0");
                }
                count = value;
            }
        }

        /// <summary>
        /// True if particles always should be shown. False if particles shouldn't
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Selector selecting players to show the particles to. Leave null to show particles to everyone
        /// </summary>
        public BaseSelector Selector { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>particle [AsBlockDust] [Block] [DisplayCoords] [Size] [Speed] [Count] [Force] [Selector]</returns>
        public override string GetCommandString()
        {
            if (Selector is null)
            {
                if (!Force)
                {
                    return $"particle item {item.IDDataString} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count}";
                }
                else
                {
                    return $"particle item {item.IDDataString} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")}";
                }
            }
            else
            {
                return $"particle item {item.IDDataString} {DisplayCoords.GetVectorString()} {Size.X.ToMinecraftDouble()} {Size.Y.ToMinecraftDouble()} {Size.Z.ToMinecraftDouble()} {Speed.ToMinecraftDouble()} {Count} {(Force ? "force" : "normal")} {Selector.GetSelectorString()}";
            }
        }
    }
}
