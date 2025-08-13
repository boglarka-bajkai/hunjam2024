using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;
using View.Tiles;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that is the starting point and finish point of the level.
    /// </summary>
    [RequireComponent(typeof(StartTileRenderer))]
    public sealed class StartTile : AccentTile
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
            _instance = this;
        }
        protected override void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
            Destroy(gameObject);
        }

        public override bool CanEnter(Character character) => true;

        public override bool CanEnter(Tile tile) => true;
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