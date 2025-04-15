using System;
using System.Collections.Generic;
using Logic;
using UnityEngine;

[Serializable]
public class TileConnection
{
    public Vector3Int PressurePlatePosition;
    public List<Vector3Int> ConnectedTiles;

    public Vector PressurePlateVector => new Vector(PressurePlatePosition.x, PressurePlatePosition.y, PressurePlatePosition.z);
    public List<Vector> ConnectedVectors => ConnectedTiles.ConvertAll(v => new Vector(v.x, v.y, v.z));
}