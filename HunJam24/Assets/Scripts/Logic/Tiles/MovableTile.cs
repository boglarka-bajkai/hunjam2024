using System;
using System.Collections;
using Controls;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Logic.Tiles
{
    public class MovableTile : TileBase
    {
        /*
         * Tries to change the position of the tile to `destination`
         * Returns true on success
         */
        private Vector startPosition;
        public override Vector Position { get => base.Position; set {
                base.Position = value;
                if (startPosition == null) startPosition = value;
            }
        }
        public void Reset(){
            Position = startPosition;
        }
        public bool CouldMoveTo(Vector destinationPosition) {
            var destination = MapManager.Instance.GetTilesAt(destinationPosition);
            if (destination != null && !destination.TrueForAll(x=> x.CanMoveOn(this)))
            {
                return false;
            }
            return true;
        }
        public override bool MoveTo(Vector destinationPosition)
        {
            var destination = MapManager.Instance.GetTilesAt(destinationPosition);
            if (destination != null && !destination.TrueForAll(x=> x.CanMoveOn(this)))
            {
                return false;
            }
            var from = MapManager.Instance.GetTilesAt(_position);
            if (from != null) from.ForEach(x=> x.ExitTo(destinationPosition));
            var to = MapManager.Instance.GetTilesAt(destinationPosition);
            if (to != null) to.ForEach(x=> x.EnterFrom(_position));
            _position = destinationPosition;
            StartCoroutine(moveSoftlyTo(destinationPosition));
            return true;
        }
        const float WAITBEFORESTART = .1f;
        const float MOVE_MULTIPLIER = 1.1f;
        IEnumerator moveSoftlyTo(Vector destination) {
            float t = 0f;
            //Wait for jump anim
            yield return new WaitForEndOfFrame();
            
            while (t <= WAITBEFORESTART) {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            //Move smoothly
            t = 0f;
            Vector3 startPos = transform.position;
            if (destination.Order > GetComponent<SpriteRenderer>().sortingOrder) 
                GetComponent<SpriteRenderer>().sortingOrder = destination.Order;
            AudioManager.Instance.PlaySoundEffect("BoxSlide");
            while (t <= 1f) {
                t += Time.deltaTime * MOVE_MULTIPLIER;
                transform.position = Vector3.Lerp(startPos, destination.UnityVector, t);
                yield return new WaitForEndOfFrame();
            }
            GetComponent<SpriteRenderer>().sortingOrder = destination.Order;
            
        }

        public override Func<Character, bool> Command => character =>
        {
            var baseTile = MapManager.Instance.GetTilesAt(Position + new Vector(0, 0, -1));
            if (baseTile == null) return false;
            return character.Push(this) && character.MoveOnto(baseTile[0]);
        };

        public override bool CanMoveOnFrom(Vector position)
        {
            return false;
        }
        public override bool CanMoveInFrom(Vector position)
        {
            Vector diff = position - Position;
            Debug.Log($"diff: {diff.ToString()}");
            Vector check = Position + !diff + new Vector(0,0,-1);
            Debug.Log($"Checked tile: {check.ToString()}");
            var t = MapManager.Instance.GetTilesAt(check);
            if (t == null) Debug.Log("Ground NULL!");
            return t != null && t.TrueForAll(x=> x.CanMoveOn(this));
        }
        
        public override void UpdateSprite() { }
    }
}