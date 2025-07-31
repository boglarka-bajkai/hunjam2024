using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    public sealed class PlaceholderTile : GroundTile
    {
        public override bool CanStepOn(Character character) => true;

        public override bool CanStepOn(Tile tile) => true;
    }
}