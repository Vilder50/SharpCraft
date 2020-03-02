using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Class for vectors
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// Intializes a new <see cref="Vector"/>
        /// </summary>
        /// <param name="x">The X part of the vector</param>
        /// <param name="y">The Y part of the vector</param>
        /// <param name="z">The Z part of the vector</param>
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Intializes a new <see cref="Vector"/>
        /// </summary>
        /// <param name="number">The number in the x,y and z direction</param>
        public Vector(double number) : this(number, number, number)
        {

        }

        /// <summary>
        /// The X part of the vector
        /// </summary>
        public double X { get; protected set; }

        /// <summary>
        /// The Y part of the vector
        /// </summary>
        public double Y { get; protected set; }

        /// <summary>
        /// The Z part of the vector
        /// </summary>
        public double Z { get; protected set; }

        /// <summary>
        /// Gets the vector as a string
        /// </summary>
        /// <returns>The vector as a string</returns>
        public virtual string GetVectorString()
        {
            return $"{GetXString()} {GetYString()} {GetZString()}";
        }

        /// <summary>
        /// Get's the string for the X part of the vector
        /// </summary>
        /// <returns>The string for the X part of the vector</returns>
        public virtual string GetXString()
        {
            return X.ToMinecraftDouble();
        }

        /// <summary>
        /// Get's the string for the Y part of the vector
        /// </summary>
        /// <returns>The string for the Y part of the vector</returns>
        public virtual string GetYString()
        {
            return Y.ToMinecraftDouble();
        }

        /// <summary>
        /// Get's the string for the Z part of the vector
        /// </summary>
        /// <returns>The string for the Z part of the vector</returns>
        public virtual string GetZString()
        {
            return Z.ToMinecraftDouble();
        }

        /// <summary>
        /// Gets the vector as a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="asType">Not used</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>The vector as a <see cref="DataPartArray"/></returns>
        public virtual DataPartArray GetAsArray(ID.NBTTagType? asType, object[] extraConversionData)
        {
            return new DataPartArray(new double[] { X, Y, Z }, null, new object?[0]);
        }

        /// <summary>
        /// Converts this vector into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: x path name, 1: y path name, 2: z path name, 3: if json</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public virtual DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (!(conversionData.Length == 3 || conversionData.Length == 4))
            {
                throw new ArgumentException("There has to be 3-4 conversion params to convert a coordinate to a data object.");
            }

            bool json = false;
            if (conversionData.Length == 4 && conversionData[3] is bool isJson)
            {
                json = isJson;
            }

            if (conversionData[0] is string xName && conversionData[1] is string yName && conversionData[2] is string zName)
            {
                DataPartObject dataObject = new DataPartObject();
                dataObject.AddValue(new DataPartPath(xName, new DataPartTag(X, isJson: json), json));
                dataObject.AddValue(new DataPartPath(yName, new DataPartTag(Y, isJson: json), json));
                dataObject.AddValue(new DataPartPath(zName, new DataPartTag(Z, isJson: json), json));
                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 3 first conversion params has be be strings.");
            }
        }

        /// <summary>
        /// The type of coordinate
        /// </summary>
        public virtual ID.CoordType CoordType { get => ID.CoordType.Vector; }

        /// <summary>
        /// A static coordinate which is positive in x
        /// </summary>
        public static readonly Vector PositiveX = new Vector(1, 0, 0);

        /// <summary>
        /// A static coordinate which is positive in x
        /// </summary>
        public static readonly Vector NegativeX = new Vector(-1, 0, 0);

        /// <summary>
        /// A static coordinate which is positive in y
        /// </summary>
        public static readonly Vector PositiveY = new Vector(0, 1, 0);

        /// <summary>
        /// A static coordinate which is positive in y
        /// </summary>
        public static readonly Vector NegativeY = new Vector(0, -1, 0);

        /// <summary>
        /// A static coordinate which is positive in z
        /// </summary>
        public static readonly Vector PositiveZ = new Vector(0, 0, 1);

        /// <summary>
        /// A static coordinate which is positive in z
        /// </summary>
        public static readonly Vector NegativeZ = new Vector(0, 0, -1);

        /// <summary>
        /// Returns a direction based on the given number
        /// </summary>
        /// <param name="number">The number to get the direction for</param>
        /// <param name="ignoreAxis">If set it won't return directions on the given axis</param>
        /// <returns>A direction</returns>
        public static Vector NumberToDirection(int number, ID.Axis? ignoreAxis = null)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number may not be less than 0");
            }
            if (ignoreAxis is null)
            {
                if (number > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), "Number may not be higher than 5");
                }
            }
            else
            {
                if (number > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), "Number may not be higher than 3. (Since 4 and 5 is the ignored directions)");
                }
            }

            int getDirection = number;
            switch (ignoreAxis)
            {
                case ID.Axis.x:
                    getDirection += 2;
                    break;
                case ID.Axis.y:
                    if (number > 1)
                    {
                        getDirection += 2;
                    }
                    break;
            }

            switch (getDirection)
            {
                default:
                case 0:
                    return NegativeX;
                case 1:
                    return PositiveX;
                case 2:
                    return NegativeY;
                case 3:
                    return PositiveY;
                case 4:
                    return NegativeZ;
                case 5:
                    return PositiveZ;
            }
        }

        #region operator
        /// <summary>
        /// Adds the given vectors together
        /// </summary>
        /// <param name="vector1">One of the vectors to add</param>
        /// <param name="vector2">One of the vectors to add</param>
        /// <returns>The vectors added together</returns>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        /// <summary>
        /// subtracts the given vectors from each other
        /// </summary>
        /// <param name="vector1">The vector to subtract from</param>
        /// <param name="vector2">The vector to subtract</param>
        /// <returns>The vectors subtracted from each other</returns>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        /// <summary>
        /// Multiplies the given vectors together
        /// </summary>
        /// <param name="vector1">One of the vectors to multiply</param>
        /// <param name="vector2">One of the vectors to multiply</param>
        /// <returns>The vectors multiplied together</returns>
        public static Vector operator *(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);
        }

        /// <summary>
        /// divied the given vectors with each other
        /// </summary>
        /// <param name="vector1">The vector to divide</param>
        /// <param name="vector2">The vector to divide with</param>
        /// <returns>The vectors subtracted from each other</returns>
        public static Vector operator /(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X / vector2.X, vector1.Y / vector2.Y, vector1.Z / vector2.Z);
        }

        /// <summary>
        /// Multiplies the given vector with the given number
        /// </summary>
        /// <param name="vector">The vector to multiply</param>
        /// <param name="number">The number to multiply with</param>
        /// <returns>The multiplied vector</returns>
        public static Vector operator *(Vector vector, double number)
        {
            return new Vector(vector.X * number, vector.Y * number, vector.Z * number);
        }

        /// <summary>
        /// Divides the given vector with the given number
        /// </summary>
        /// <param name="vector">The vector to divide</param>
        /// <param name="number">The number to divide with</param>
        /// <returns>The divided vector</returns>
        public static Vector operator /(Vector vector, double number)
        {
            return new Vector(vector.X / number, vector.Y / number, vector.Z / number);
        }
        #endregion
    }

    /// <summary>
    /// Class for int vectors
    /// </summary>
    public class IntVector : Vector, IConvertableToDataArray, IConvertableToDataObject
    {
        /// <summary>
        /// Intializes a new <see cref="IntVector"/>
        /// </summary>
        /// <param name="x">The X part of the vector</param>
        /// <param name="y">The Y part of the vector</param>
        /// <param name="z">The Z part of the vector</param>
        public IntVector(int x, int y, int z) : base(x, y, z)
        {

        }

        /// <summary>
        /// Intializes a new <see cref="IntVector"/>
        /// </summary>
        /// <param name="number">The number in the x,y and z direction</param>
        public IntVector(int number) : base(number)
        {

        }

        /// <summary>
        /// Intializes a new <see cref="IntVector"/>
        /// </summary>
        /// <param name="vector">DoubleVector to convert into int vector</param>
        public IntVector(Vector vector) : base((int)vector.X, (int)vector.Y, (int)vector.Z)
        {

        }

        /// <summary>
        /// Gets the vector as a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="asType">Not used</param>
        /// <param name="extraConversionData">Not used</param>
        /// <returns>The vector as a <see cref="DataPartArray"/></returns>
        public override DataPartArray GetAsArray(ID.NBTTagType? asType, object?[] extraConversionData)
        {
            return new DataPartArray(new int[] { (int)X, (int)Y, (int)Z }, null, new object?[0]);
        }

        /// <summary>
        /// Converts this vector into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: x path name, 1: y path name, 2: z path name, 3: if json</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public override DataPartObject GetAsDataObject(object?[] conversionData)
        {
            if (conversionData is null)
            {
                throw new ArgumentNullException(nameof(conversionData), "ConversionData may not be null");
            }

            if (!(conversionData.Length == 3 || conversionData.Length == 4))
            {
                throw new ArgumentException("There has to be 3-4 conversion params to convert a coordinate to a data object.");
            }

            bool json = false;
            if (conversionData.Length == 4 && conversionData[3] is bool isJson)
            {
                json = isJson;
            }

            if (conversionData[0] is string xName && conversionData[1] is string yName && conversionData[2] is string zName)
            {
                DataPartObject dataObject = new DataPartObject();
                dataObject.AddValue(new DataPartPath(xName, new DataPartTag((int)X, isJson: json), json));
                dataObject.AddValue(new DataPartPath(yName, new DataPartTag((int)Y, isJson: json), json));
                dataObject.AddValue(new DataPartPath(zName, new DataPartTag((int)Z, isJson: json), json));
                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 3 first conversion params has be be strings.");
            }
        }
    }
}
