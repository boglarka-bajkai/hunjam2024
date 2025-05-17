using System.Linq;
using Model;
using Model.Characters;
using Model.Data;
using Model.Tiles;
using Model.Tiles.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Control.Input
{
    public class InputSystem : MonoBehaviour
    {
        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.started) return; // Return if the input is not initialized
            if (GameManager.Instance.CurrentGameState != GameState.InGame) return; // Return if the game is not in the InGame state
            Vector2 screenPosition = Mouse.current != null && Mouse.current.leftButton.isPressed
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current?.primaryTouch.position.ReadValue() ?? Vector2.zero;
            var ray = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(screenPosition));
            if (ray.Length <= 0) return;

            var rayFirst =
                ray
                    .OrderByDescending(x => x.collider.GetComponentInChildren<SpriteRenderer>().sortingOrder)
                    .First();
            var tile = rayFirst.collider.GetComponent<Tile>();
            if (tile == null) return;
            if (PlayerCharacter.Instance == null)
            {
                Debug.LogError("No player but we are in-game!");
                return; // Return if the player character is not initialized
            }
            Coordinate position = tile is ITopTile ? tile.Position : tile.Position.Above;
            if (!PlayerCharacter.Instance.Move(position))
            {
                Debug.Log($"Player could not move to tile {tile.name}");
            }
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Read the mouse delta value
                var delta = context.ReadValue<Vector2>();
                // Invert the delta to move the camera in the opposite direction
                Vector3 move = new Vector3(-delta.x * 0.0025f, -delta.y * 0.0025f, 0);
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