using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class CloneManager
    {
        private readonly List<Clone> _clones = new();
        private readonly List<Func<Character, bool>> _fullHistory = new();

        public List<Clone> GetClonesAt(Vector position)
        {
            return _clones.Where(clone => clone.Character.Position.Equals(position)).ToList();
        }

        /*
         * Call this only as the player's character acts
         */
        public void UpdateHistory(Func<Character, bool> action)
        {
            _fullHistory.Add(action);

            _clones.ForEach(clone => { clone.UpdateHistory(action); });
        }

        public void Spawn()
        {
            var clonedHistory = new Queue<Func<Character, bool>>(_fullHistory.Count);

            _fullHistory.ForEach(item => { clonedHistory.Enqueue((Func<Character, bool>)item.Clone()); });

            // TODO: Pls fix Character instantiation
            _clones.Add(new Clone(clonedHistory, new Character()));
        }
    }
}