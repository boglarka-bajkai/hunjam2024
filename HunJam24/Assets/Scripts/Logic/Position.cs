using System;
using UnityEngine;

namespace Logic
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Position(int x, int y, int z)
        {
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
    }
}