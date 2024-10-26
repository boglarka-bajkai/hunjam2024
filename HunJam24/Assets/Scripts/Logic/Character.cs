using Logic.Tiles;
using UnityEngine;
using System.Collections.Generic;

namespace Logic
{
    public class Character : MonoBehaviour
    {
        private CloneManager _cloneManager;
        public Tile Tile { get; private set; }

        /***********
         * GETTERS
         ***********/
        public Vector Position => Tile.Position + new Vector(0, 0, 1);

        public bool ShouldBeDead => _cloneManager.GetClonesAt(Position).Count != 0;

        /*
         * Returns all tiles that the character could successfully move ONTO
         */
        public List<Tile> ValidMoveDestinations()
        {
            var result = new List<Tile> { Tile };

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
        public void SetStartingTile(Tile t)
        {
            if (Tile == null) Tile = t;
        }

        /*
         * Character will try to move ONTO the tile at `destination`
         * The characters moves if it can and returns `true`, otherwise `false`.
         */
        public bool MoveOnto(Tile destination)
        {
            if (destination.IsOnSameLevel(Tile) && !Tile.IsNextTo(destination))
                return false;

            if (destination.IsOnNeighboringLevel(Tile) && destination.DistanceFrom(Tile).Length != 2)
                return false;

            var tileOnNextTile =
                MapManager.Instance.GetTileAt(destination.Position + new Vector(0, 0, 1));
            if (tileOnNextTile == null || !tileOnNextTile.AcceptCharacter(this))
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