using System;
using System.Collections.Generic;
using System.Data;
using Model.Data;
using Model.Level.Data;
using Model.LevelEditor.Helper;
using Model.Tiles;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;

namespace Model.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the MapEditor.
        /// </summary>
        static LevelEditor _instance;
        /// <summary>
        /// Gets the singleton instance of the MapEditor.
        /// </summary>
        /// <returns>The singleton instance of the MapEditor.</returns>
        public static LevelEditor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelEditor();
                }
                return _instance;
            }
        }
        /// <summary>
        /// Awake method to ensure the singleton instance is set up correctly.
        /// </summary>
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                throw new System.Exception("MapEditor instance already exists!");
            }
        }
        #endregion

        #region Editing
        void Start()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }
        
        void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
            DestroyPreview();
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.EditingLevel)
            {
                StartEditing();
            }
            else
            {
                DestroyPreview();
            }
        }

        [SerializeField] GameObject groundPrefab;
        [SerializeField] GameObject startPrefab;
        [SerializeField] GameObject placeholderPrefab;
        [SerializeField] List<AllowedTile> allowedTiles = new List<AllowedTile>();
        public List<AllowedTile> AllowedTiles => allowedTiles;
        /// <summary>
        /// The currently selected tile prefab.
        /// </summary>
        Tile selectedTile = null;
        /// <summary>
        /// An instance of the selected tile prefab that is used for previewing the placement.
        /// </summary>
        Tile previewTile = null;
        /// <summary>
        /// The placed preview tiles that are used for visualizing the placement in the editor.
        /// </summary>
        List<Tile> placedPreviewTiles = new List<Tile>();
        /// <summary>
        /// The position of the preview tile in the editor, used for placing tiles.
        /// </summary>
        Coordinate previewPosition = new Coordinate(0, 0, 0);
        /// <summary>
        /// The list of placed tiles in the level editor, used for saving the level data.
        /// </summary>
        List<TilePlacement> placedTiles = new List<TilePlacement>();

        TileData placementData = null;
        public void SetPlacementData(TileData data)
        {
            placementData = data;
        }
        void StartEditing()
        {
            for (int i = -8; i <= 8; i++)
            {
                for (int j = -8; j <= 8; j++)
                {
                    // Place the ground tiles in a grid around the origin
                    TilePlacement previewPlacement = new TilePlacement(
                        new Coordinate(i, j, 0),
                        placeholderPrefab
                    );
                    var tile = Instantiate(previewPlacement.TilePrefab, transform).GetComponent<Tile>();
                    tile.gameObject.name = $"{previewPlacement.TilePrefab.name}_{previewPlacement.Coordinate}_Editor";
                    tile.Initialize(previewPlacement.Coordinate, null);
                    placedPreviewTiles.Add(tile);
                }
            }
            foreach (var tilePlacement in placedTiles)
            {
                Tile tile = Instantiate(tilePlacement.TilePrefab, transform).GetComponent<Tile>();
                tile.gameObject.name = $"{tilePlacement.TilePrefab.name}_{tilePlacement.Coordinate}_Editor";
                tile.Initialize(tilePlacement.Coordinate, tilePlacement.TileData);
                placedPreviewTiles.Add(tile);
            }
            selectedTile = allowedTiles[0].TilePrefab.GetComponent<Tile>();
            previewTile = Instantiate(allowedTiles[0].TilePrefab, transform).GetComponent<Tile>();
            previewTile.gameObject.name = $"{selectedTile.name}_Preview";
            previewTile.Position = previewPosition;
            Debug.Log($"Started editing level with selected tile: {selectedTile.name}");
        }
        public void ResetEditor()
        {
            selectedTile = null;
            if (previewTile != null) Destroy(previewTile.gameObject);
            previewTile = null;
            placedTiles.Clear();
            foreach (var tile in placedPreviewTiles)
            {
                Destroy(tile.gameObject);
            }
            placedPreviewTiles.Clear();
            // Place the ground and start tiles at the origin
            TilePlacement groundPlacement = new TilePlacement(
                new Coordinate(0, 0, 0),
                groundPrefab
            );
            TilePlacement startPlacement = new TilePlacement(
                new Coordinate(0, 0, 1),
                startPrefab
            );
            placedTiles.Add(groundPlacement);
            placedTiles.Add(startPlacement);
        }

        void DestroyPreview()
        {
            Debug.Log("Destroying preview tiles...");
            if (previewTile != null)
            {
                Destroy(previewTile.gameObject);
                previewTile = null;
            }
            foreach (var tile in placedPreviewTiles)
            {
                Destroy(tile.gameObject);
            }
            placedPreviewTiles.Clear();
            foreach (var child in transform)
            {
                if (child is Transform childTransform && (childTransform.name.EndsWith("_Editor") || childTransform.name.EndsWith("_Preview")))
                {
                    // Destroy all child objects that are part of the editor preview
                    Debug.Log($"Destroying child: {childTransform.name}");
                    Destroy(childTransform.gameObject);
                }
            }
        }
        public void Select(string tileName)
        {
            foreach (var allowedTile in allowedTiles)
            {
                if (allowedTile.TileName == tileName)
                {
                    selectedTile = allowedTile.TilePrefab.GetComponent<Tile>();
                    previewTile = Instantiate(selectedTile);
                    previewTile.gameObject.name = $"{selectedTile.name}_Preview";
                    previewTile.Position = previewPosition;
                    Debug.Log($"Selected tile: {selectedTile.name}");
                    if (selectedTile is AccentTile)
                    {
                        previewPosition = new Coordinate(previewPosition.X, previewPosition.Y, 1);
                    }
                    else if (selectedTile is GroundTile)
                    {
                        previewPosition = new Coordinate(previewPosition.X, previewPosition.Y, 0);
                    }
                    //Check if selectedTile has Attribute TileDataType
                    Type t = selectedTile.GetType();
                    var attribute = (TileDataTypeAttribute)Attribute.GetCustomAttribute(t, typeof(TileDataTypeAttribute));
                    if (attribute == null)
                    {
                        placementData = null; // No TileDataTypeAttribute found, set to null
                    }
                    else if (attribute.DataType == typeof(ConnectedTileData))
                    {
                        placementData = new ConnectedTileData(TileConnectionGroup.CYAN);
                    }
                    else if (attribute.DataType == typeof(TimerTileData))
                    {
                        placementData = new TimerTileData(TileConnectionGroup.CYAN, true, 3);
                    }

                    return;
                }
            }
        }

        public void DeleteAt(Coordinate coordinate)
        {
            if (coordinate == new Coordinate(0, 0, 0) || coordinate == new Coordinate(0, 0, 1)) return; // Prevent deleting the origin tile
            placedTiles.RemoveAll(tilePlacement =>
                tilePlacement.Coordinate == coordinate || tilePlacement.Coordinate == coordinate.Above);

            foreach (var tile in placedPreviewTiles)
            {
                if (tile.Position == coordinate || tile.Position == coordinate.Above)
                {
                    if (tile is PlaceholderTile) continue; // Skip placeholder tiles
                    Destroy(tile.gameObject);
                }
            }
            placedPreviewTiles.RemoveAll(tile => tile.Position == coordinate || tile.Position == coordinate.Above);
        }
        public void Move(Coordinate coordinate)
        {
            if (selectedTile != null && selectedTile is AccentTile)
            {
                previewPosition = coordinate.Above;
            }
            else
            {
                previewPosition = coordinate;
            }
            if (previewTile != null)
            {
                previewTile.Position = previewPosition;
            }
        }

        public void Place()
        {
            if (selectedTile == null) return;
            TilePlacement tilePlacement = new TilePlacement(
                previewPosition,
                selectedTile.gameObject,
                placementData
            );
            placedTiles.Add(tilePlacement);
            Tile tile = Instantiate(tilePlacement.TilePrefab, transform).GetComponent<Tile>();
            tile.gameObject.name = $"{tilePlacement.TilePrefab.name}_{tilePlacement.Coordinate}_Editor";
            tile.Initialize(tilePlacement.Coordinate, tilePlacement.TileData);
            tile.Position = previewPosition;
            placedPreviewTiles.Add(tile);
        }

        public LevelData Level  
        {
            get
            {
                LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
                levelData.tilePlacements = new List<TilePlacement>();
                levelData.tilePlacements.AddRange(placedTiles);
                levelData.threeStarThreshold = 0;
                levelData.twoStarThreshold = 0;
                
                return levelData;
            }
        }
        #endregion
    }
}