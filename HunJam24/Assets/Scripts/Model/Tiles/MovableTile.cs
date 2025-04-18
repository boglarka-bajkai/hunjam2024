using System.Collections.Generic;
using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Level;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that can be moved by a character.
    /// </summary>
    /// <remarks>
    /// Moving means that the tile is pushed by a character, when the character moves into the tile.
    /// The tile can only be stepped in when there is room to move the tile.
    /// </remarks>
    class MovableTile : Tile
    {
        public override bool CanEnter(Character character) {
            Coordinate newPos = this.Position + (this.Position - character.Position);
            List<Tile> tilesInNewPos = LevelManager.Instance.GetTilesAt(newPos);
            if (tilesInNewPos.Count != 0 && !tilesInNewPos.TrueForAll(x=> x.CanEnter(this))) {
                //If there are tiles in new position, and this tiles is not allowed to enter any
                return false;
            }
            List<Tile> tilesBelowNewPos = LevelManager.Instance.GetTilesAt(newPos.Below);
            if (tilesBelowNewPos.Count == 0 || !tilesBelowNewPos.TrueForAll(x => x.CanStepOn(this))){
                //If there are no tiles below the new position, or any tiles below do not allow this tile to step on them
                return false;
            }
            return true;
        }

        public override bool CanEnter(Tile tile) => false;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;

        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                //TODO: move the tile
                return true;
            }
            return false;
        }
    }
}