using SharpCraft.Data;
using System;

namespace SharpCraft
{
    /// <summary>
    /// An object for rotations
    /// </summary>
    public class Rotation : IConvertableToDataArray
    {
        /// <summary>
        /// The rotation
        /// </summary>
        public double XRot, YRot;

        /// <summary>
        /// If the rotation is relative or not
        /// </summary>
        public bool RXRot, RYRot;

        /// <summary>
        /// Creates a new rotation object with the specified rotation
        /// </summary>
        /// <param name="xrot">the x rotation</param>
        /// <param name="yrot">the y rotation (goes from -90 (up) to 90 (down))</param>
        /// <param name="RelativeXRot">if the x rotation is relative or not</param>
        /// <param name="RelativeYRot">if the y rotation is relative or not</param>
        public Rotation(double xrot, double yrot,bool RelativeXRot = false, bool RelativeYRot = false)
        {
            XRot = xrot;
            YRot = yrot;
            RXRot = RelativeXRot;
            RYRot = RelativeYRot;
        }
        /// <summary>
        /// Creates a new rotation of the specified type
        /// </summary>
        /// <param name="Relative">If the whole rotation is relative or not</param>
        /// <param name="xrot">the x rotation</param>
        /// <param name="yrot">the y rotation (goes from -90 (up) to 90 (down))</param>
        public Rotation(bool Relative, double xrot, double yrot)
        {
            XRot = xrot;
            YRot = yrot;
            RXRot = Relative;
            RYRot = Relative;
        }

        /// <summary>
        /// Gets the raw rotation
        /// </summary>
        /// <returns>the raw rotation used by the game</returns>
        public override string ToString()
        {
            string TempString = "";
            if (RXRot) { TempString = "~"; }
            TempString += XRot.ToString().Replace(",", ".") + " ";
            if (RYRot) { TempString += "~"; }
            TempString += YRot.ToString().Replace(",", ".");
            return TempString;
        }

        /// <summary>
        /// Converts this rotation into a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">The type of array</param>
        /// <returns>the made <see cref="DataPartArray"/></returns>
        public DataPartArray GetAsArray(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagDoubleArray)
            {
                DataPartArray dataArray = new DataPartArray(new double[] { XRot, YRot }, null, null);
                return dataArray;
            }
            else
            {
                throw new ArgumentException("Can only convert the rotation in a double array");
            }
        }
    }
}
