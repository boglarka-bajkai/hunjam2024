﻿using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Tiles;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        private static int movingCount = 0;
        public static bool IsAnyMoving => movingCount > 0;
        private TileBase Tile { get; set; }

        /***********
         * GETTERS
         ***********/
        public Vector Position => Tile.Position + new Vector(0, 0, 1);

        /*
         * Returns all tiles that the character could successfully move ONTO
         */
        public List<TileBase> ValidMoveOntoDestinations()
        {
            var result = new List<TileBase> { Tile };

            for (var zOffset = -1; zOffset <= 1; zOffset++)
            {
                foreach (var neighbour in Tile.GetNeighboursInLevel(Tile.Position.Z + zOffset))
                {
                    var targetTile = MapManager.Instance.GetTilesAt(neighbour.Position + new Vector(0, 0, 1));

                    if (targetTile == null)
                    {
                        if (neighbour.CanMoveOnFrom(Position)) {
                            Debug.Log($"{neighbour.name} added from targetTile null");
                            result.Add(neighbour);
                        }
                    }
                    else if (targetTile.TrueForAll(x=> x.CanMoveInFrom(Position)))
                    {
                        result.AddRange(targetTile);
                        if (targetTile.TrueForAll(x => x is not MovableTile)) result.Add(neighbour);
                    }
                }
            }

            return result;
        }

        /**********
         * ACTIONS
         **********/
        public void SetStartingTile(TileBase t)
        {
            if (Tile == null) Tile = t;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = Position.Order;
        }


        /*
         * Character will try to move ONTO the tile at `destination`
         * The characters moves if it can and returns `true`, otherwise `false`.
         */
        public bool MoveOnto(TileBase destination)
        {
            var top = MapManager.Instance.GetTilesAt(destination.Position + new Vector(0,0,1));
            if (!ValidMoveOntoDestinations().Contains(destination))
            {
                Debug.Log("moveonto early");
                return false;
            }

            var tileOnNextTile =
                MapManager.Instance.GetTilesAt(destination.Position + new Vector(0, 0, 1));
            
            if (tileOnNextTile != null && !tileOnNextTile.TrueForAll(x=> x.CanMoveInFrom(Position)))
            {
                return false;
            }

			var dirVec = Position.DistanceFrom(destination.Position);
			Debug.Log(dirVec);
			Animator animator = GetComponent<Animator>();
			if (dirVec.Equals(new Vector(1, 0, -1)))
			{
				Debug.Log("UR");
				animator.SetInteger("dir", 0);
			}
			if (dirVec.Equals(new Vector(0, -1, -1)))
			{
				Debug.Log("UL");
				animator.SetInteger("dir", 1);
			}
			if (dirVec.Equals(new Vector(-1, 0, -1)))
			{
				Debug.Log("DL");
				animator.SetInteger("dir", 2);
			}
			if (dirVec.Equals(new Vector(0, 1, -1)))
			{
				Debug.Log("DR");
				animator.SetInteger("dir", 3);
			}

			StartCoroutine(moveSoftlyTo(destination));

            
            if (top != null) top.ForEach(x=> x.EnterFrom(Position));
            return true;
        }
        const float WAITBEFORESTART = .1f;
        const float MOVE_MULTIPLIER = 1.1f;
        bool pushing = false;
        IEnumerator moveSoftlyTo(TileBase destination) {
            movingCount++;
            if (this is Player) MapManager.Instance.ResetTiles();
            float t = 0f;
            //Wait for jump anim
            yield return new WaitForEndOfFrame();
            Animator animator = GetComponent<Animator>();
            string animName = "";
            switch(animator.GetInteger("dir")){
                case 0: animName = pushing ? "player_push_UR" :"player_jump_UR"; break;
                case 1: animName = pushing ? "player_push_UL" :"player_jump_UL"; break;
                case 2: animName = pushing ? "player_push_DL" :"player_jump_DL"; break;
                case 3: animName = pushing ? "player_push_DR" :"player_jump_DR"; break;
                default: break;
            }
            if (animName != ""){
                animator.Play(animName);
            }

            while (t <= WAITBEFORESTART) {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            //Move smoothly
            t = 0f;
            Vector destinationV = destination.Position + new Vector(0,0,1);
            Vector3 startPos = transform.position;
            Debug.Log($"started from {Position.ToString()}");
            if (destinationV.Order > Position.Order) 
                GetComponent<SpriteRenderer>().sortingOrder = destinationV.Order;
            while (t <= 1f) {
                t += Time.deltaTime * MOVE_MULTIPLIER;
                transform.position = Vector3.Lerp(startPos, destinationV.UnityVector, t);
                yield return new WaitForEndOfFrame();
            }
            Tile = destination;
            transform.position = Position.UnityVector;
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
            if (this is Player) MapManager.Instance.PlayerMoved(Tile);
            movingCount--;
            pushing = false;
        }

        
        public bool Push(MovableTile movableTile)
        {
            Debug.Log("push");
            var distance = Position.DistanceFrom(movableTile.Position);
            if (distance.Length != 1)
            {
                Debug.Log("push early return");
                return false;
            }

            var destination = movableTile.Position + distance;
            var ground = MapManager.Instance.GetTilesAt(destination + new Vector(0,0,-1));
            Debug.Log("asd");
            Debug.Log($"zugugt{(destination + new Vector(0,0,-1)).ToString()}");
            if (ground == null) Debug.Log("null");
            if (ground == null || !ground.TrueForAll(x=> x.CanMoveOn(movableTile))) return false;
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("pushing");
            pushing = true;
            return movableTile.MoveTo(destination);
        }
    }
}