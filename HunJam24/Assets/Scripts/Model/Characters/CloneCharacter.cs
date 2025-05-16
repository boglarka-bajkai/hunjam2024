using System.Collections;
using System.Collections.Generic;
using Model.Data;
using UnityEngine;

namespace Model.Characters
{
    public sealed class CloneCharacter : Character
    {
        /// <summary>
        /// Required to check whether the player and clone have "jumped over" each other.
        /// </summary>
        public Coordinate PreviousPosition { get; private set; }
        readonly Queue<Coordinate> path = new Queue<Coordinate>();
        public override void Initialize(Coordinate startPosition)
        {
            base.Initialize(startPosition);
            CloneManager.Instance.Steps.ForEach(step => path.Enqueue(step));
        }

        public void AddStep(Coordinate step)
        {
            path.Enqueue(step);
        }
        public override bool Move(Coordinate newPosition)
        {
            Coordinate tempPrevious = PreviousPosition;
            if (base.Move(newPosition))
            {
                PreviousPosition = newPosition;
                return true;
            }
            else
            {
                // If the move was not successful, revert the previous position to the current position.
                PreviousPosition = tempPrevious;
                return false;
            }
        }
    }
}