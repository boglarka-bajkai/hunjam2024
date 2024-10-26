using Logic.Tiles;

namespace Logic
{
    public class Character
    {
        private MapManager _mapManager;
        private CloneManager _cloneManager;
        private Tile _tile;

        public bool StepOnto(Tile destination)
        {
            if (destination.IsOnSameLevel(_tile) && !_tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(_tile) && destination.DistanceFrom(_tile) != 2)
                return false;

            var tileOnNextTile =
                _mapManager.Get(destination.Position + new Position(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptsPlayerFrom(_tile))
            {
                return false;
            }

            _tile = destination;

            return true;
        }

        public bool ShouldBeDead()
        {
            var position = _tile.Position + new Position(0, 0, 1);
            return _cloneManager.Get(position) != null;
        }
    }
}