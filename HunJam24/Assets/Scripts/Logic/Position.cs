using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Logic
{
    public class Position
    {
        const float X_OFFSET = 0.642f;
        const float Y_OFFSET = 0.37f;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public Vector3 UnityVector => new Vector3((X+Y) * X_OFFSET, (X-Y)* Y_OFFSET + Z * 2 * Y_OFFSET, 0);
        public int Order => 10*(-X+Y+Z);
        
        public Position(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public int DistanceFrom(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);
        }

        public static Position operator +(Position lhs, Position rhs)
        {
            return new Position(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }
        public override bool Equals(object obj)
        {
            return X == X && Y == Y && Z == Z;
        }

        public override int GetHashCode()
        {
            return X * 1000000 + Y * 1000 + Z;
        }
    }
}