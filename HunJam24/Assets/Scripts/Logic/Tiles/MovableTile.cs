using System;
using UnityEngine;

namespace Logic.Tiles
{
    public class MovableTile : Tile
    {
        /*
         * Tries to change the position of the tile to `destination`
         * Returns true on success
         */
        public override bool MoveTo(Vector destinationPosition)
        {
            var destination = MapManager.Instance.GetTileAt(destinationPosition);
            if (destination != null && !destination.Accept(this))
            {
                return false;
            }

            Position = destinationPosition;
            return true;
        }
    }
}