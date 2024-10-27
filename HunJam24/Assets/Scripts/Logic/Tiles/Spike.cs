using System;
using System.Linq;
using Logic.Characters;

namespace Logic.Tiles
{
    public class Spike : TileBase
    {
        private bool _active;
        
        public override void UpdateSprite() { }

        // public override Func<Character, bool> Command => character =>
        //     AcceptsCharacter(character) &&
        //     character.MoveOnto(MapManager.Instance.GetTilesAt(Position + new Vector(0, 0, -1)).First());

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveOnFrom(Vector position) => false;

        /*
         * Checks whether the tile is able to accept another tile.
         * Acceptance means the character could be moved ONTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveOn(TileBase tile) => false;

        /*
         * Checks whether the tile is able to accept a character.
         * Acceptance means the character could be moved INTO this tile.
         * (Useful for doors, pressure plates, and other transparent objects...)
         */
        public override bool CanMoveInFrom(Vector position)
        {
            return !_active && MapManager.Instance.GetTilesAt(Position).Count < 2;
        }
    }
}