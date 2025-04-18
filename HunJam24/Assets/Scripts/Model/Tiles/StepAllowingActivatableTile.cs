using Logic.Characters;
using Model.Characters;
using Model.Data;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that you can only step on when it is activated.
    /// </summary>
    class StepAllowingActivatableTile : ActivatableTile
    {
        public override bool CanEnter(Character character) => false;
        public override bool CanEnter(Tile tile) => false;
        public override bool CanStepOn(Character character) => IsActive;
        public override bool CanStepOn(Tile tile) => IsActive;
    }
}