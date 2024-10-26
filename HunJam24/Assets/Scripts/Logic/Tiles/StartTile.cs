using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles
{
    public class StartTile : TileBase
    {
        public override bool AcceptsCharacter(Character character)
        {
            return true;
        }

        public override bool AcceptCharacter(Character character)
        {
            if (!AcceptsCharacter(character)) return false;

            Debug.Log("Happy End!");
            return true;
        }

        public override void EnterFrom(Vector pos)
        {
            //Win game
        }
    }
}