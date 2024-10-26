﻿using System;
using UnityEngine;

namespace Logic
{
    public class Vector
    {
        private const float XOffset = 0.642f;
        private const float YOffset = 0.37f;
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Vector3 UnityVector => new((X + Y) * XOffset, (X - Y) * YOffset + Z * 2 * YOffset, 0);
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

        public static Vector operator +(Vector lhs, Vector rhs)
        {
            return new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
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