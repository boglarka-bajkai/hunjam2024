
using Model;

/// <summary>
/// Enumeration representing the different game states.
/// </summary>
namespace Model.Data
{
    public enum GameState
    {
        /// <summary>
        /// The game is in the main menu.
        /// </summary>
        MainMenu,
        /// <summary>
        /// The game is in the level selection screen.
        /// </summary>
        LevelSelect,
        /// <summary>
        /// The game is currently being played.
        /// </summary>
        InGame,
        /// <summary>
        /// The game is paused.
        /// </summary>
        PauseMenu,
        /// <summary>
        /// The player has lost the current level.
        /// </summary>
        GameOver,
        /// <summary>
        /// The player has completed the current level.
        /// </summary>
        Victory,
        /// <summary>
        /// The game is in the settings menu.
        /// </summary>
        Settings,
    }
}