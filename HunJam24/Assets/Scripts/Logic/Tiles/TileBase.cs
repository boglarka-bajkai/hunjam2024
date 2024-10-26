using System;
using System.Collections.Generic;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles
{
    public class TileBase : MonoBehaviour
    {
        public Vector Position { get; set; }

        /**********
         * GETTERS
         **********/

        /*
         * Check whether this tile touches with the other tile
         */
        public bool IsNextTo(TileBase other)
        {
            return Position.DistanceFrom(other.Position).Length == 1;
        }

        public bool IsOnSameLevel(TileBase other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(TileBase other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        /*
         * Returns how much you would have to step from one tile to another if you could not move diagonally
         */
        public Vector DistanceFrom(TileBase other)
        {
            return Position.DistanceFrom(other.Position);
        }

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public virtual bool AcceptsCharacter(Character character)
        {
            return false;
        }

        /*
         * Returns existing tiles that have a distance of 1 without counting the Z dimension
         * The returned tiles' Z coordinate is the same as the `z` given here as parameter
         */
        public List<TileBase> GetNeighboursInLevel(int z)
        {
            List<TileBase> result = new();

            var zOffset = z - Position.Z;

            var neighbour = MapManager.Instance.GetTileAt(Position + new Vector(1, 0, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(0, 1, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(-1, 0, zOffset));
            if (neighbour != null) result.Add(neighbour);
            neighbour = MapManager.Instance.GetTileAt(Position + new Vector(0, -1, zOffset));
            if (neighbour != null) result.Add(neighbour);

            return result;
        }

        /**********
         * ACTIONS
         **********/

        /*
         * !!! Also accepts character that is already accepted
         */
        public bool AcceptTile(TileBase tile)
        {
            return false;
        }

        public bool AcceptCharacter(Character character)
        {
            return false;
        }

        public virtual bool CanMoveOnFrom(Vector position) => true;

        public virtual bool CanMoveInFrom(Vector position) => false;

        /*
         * Tries to change the position of the tile to `destination`
         * Returns true on success
         */
        public virtual bool MoveTo(Vector destinationPosition)
        {
            return false;
        }


        public virtual void EnterFrom(Vector position)
        {
        }

        public virtual void ExitTo(Vector position)
        {
        }


        public virtual void CommandPlayer(Player player)
        {
            if (player.MoveOnto(this) == false)
            {
                Debug.Log($"failed to move to {name}");
            }
            
            CloneManager.Instance.UpdateHistory(clone => clone.MoveOnto(this));
        }
    }
}