using System;
using Model;
using Model.Data;
using Model.Tiles;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;
using View.Tiles.Helpers;

namespace View.Tiles
{
    public class ActivatableTileRenderer : MonoBehaviour
    {
        void Awake()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.InGame)
            {
                TileConnectionGroup t = gameObject.GetComponent<IActivatable>().TileGroup;
                Color c = ConnectedTileColorMappings.ColorMappings[t];
                foreach (var s in gameObject.GetComponentsInChildren<SpriteRenderer>(true))
                {
                    s.color = c;
                }
            }
        }
    }
}