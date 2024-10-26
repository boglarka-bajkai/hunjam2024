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
                        if (neighbour.CanMoveOnFrom(Position)) {
                            Debug.Log($"{neighbour.name} added from targetTile null");
                            result.Add(neighbour);
                        }
                    }
                    else if (targetTile.CanMoveInFrom(Position))
                    {
                        Debug.Log($"{neighbour.name} added at canmovein");
                        result.Add(targetTile);
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
            
            if (tileOnNextTile != null && !tileOnNextTile.CanMoveOnFrom(Position))
            {
                return false;
            }

            Tile = destination;
            transform.position = Position.UnityVector;
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
            MapManager.Instance.PlayerMoved(Tile);

            return true;
        }

        
        public bool Push(MovableTile movableTile)
        {
            Debug.Log("push");
            var distance = Position.DistanceFrom(movableTile.Position);
            if (distance.Length != 1)
            {
                Debug.Log("push early return");
                return false;
            }

            var destination = movableTile.Position + distance;
            var ground = MapManager.Instance.GetTileAt(destination + new Vector(0,0,-1));
            Debug.Log("asd");
            Debug.Log($"zugugt{(destination + new Vector(0,0,-1)).ToString()}");
            if (ground == null) Debug.Log("null");
            if (ground == null || !ground.CanMoveOn(movableTile)) return false;
            return movableTile.MoveTo(destination);
        }
    }
}