using System.Linq;
using Logic;
using Model.Characters;
using Model.Data;
using Model.Level;
using Model.Tiles.Interfaces;
using Unity.Collections;
using UnityEngine;
using View.Animated;
namespace Model.Characters
{
    [RequireComponent(typeof(PlayerAnimation))]
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
        public static PlayerCharacter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<PlayerCharacter>();
                }
                return _instance;
            }
        }
        /// <summary>
        /// Awake method to ensure the singleton instance is set up correctly.
        /// </summary>
        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);   
        }
        void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
            Destroy(gameObject);
        }
        #endregion

        public override bool Move(Coordinate newPosition)
        {
            // Save clones that are at the new position where the player is trying to move (before moving)
            var clonesAtNewPos = CloneManager.Instance.GetClonesAt(newPosition);
            var oldPos = Position;
            if (!base.Move(newPosition)) return false;
            // If any clones are at the new position, game over
            if (CloneManager.Instance.GetClonesAt(newPosition).Count > 0)
            {
                GameManager.Instance.LevelLost();
                Debug.LogWarning("Lost because of clone at new position");
                return false;
            }
            // Save clones that are now at the position where the player was (after moving)
            var clonesAtOldPos = CloneManager.Instance.GetClonesAt(oldPos);
            // If there is an intersection, that clone and the player jumped over each other
            if (clonesAtNewPos.Intersect(clonesAtOldPos).Any())
            {
                // If any clones are at the old position, game over
                GameManager.Instance.LevelLost();
                Debug.LogWarning("Lost because of jumping over clone");
                return false;
            }
            if (DiesAtTile(newPosition) || CloneManager.Instance.AnyDies())
            {
                GameManager.Instance.LevelLost();
                Debug.LogWarning("Lost because of stepping on lethal tile");
                return false;
            }
            return true;
        }

    }
}