using Model;
using Model.Data;
using Model.Tiles.Helpers;
using UnityEngine;

namespace View
{
    public class LoopShader : MonoBehaviour
    {
        [SerializeField] Material invertMaterial;
        Camera mainCamera;
        void Start()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            CheckpointHelper.OnCheckpointActivated += OnLoop;
        }
        void Awake()
        {
            mainCamera = GetComponent<Camera>();
        }
        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
            CheckpointHelper.OnCheckpointActivated -= OnLoop;
            mainCamera.ResetReplacementShader();
        }

        void OnLoop()
        {
            mainCamera.SetReplacementShader(invertMaterial.shader, null);
        }
        
        void OnGameStateChanged(GameState gameState)
        {
            mainCamera.ResetReplacementShader();
        }
    }
}