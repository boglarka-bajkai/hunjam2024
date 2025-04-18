using Logic.Characters;
using Model.Characters;
using Model.Data;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that is the starting point and finish point of the level.
    /// </summary>
    public class StartTile : Tile
    {
        public override bool CanEnter(Character character) => true;

        public override bool CanEnter(Tile tile) => true;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;
        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                //TODO: win map
                return true;
            }
            return false;
        }
    }
}