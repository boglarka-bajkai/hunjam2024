using TMPro;

namespace Logic.Tiles{
    public class ActivatableTile : DeactivatableTile {
        void Start(){
            _active = false;
        }

        public override void Activate()
        {
            _active = true;
            active.SetActive(true);
            inactive.SetActive(false);

        }

        public override void Deactivate()
        {
            _active = false;
            inactive.SetActive(true);
            active.SetActive(false);
        }

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
    }
}