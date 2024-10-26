using Logic.Tiles;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class Character : MonoBehaviour
    {
        private MapManager _mapManager;
        private CloneManager _cloneManager;
        public Tile _tile { get; private set; }

        /***********
         * GETTERS
         ***********/
        private Vector Position => _tile.Position + new Vector(0, 0, 1);

        public bool ShouldBeDead => _cloneManager.Get(Position) != null;

        /*
         * Returns all tiles that the character could successfully move ONTO
         */
        public List<Tile> ValidMoveDestinations()
        {
            var result = new List<Tile>();

            for (var z = -1; z <= 1; z++)
            {
                foreach (var neighbour in _tile.GetNeighboursInLevel(z))
                {
                    var targetTile = MapManager.Instance.GetTileAt(neighbour.Position + new Vector(0, 0, 1));

                    if (targetTile == null)
                    {
                        result.Add(neighbour);
                    }
                    else if (targetTile.AcceptsCharacter(this))
                    {
                        result.Add(neighbour);
                    }
                }
            }

            return result;
        }

        /**********
         * ACTIONS
         **********/
        public void SetStartingTile(Tile t)
        {
            if (_tile == null) _tile = t;
        }

        /*
         * Character will try to move ONTO the tile at `destination`
         * The characters moves if it can and returns `true`, otherwise `false`.
         */
        public bool MoveOnto(Tile destination)
        {
            if (destination.IsOnSameLevel(_tile) && !_tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(_tile) && destination.DistanceFrom(_tile).Length != 2)
                return false;

            var tileOnNextTile =
                _mapManager.GetTileAt(destination.Position + new Vector(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptCharacter(this))
            {
                return false;
            }

            _tile = destination;
            MapManager.Instance.PlayerMoved(_tile);
            return true;
        }

        /*
         * Character will try to push the given `movableTile` ONTO the tile at `destination`
         * The `movableTile` is pushed when the push is valid and returns `true`, otherwise `false`.
         * THE CHARACTER DOES NOT MOVE !!!
         */
        public bool PushOnto(Tile movableTile, Tile destination)
        {
            var destinationPosition = destination.Position + new Vector(0, 0, 1);
            var positionToDestinationDistance = Position.DistanceFrom(destinationPosition);
            if (!(
                    Position.DistanceFrom(movableTile.Position).Length == 1 &&
                    positionToDestinationDistance.Length == 2 &&
                    positionToDestinationDistance.IsPureDirectional()
                ))
            {
                return false;
            }

            return movableTile.MoveTo(destinationPosition);
        }
    }
}