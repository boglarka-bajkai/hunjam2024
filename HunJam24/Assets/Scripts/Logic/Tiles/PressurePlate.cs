using System.Collections.Generic;
using System;
using Controls;
using Logic.Characters;
using UnityEngine;

namespace Logic.Tiles{
    public class PressurePlate : TileBase {
        [SerializeField] GameObject active;
        [SerializeField] GameObject inactive;
        public override void UpdateSprite() { }
         public override Vector Position { get => base.Position; set {
                base.Position = value;
                active.GetComponent<SpriteRenderer>().sortingOrder = value.Order-1;
                inactive.GetComponent<SpriteRenderer>().sortingOrder = value.Order-1;
            }
        }
        List<ActivationListener> listeners = new();
        public void Subscribe(ActivationListener listener) => listeners.Add(listener);
        public override bool CanMoveInFrom(Vector position)
        {
            return true;
        }
        public override bool CanMoveOn(TileBase tile)
        {
            return true;
        }
        public override bool CanMoveOnFrom(Vector position)
        {
            return false;
        }

        public override void EnterFrom(Vector position)
        {
            listeners.ForEach(x=>x.Activate());
            active.SetActive(true);
            inactive.SetActive(false);
            
            AudioManager.Instance.PlaySoundEffect("PressurePlate");
        }

        public override void ExitTo(Vector position)
        {
            listeners.ForEach(x=>x.Deactivate());
            active.SetActive(false);
            inactive.SetActive(true);
        }
        public override Func<Character, bool> Command => character =>
        {
            var baseTile = MapManager.Instance.GetTilesAt(Position + new Vector(0, 0, -1));
            if (baseTile == null) return false;
            return character.MoveOnto(baseTile[0]);
        };
    }
}