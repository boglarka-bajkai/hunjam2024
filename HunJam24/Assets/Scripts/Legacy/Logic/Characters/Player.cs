using System.Linq;
using Logic.Tiles;
using Unity.Collections;

namespace Logic.Characters
{
    public class Player : CharacterLegacy
    {
        private CloneManager _cloneManager;

        public bool ShouldBeDead => CloneManager.Instance.GetClonesAt(Position).Count != 0 
            || (MapManager.Instance.GetTilesAt(Position) != null && 
            (MapManager.Instance.GetTilesAt(Position).Where(x=> x is Spike spike && spike.Active).Count() > 0 //Spike is active
            || MapManager.Instance.GetTilesAt(Position).Where(x=> x is MovableTile).Count() > 0
            || MapManager.Instance.GetTilesAt(Tile.Position).Where(x => x is DeactivatableTile dTile && !dTile.Active).Count() > 0)); //Box on tile
    }
}