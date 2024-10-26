using System.Collections.Generic;
using Logic.Tiles;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        private TileBase Tile { get; set; }

        /***********
         * GETTERS
         ***********/
        public Vector Position => Tile.Position + new Vector(0, 0, 1);

        /*
         * Returns all tiles that the character could successfully move ONTO
         */
        public List<TileBase> ValidMoveOntoDestinations()
        {
            var result = new List<TileBase> { Tile };

            for (var zOffset = -1; zOffset <= 1; zOffset++)
            {
                foreach (var neighbour in Tile.GetNeighboursInLevel(Tile.Position.Z + zOffset))
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
        public void SetStartingTile(TileBase t)
        {
            if (Tile == null) Tile = t;
        }

        /*
         * Character will try to move ONTO the tile at `destination`
         * The characters moves if it can and returns `true`, otherwise `false`.
         */
        public bool MoveOnto(TileBase destination)
        {
            if (!ValidMoveOntoDestinations().Contains(destination))
            {
                return false;
            }

            var tileOnNextTile =
                MapManager.Instance.GetTileAt(destination.Position + new Vector(0, 0, 1));
            if (tileOnNextTile != null && !tileOnNextTile.AcceptCharacter(this))
            {
                return false;
            }

            Tile = destination;
            transform.position = Position.UnityVector;
            MapManager.Instance.PlayerMoved(Tile);

            return true;
        }

        /*
         * Character will try to push the given `movableTile` ONTO the tile at `destination`
         * The `movableTile` is pushed when the push is valid and returns `true`, otherwise `false`.
         * THE CHARACTER DOES NOT MOVE !!!
         */
        public bool PushOnto(TileBase movableTile, TileBase destination)
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
        
        public bool Push(TileBase movableTile)
        {
            var distance = Position.DistanceFrom(movableTile.Position);
            if (distance.Length != 1)
            {
                return false;
            }

            var destination = Position + distance;
            return movableTile.MoveTo(destination);
        }
    }
}