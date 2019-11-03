using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which changes one of the boolean gamerules
    /// </summary>
    public class GameruleSetBoolCommand : ICommand
    {
        /// <summary>
        /// Intializes a new <see cref="GameruleSetBoolCommand"/>
        /// </summary>
        /// <param name="gamerule">The gamerule to change</param>
        /// <param name="changeTo">The value to change to. Leave null to not do a change.</param>
        public GameruleSetBoolCommand(ID.BoolGamerule gamerule, bool? changeTo)
        {
            Gamerule = gamerule;
            ChangeTo = changeTo;
        }

        /// <summary>
        /// The gamerule to change
        /// </summary>
        public ID.BoolGamerule Gamerule { get; set; }

        /// <summary>
        /// The value to change to. Leave null to not do a change.
        /// </summary>
        public bool? ChangeTo { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>gamerule [Gamerule] [ChangeTo]</returns>
        public string GetCommandString()
        {
            if (ChangeTo is null)
            {
                return $"gamerule {Gamerule}";
            }
            else
            {
                return $"gamerule {Gamerule} {ChangeTo}";
            }
        }
    }

    /// <summary>
    /// Command which changes one of the integer gamerules
    /// </summary>
    public class GameruleSetIntCommand : ICommand
    {
        private int? changeTo;

        /// <summary>
        /// Intializes a new <see cref="GameruleSetIntCommand"/>
        /// </summary>
        /// <param name="gamerule">The gamerule to change</param>
        /// <param name="changeTo">The value to change to. Leave null to not do a change.</param>
        public GameruleSetIntCommand(ID.BoolGamerule gamerule, int? changeTo)
        {
            Gamerule = gamerule;
            ChangeTo = changeTo;
        }

        /// <summary>
        /// The gamerule to change
        /// </summary>
        public ID.BoolGamerule Gamerule { get; set; }

        /// <summary>
        /// The value to change to. Leave null to not do a change.
        /// </summary>
        public int? ChangeTo 
        {
            get => changeTo;
            set
            {
                if (!(value is null) && value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(ChangeTo), "ChangeTo may not be less than 0");
                }
                changeTo = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>gamerule [Gamerule] [ChangeTo]</returns>
        public string GetCommandString()
        {
            if (ChangeTo is null)
            {
                return $"gamerule {Gamerule}";
            }
            else
            {
                return $"gamerule {Gamerule} {ChangeTo}";
            }
        }
    }
}
