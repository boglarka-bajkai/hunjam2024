using System;
using System.Linq;
using Logic.Characters;

namespace Logic.Tiles
{
    public class Spike : TileBase
    {
        private bool _active;
        
        public override void UpdateSprite() { }

        // public override Func<Character, bool> Command => character =>
        //     AcceptsCharacter(character) &&
        //     character.MoveOnto(MapManager.Instance.GetTilesAt(Position + new Vector(0, 0, -1)).First());
    }
}