using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that is the starting point and finish point of the level.
    /// </summary>
    public sealed class StartTile : Tile, ITopTile
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the StartTile.
        /// </summary>
        private static StartTile _instance;
        public static StartTile Instance {
            get {
                if (_instance == null) {
                    Debug.LogError("StartTile instance is null. Make sure to assign it in the inspector.");
                }
                return _instance;
            }
        }
        #endregion

        public override void Initialize(Coordinate position, TileData data)
        {
            base.Initialize(position, data);
            if (_instance != null)
            {
                Debug.LogError("StartTile instance already exists. Only one StartTile is allowed.");
                return;
            }
            _instance = this;
        }
        void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public override bool CanEnter(Character character) => true;

        public override bool CanEnter(Tile tile) => true;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;
        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                if (CheckpointHelper.Instance.AllCheckpointsActivated && character is PlayerCharacter)
                {
                    GameManager.Instance.LevelCompleted();
                }
                return true;
            }
            return false;
        }
    }
}