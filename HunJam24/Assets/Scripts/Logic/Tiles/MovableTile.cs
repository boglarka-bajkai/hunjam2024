using Logic.Characters;
using UnityEngine;

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
                Debug.Log("failed to accept tile");
                return false;
            }

            Position = destinationPosition;
            transform.position = Position.UnityVector;

            return true;
        }

        public override void CommandPlayer(Player player)
        {
            var baseTile = MapManager.Instance.GetTileAt(Position + new Vector(0, 0, -1));
            if (player.Push(this) == false)
            {
                Debug.Log($"failed to push {name}");
            }

            if (player.MoveOnto(baseTile) == false)
            {
                Debug.Log($"failed to move to {baseTile.name} after pushing {name}");
            }

            CloneManager.Instance.UpdateHistory(
                clone => clone.Push(this) && player.MoveOnto(baseTile)
            );
        }
    }
}