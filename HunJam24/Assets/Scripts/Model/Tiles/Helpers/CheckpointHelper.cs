using System;
using UnityEngine;

namespace Model.Tiles.Helpers 
{
    public class CheckpointHelper
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the CheckpointHelper.
        /// </summary>
        static CheckpointHelper _instance;
        /// <summary>
        /// Gets the singleton instance of the CheckpointHelper.
        /// </summary>
        /// <returns>The singleton instance of the CheckpointHelper.</returns>
        public static CheckpointHelper Instance {
            get {
                if (_instance == null) {
                    _instance = new CheckpointHelper();
                }
                return _instance;
            }
        }
        #endregion

        #region Checkpoint Management
        public static event Action OnCheckpointActivated;
        /// <summary>
        /// The number of checkpoints in the level.
        /// </summary>
        int checkpointCount = 0;
        /// <summary>
        /// The number of checkpoints in the level.
        /// </summary>
        public int CheckpointCount => checkpointCount;

        /// <summary>
        /// The function a checkpoint tile calls when it is created.
        /// </summary>
        public void CheckpointCreated() {
            checkpointCount++;
        }
        /// <summary>
        /// The function a checkpoint tile calls when it is activated.
        /// </summary>
        public void CheckpointActivated() {
            checkpointCount--;
            OnCheckpointActivated?.Invoke();
        }
        /// <summary>
        /// Used to check if all checkpoints are activated.
        /// </summary>
        /// <returns>True if all checkpoints are activated, false otherwise.</returns>
        public bool AllCheckpointsActivated => checkpointCount == 0;

        /// <summary>
        /// The function to reset the checkpoint count when the level is reset.
        /// </summary>
        public void ResetCheckpointCount() {
            checkpointCount = 0;
        }
        #endregion
    }
}