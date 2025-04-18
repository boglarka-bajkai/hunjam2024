using Logic.Characters;
using Model.Characters;
using Model.Data;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that you can only enter when it is activated.
    /// </summary>
    class EnterAllowingAtivatableTile : ActivatableTile
    {
        public override bool CanEnter(Character character) => IsActive;
        public override bool CanEnter(Tile tile) => IsActive;
        public override bool CanStepOn(Character character) => false;
        public override bool CanStepOn(Tile tile) => false;
    }
}