namespace Logic
{
    public class Tile
    {
        private Position _position;

        public bool IsNextTo(Tile other)
        {
            return _position.Distance(other._position) == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return _position.Z == other._position.Z;
        }
    }
}