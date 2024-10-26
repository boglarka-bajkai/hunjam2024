using System;

namespace Logic
{
    public class Tile
    {
        private Position _position;

        public bool IsNextTo(Tile other)
        {
            return _position.DistanceFrom(other._position) == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return _position.Z == other._position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(_position.Z - other._position.Z) == 1;
        }

        public int DistanceFrom(Tile other)
        {
            return _position.DistanceFrom(other._position);
        }
    }
}