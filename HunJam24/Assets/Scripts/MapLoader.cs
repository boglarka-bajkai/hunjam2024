using System.Collections.Generic;
using Logic;
using UnityEngine;
using Logic.Tiles;
using System;
public class MapLoader : MonoBehaviour
{

    private static MapLoader _instance;
    void Awake() {
        if (_instance != null) Destroy(this);
        _instance = this;
    }
    public static MapLoader Instance => _instance;
    [SerializeField] int mapWidth = 5;
    [SerializeField] int mapHeight = 5;
    [SerializeField] GameObject tile;

    

    Dictionary<Vector, string> Map1 =>
        new Dictionary<Vector, string>()
        {
            { new Vector(0, 0, 0), "Base" },
            { new Vector(0, 1, 0), "Base" },
            { new Vector(0, 2, 0), "Base" },
            { new Vector(1, 2, 0), "Base" },
            { new Vector(2, 2, 0), "Base" },
            { new Vector(2, 1, 0), "Base" },
            { new Vector(2, 0, 0), "Base" },
            { new Vector(1, 0, 0), "Base" },
            { new Vector(3, 0, 0), "Base" },
            { new Vector(0, 0, 1), "Start" },
            //{ new Vector(2, 0, 1), "Box" },
            { new Vector(2, 2, 1), "Checkpoint" },
            { new Vector(0, 2, 1), "Pressureplate"},
            { new Vector(1, 0, 1), "Spike"}
        };
    Dictionary<Vector, string> Map2 =>
        new Dictionary<Vector, string>()
        {
            { new Vector(1, 0, 0), "Base" },
            { new Vector(1, 0, 1), "Checkpoint" },
            { new Vector(1, 1, 0), "Base" },
            { new Vector(1, 2, 0), "Base" }, //map.Add(new Vector(1, 2, 1), "Spike");
            { new Vector(0, 3, 0), "Base" },
            { new Vector(1, 3, 0), "Base" },
            { new Vector(2, 3, 0), "Base" },
            { new Vector(0, 4, 0), "Base" },
            { new Vector(1, 4, 0), "Base" }, //map.Add(new Vector(1, 2, 1), "Plate");
            { new Vector(2, 4, 0), "Base" },
            { new Vector(0, 5, 0), "Base" },
            { new Vector(1, 5, 0), "Base" },
            { new Vector(1, 5, 1), "Box" },
            { new Vector(2, 5, 0), "Base" },
            { new Vector(0, 6, 0), "Base" },
            { new Vector(1, 6, 0), "Base" },
            { new Vector(2, 6, 0), "Base" },
            { new Vector(1, 7, 0), "Base" },
            { new Vector(1, 7, 1), "Start" }
        };
    Dictionary<Vector, string> Map3 =>
        new Dictionary<Vector, string>()
        {
            { new Vector(3, 0, 0), "Base" },
            { new Vector(0, 1, 0), "Base" },
            { new Vector(0, 2, 0), "Base" },
            { new Vector(0, 3, 0), "Base" },
            { new Vector(0, 3, 1), "Start" },
            { new Vector(1, 1, 0), "Base" },
            { new Vector(1, 3, 0), "Base" },
            { new Vector(2, 1, 0), "Base" },
            { new Vector(2, 3, 0), "Base" },
            { new Vector(3, 1, 0), "Base" },
            { new Vector(3, 2, 0), "Base" },
            { new Vector(3, 2, 1), "Box" },
            { new Vector(3, 3, 0), "Base" },
            { new Vector(4, 1, 0), "Base" },
            { new Vector(4, 3, 0), "Base" },
            { new Vector(5, 1, 0), "Base" },
            { new Vector(5, 3, 0), "Base" },
            { new Vector(6, 1, 0), "Base" },
            { new Vector(6, 2, 0), "Base" },
            { new Vector(6, 3, 0), "Base" },
            { new Vector(6, 3, 1), "Checkpoint" }
        };

    int currentMap = 0;
    List<Dictionary<Vector, string>> maps = new();
    public void Start() {
        maps.Add(Map1);
        maps.Add(Map2);
        maps.Add(Map3);
        TryLoadNextMap();
    }

    List<Tuple<Vector, Vector>> Conn1 = new(){
        new(new Vector(1, 0, 1), new Vector(0,2,1)),
    };
    public void TryLoadNextMap() {
        if (currentMap < maps.Count){
            MapManager.Instance.SetMap(maps[currentMap++], Conn1);
        }
        else {
            //TODO: Victory screen
        }
        
    }
}
