using System;
using Model;
using Model.Data;
using Model.Tiles;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using UnityEngine;
using View.Tiles.Helpers;

namespace View.Tiles
{
    public class StartTileRenderer : MonoBehaviour
    {  
         [SerializeField]
        [Tooltip("The gameobject that is active when all checkpoints are activated.")]
        private GameObject activeSelf;
        void Start()
        {
            CheckpointHelper.OnCheckpointActivated += OnCheckpointActivated;
            activeSelf.SetActive(CheckpointHelper.Instance.AllCheckpointsActivated);
        }

        void OnDestroy()
        {
            CheckpointHelper.OnCheckpointActivated -= OnCheckpointActivated;
        }

        private void OnCheckpointActivated()
        {
            activeSelf.SetActive(CheckpointHelper.Instance.AllCheckpointsActivated);
        }
    }
}