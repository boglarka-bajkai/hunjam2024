using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles{
    public class StartTile : TileBase {
        
        public override void EnterFrom(Vector pos)
        {
            Debug.Log("Happy End!");
        }
        
        public override void UpdateSprite() { }
    }
}