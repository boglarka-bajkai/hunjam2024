namespace Logic.Tiles
{
    public class MovableTile : TileBase
    {
        /*
         * Tries to change the position of the tile to `destination`
         * Returns true on success
         */
        public override bool MoveTo(Vector destinationPosition)
        {
            var destination = MapManager.Instance.GetTileAt(destinationPosition);
            if (destination != null && !destination.AcceptTile(this))
            {
                return false;
            }

            Position = destinationPosition;
            return true;
        }
    }
}