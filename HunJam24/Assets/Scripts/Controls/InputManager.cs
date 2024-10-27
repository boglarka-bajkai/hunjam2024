using System.Linq;
using Logic;
using Logic.Tiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class InputManager : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;

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
    }
}