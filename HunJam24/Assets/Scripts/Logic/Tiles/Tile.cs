using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Tiles
{
    public class Tile : MonoBehaviour
    {   
        public Position Position { get; private set; }
        public void SetPosition(Position pos){
            Position = pos;
        }
        public bool IsNextTo(Tile other)
        {
            return Position.DistanceFrom(other.Position) == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        public int DistanceFrom(Tile other)
        {
            return Position.DistanceFrom(other.Position);
        }

        public virtual bool AcceptsPlayerFrom(Tile other)
        {
            return false;
        }
        /// <summary>
        /// Returns the list of valid neighbor tiles that can be stepped upon
        /// </summary>
        /// <returns>
        ///     List of valid tiles
        /// </returns>
        public List<Tile> GetValidNeighbors() {
            List<Tile> tiles = new();
            // Same Level
            var tile = MapManager.Instance.GetTileAt(Position + new Position(1, 0, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(-1, 0, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(0, 1, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(0, -1, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            // One Above
            tile = MapManager.Instance.GetTileAt(Position + new Position(1, 0, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(-1, 0, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(0, 1, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Position(0, -1, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tiles.Add(this);
            return tiles;
        }

        public virtual void Enter() {}
        public virtual void Exit() {}
    }
}