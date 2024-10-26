using JetBrains.Annotations;
using Logic.Tiles;

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

            var tileOnNextTile =
                _mapManager.Get(destination.Position + new Position(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptsPlayerFrom(_tile))
            {
                return false;
            }

            _tile = destination;

            return true;
        }
    }
}