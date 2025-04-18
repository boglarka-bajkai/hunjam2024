using Logic.Characters;
using Model.Characters;
using Model.Data;

namespace Model.Tiles
{
    class GroundTile : Tile
    {
        public override bool CanEnter(Character character) => false;

        public override bool CanEnter(Tile tile) => false;

        public override bool CanStepOn(Character character) => true;

        public override bool CanStepOn(Tile tile) => true;
    }
}