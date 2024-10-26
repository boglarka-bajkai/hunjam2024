using System;
using System.Collections.Generic;

namespace Logic
{
    public class Clone
    {
        private readonly Queue<Func<Character, bool>> _history;

        public Character Character { get; }


        /*
         * Constructor ?
         */
        public Clone(Queue<Func<Character, bool>> history, Character clonedCharacter)
        {
            _history = history;
            Character = clonedCharacter;
        }

        /*
         * Adds fresh action to local history of the clone
         */
        public void UpdateHistory(Func<Character, bool> action)
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