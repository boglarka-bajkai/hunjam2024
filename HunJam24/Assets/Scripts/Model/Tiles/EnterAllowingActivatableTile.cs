using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that you can only enter when it is activated.
    /// </summary>
    public sealed class EnterAllowingAtivatableTile : ActivatableTile, ITopTile
    {
        public override bool CanEnter(Character character) => IsActive;
        public override bool CanEnter(Tile tile) => IsActive;
        public override bool CanStepOn(Character character) => false;
        public override bool CanStepOn(Tile tile) => false;
    }
}