using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic
{
    public class CloneManager : MonoBehaviour
    {
        static CloneManager _instance;
        void Awake() {
            if (_instance != null) Destroy(this);
            _instance = this;
        }
        public static CloneManager Instance {get;}


        [SerializeField] GameObject clonePrefab;
        private readonly List<Clone> _clones = new();
        public readonly List<Func<Player, bool>> _fullHistory = new();

        public List<Clone> GetClonesAt(Vector position)
        {
            return _clones.Where(clone => clone.Character.Position.Equals(position)).ToList();
        }

        /*
         * Call this only as the player's character acts
         */
        public void UpdateHistory(Func<Player, bool> action)
        {
            _fullHistory.Add(action);

            //_clones.ForEach(clone => { clone.UpdateHistory(action); });
        }

        void Update() {
            if (Input.GetKeyUp(KeyCode.Space)){
                Spawn();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)){
                _clones.ForEach(x=> x.Step());
            }
        }
        public void Spawn()
        {
            var clonedHistory = new Queue<Func<Player, bool>>(_fullHistory.Count);

            _fullHistory.ForEach(item => { clonedHistory.Enqueue((Func<Player, bool>)item.Clone()); });

            // TODO: Pls fix Character instantiation
            var pos = MapManager.Instance.StartTile.Position + new Vector(0,0,1);
            var c = Instantiate(clonePrefab, pos.UnityVector, Quaternion.identity);
            //_clones.Add(new Clone(clonedHistory, new Player()));
            _clones.Add(c.GetComponent<Clone>());
        }
    }
}