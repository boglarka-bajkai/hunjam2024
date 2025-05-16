using System.Collections.Generic;
using Logic;
using Model.Characters;
using Model.Characters.Helpers;
using Model.Data;
using Model.Tiles.Helpers;
using UnityEngine;
using UnityEngine.UIElements;
namespace Model.Characters
{
    public class CloneManager : MonoBehaviour
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the CloneManager.
        /// </summary>
        private static CloneManager _instance;
        /// <summary>
        /// Gets the singleton instance of the CloneManager.
        /// /// </summary>
        /// <returns>The singleton instance of the CloneManager.</returns>
        public static CloneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<CloneManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("CloneManager");
                        _instance = go.AddComponent<CloneManager>();
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
        #region Clone Management
        /// <summary>
        /// The clones that are currently in the level.
        /// </summary>
        public List<CloneCharacter> clones { get; private set; } = new List<CloneCharacter>();
        /// <summary>
        /// The clones that are currently in the level.
        /// </summary>
        /// <returns>The clones that are currently in the level.</returns>

        /// <summary>
        /// The steps the player has taken, cached for new clones to use.
        /// </summary>
        public List<Coordinate> Steps { get; private set; } = new List<Coordinate>();
        /// <summary>
        /// Adds a step to the list of steps.
        /// </summary>
        /// <param name="position">The position of the step.</param>
        public void AddStep(Coordinate position)
        {
            Steps.Add(position);
        }

        /// <summary>
        /// Adds a clone to the list of clones.
        /// </summary>
        void AddClone()
        {
            CloneCharacter clone = CharacterFactory.Instance.CreateClone();
            Debug.Log($"Clone created: {clone}");
            if (clone == null) return;
            clones.Add(clone);
        }

        void Start()
        {
            CheckpointHelper.OnCheckpointActivated += AddClone;
            GameManager.OnTick += AddStep;
        }
        void OnDestroy()
        {
            CheckpointHelper.OnCheckpointActivated -= AddClone;
            GameManager.OnTick -= AddStep;
        }
        #endregion
    }
}