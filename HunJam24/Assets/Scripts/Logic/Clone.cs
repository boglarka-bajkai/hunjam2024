using System;
using System.Collections.Generic;
using Logic.Characters;
using UnityEngine;

namespace Logic
{
    public class Clone : MonoBehaviour
    {
        private Queue<Func<CloneCharacter, bool>> _history;

        public CloneCharacter Character { get; private set; }


        /*
         * Call this once after instantiating
         */
        public void SetHistory(Queue<Func<CloneCharacter, bool>> history)
        {
            _history = history;
        }

        /*
         * Call this once after instantiating
         */
        public void SetCharacter(CloneCharacter character)
        {
            Character = character;
        }


        /*
         * Adds fresh action to local history of the clone
         */
        public void UpdateHistory(Func<CloneCharacter, bool> action)
        {
            _history.Enqueue(action);
        }

        /*
         * Replays one action of the clone
         * Returns false if the action cannot be fulfilled
         */
        public bool Step()
        {
            if (_history.Count == 0)
            {
                return true;
            }

            var command = _history.Dequeue();
            return command.Invoke(Character);
        }
    }
}