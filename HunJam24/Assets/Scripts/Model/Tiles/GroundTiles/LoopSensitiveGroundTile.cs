using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that reacts to time loops.
    /// It allows PlayerCharacter to step on it in normal time,
    /// and CloneCharacter to step on it in looped time.
    /// Other tiles like boxes can step on it regardless of the time state.
    /// </summary>
    public sealed class InstableGround : GroundTile, ILoopAware
    {
        bool IsInNormalTime = true;
        public override bool CanStepOn(Character character)
        {
            if (IsInNormalTime && character is PlayerCharacter) return true;
            else if (!IsInNormalTime && character is CloneCharacter) return true;
            return false;
        }
        public override bool CanStepOn(Tile tile) => false;

        public void OnLoop()
        {
            IsInNormalTime = false;
        }
    }
}