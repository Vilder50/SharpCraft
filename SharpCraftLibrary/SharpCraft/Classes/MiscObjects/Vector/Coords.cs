using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for relative and world coordinates
    /// </summary>
    public class Coords : Vector
    {
        /// <summary>
        /// Intializes a new <see cref="Coords"/>
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="z">The Z coordinate</param>
        /// <param name="relativeX">If the X coordinate should be relative</param>
        /// <param name="relativeY">If the Y coordinate should be relative</param>
        /// <param name="relativeZ">If the Z coordinate should be relative</param>
        public Coords(double x, double y, double z, bool relativeX = true, bool relativeY = true, bool relativeZ = true) : base(x, y, z)
        {
            RelativeX = relativeX;
            RelativeY = relativeY;
            RelativeZ = relativeZ;
        }

        /// <summary>
        /// Intializes a new relative <see cref="Coords"/> (same as ~ ~ ~)
        /// </summary>
        public Coords() : this(0, 0, 0, true, true, true)
        {

        }

        /// <summary>
        /// Intializes a new <see cref="Coords"/>
        /// </summary>
        /// <param name="number">The number in the x,y and z direction</param>
        /// <param name="relative">If the coordinates should be relative</param>
        public Coords(double number, bool relative = true) : this(number,number,number,relative,relative,relative)
        {

        }

        /// <summary>
        /// Intializes a new <see cref="Coords"/>
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        /// <param name="z">The Z coordinate</param>
        /// <param name="relative">If the coordinates should be relative</param>
        public Coords(bool relative, double x, double y, double z) : base(x, y, z)
        {
            RelativeX = relative;
            RelativeY = relative;
            RelativeZ = relative;
        }

        /// <summary>
        /// If the X coordinate should be relative
        /// </summary>
        public bool RelativeX { get; protected set; }

        /// <summary>
        /// If the Y coordinate should be relative
        /// </summary>
        public bool RelativeY { get; protected set; }

        /// <summary>
        /// If the Z coordinate should be relative
        /// </summary>
        public bool RelativeZ { get; protected set; }

        /// <summary>
        /// Get's the string for the X coordinate
        /// </summary>
        /// <returns>The string for the X coordinate</returns>
        public override string GetXString()
        {
            return GetCoordString(X, RelativeX);
        }

        /// <summary>
        /// Get's the string for the Y coordinate
        /// </summary>
        /// <returns>The string for the Y coordinate</returns>
        public override string GetYString()
        {
            return GetCoordString(Y, RelativeY);
        }

        /// <summary>
        /// Get's the string for the Z coordinate
        /// </summary>
        /// <returns>The string for the Z coordinate</returns>
        public override string GetZString()
        {
            return GetCoordString(Z, RelativeZ);
        }

        /// <summary>
        /// The type of coordinate
        /// </summary>
        public override ID.CoordType CoordType
        {
            get
            {
                if (RelativeX && RelativeY && RelativeZ)
                {
                    return ID.CoordType.Relative;
                }
                else if (!RelativeX && !RelativeY && !RelativeZ)
                {
                    return ID.CoordType.Normal;
                }
                else
                {
                    return ID.CoordType.Mixed;
                }
            }
        }

        /// <summary>
        /// Checks if the given <see cref="Coords"/> has the same coordinates relative as this coords
        /// </summary>
        /// <param name="coords">The coords to check</param>
        /// <returns>True if they are relative in the same places</returns>
        public bool SameRelativeCoords(Coords coords)
        {
            return coords.RelativeX == RelativeX && coords.RelativeY == RelativeY && coords.RelativeZ == RelativeZ;
        }

        private string GetCoordString(double number, bool relative)
        {
            string prefix = "";
            if (relative)
            {
                prefix = "~";
                if (number == 0)
                {
                    return prefix;
                }
            }
            string numberPart = number.ToMinecraftDouble();
            if (number > 0 && number < 1)
            {
                numberPart = numberPart.Substring(1);
            }
            if (number < 0 && number > -1)
            {
                numberPart = "-" + numberPart.Substring(2);
            }
            return prefix + numberPart;
        }

        #region operators
        /// <summary>
        /// Adds the vector to the coords
        /// </summary>
        /// <param name="coords">The coords to add to</param>
        /// <param name="vector">The vector to add</param>
        /// <returns>The vector added to the coords</returns>
        public static Coords operator +(Coords coords, Vector vector)
        {
            return new Coords(coords.X + vector.X, coords.Y + vector.Y, coords.Z + vector.Z, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }

        /// <summary>
        /// subtracts the vector from the coords
        /// </summary>
        /// <param name="coords">The coords to subtract from</param>
        /// <param name="vector">The vector to subtract</param>
        /// <returns>The vector subtracted from the coords</returns>
        public static Coords operator -(Coords coords, Vector vector)
        {
            return new Coords(coords.X - vector.X, coords.Y - vector.Y, coords.Z - vector.Z, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }

        /// <summary>
        /// multiplies the coords with the vector
        /// </summary>
        /// <param name="coords">The coords to multiply</param>
        /// <param name="vector">The vector to multiply with</param>
        /// <returns>The coords multiplied with the vector</returns>
        public static Coords operator *(Coords coords, Vector vector)
        {
            return new Coords(coords.X * vector.X, coords.Y * vector.Y, coords.Z * vector.Z, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }

        /// <summary>
        /// divides the coords with the vector
        /// </summary>
        /// <param name="coords">The coords to divide</param>
        /// <param name="vector">The vector to divide with</param>
        /// <returns>The coords divided by the vector</returns>
        public static Coords operator /(Coords coords, Vector vector)
        {
            return new Coords(coords.X / vector.X, coords.Y / vector.Y, coords.Z / vector.Z, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }

        /// <summary>
        /// multiplies the coords with the number
        /// </summary>
        /// <param name="coords">The coords to multiply</param>
        /// <param name="number">The number to multiply with</param>
        /// <returns>The coords multiplied with the number</returns>
        public static Coords operator *(Coords coords, double number)
        {
            return new Coords(coords.X * number, coords.Y * number, coords.Z * number, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }

        /// <summary>
        /// divides the coords with the number
        /// </summary>
        /// <param name="coords">The coords to divide</param>
        /// <param name="number">The number to divide with</param>
        /// <returns>The coords divided with the number</returns>
        public static Coords operator /(Coords coords, double number)
        {
            return new Coords(coords.X / number, coords.Y / number, coords.Z / number, coords.RelativeX, coords.RelativeY, coords.RelativeZ);
        }
        #endregion
    }
}
