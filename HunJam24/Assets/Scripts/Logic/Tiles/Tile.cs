using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Tiles
{
    public class Tile : MonoBehaviour
    {
        public Vector Position { get; private set; }


        /*
         * Check whether this tile touches with the other tile
         */
        public bool IsNextTo(Tile other)
        {
            return Position.DistanceFrom(other.Position).Length == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        /*
         * Returns how much you would have to step from one tile to another if you could not move diagonally
         */
        public Vector DistanceFrom(Tile other)
        {
            return Position.DistanceFrom(other.Position);
        }

        /*
         * Checks whether the tile is able to accept a player from another tile.
         * Acceptance means the player could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
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
        public List<Tile> GetValidNeighbors()
        {
            List<Tile> tiles = new();
            // Same Level
            var tile = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, 0));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            // One Above
            tile = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tile = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, 1));
            if (tile != null && tile.AcceptsPlayerFrom(this)) tiles.Add(tile);
            tiles.Add(this);
            return tiles;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}