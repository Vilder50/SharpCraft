namespace SharpCraft
{
    /// <summary>
    /// An object for coordinates
    /// </summary>
    public class Coords
    {
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
        public bool LocalCoords = false;

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
                LocalCoords = true;
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
        public override string ToString()
        {
            string TempString = "";
            if (RX) { TempString = "~"; }
            TempString += X.ToString().Replace(",", ".") + " ";
            if (RY) { TempString += "~"; }
            TempString += Y.ToString().Replace(",", ".") + " ";
            if (RZ) { TempString += "~"; }
            TempString += Z.ToString().Replace(",", ".");
            if (LocalCoords == true) { return TempString.Replace("~", "^"); }
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
                if (RX) { TempString = "~"; }
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
                if (RY) { TempString = "~"; }
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
                if (RZ) { TempString = "~"; }
                TempString += Z.ToString().Replace(",", ".");
                return TempString;
            }
        }
    }
}
