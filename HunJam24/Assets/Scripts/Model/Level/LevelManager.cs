using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        #region Level Selection
        [Tooltip("The level sets in game, should be in the order they are unlocked (with star requirements ascending).")]
        [SerializeField] List<LevelSet> levelSets = new List<LevelSet>(); public List<LevelSet> LevelSets => levelSets;

        LevelSet currentLevelSet; public LevelSet CurrentLevelSet => currentLevelSet;
        public void SelectLevelSet(LevelSet levelSet)
        {
            if (!levelSet.IsUnlocked) return;
            Debug.Log($"Selected level set: {levelSet.LevelSetName}");
            currentLevelSet = levelSet;
        }

        public int StarsBefore(LevelSet levelSet)
        {
            int index = levelSets.IndexOf(levelSet);
            if (index == 0) return 0; // No stars before the first level set
            if (index < 0 || index >= levelSets.Count)
            {
                Debug.LogError($"Level set {levelSet.LevelSetName} is not part of the game.");
                return 0;
            }
            return levelSets.Take(index - 1).Sum(ls => ls.TotalStars);
        }

        LevelData currentLevel; public LevelData CurrentLevel => currentLevel;
        public void SelectLevel(LevelData levelData)
        {
            int index = currentLevelSet.Levels.IndexOf(levelData);
            if (index < 0 || index >= currentLevelSet.Levels.Count)
            {
                Debug.LogError($"Level {levelData.LevelName} is not part of the selected level set {currentLevelSet.LevelSetName}.");
                return;
            }
            if (index > 0 && currentLevelSet.Levels[index - 1].CollectedStars == 0)
            {
                Debug.LogError($"Level {levelData.LevelName} is not unlocked yet. Please complete the previous level first.");
                return;
            }
            Debug.Log($"Selected level: {levelData.LevelName}");
            currentLevel = levelData;
        }

        public void SelectPreviewLevel(LevelData levelData)
        {
            currentLevel = levelData;
        }

        public LevelData NextLevel(LevelData currentLevel) {
            if (currentLevelSet == null || currentLevelSet.Levels == null || currentLevelSet.Levels.Count == 0 || !currentLevelSet.Levels.Contains(currentLevel))
            {
                Debug.LogError("Current level set is not set or has no levels."); // This should not happen in production, as this only gets called in context of a level set.
                return null;
            }
            return currentLevelSet.Levels.ElementAtOrDefault(currentLevelSet.Levels.IndexOf(currentLevel) + 1);
        }
        public LevelData PreviousLevel(LevelData currentLevel) {
            if (currentLevelSet == null || currentLevelSet.Levels == null || currentLevelSet.Levels.Count == 0 || !currentLevelSet.Levels.Contains(currentLevel))
            {
                Debug.LogError("Current level set is not set or has no levels."); // This should not happen in production, as this only gets called in context of a level set.
                return null;
            }
            return currentLevelSet.Levels.ElementAtOrDefault(currentLevelSet.Levels.IndexOf(currentLevel) - 1);
        }
        #endregion

        #region Loaded Level Management
        /// <summary>
        /// The tiles that are currently loaded in the level.
        /// </summary>
        readonly List<Tile> loadedTiles = new List<Tile>();
        /// <summary>
        /// The tiles that are currently loaded in the level.
        /// </summary>
        public List<Tile> LoadedTiles => loadedTiles;

        /// <summary>
        /// The currently loaded level.
        /// </summary>
        /// <param name="CurrentLevel">The level to load</param>
        public void LoadLevel()
        {
            Debug.Log($"Loading level: {CurrentLevel.LevelName}");
            UnloadLevel();
            Debug.Log("Spawning tiles...");
            foreach (TilePlacement tilePlacement in CurrentLevel.TilePlacements)
            {
                Tile tile = TileFactory.CreateTile(tilePlacement, transform);
                tile.gameObject.name = $"{tilePlacement.TilePrefab.name}_{tilePlacement.Coordinate}";
                loadedTiles.Add(tile);
            }
            if (StartTile.Instance == null)
            {
                Debug.LogError("No StartTile found in the loaded level.");
                return;
            }
            CharacterFactory.Instance.CreatePlayer();
            steps = 0; // Reset steps for the new level
        }
        public void UnloadLevel()
        {
            Debug.Log("Unloading level...");
            foreach (Tile tile in loadedTiles)
            {
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
        public List<GroundTile> GetGroundTilesAt(Coordinate coordinate)
        {
            List<GroundTile> groundTilesAtCoordinate = new List<GroundTile>();
            foreach (Tile tile in loadedTiles)
            {
                if (tile.Position == coordinate && tile is GroundTile groundTile)
                {
                    groundTilesAtCoordinate.Add(groundTile);
                }
            }
            return groundTilesAtCoordinate;
        }
        public List<AccentTile> GetAccentTilesAt(Coordinate coordinate)
        {
            List<AccentTile> accentTilesAtCoordinate = new List<AccentTile>();
            foreach (Tile tile in loadedTiles)
            {
                if (tile.Position == coordinate && tile is AccentTile accentTile)
                {
                    accentTilesAtCoordinate.Add(accentTile);
                }
            }
            return accentTilesAtCoordinate;
        }
        #endregion
        #region Checkpoint and Tick event handling
        void Start()
        {
            CheckpointHelper.OnCheckpointActivated += OnLoop;
            GameManager.OnTick += OnTick;
        }

        void OnLoop()
        {
            if (loadedTiles == null) return;
            foreach (Tile tile in loadedTiles.Where(t => t is ILoopAware))
            {
                (tile as ILoopAware).OnLoop();
            }
        }

        void OnDestroy()
        {
            CheckpointHelper.OnCheckpointActivated -= OnLoop;
            GameManager.OnTick -= OnTick;
        }

        int steps = 0;
        void OnTick(Coordinate _)
        {
            steps++;
        }
        public void CompleteLevel()
        {
            currentLevel.SetCompletionScore(steps);
            Debug.Log($"Level {currentLevel.LevelName} completed with {steps} steps.");
            UnloadLevel();
        }

        #endregion
    }
    
}