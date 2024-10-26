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
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
        }

        // public bool TryMoveTo(Vector position){
        //     var current = MapManager.Instance.GetTileAt(Position);
        //     var target = MapManager.Instance.GetTileAt(position);
        //     bool re = false;
        //     if (Position.HorizontalDistance(position) > 1 || Position.VerticalDistance(position) > 2) return false;
        //     //Staying
        //     if (position == Position) re = true;
        //     // Nothing there
        //     else if (target == null) {
        //         //Only move if solid ground beneath
        //         var ground = MapManager.Instance.GetTileAt(position + new Vector(0,0,-1));
        //         if (ground != null && ground.CanMoveOnFrom(position)) {
        //             //Valid ground beneath
        //             re = true;
        //         }
        //         //Non valid ground beneath
        //     }
        //     //Has something
        //     else {
        //         if (target.CanMoveInFrom(position)) {
        //             //Entering enterable object
        //             target.EnterFrom(position);
        //             re = true;
        //         }
        //         //Non-enterable object
        //     }
        //     if (re) {
        //         if (current != null) current.ExitTo(position);
        //         transform.position = position.UnityVector;
        //     }
        //     return re;
        // }

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
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
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

            var destination = movableTile.Position + distance;
            return movableTile.MoveTo(destination);
        }
    }
}