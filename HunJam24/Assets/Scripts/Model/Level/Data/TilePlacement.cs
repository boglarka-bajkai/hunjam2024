using System;
using Model.Data;
using Model.Tiles.Data;
using UnityEngine;

namespace Model.Level.Data
{
    /// <summary>
    /// Represents a tile placed in the level.
    /// Has a coordinate and a prefab reference.
    /// Optionally has tile data based on the tile type.
    /// </summary>
    [Serializable]
    public class TilePlacement
    {
        /// <summary>
        /// The coordinate of the tile in the level.
        /// </summary>
        [SerializeField] Coordinate coordinate; public Coordinate Coordinate => coordinate;
        /// <summary>
        /// The prefab of the tile, must have a Tile component attached to it.
        /// </summary>
        [SerializeField] GameObject tilePrefab; public GameObject TilePrefab => tilePrefab;

        /// <summary>
        /// The data for the tile, based on the tile type.
        /// </summary>
        [SerializeReference] TileData tileData; public TileData TileData => tileData;
    }
}
