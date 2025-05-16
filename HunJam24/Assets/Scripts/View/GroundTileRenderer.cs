using System;
using Model;
using Model.Data;
using Model.Level;
using Model.Tiles;
using UnityEngine;

namespace View.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(GroundTile))]
    public class GroundTileRenderer : MonoBehaviour
    {
        GroundTile groundTile;
        void Awake()
        {
            Debug.Log("GroundTileRenderer Awake");
            GameManager.OnGameStateChanged += OnGameStateChanged;
            groundTile = GetComponent<GroundTile>();
        }
        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        void OnGameStateChanged(GameState gameState)
        {
            Debug.Log("Game state changed to: " + gameState);
            Coordinate c = groundTile.Position;
            string i1 = (LevelManager.Instance.GetTilesAt(new Coordinate(c.X, c.Y - 1, c.Z)).Count == 0) ? "0" : "1";
            string i2 = (LevelManager.Instance.GetTilesAt(new Coordinate(c.X + 1, c.Y, c.Z)).Count == 0) ? "0" : "1";
            string i3 = (LevelManager.Instance.GetTilesAt(new Coordinate(c.X - 1, c.Y, c.Z)).Count == 0) ? "0" : "1";
            string i4 = (LevelManager.Instance.GetTilesAt(new Coordinate(c.X, c.Y + 1, c.Z)).Count == 0) ? "0" : "1";
            
            string spriteName = "Qube/Qube" + i1 + i2 + i3 + i4;
            Sprite newSprite = Resources.Load<Sprite>(spriteName);

            if (newSprite != null)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite not found: " + spriteName);
            }
        }
    }
}