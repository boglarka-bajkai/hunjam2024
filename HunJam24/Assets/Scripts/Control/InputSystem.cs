using System.Collections;
using System.Linq;
using Model;
using Model.Characters;
using Model.Data;
using Model.Level;
using Model.Tiles;
using Model.Tiles.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using View.Animated;

namespace Control.Input
{
    public class InputSystem : MonoBehaviour
    {
        new Camera camera;
        // Player movement
        bool canMove = true;
        void Awake()
        {
            GameManager.OnTick += OnTick;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            PlayerAnimation.OnAnimationFinished += OnPlayerAnimationFinished;
            camera = GetComponent<Camera>();
            if (camera == null)
            {
                Debug.LogError("Camera component not found on InputSystem GameObject.");
            }
        }

        void OnTick(Coordinate _)
        {
            canMove = false;
        }

        void OnPlayerAnimationFinished()
        {
            canMove = true;
        }

        void OnGameStateChanged(GameState gameState)
        {
            canMove = gameState == GameState.InGame;
            if (gameState == GameState.InGame)
            {
                var tiles = LevelManager.Instance.LoadedTiles.Where(tile => tile is IGroundTile);

                if (tiles != null && tiles.Any())
                {
                    //Find middle of tiles renderers
                    Bounds combinedBounds = new Bounds(tiles.ElementAt(0).transform.position, Vector3.zero);
                    foreach (var tile in tiles)
                    {
                        var renderer = tile.GetComponentInChildren<SpriteRenderer>();
                        if (renderer != null)
                        {
                            combinedBounds.Encapsulate(renderer.bounds);
                        }
                    }
                    // Set camera position to the middle of the tiles
                    camera.transform.position = new Vector3(
                        combinedBounds.center.x,
                        combinedBounds.center.y,
                        camera.transform.position.z
                    );
                }
            }
        }
        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.started) return; // Return if the input is not a tap (i.e., not started)
            if (GameManager.Instance.CurrentGameState != GameState.InGame) return; // Return if the game is not in the InGame state
            if (!canMove) return; // Return if the player is not allowed to move (currently in animation)
            Vector2 screenPosition = Mouse.current != null && Mouse.current.leftButton.isPressed
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current?.primaryTouch.position.ReadValue() ?? Vector2.zero;
            var ray = Physics2D.GetRayIntersectionAll(camera.ScreenPointToRay(screenPosition));
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

        // Camera movement
        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.performed && zoomCoroutine == null)
            {
                // Read the mouse delta value
                var delta = context.ReadValue<Vector2>();
                // Invert the delta to move the camera in the opposite direction
                Vector3 move = new Vector3(-delta.x * 0.0025f, -delta.y * 0.0025f, 0);
                // Apply the movement to the camera's transform
                //clamp to between x = [-8, 8] and y = [-3, 3]
                if (camera.transform.position.x + move.x > 8 || camera.transform.position.x + move.x < -8)
                {
                    move.x = 0;
                }
                if (camera.transform.position.y + move.y > 3 || camera.transform.position.y + move.y < -3)
                {
                    move.y = 0;
                }
                camera.transform.position += move;
            }
        }


        // Zooming
        Coroutine zoomCoroutine;
        Vector2 firstFingerPosition, secondFingerPosition;
        float previousDistance;
        const float zoomSpeed = 0.005f;
        public void OnSecondTouchDetected(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.CurrentGameState != GameState.InGame) return; // Return if the game is not in the InGame state
            if (context.started && zoomCoroutine == null)
            {
                previousDistance = Vector2.Distance(firstFingerPosition, secondFingerPosition);
                zoomCoroutine = StartCoroutine(DoZoom());
            }
            else if (context.canceled && zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }
        }
        public void OnFirstFingerTouch(InputAction.CallbackContext context)
        {
            firstFingerPosition = context.ReadValue<Vector2>();
        }

        public void OnSecondFingerTouch(InputAction.CallbackContext context)
        {
            secondFingerPosition = context.ReadValue<Vector2>();
        }


        IEnumerator DoZoom()
        {
            while (true)
            {
                Debug.Log($"First Finger Position: {firstFingerPosition}, Second Finger Position: {secondFingerPosition}");
                float currentDistance = Vector2.Distance(firstFingerPosition, secondFingerPosition);
                float delta = currentDistance - previousDistance;
                Debug.Log($"Zooming: {delta}");
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - delta * zoomSpeed, 1f, 7f);
                previousDistance = currentDistance;
                yield return null;
            }
        }
    }
}