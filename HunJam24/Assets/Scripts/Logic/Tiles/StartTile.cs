using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles{
    public class StartTile : SimpleTile {
        public override bool AcceptCharacter(Character character)
        {
            Debug.Log("Happy End!");
            return true;
        }
        
        public override void EnterFrom(Vector pos)
        {
            //Win game
        }
    }
}