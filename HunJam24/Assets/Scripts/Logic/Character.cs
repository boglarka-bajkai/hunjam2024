namespace Logic
{
    public class Character
    {
        private MapManager _mapManager;
        private Tile _tile;

        public bool StepTo(Tile destination)
        {
            if (destination.IsOnSameLevel(_tile) && !_tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(_tile) && destination.DistanceFrom(_tile) != 2)
                return false;

            // TODO: check for transparent tiles (door, pressure plate, ...)
            if (_mapManager.Get(destination.Position + new Position(0, 0, 1)) != null)
            {
                return false;
            }

            _tile = destination;

            return true;
        }
    }
}