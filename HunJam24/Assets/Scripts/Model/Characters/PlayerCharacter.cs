using Model.Characters;
using Model.Data;
using UnityEngine;
namespace Model.Characters
{
    public class PlayerCharacter : Character
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the PlayerCharacter.
        /// </summary>
        private static PlayerCharacter _instance;
        /// <summary>
        /// Gets the singleton instance of the PlayerCharacter.
        /// </summary>
        /// <returns>The singleton instance of the PlayerCharacter.</returns>
        public static PlayerCharacter Instance {
            get {
                if (_instance == null) {
                    _instance = FindFirstObjectByType<PlayerCharacter>();
                    if (_instance == null) {
                        Debug.LogError("PlayerCharacter instance is null. Make sure to create one.");
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// Awake method to ensure the singleton instance is set up correctly.
        /// </summary>
        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            } else if (_instance != this) {
                Destroy(gameObject);
            }
        }
        private void OnDestroy() {
            if (_instance == this) {
                _instance = null;
            }
        }
        #endregion

        public override bool Move(Coordinate newPosition)
        {
            if (!base.Move(newPosition)) return false;
            CloneManager.Instance.AddStep(newPosition);
            return true;
        }
    
    }
}