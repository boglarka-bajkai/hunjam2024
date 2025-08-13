using Model.Characters;
using Model.Level;
using Model.Tiles;
using UnityEngine;
namespace Model.Characters.Helpers
{
    public class CharacterFactory : MonoBehaviour
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the CharacterFactory.
        /// </summary>
        private static CharacterFactory _instance;
        /// <summary>
        /// Gets the singleton instance of the CharacterFactory.
        /// </summary>
        /// <returns>The singleton instance of the CharacterFactory.</returns>
        /// <remarks>Use this property to access the CharacterFactory instance.</remarks>
        public static CharacterFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<CharacterFactory>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("CharacterFactory");
                        _instance = go.AddComponent<CharacterFactory>();
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// Awake method to ensure the singleton instance is set up correctly.
        /// </summary>
        /// <remarks>This method is called when the script instance is being loaded.</remarks>
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

        [Tooltip("The prefab for the clone.")]
        [SerializeField] private GameObject clonePrefab;
        [Tooltip("The prefab for the player.")]
        [SerializeField] private GameObject playerPrefab;

        public PlayerCharacter CreatePlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogWarning("Player prefab is null.");
                return null;
            }

            GameObject obj = Instantiate(playerPrefab);
            var player = obj.GetComponent<PlayerCharacter>();

            if (player == null)
            {
                Debug.LogWarning($"Player prefab {playerPrefab.name} does not have a Player component.");
                return null;
            }
            player.Initialize(StartTile.Instance.Position);
            player.gameObject.name = "Player";
            player.gameObject.transform.SetParent(LevelManager.Instance.transform);
            return player;
        }

        public CloneCharacter CreateClone()
        {
            if (clonePrefab == null)
            {
                Debug.LogWarning("Clone prefab is null.");
                return null;
            }

            GameObject obj = Instantiate(clonePrefab);
            var clone = obj.GetComponent<CloneCharacter>();

            if (clone == null)
            {
                Debug.LogWarning($"Clone prefab {clonePrefab.name} does not have a Clone component.");
                return null;
            }
            clone.Initialize(StartTile.Instance.Position);
            clone.gameObject.name = "Clone";
            clone.gameObject.transform.SetParent(LevelManager.Instance.transform);
            return clone;
        }


    }
}