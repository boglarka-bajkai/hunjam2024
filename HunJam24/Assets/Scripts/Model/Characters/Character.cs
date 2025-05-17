using System;
using Model.Data;
using Model.Level;
using Model.Other;
using UnityEngine;

namespace Model.Characters
{
    public abstract class Character : MonoBehaviour, IMoveNotifier
    {
        Coordinate position;
        public Coordinate Position => position;

        public event Action<Coordinate, Coordinate> OnMove;

        /// <summary>
        /// Initializes the character with a starting position.
        /// </summary>
        /// <param name="startPosition">The starting position</param>
        public virtual void Initialize(Coordinate startPosition)
        {
            position = startPosition;
            transform.position = startPosition.AsUnityVector;
            GetComponentInChildren<SpriteRenderer>().sortingOrder = startPosition.RenderOrder + 1;
        }
        public virtual bool Move(Coordinate newPosition)
        {
            // Check if can move
            if (!CanMoveTo(newPosition)) return false;
            // Tick the game manager (only if player character, ugly solution 
            // but no clean way to place it in the middle of the control loop)
            if (this is PlayerCharacter) GameManager.InvokeTick(newPosition);
            // Leave old position
            OnMove?.Invoke(position, newPosition);
            LevelManager.Instance.GetTilesAt(position).ForEach(x => x.ExitTo(this, newPosition));
            LevelManager.Instance.GetTilesAt(position.Below).ForEach(x => x.ExitTo(this, position));
            // Enter new position
            LevelManager.Instance.GetTilesAt(newPosition).ForEach(x => x.Enter(this));
            LevelManager.Instance.GetTilesAt(newPosition.Below).ForEach(x => x.StepOn(this));
            //Finally set the new position
            position = newPosition;
            return true;
        }

        protected bool CanMoveTo(Coordinate newPosition)
        {
            int xDiff = Math.Abs(newPosition.X - position.X);
            int yDiff = Math.Abs(newPosition.Y - position.Y);
            // If the position is not next to the current position, return false
            if (!((xDiff <= 1 && yDiff == 0) || (xDiff == 0 && yDiff <= 1)))
            {
                return false;
            }
            //If there is nothing to step on, return false
            if (LevelManager.Instance.GetTilesAt(newPosition.Below).Count == 0)
            {
                return false;
            }
            // If the position below is blocking the character, return false
            if (!LevelManager.Instance.GetTilesAt(newPosition.Below).TrueForAll(x => x.CanStepOn(this)))
            {
                return false;
            }
            // If the position is blocking the character, return false
            if (!LevelManager.Instance.GetTilesAt(newPosition).TrueForAll(x => x.CanEnter(this)))
            {
                return false;
            }
            // Otherwise, return true
            return true;
        }

        
    }
}