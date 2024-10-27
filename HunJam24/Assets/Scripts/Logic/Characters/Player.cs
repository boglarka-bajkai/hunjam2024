using System.Linq;
using Logic.Tiles;
using Unity.Collections;

namespace Logic.Characters
{
    public class Player : Character
    {
        private CloneManager _cloneManager;

        public bool ShouldBeDead => CloneManager.Instance.GetClonesAt(Position).Count != 0 
            || (MapManager.Instance.GetTilesAt(Position) != null && MapManager.Instance.GetTilesAt(Position).Where(x=> x is Spike && (x as Spike).Active).Count() > 0);
    }
}