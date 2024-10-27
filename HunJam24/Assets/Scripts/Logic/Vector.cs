using System;
using Logic.Tiles;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Logic
{
    public class Vector
    {
        public static Vector3 globalOffset;
        private const float XOffset = 0.642f;
        private const float YOffset = 0.37f;
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Vector3 UnityVector =>
            new Vector3((X + Y) * XOffset, (X - Y) * YOffset + Z * 2 * YOffset, 0) + globalOffset;
        public int Order => 10 * (-X + Y + Z);

        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int Length => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public bool IsPureDirectional()
        {
            if (Y == 0 && Z == 0) return true;
            if (Z == 0 && X == 0) return true;
            if (X == 0 && Y == 0) return true;

            return false;
        }

        /*
         * Returns how much you would have to step from one position to another if you could not move diagonally
         */
        public Vector DistanceFrom(Vector other)
        {
            return new Vector(other.X - X, other.Y - Y, other.Z - Z);
        }
        public int HorizontalDistance(Vector other) {
            return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        }
        public int VerticalDistance(Vector other) {
            return Math.Abs(other.Z - Z);
        }

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vector operator -(Vector lhs, Vector rhs){
            return new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z -rhs.Z);
        }
        public static Vector operator !(Vector v) {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector) {
                Vector o = obj as Vector;
                return X == o.X && Y == o.Y && Z == o.Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X * 1000000 + Y * 1000 + Z;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}