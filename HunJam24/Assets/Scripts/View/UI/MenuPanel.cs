using Model;
using Model.Data;
using UnityEngine;

namespace View.UI
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] GameState activeGameState;
        void Awake()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        void OnGameStateChanged(GameState gameState)
        {
            gameObject.SetActive(gameState == activeGameState); 
        }
    }
}