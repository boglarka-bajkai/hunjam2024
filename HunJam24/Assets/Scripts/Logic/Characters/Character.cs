using System.Collections.Generic;
using Logic.Tiles;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
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
			if (dirVec.Equals(new Vector(-1, -0, -1)))
			{
				Debug.Log("DL");
				animator.SetInteger("dir", 2);
			}
			if (dirVec.Equals(new Vector(0, 1, -1)))
			{
				Debug.Log("DR");
				animator.SetInteger("dir", 3);
			}
			animator.SetTrigger("jumping");
			Tile = destination;
            transform.position = Position.UnityVector;
            GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
            if (this is Player) MapManager.Instance.PlayerMoved(Tile);
            if (top != null) top.ForEach(x=> x.EnterFrom(Position));
			if (this is Player)
			{
				var p = this as Player;
				if(p.ShouldBeDead)
				{
					ScreenCapture.CaptureScreenshot("Died.png");
					SceneManager.LoadScene("LoseScreen");
				}
			}
            return true;
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
            return movableTile.MoveTo(destination);
        }
    }
}