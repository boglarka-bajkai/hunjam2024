using UnityEngine;

namespace Model.Data
{
    [System.Serializable]
    /// <summary>
    /// Represents a coordinate in an isometric grid system.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// The offset for x coordinates when converting to Unity coordinates.
        /// </summary>
        [System.NonSerialized]
        private const float X_RATIO = 0.642f;
        /// <summary>
        /// The offset for y coordinates when converting to Unity coordinates.
        /// </summary>
        [System.NonSerialized]
        private const float Y_RATIO = 0.37f;

        [Tooltip("Isometric X coordinate, going from top left to bottom right.")]
        [SerializeField] readonly int x; public int X => x;
        [Tooltip("Isometric Y coordinate, going from top right to bottom left.")]
        [SerializeField] readonly int y; public int Y => y;
        [Tooltip("Isometric Z coordinate, going from bottom to top.")]
        [SerializeField] readonly int z; public int Z => z;
        /// <summary>
        /// Creates a new Coordinate object with the specified x, y, and z values.
        /// </summary>
        public Coordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        /// <summary>
        /// Creates a new Coordinate object cloning the x, y, and z values of another.
        /// </summary>
        public Coordinate(Coordinate other)
        {
            x = other.x;
            y = other.y;
            z = other.z;
        }
        /// <summary>
        /// Returns the isometric coordinates as a Unity Vector3.
        /// </summary>
        public Vector3 AsUnityVector =>
            new Vector3((x + y) * X_RATIO, (x - y) * Y_RATIO + z * 2 * Y_RATIO, 0);
        /// <summary>
        /// Returns the Render Order of the coordinate, used for sorting objects in the scene.
        /// </summary>
        public int RenderOrder => 10 * (-x + y + z);

        #region Operators
        /// <summary>
        /// Two coordinates are equal if their x, y, and z values are equal.
        /// </summary>
        /// <param name="a">The first coordinate to check</param>
        /// <param name="b">>The second coordinate to check</param>
        /// <returns>True if the coordinates are equal, false otherwise.</returns>
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            if (ReferenceEquals(a, b)) return true;
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        /// <summary>
        /// Two coordinates are not equal if their x, y, or z values are not equal.
        /// </summary>
        /// <param name="a">The first coordinate to check</param>
        /// <param name="b">The second coordinate to check</param>
        /// <returns>>True if the coordinates are not equal, false otherwise.</returns>
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            if (ReferenceEquals(a, b)) return false;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return true;
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }
        /// <summary>
        /// Checks if the current coordinate is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with</param>
        /// <returns>True if the object is a Coordinate and has the same x, y, and z values, false otherwise.</returns>
        /// <remarks>Overrides the default Equals method.</remarks>
        public override bool Equals(object obj)
        {
            if (obj is Coordinate other)
            {
                return x == other.x && y == other.y && z == other.z;
            }
            return false;
        }
        /// <summary>
        /// Returns a hash code for the current coordinate.
        /// </summary>
        /// <returns>The hascode created based on the coordinate</returns>
        public override int GetHashCode()
        {
            return System.HashCode.Combine(x, y, z);
        }
        /// <summary>
        /// Adds two coordinates together, returning a new coordinate with the summed values.
        /// </summary>
        /// <param name="a">The first coordinate</param>
        /// <param name="b">The second coordinate</param>
        /// <returns>>A new coordinate with the summed x, y, and z values</returns>
        public static Coordinate operator+(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        /// <summary>
        /// Subtracts one coordinate from another, returning a new coordinate with the difference of the values.
        /// </summary>
        /// <param name="a">The first coordinate to subtract from</param>
        /// <param name="b">>The second coordinate to subtract</param>
        /// <returns>>A new coordinate with the difference of the x, y, and z values</returns>
        public static Coordinate operator-(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        #endregion

        #region Miscellaneous Methods

        public Coordinate Below => new Coordinate(x, y, z - 1);
        public Coordinate Above => new Coordinate(x, y, z + 1);

        #endregion
        
    }

}