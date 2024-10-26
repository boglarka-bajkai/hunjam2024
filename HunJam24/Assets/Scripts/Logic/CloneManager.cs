using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Characters;
using UnityEngine;

namespace Logic
{
    public class CloneManager : MonoBehaviour
    {
        static CloneManager _instance;

        void Awake()
        {
            if (_instance != null) Destroy(this);
            _instance = this;
        }
        public static CloneManager Instance => _instance;


        [SerializeField] GameObject clonePrefab;
        private readonly List<Clone> _clones = new();
        public readonly List<Func<CloneCharacter, bool>> _fullHistory = new();

        public List<Clone> GetClonesAt(Vector position)
        {
            return _clones.Where(clone => clone.Character.Position.Equals(position)).ToList();
        }

        /*
         * Call this only as the player's character acts
         */
        public void UpdateHistory(Func<CloneCharacter, bool> action)
        {
            //TileHistory.Add(tile);

            _clones.ForEach(clone => { clone.UpdateHistory(action); });
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Spawn();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _clones.ForEach(clone => clone.Step());
            }
        }

        private void Spawn()
        {
            var clonedHistory = new Queue<Func<CloneCharacter, bool>>(_fullHistory.Count);

            _fullHistory.ForEach(item => { clonedHistory.Enqueue((Func<CloneCharacter, bool>)item.Clone()); });

            var startingPosition = MapManager.Instance.StartTile.Position + new Vector(0, 0, 1);
            var cloneGameObject = Instantiate(clonePrefab, startingPosition.UnityVector, Quaternion.identity);
            var cloneCharacter = cloneGameObject.GetComponent<CloneCharacter>();
            cloneCharacter.SetStartingTile(MapManager.Instance.StartTile);
            var clone = cloneGameObject.GetComponent<Clone>();
            clone.SetHistory(clonedHistory);
            clone.SetCharacter(cloneCharacter);
            _clones.Add(clone);
        }
    }
}