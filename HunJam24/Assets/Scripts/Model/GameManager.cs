using UnityEngine;
using Model.Data;
using Model.Level;
using Model.LevelEditor;
namespace Model
{
    /// <summary>
    /// Singleton class that manages the game state and data.
    /// </summary>
    /// <remarks>
    /// This class is responsible for managing the global game state. 
    /// It receives player input from Control, and updates the game state accordingly.
    /// It also updates the View with the current game state.
    /// </remarks>
    public class GameManager : MonoBehaviour
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the GameManager.
        /// </summary>
        private static GameManager _instance;
        /// <summary>
        /// Gets the singleton instance of the GameManager.
        /// </summary>
        /// <returns>The singleton instance of the GameManager.</returns>
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
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

        #region State Event Management

        void Start()
        {
            MainMenu();
        }
        /// <summary>
        /// The current game state.
        /// </summary>
        GameState currentGameState = GameState.MainMenu;
        /// <summary>
        /// The current game state.
        /// </summary>
        /// <returns>The current game state.</returns>
        public GameState CurrentGameState => currentGameState;

        /// <summary>
        /// An event that is triggered when the game state changes.
        /// </summary>
        public static event System.Action<GameState> OnGameStateChanged;

        /// <summary>
        /// An event that is triggered when the player makes a valid move.
        /// </summary>
        public static event System.Action<Coordinate> OnTick;

        public static void InvokeTick(Coordinate coordinate)
        {
            OnTick?.Invoke(coordinate);
        }

        #endregion

        #region Game State Management
        /// <summary>
        /// Starts the game with the given map.
        /// </summary>
        /// <param name="mapIndex">The index of the map to start the game with.</param>
        /// <remarks>
        /// This method loads the map, sets the game state to InGame and triggers the OnGameStateChanged event.
        /// </remarks>
        // public void StartGame(int mapIndex) {
        //     currentMapIndex = mapIndex;
        //     LevelManager.Instance.LoadLevel(LevelManager.Instance.LevelSets[mapIndex].Levels[0]);
        //     currentGameState = GameState.InGame;
        //     OnGameStateChanged?.Invoke(currentGameState);
        // }
        //Temp startgame without mapIndex
        public void StartGame()
        {
            LevelManager.Instance.LoadLevel();
            currentGameState = GameState.InGame;
            OnGameStateChanged?.Invoke(currentGameState);
            //TODO: level select based startgame
        }

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void PauseGame()
        {
            currentGameState = GameState.PauseMenu;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public void ResumeGame()
        {
            currentGameState = GameState.InGame;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame()
        {
            //StartGame(currentMapIndex);
            StartGame();
        }

        /// <summary>
        /// Ends the game and returns to the main menu.
        /// </summary>
        public void MainMenu()
        {
            LevelManager.Instance.UnloadLevel();
            currentGameState = GameState.MainMenu;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        /// <summary>
        /// Signals that the played has completed the level.
        /// </summary>
        public void LevelCompleted()
        {
            currentGameState = GameState.Victory;
            OnGameStateChanged?.Invoke(currentGameState);
            LevelManager.Instance.CompleteLevel();
        }

        /// <summary>
        /// Signals that the player is selecting the next level to play.
        /// </summary>
        public void LevelSelect()
        {
            currentGameState = GameState.LevelSelect;
            OnGameStateChanged?.Invoke(currentGameState);
        }
        /// <summary>
        /// Signals that the player is selecting the level set to play.
        /// </summary>
        public void LevelSetSelect()
        {
            currentGameState = GameState.LevelSetSelect;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        /// <summary>
        /// Signals that the player has entered settings.
        /// </summary>
        public void Settings()
        {
            currentGameState = GameState.Settings;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        public void LevelLost()
        {
            currentGameState = GameState.GameOver;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        public void EditLevel(bool reset = true)
        {
            if (reset) LevelEditor.LevelEditor.Instance.ResetEditor();
            currentGameState = GameState.EditingLevel;
            OnGameStateChanged?.Invoke(currentGameState);
        }

        public void TestLevel()
        {
            LevelManager.Instance.SelectPreviewLevel(LevelEditor.LevelEditor.Instance.Level);
            currentGameState = GameState.TestingLevel;
            LevelManager.Instance.LoadLevel();
            OnGameStateChanged?.Invoke(currentGameState);
        }

        #endregion
    }
}