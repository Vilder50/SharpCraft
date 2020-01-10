using System;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object for coordinates
    /// </summary>
    public class Coords : IConvertableToDataObject, IConvertableToDataArray
    {
        /// <summary>
        /// A static coordinate which is positive in x
        /// </summary>
        public static readonly Coords PositiveX = new Coords(ID.CoordType.Vector, 1, 0, 0);

        /// <summary>
        /// A static coordinate which is positive in x
        /// </summary>
        public static readonly Coords NegativeX = new Coords(ID.CoordType.Vector, -1, 0, 0);

        /// <summary>
        /// A static coordinate which is positive in y
        /// </summary>
        public static readonly Coords PositiveY = new Coords(ID.CoordType.Vector, 0, 1, 0);

        /// <summary>
        /// A static coordinate which is positive in y
        /// </summary>
        public static readonly Coords NegativeY = new Coords(ID.CoordType.Vector, 0, -1, 0);

        /// <summary>
        /// A static coordinate which is positive in z
        /// </summary>
        public static readonly Coords PositiveZ = new Coords(ID.CoordType.Vector, 0, 0, 1);

        /// <summary>
        /// A static coordinate which is positive in z
        /// </summary>
        public static readonly Coords NegativeZ = new Coords(ID.CoordType.Vector, 0, 0, -1);

        /// <summary>
        /// The coordinate
        /// </summary>
        public double X, Y, Z;

        /// <summary>
        /// If the coordinate is relative or not
        /// </summary>
        public bool RX, RY, RZ;

        /// <summary>
        /// If all the coordinates are local
        /// </summary>
        public ID.CoordType CoordType;

        /// <summary>
        /// Creates a coordinate with the given parameters
        /// Note that the coordinates are relative if nothing else is specified
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="z">The z coordinate</param>
        /// <param name="RelativeX">If the x coordinate is relative or not</param>
        /// <param name="RelativeY">If the y coordinate is relative or not</param>
        /// <param name="RelativeZ">If the z coordinate is relative or not</param>
        public Coords(double x, double y, double z, bool RelativeX = true, bool RelativeY = true, bool RelativeZ = true)
        {
            if (RelativeX == RelativeY && RelativeY == RelativeZ)
            {
                if (RelativeX)
                {
                    CoordType = ID.CoordType.Relative;
                }
                else
                {
                    CoordType = ID.CoordType.Normal;
                }
            }
            else
            {
                CoordType = ID.CoordType.Mixed;
            }
            X = x;
            Y = y;
            Z = z;
            RX = RelativeX;
            RY = RelativeY;
            RZ = RelativeZ;
        }

        /// <summary>
        /// Creates a coordinate of the specified type
        /// </summary>
        /// <param name="type">The type of coordinates</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="z">The z coordinate</param>
        public Coords(ID.CoordType type, double x, double y, double z)
        {
            CoordType = type;

            X = x;
            Y = y;
            Z = z;
            if (type == ID.CoordType.Relative)
            {
                RX = true;
                RY = true;
                RZ = true;
            }
            else if (type == ID.CoordType.Local)
            {
                RX = true;
                RY = true;
                RZ = true;
            }
        }

        /// <summary>
        /// Creates a relative coordinate with the coords 0,0,0
        /// </summary>
        public Coords()
        {
            RX = true;
            RY = true;
            RZ = true;
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// Gets the raw coordinate
        /// </summary>
        /// <returns>the raw coordinate used by the game</returns>
        public string GetCoordString()
        {
            string TempString = "";
            if (RX) { TempString = "~"; }
            TempString += X.ToString().Replace(",", ".") + " ";
            if (RY) { TempString += "~"; }
            TempString += Y.ToString().Replace(",", ".") + " ";
            if (RZ) { TempString += "~"; }
            TempString += Z.ToString().Replace(",", ".");
            if (CoordType == ID.CoordType.Local) { return TempString.Replace("~", "^"); }
            return TempString;
        }

        /// <summary>
        /// Returns the raw x coordinate used by the game
        /// </summary>
        public string StringX
        {
            get
            {
                string TempString = "";
                if (RX) { TempString = CoordType == ID.CoordType.Local ? "^" : "~"; }
                TempString += X.ToString().Replace(",", ".");
                return TempString;
            }
        }

        /// <summary>
        /// Returns the raw y coordinate used by the game
        /// </summary>
        public string StringY
        {
            get
            {
                string TempString = "";
                if (RY) { TempString = CoordType == ID.CoordType.Local ? "^" : "~"; }
                TempString += Y.ToString().Replace(",", ".");
                return TempString;
            }
        }

        /// <summary>
        /// Returns the raw z coordinate used by the game
        /// </summary>
        public string StringZ
        {
            get
            {
                string TempString = "";
                if (RZ) { TempString = CoordType == ID.CoordType.Local ? "^" : "~"; }
                TempString += Z.ToString().Replace(",", ".");
                return TempString;
            }
        }

        /// <summary>
        /// Takes 2 coordinates and checks if they are of the same type
        /// </summary>
        /// <param name="coords1"></param>
        /// <param name="coords2"></param>
        /// <returns>If they are the same or not</returns>
        public static bool SameTypeCoords(Coords coords1, Coords coords2)
        {
            if (coords1.CoordType != coords2.CoordType) { return false; }
            if (coords1.RX != coords2.RX) { return false; }
            if (coords1.RY != coords2.RY) { return false; }
            if (coords1.RZ != coords2.RZ) { return false; }
            return true;
        }

        /// <summary>
        /// Adds the 2 coordinates together
        /// </summary>
        /// <param name="coords1">one of the coordinates</param>
        /// <param name="coords2">one of the coordinates</param>
        /// <returns><paramref name="coords1"/> added together with <paramref name="coords2"/></returns>
        public static Coords operator + (Coords coords1, Coords coords2)
        {
            if (!SameTypeCoords(coords1, coords2) || coords2.CoordType == ID.CoordType.Vector)
            {
                throw new ArgumentException("The coordinates aren't of the same type");
            }

            return new Coords(coords1.X + coords2.X, coords1.Y + coords2.Y, coords1.Z + coords2.Z, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// subtracts the 2 coordinates
        /// </summary>
        /// <param name="coords1">the coordinate to subtract from</param>
        /// <param name="coords2">the coordinate to subtract</param>
        /// <returns><paramref name="coords2"/> subtracted from <paramref name="coords1"/></returns>
        public static Coords operator - (Coords coords1, Coords coords2)
        {
            if (!SameTypeCoords(coords1, coords2) || coords2.CoordType == ID.CoordType.Vector)
            {
                throw new ArgumentException("The coordinates aren't of the same type");
            }

            return new Coords(coords1.X - coords2.X, coords1.Y - coords2.Y, coords1.Z - coords2.Z, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// divides the 2 coordinates
        /// </summary>
        /// <param name="coords1">the coordinate to divide</param>
        /// <param name="coords2">the coordinate used to divide with</param>
        /// <returns><paramref name="coords1"/> diveded by <paramref name="coords2"/></returns>
        public static Coords operator / (Coords coords1, Coords coords2)
        {
            if (!SameTypeCoords(coords1, coords2) || coords2.CoordType == ID.CoordType.Vector)
            {
                throw new ArgumentException("The coordinates aren't of the same type");
            }

            return new Coords(coords1.X / coords2.X, coords1.Y / coords2.Y, coords1.Z / coords2.Z, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// multiplies the 2 coordinates
        /// </summary>
        /// <param name="coords1">one of the coordinates</param>
        /// <param name="coords2">one of the coordinates</param>
        /// <returns><paramref name="coords1"/> diveded by <paramref name="coords2"/></returns>
        public static Coords operator * (Coords coords1, Coords coords2)
        {
            if (!SameTypeCoords(coords1, coords2) || coords2.CoordType == ID.CoordType.Vector)
            {
                throw new ArgumentException("The coordinates aren't of the same type");
            }

            return new Coords(coords1.X * coords2.X, coords1.Y * coords2.Y, coords1.Z * coords2.Z, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// multiplies <paramref name="coords1"/> with a number
        /// </summary>
        /// <param name="coords1">the coordinate</param>
        /// <param name="number">the number to multiply with</param>
        /// <returns><paramref name="coords1"/> multiplied by <paramref name="number"/></returns>
        public static Coords operator * (Coords coords1, double number)
        {
            return new Coords(coords1.X * number, coords1.Y * number, coords1.Z * number, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// divides <paramref name="coords1"/> with a number
        /// </summary>
        /// <param name="coords1">the coordinate</param>
        /// <param name="number">the number to divide with</param>
        /// <returns><paramref name="coords1"/> divied by <paramref name="number"/></returns>
        public static Coords operator / (Coords coords1, double number)
        {
            return new Coords(coords1.X / number, coords1.Y / number, coords1.Z / number, coords1.RX, coords1.RY, coords1.RZ)
            {
                CoordType = coords1.CoordType
            };
        }

        /// <summary>
        /// Checks if 2 coordinates are the same
        /// </summary>
        /// <param name="coords1">one of the coordinates</param>
        /// <param name="coords2">one of the coordinates</param>
        /// <returns>if they are the same or not</returns>
        public static bool operator == (Coords coords1, Coords coords2)
        {
            if (coords1 is null && coords2 is null) { return true; }
            if (coords1 is null && !(coords2 is null)) { return false; }
            if (coords2 is null && !(coords1 is null)) { return false; }
            if (!SameTypeCoords(coords1, coords2)) { return false; }
            if (coords1.X != coords2.X) { return false; }
            if (coords1.Y != coords2.Y) { return false; }
            if (coords1.Z != coords2.Z) { return false; }
            return true;
        }

        /// <summary>
        /// Checks if 2 coordinates aren't the same
        /// </summary>
        /// <param name="coords1">one of the coordinates</param>
        /// <param name="coords2">one of the coordinates</param>
        /// <returns>true if they aren't the same. false if they are the same</returns>
        public static bool operator != (Coords coords1, Coords coords2)
        {
            return !(coords1 == coords2);
        }

        /// <summary>
        /// Checks if the object is equal to this coordinate
        /// </summary>
        /// <param name="obj">the object</param>
        /// <returns>if they are equal</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Coords))
            {
                return false;
            }
            else
            {
                return this == (obj as Coords);
            }
        }

        /// <summary>
        /// Returns the hashcode of this instance
        /// </summary>
        /// <returns>the hashcode of this instance</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Takes a number and outputs a direction based of the number.
        /// 0: negative x.
        /// 1: positive x.
        /// 2: negative y.
        /// 3: positive y.
        /// 4: negative z.
        /// 5: positive z.
        /// </summary>
        /// <param name="number">the ID of the direction</param>
        /// <param name="ignoreAxis">If an axis is set it will be skipped in the number to direction list.</param>
        /// <returns>the chosen direction</returns>
        public static Coords NumberToDirection(int number, ID.Axis? ignoreAxis = null)
        {
            if (ignoreAxis != null)
            {
                if (number > 3)
                {
                    throw new ArgumentException("number cannot be higher than 3 since ignoreAxis is set which removes some of the coordinates from the list.");
                }
            }
            else
            {
                if (number > 5)
                {
                    throw new ArgumentException("number cannot be higher than 5.");
                }
            }
            switch(ignoreAxis)
            {
                case ID.Axis.x:
                    if (number < 2)
                    {
                        number += 2;
                    }
                    break;
                case ID.Axis.y:
                    if (number == 2 || number == 3)
                    {
                        number += 2;
                    }
                    break;
            }
            switch(number)
            {
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
                default:
                    return PositiveZ;
            }
        }

        /// <summary>
        /// Converts this coordinate into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: x path name, 1: y path name, 2: z path name, 3: type of the coord values, 4: if json</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (!(conversionData.Length == 4 || conversionData.Length == 5))
            {
                throw new ArgumentException("There has to be 4-5 conversion params to convert a coordinate to a data object.");
            }

            bool json = false;
            if (conversionData.Length == 4 && conversionData[4] is bool isJson)
            {
                json = isJson;
            }

            if (conversionData[0] is string xName && conversionData[1] is string yName && conversionData[2] is string zName && conversionData[3] is ID.NBTTagType forceType)
            {
                DataPartObject dataObject = new DataPartObject();
                if (forceType == ID.NBTTagType.TagInt)
                {
                    dataObject.AddValue(new DataPartPath(xName, new DataPartTag((int)X, isJson: json), json));
                    dataObject.AddValue(new DataPartPath(yName, new DataPartTag((int)Y, isJson: json), json));
                    dataObject.AddValue(new DataPartPath(zName, new DataPartTag((int)Z, isJson: json), json));
                }
                else
                {
                    throw new ArgumentException("Coord values cannot convert to the given type");
                }

                return dataObject;
            }
            else
            {
                throw new ArgumentException("The 3 conversion params has be be strings and 1 has to be an NBT enum to convert a coordinate to a data object.");
            }
        }

        /// <summary>
        /// Converts this coordinate into a <see cref="DataPartArray"/>
        /// </summary>
        /// <param name="extraConversionData">Not used</param>
        /// <param name="asType">The type of array</param>
        /// <returns>the made <see cref="DataPartArray"/></returns>
        public DataPartArray GetAsArray(ID.NBTTagType? asType, object[] extraConversionData)
        {
            if (asType == ID.NBTTagType.TagDoubleArray)
            {
                return new DataPartArray(new double[] { X, Y, Z }, null, null);
            }
            else
            {
                throw new ArgumentException("Can only convert the coordinate in a double array");
            }
        }
    }
}
