using System.Collections.Generic;
using Logic;
using UnityEngine;
using Logic.Tiles;
public class TestMapbuilder : MonoBehaviour
{
    [SerializeField] int mapWidth = 5;
    [SerializeField] int mapHeight = 5;
    [SerializeField] GameObject tile;

    void Start(){
        Dictionary<Vector, string> map = new();
        map.Add(new Vector(0, 0, 0), "Base");
        map.Add(new Vector(0, 1, 0), "Base");
        map.Add(new Vector(0, 2, 0), "Base");
        map.Add(new Vector(1, 2, 0), "Base");
        map.Add(new Vector(2, 2, 0), "Base");
        map.Add(new Vector(2, 1, 0), "Base");
        map.Add(new Vector(2, 0, 0), "Base");
        map.Add(new Vector(1, 0, 0), "Base");
        map.Add(new Vector(0, 0, 1), "Start");
        map.Add(new Vector(2, 2, 1), "Checkpoint");
        
        MapManager.Instance.SetMap(map);
    }
}
