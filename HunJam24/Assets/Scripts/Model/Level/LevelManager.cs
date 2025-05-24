using System.Collections.Generic;
using System.Linq;
using Model.Characters;
using Model.Characters.Helpers;
using Model.Data;
using Model.Level.Data;
using Model.Tiles;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;

namespace Model.Level
{
    /// <summary>
    /// Singleton class that manages loading the levels and the currently loaded level.
    /// </summary>
    /// <remarks>
    /// This class is responsible for managing the levels in the game.
    /// It loads the levels, and keeps track of the currently loaded level, its tiles and characters.
    /// It also handles cleaning up after a level is completed.
    /// </remarks>
    public class LevelManager : MonoBehaviour
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the MapManager.
        /// </summary>
        static LevelManager _instance;
        /// <summary>
        /// Gets the singleton instance of the MapManager.
        /// </summary>
        /// <returns>The singleton instance of the MapManager.</returns>
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<LevelManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<LevelManager>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Awake method to ensure the singleton instance is set up correctly.
        /// </summary>
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Level Set Management
        [Tooltip("The level sets in game, should be in the order they are unlocked (with star requirements ascending).")]
        [SerializeField] List<LevelSet> levelSets = new List<LevelSet>(); public List<LevelSet> LevelSets => levelSets;

        #endregion

        #region Loaded Level Management
        /// <summary>
        /// The tiles that are currently loaded in the level.
        /// </summary>
        List<Tile> loadedTiles = new List<Tile>();
        /// <summary>
        /// The tiles that are currently loaded in the level.
        /// </summary>
        public List<Tile> LoadedTiles => loadedTiles;

        /// <summary>
        /// The currently loaded level.
        /// </summary>
        /// <param name="levelData">The level to load</param>
        public void LoadLevel(LevelData levelData)
        {
            Debug.Log($"Loading level: {levelData.LevelName}");
            UnloadLevel();
            Debug.Log("Spawning tiles...");
            foreach (TilePlacement tilePlacement in levelData.TilePlacements)
            {
                Tile tile = TileFactory.CreateTile(tilePlacement, transform);
                loadedTiles.Add(tile);
            }
            if (StartTile.Instance == null)
            {
                Debug.LogError("No StartTile found in the loaded level.");
                return;
            }
            CharacterFactory.Instance.CreatePlayer();
        }
        public void UnloadLevel()
        {
            Debug.Log("Unloading level...");
            foreach (Tile tile in loadedTiles)
            {
                Debug.Log($"Destroying tile: {tile.name}");
                Destroy(tile.gameObject);
            }
            loadedTiles.Clear();
            CheckpointHelper.Instance.ResetCheckpointCount();
            CloneManager.Instance.ClearClones();
            if (PlayerCharacter.Instance != null) Destroy(PlayerCharacter.Instance);
        }

        public List<Tile> GetTilesAt(Coordinate coordinate)
        {
            List<Tile> tilesAtCoordinate = new List<Tile>();
            foreach (Tile tile in loadedTiles)
            {
                if (tile.Position == coordinate)
                {
                    tilesAtCoordinate.Add(tile);
                }
            }
            return tilesAtCoordinate;
        }
        #endregion

        void Start()
        {
            CheckpointHelper.OnCheckpointActivated += OnLoop;
        }

        void OnLoop()
        {
            if (loadedTiles == null) return;
            foreach (Tile tile in loadedTiles.Where(t => t is ILoopListener))
            {
                (tile as ILoopListener).OnLoop();
            }
        }

        void OnDestroy()
        {
            CheckpointHelper.OnCheckpointActivated -= OnLoop;
        }
    }
    
}