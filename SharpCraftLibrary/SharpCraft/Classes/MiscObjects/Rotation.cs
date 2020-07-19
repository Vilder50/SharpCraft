using SharpCraft.Data;
using System;

namespace SharpCraft
{
    /// <summary>
    /// An object for rotations
    /// </summary>
    public class Rotation : IConvertableToDataArray<double>
    {
        private double y;
        private bool yRelative;

        private double x;
        private bool xRelative;

        /// <summary>
        /// Creates a new rotation object with the specified rotation
        /// </summary>
        /// <param name="xRotation">The vertical rotation</param>
        /// <param name="yRotation">The horizontal rotation</param>
        /// <param name="xRelative">if the vertical rotation is relative or not</param>
        /// <param name="yRelative">if the horizontal rotation is relative or not</param>
        public Rotation(double yRotation, double xRotation, bool yRelative = false, bool xRelative = false)
        {
            X = xRotation;
            Y = yRotation;
            this.xRelative = xRelative;
            this.yRelative = yRelative;
        }
        /// <summary>
        /// Creates a new rotation of the specified type
        /// </summary>
        /// <param name="relative">If the whole rotation is relative or not</param>
        /// <param name="xRotation">The vertical rotation</param>
        /// <param name="yRotation">The horizontal rotation</param>
        public Rotation(bool relative, double yRotation, double xRotation) : this(yRotation, xRotation, relative, relative)
        {
            
        }

        /// <summary>
        /// The vertical rotation
        /// </summary>
        [ArrayPath(1)]
        public double X { get => x; set => x = value; }

        /// <summary>
        /// The horizontal rotation
        /// </summary>
        [ArrayPath(0)]
        public double Y { get => y; set => y = value; }

        /// <summary>
        /// If the Horizontal rotation is relative or not
        /// </summary>
        public bool XRelative { get => xRelative; set => xRelative = value; }

        /// <summary>
        /// If the Vertical rotation is relative or not
        /// </summary>
        public bool YRelative { get => yRelative; set => yRelative = value; }

        /// <summary>
        /// Gets the raw rotation
        /// </summary>
        /// <returns>the raw rotation used by the game</returns>
        public string GetRotationString()
        {
            return GetRotationString(Y,YRelative) + " " + GetRotationString(X, XRelative);
        }

        private string GetRotationString(double number, bool relative)
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

        /// <summary>
        /// Converts this rotation into a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">The type of array</param>
        /// <returns>the made <see cref="DataPartArray"/></returns>
        public DataPartArray GetAsArray(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagDoubleArray)
            {
                DataPartArray dataArray = new DataPartArray(new double[] { Y, X }, null, new object?[0]);
                return dataArray;
            }
            else if(asType == ID.NBTTagType.TagFloatArray)
            {
                DataPartArray dataArray = new DataPartArray(new float[] { (float)Y, (float)X }, null, new object?[0]);
                return dataArray;
            }
            else
            {
                throw new ArgumentException("Can only convert the rotation in a double array");
            }
        }

        /// <summary>
        /// Used for getting the datapath for this array. Method throws an exception if called.
        /// </summary>
        /// <returns>An object to continue the datapath on</returns>
        [PathArrayGetter]
        public double[] PathArray()
        {
            throw new PathGettingMethodCallException();
        }
    }
}
