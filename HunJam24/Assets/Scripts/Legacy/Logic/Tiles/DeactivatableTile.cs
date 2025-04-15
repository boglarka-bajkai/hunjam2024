using System;
using System.Linq;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles
{
    public class DeactivatableTile : TileBase, ActivationListener
    {
        [SerializeField] protected GameObject active;
        [SerializeField] protected GameObject inactive;
        protected bool _active = true;
        public bool Active => _active;
        public override Vector Position { get => base.Position; set {
                base.Position = value;
                active.GetComponent<SpriteRenderer>().sortingOrder = value.Order;
                inactive.GetComponent<SpriteRenderer>().sortingOrder = value.Order;
            }
        }
        public override void UpdateSprite() { }

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveOnFrom(Vector position) => _active;

        /*
         * Checks whether the tile is able to accept another tile.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveOn(TileBase tile) => _active;

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveInFrom(Vector position)
        {
            return false;
        }

        public virtual void Activate()
        {
            _active = false;
            active.SetActive(false);
            inactive.SetActive(true);
        }

        public virtual void Deactivate()
        {
            _active = true;
            inactive.SetActive(false);
            active.SetActive(true);
        }

    }
}