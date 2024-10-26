using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles
{
    public class SimpleTile : TileBase
    {
        public override void CommandPlayer(Player player)
        {
            if (player.MoveOnto(this) == false)
            {
                Debug.Log($"failed to move to {name}");
            }
            
            CloneManager.Instance.UpdateHistory(clone => clone.MoveOnto(this));
        }
    }
}