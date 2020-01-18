using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Class for local coordinates
    /// </summary>
    public class LocalCoords : Vector
    {
        /// <summary>
        /// Intializes a new <see cref="LocalCoords"/>
        /// </summary>
        /// <param name="x">The X local coordinate</param>
        /// <param name="y">The Y local coordinate</param>
        /// <param name="z">The Z local coordinate</param>
        public LocalCoords(double x, double y, double z) : base(x, y, z)
        {

        }

        /// <summary>
        /// Intializes a new <see cref="LocalCoords"/> (same as ^ ^ ^)
        /// </summary>
        public LocalCoords() : base(0,0,0)
        {

        }

        /// <summary>
        /// Get's the string for the X local coordinate
        /// </summary>
        /// <returns>The string for the X local coordinate</returns>
        public override string GetXString()
        {
            return GetCoordString(X);
        }

        /// <summary>
        /// Get's the string for the Y local coordinate
        /// </summary>
        /// <returns>The string for the Y local coordinate</returns>
        public override string GetYString()
        {
            return GetCoordString(Y);
        }

        /// <summary>
        /// Get's the string for the Z local coordinate
        /// </summary>
        /// <returns>The string for the Z local coordinate</returns>
        public override string GetZString()
        {
            return GetCoordString(Z);
        }

        /// <summary>
        /// The type of coordinate
        /// </summary>
        public override ID.CoordType CoordType { get => ID.CoordType.Local; }

        private string GetCoordString(double number)
        {
            if (number == 0)
            {
                return "^";
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
            return "^" + numberPart;
        }

        #region operators
        /// <summary>
        /// Adds the vector to the coords
        /// </summary>
        /// <param name="coords">The coords to add to</param>
        /// <param name="vector">The vector to add</param>
        /// <returns>The vector added to the coords</returns>
        public static LocalCoords operator +(LocalCoords coords, Vector vector)
        {
            return new LocalCoords(coords.X + vector.X, coords.Y + vector.Y, coords.Z + vector.Z);
        }

        /// <summary>
        /// subtracts the vector from the coords
        /// </summary>
        /// <param name="coords">The coords to subtract from</param>
        /// <param name="vector">The vector to subtract</param>
        /// <returns>The vector subtracted from the coords</returns>
        public static LocalCoords operator -(LocalCoords coords, Vector vector)
        {
            return new LocalCoords(coords.X - vector.X, coords.Y - vector.Y, coords.Z - vector.Z);
        }

        /// <summary>
        /// multiplies the coords with the vector
        /// </summary>
        /// <param name="coords">The coords to multiply</param>
        /// <param name="vector">The vector to multiply with</param>
        /// <returns>The coords multiplied with the vector</returns>
        public static LocalCoords operator *(LocalCoords coords, Vector vector)
        {
            return new LocalCoords(coords.X * vector.X, coords.Y * vector.Y, coords.Z * vector.Z);
        }

        /// <summary>
        /// divides the coords with the vector
        /// </summary>
        /// <param name="coords">The coords to divide</param>
        /// <param name="vector">The vector to divide with</param>
        /// <returns>The coords divided by the vector</returns>
        public static LocalCoords operator /(LocalCoords coords, Vector vector)
        {
            return new LocalCoords(coords.X / vector.X, coords.Y / vector.Y, coords.Z / vector.Z);
        }

        /// <summary>
        /// multiplies the coords with the number
        /// </summary>
        /// <param name="coords">The coords to multiply</param>
        /// <param name="number">The number to multiply with</param>
        /// <returns>The coords multiplied with the number</returns>
        public static LocalCoords operator *(LocalCoords coords, double number)
        {
            return new LocalCoords(coords.X * number, coords.Y * number, coords.Z * number);
        }

        /// <summary>
        /// divides the coords with the number
        /// </summary>
        /// <param name="coords">The coords to divide</param>
        /// <param name="number">The number to divide with</param>
        /// <returns>The coords divided with the number</returns>
        public static LocalCoords operator /(LocalCoords coords, double number)
        {
            return new LocalCoords(coords.X / number, coords.Y / number, coords.Z / number);
        }
        #endregion
    }
}
