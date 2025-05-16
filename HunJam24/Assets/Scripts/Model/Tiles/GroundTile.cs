using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    public sealed class GroundTile : Tile, IGroundTile
    {
        public override bool CanEnter(Character character) => false;

        public override bool CanEnter(Tile tile) => false;

        public override bool CanStepOn(Character character) => true;

        public override bool CanStepOn(Tile tile) => true;
    }
}