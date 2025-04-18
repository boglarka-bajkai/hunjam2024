using System;
using Model.Level.Data;
using Model.Tiles;
using UnityEngine;
namespace Model.Tiles.Helpers
{
    public class TileFactory
    {
        public static Tile CreateTile(TilePlacement placement, Transform parent = null)
        {
            if (placement.TilePrefab == null)
            {
                Debug.LogWarning("Tile prefab is null.");
                return null;
            }

            GameObject obj = GameObject.Instantiate(placement.TilePrefab, parent);
            obj.transform.position = placement.Coordinate.AsUnityVector;

            var tile = obj.GetComponent<Tile>();
            if (tile == null)
            {
                Debug.LogWarning($"Tile prefab {placement.TilePrefab.name} does not have a Tile component.");
                return null;
            }

            var expectedType = GetExpectedTileDataType(tile.GetType());

            if (expectedType != null && placement.TileData != null && !expectedType.IsInstanceOfType(placement.TileData))
            {
                Debug.LogWarning($"TileData for {tile.name} is not of expected type {expectedType.Name}. Was {placement.TileData.GetType().Name}.");
            }

            tile.Initialize(placement.Coordinate, placement.TileData);
            return tile;
        }

        public static Type GetExpectedTileDataType(Type tileType)
        {
            var attr = Attribute.GetCustomAttribute(tileType, typeof(TileDataTypeAttribute)) as TileDataTypeAttribute;
            return attr?.DataType;
        }
    }
}
