using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that you can only step on when it is activated.
    /// </summary>
    public sealed class StepAllowingActivatableTile : ActivatableTile, IGroundTile
    {
        public override bool CanEnter(Character character) => false;
        public override bool CanEnter(Tile tile) => false;
        public override bool CanStepOn(Character character) => IsActive;
        public override bool CanStepOn(Tile tile) => IsActive;
    }
}