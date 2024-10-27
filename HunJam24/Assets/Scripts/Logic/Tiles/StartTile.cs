using System;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles{
    public class StartTile : TileBase {
        public void CheckAllCheckpoints() {
            if (MapManager.Instance.Map.FindAll(x=> x is CheckPointTile && !(x as CheckPointTile).Activated).Count == 0) {
                GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
        public override void EnterFrom(Vector pos)
        {
            Debug.Log("Happy End!");
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

        public override void UpdateSprite() { }
    }
}