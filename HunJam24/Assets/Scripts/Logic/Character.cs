using Logic.Tiles;

namespace Logic
{
    public class Character
    {
        private MapManager _mapManager;
        private CloneManager _cloneManager;
        private Tile _tile;

        /***********
         * GETTERS
         */
        public bool ShouldBeDead
        {
            get
            {
                var position = _tile.Position + new Vector(0, 0, 1);
                return _cloneManager.Get(position) != null;
            }
        }

        /*
         * ACTIONS
         */
        public bool StepOnto(Tile destination)
        {
            if (destination.IsOnSameLevel(_tile) && !_tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(_tile) && destination.DistanceFrom(_tile).Length != 2)
                return false;

            var tileOnNextTile =
                _mapManager.GetTileAt(destination.Position + new Vector(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptsPlayerFrom(_tile))
            {
                return false;
            }

            _tile = destination;

            return true;
        }

        // public bool Push(Tile box, Tile destination)
        // {
        //     if (_tile.IsNextTo(box) && box.IsNextTo(destination))
        //
        //         return box.Move(destination);
        // }
    }
}