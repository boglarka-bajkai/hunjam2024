using System;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            if (destination != null && !destination.CanMoveOn(this))
            {
                Debug.Log("moveto early return");
                return false;
            }

            Position = destinationPosition;
            transform.position = Position.UnityVector;
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;

            return true;
        }

        public override Func<Character, bool> Command => character =>
        {
            var baseTile = MapManager.Instance.GetTileAt(Position + new Vector(0, 0, -1));
            return character.Push(this) && character.MoveOnto(baseTile);
        };

        public override bool CanMoveOnFrom(Vector position)
        {
            return false;
        }
        public override bool CanMoveInFrom(Vector position)
        {
            Vector diff = position - Position;
            Debug.Log($"diff: {diff.ToString()}");
            Vector check = Position + !diff + new Vector(0,0,-1);
            Debug.Log($"Checked tile: {check.ToString()}");
            TileBase t = MapManager.Instance.GetTileAt(check);
            if (t == null) Debug.Log("Ground NULL!");
            return t != null && t.CanMoveOn(this);
        }
    }
}