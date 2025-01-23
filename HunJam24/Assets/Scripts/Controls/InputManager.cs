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
        public void OnTouch(InputAction.CallbackContext context)
        {
            //Do the same as OnClick but with touch
            if (!context.started) return;
            if (Character.IsAnyMoving) return; //Cant move while player or clones are moving
            var ray = Physics2D.GetRayIntersectionAll(_camera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue()));
            if (ray.Length <= 0) return;

            var rayFirst =
                ray
                    .OrderByDescending(x => x.collider.GetComponentInChildren<SpriteRenderer>().sortingOrder)
                    .First();

            var tile = rayFirst.collider.GetComponent<TileBase>();
            if (!CommandExecutor.Execute(tile.Command))
            {
                Debug.Log($"Player could not execute command with tile {tile.name}");
            }
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
            if (!CommandExecutor.Execute(tile.Command))
            {
                Debug.Log($"Player could not execute command with tile {tile.name}");
            }
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Read the mouse delta value
                var delta = context.ReadValue<Vector2>();
                // Invert the delta to move the camera in the opposite direction
                Vector3 move = new Vector3(-delta.x*0.0025f, -delta.y*0.0025f, 0);
                // Apply the movement to the camera's transform
                //clamp to between x = [-8, 8] and y = [-3, 3]
                if (Camera.main.transform.position.x + move.x > 8 || Camera.main.transform.position.x + move.x < -8)
                {
                    move.x = 0;
                }
                if (Camera.main.transform.position.y + move.y > 3 || Camera.main.transform.position.y + move.y < -3)
                {
                    move.y = 0;
                }
                Camera.main.transform.position += move;
            }
        }
    }
}