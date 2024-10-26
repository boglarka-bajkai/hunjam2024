using System;
using System.Collections.Generic;

namespace Logic
{
    public class Clone
    {
        private Stack<Func<Character, bool>> _fullHistory;
        private Stack<Func<Character, bool>> _history;

        public Character Character { get; }

        /*
         * 
         */
        public bool Step()
        {
            if (_history.Count == 0)
            {
                return true;
            }

            var command = _history.Pop();
            return command.Invoke(Character);
        }
    }
}