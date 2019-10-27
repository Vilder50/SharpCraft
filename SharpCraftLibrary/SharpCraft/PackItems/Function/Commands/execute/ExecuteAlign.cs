using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for aligning "execute at coordinates" to the exact coordinates of the block the the execute at coordinates are refering to
    /// </summary>
    public class ExecuteAlign : BaseExecuteCommand
    {
        private bool alignX;
        private bool alignY;
        private bool alignZ;

        /// <summary>
        /// Intializes a new <see cref="ExecuteAlign"/> command
        /// </summary>
        /// <param name="alignX">If it should align in the x direction</param>
        /// <param name="alignY">If it should align in the y direction</param>
        /// <param name="alignZ">If it should align in the z direction</param>
        public ExecuteAlign(bool alignX, bool alignY, bool alignZ)
        {
            this.alignX = alignX;
            this.alignY = alignY;
            this.alignZ = alignZ;
            ValidateAlignment(AlignX, AlignY, AlignZ);
        }

        /// <summary>
        /// Intializes a new <see cref="ExecuteAlign"/> command which aligns in all directions
        /// </summary>
        public ExecuteAlign()
        {
            alignX = true;
            alignY = true;
            alignZ = true;
        }

        /// <summary>
        /// If it should align in the x direction
        /// </summary>
        public bool AlignX 
        { 
            get => alignX;
            set 
            {
                ValidateAlignment(value, AlignY, AlignZ);
                alignX = value;
            }
        }

        /// <summary>
        /// If it should align in the y direction
        /// </summary>
        public bool AlignY 
        { 
            get => alignY;
            set
            {
                ValidateAlignment(AlignX, value, AlignZ);
                alignY = value;
            }
        }

        /// <summary>
        /// If it should align in the z direction
        /// </summary>
        public bool AlignZ 
        { 
            get => alignZ;
            set
            {
                ValidateAlignment(AlignX, AlignY, value);
                alignZ = value;
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>align [xyz]</returns>
        protected override string GetExecutePart()
        {
            string command = "align ";
            if (AlignX)
            {
                command += "x";
            }
            if (AlignY)
            {
                command += "y";
            }
            if (AlignZ)
            {
                command += "z";
            }
            return command;
        }

        private void ValidateAlignment(bool x, bool y , bool z)
        {
            if (!x && !y && !z)
            {
                throw new ArgumentException("All axes may not be false (No reason to use execute align then)");
            }
        }
    }
}
