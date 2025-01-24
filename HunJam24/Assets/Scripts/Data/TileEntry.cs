using System;
using Logic;
using UnityEngine;

[Serializable]
public class TileEntry
{
    public Vector3Int Position;
    public string TileName;

    public Vector Vector => new Vector(Position.x, Position.y, Position.z);

    public TileEntry(Vector3Int position, string tileName)
    {
        Position = position;
        TileName = tileName;
    }
}