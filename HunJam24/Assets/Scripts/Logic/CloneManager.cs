﻿using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Characters;
using UnityEngine;

namespace Logic
{
    public class CloneManager : MonoBehaviour
    {
        void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        public static CloneManager Instance { get; private set; }


        [SerializeField] private GameObject clonePrefab;
        private readonly List<Clone> _clones = new();
        private readonly List<Func<CloneCharacter, bool>> _fullHistory = new();

        public List<Clone> GetClonesAt(Vector position)
        {
            return _clones.Where(clone => clone.Character.Position.Equals(position)).ToList();
        }

        /*
         * Call this only as the player's character acts
         */
        public void UpdateHistory(Func<CloneCharacter, bool> action)
        {
            _fullHistory.Add(action);

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
                Tick();
            }
        }
        public void Tick() {
            _clones.ForEach(clone => clone.Step());
        }
        public void Spawn()
        {
            var clonedHistory = new Queue<Func<CloneCharacter, bool>>(_fullHistory.Count);
            _fullHistory.ForEach(item => { clonedHistory.Enqueue((Func<CloneCharacter, bool>)item.Clone()); });
            Debug.Log($"clonedHistory: {clonedHistory.Count}");

            var startingPosition = MapManager.Instance.StartTile.Position;
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