using System;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles{
    public class CheckPointTile : TileBase {
        bool activated = false;
        public override void EnterFrom(Vector position)
        {
            if (activated) return;
            CloneManager.Instance.Spawn();
            activated = true;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        public override bool CanMoveInFrom(Vector position)
        {
            return true;
        }
        public override bool CanMoveOn(TileBase tile)
        {
            return false;
        }
        public override bool CanMoveOnFrom(Vector position)
        {
            return false;
        }
        public override Func<Character, bool> Command => character =>
        {
            var baseTile = MapManager.Instance.GetTileAt(Position + new Vector(0, 0, -1));
            Debug.Log($"baseTile {baseTile.name}");
            return character.MoveOnto(baseTile);
        };
    }
}