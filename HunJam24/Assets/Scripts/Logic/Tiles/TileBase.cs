﻿using System;
using System.Collections.Generic;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles
{
    public class TileBase : MonoBehaviour
    {
        public virtual void UpdateSprite()
        {
            String i1 = (MapManager.Instance.GetTilesAt(new Vector(Position.X,Position.Y-1,Position.Z)) == null)?"0":"1";
            String i2 = (MapManager.Instance.GetTilesAt(new Vector(Position.X+1,Position.Y,Position.Z)) == null)?"0":"1";
            String i3 = (MapManager.Instance.GetTilesAt(new Vector(Position.X-1,Position.Y,Position.Z)) == null)?"0":"1";
            String i4 = (MapManager.Instance.GetTilesAt(new Vector(Position.X,Position.Y+1,Position.Z)) == null)?"0":"1";

            String spriteName = "Qube/Qube" + i1 + i2 + i3 + i4;
            Sprite newSprite = Resources.Load<Sprite>(spriteName);

            if (newSprite != null)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite not found: " + spriteName);
            }
        }
             
        protected Vector _position;
        public virtual Vector Position
        {
            get => _position;
            set
            {
                if (_position == value) return;
                _position = value;

                transform.position = value.UnityVector;
                GetComponentInChildren<SpriteRenderer>().sortingOrder = value.Order;
            }
        }

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
         * Returns existing tiles that have a distance of 1 without counting the Z dimension
         * The returned tiles' Z coordinate is the same as the `z` given here as parameter
         */
        public List<TileBase> GetNeighboursInLevel(int z)
        {
            List<TileBase> result = new();

            var zOffset = z - Position.Z;

            var neighbour = MapManager.Instance.GetTilesAt(Position + new Vector(1, 0, zOffset));
            if (neighbour != null) result.AddRange(neighbour);
            neighbour = MapManager.Instance.GetTilesAt(Position + new Vector(0, 1, zOffset));
            if (neighbour != null) result.AddRange(neighbour);
            neighbour = MapManager.Instance.GetTilesAt(Position + new Vector(-1, 0, zOffset));
            if (neighbour != null) result.AddRange(neighbour);
            neighbour = MapManager.Instance.GetTilesAt(Position + new Vector(0, -1, zOffset));
            if (neighbour != null) result.AddRange(neighbour);

            return result;
        }

        public virtual Func<Character, bool> Command => character => character.MoveOnto(this);

        /**********
         * ACTIONS
         **********/

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public virtual bool CanMoveOnFrom(Vector position) => true;
        /*
         * Checks whether the tile is able to accept another tile.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public virtual bool CanMoveOn(TileBase tile) => true;
        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
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

    }
}