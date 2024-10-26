using System;

namespace Logic.Tiles
{
    public class Tile
    {
        public Position Position { get; }
        

        public bool IsNextTo(Tile other)
        {
            return Position.DistanceFrom(other.Position) == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        public int DistanceFrom(Tile other)
        {
            return Position.DistanceFrom(other.Position);
        }

        public bool AcceptsPlayerFrom(Tile other)
        {
            throw new NotImplementedException();
        }
    }
}