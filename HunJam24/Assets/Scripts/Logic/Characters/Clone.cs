using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class Clone : MonoBehaviour
    {
        private readonly Queue<Func<Player, bool>> _history;

        public Player Character { get; }
        int step = 0;

        /*
         * Constructor ?
         */
        public Clone(Queue<Func<Player, bool>> history, Player clonedCharacter)
        {
            _history = history;
            Character = clonedCharacter;
        }

        /*
         * Adds fresh action to local history of the clone
         */
        public void UpdateHistory(Func<Player, bool> action)
        {
            _history.Enqueue(action);
        }

        /*
         * Replays one action of the clone
         * Returns false if the action cannot be fulfilled
         */
        public bool Step()
        {
            // if (_history.Count == 0)
            // {
            //     return true;
            // }

            var command = CloneManager.Instance._fullHistory[step++];
            return command.Invoke(Character);
        }
    }
}