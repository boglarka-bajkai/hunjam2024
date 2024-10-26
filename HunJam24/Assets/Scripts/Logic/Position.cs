using System;
using UnityEngine;

namespace Logic
{
    class Position
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public int Distance(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);
        }
    }
}