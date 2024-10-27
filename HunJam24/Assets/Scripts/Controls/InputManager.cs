using System.Linq;
using Logic;
using Logic.Characters;
using Logic.Tiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class InputManager : MonoBehaviour
    {
        private Camera _camera;
        private AudioManager _audioManager;

        private void Awake()
        {
            _camera = Camera.main;
            _audioManager = GetComponent<AudioManager>();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            if (Character.IsAnyMoving) return; //Cant move while player or clones are moving
            var ray = Physics2D.GetRayIntersectionAll(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (ray.Length <= 0) return;

            var rayFirst =
                ray
                    .OrderByDescending(x => x.collider.GetComponentInChildren<SpriteRenderer>().sortingOrder)
                    .First();

            var tile = rayFirst.collider.GetComponent<TileBase>();
            if (tile == null) Debug.Log("tile null!!!!");
            if (!CommandExecutor.Execute(tile.Command))
            {
                Debug.Log($"Player could not execute command with tile {tile.name}");
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _audioManager.PlayReversedMusic(2f);
                _audioManager.PlaySoundEffect("Rewind", false);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                _audioManager.PlaySoundEffect("Rewind", false);
            }
        }
    }
}