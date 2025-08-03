using System.Collections;
using System.Collections.Generic;
using Model.Data;
using UnityEngine;
using View.Animated;
using View.Sound;

namespace Model.Characters
{
    [RequireComponent(typeof(CharacterAnimation))]
    [RequireComponent(typeof(CharacterSoundEffects))]
    public sealed class CloneCharacter : Character
    {
        /// <summary>
        /// Required to check whether the player and clone have "jumped over" each other.
        /// </summary>
        public Coordinate PreviousPosition { get; private set; }
        readonly Queue<Coordinate> path = new Queue<Coordinate>();
        void Start()
        {
            GameManager.OnTick += Tick;
        }
        void OnDestroy()
        {
            GameManager.OnTick -= Tick;
        }

        void Tick(Coordinate newPostion)
        {
            path.Enqueue(newPostion);
            if (CanMoveTo(path.Peek()))
            {
                Move(path.Dequeue());
            }
        }
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