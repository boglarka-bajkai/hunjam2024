using System.Collections.Generic;
using Logic;
using UnityEngine;
using Logic.Tiles;
using System;
using Logic.Characters;
public class MapLoader : MonoBehaviour
{

    private static MapLoader _instance;
    void Awake() {
        if (_instance != null) Destroy(this);
        _instance = this;
    }
    public static MapLoader Instance => _instance;

    

    // Dictionary<Vector, string> Map1 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(0, 0, 0), "Base" },
    //         { new Vector(0, 1, 0), "Base" },
    //         { new Vector(0, 2, 0), "Base" },
    //         { new Vector(1, 2, 0), "Base" },
    //         { new Vector(2, 2, 0), "Base" },
    //         { new Vector(2, 1, 0), "Base" },
    //         { new Vector(2, 0, 0), "Base" },
    //         { new Vector(1, 0, 0), "Base" },
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(0, 0, 1), "Start" },
    //         { new Vector(0, 1, 1), "Box" },
    //         { new Vector(2, 2, 1), "Checkpoint" },
    //         { new Vector(0, 2, 1), "Pressureplate"},
    //         { new Vector(2, 0, 1), "Spike"}
    //     };
    // List<Tuple<Vector, Vector>> Conn1 = new() {
    //     new(new Vector(2,0,1), new Vector(0,2,1)),
    // };
    // Dictionary<Vector, string> Map2 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(0, 2, 0), "Base" },
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(0, 4, 0), "Base" },{ new Vector(0, 4, 1), "Start" },
    //         { new Vector(1, 2, 0), "Base" },
    //         { new Vector(1 ,4, 0), "Base" },
    //         { new Vector(2, 0, 0), "Base" },
    //         { new Vector(2, 1, 0), "Base" },
    //         { new Vector(2, 2, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" },
    //         { new Vector(2, 4, 0), "Base" },
    //         { new Vector(3, 0, 0), "Base" },
    //         { new Vector(3, 2, 0), "Base" },
    //         { new Vector(4, 0, 0), "Base" },{ new Vector(4, 0, 1), "Checkpoint" },
    //         { new Vector(4, 1, 0), "Base" },
    //         { new Vector(4, 2, 0), "Base" }
    //     };
    // Dictionary<Vector, string> Map3 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(3, 0, 0), "Base" },
    //         { new Vector(0, 1, 0), "Base" },
    //         { new Vector(0, 2, 0), "Base" },
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(0, 3, 1), "Start" },
    //         { new Vector(1, 1, 0), "Base" },
    //         { new Vector(1, 3, 0), "Base" },
    //         { new Vector(2, 1, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" },
    //         { new Vector(3, 1, 0), "Base" },
    //         { new Vector(3, 2, 0), "Base" },
    //         { new Vector(3, 1, 1), "Box" },
    //         { new Vector(3, 3, 0), "Base" },
    //         { new Vector(4, 1, 0), "Base" },
    //         { new Vector(4, 3, 0), "Base" },
    //         { new Vector(5, 1, 0), "Base" },
    //         { new Vector(5, 3, 0), "Base" },
    //         { new Vector(6, 1, 0), "Base" },
    //         { new Vector(6, 2, 0), "Base" },
    //         { new Vector(6, 3, 0), "Base" },
    //         { new Vector(6, 3, 1), "Checkpoint" }
    //     };
    
    // Dictionary<Vector, string> Map5 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(1, 0, 0), "Base" },
    //         { new Vector(1, 0, 1), "Checkpoint" },
    //         { new Vector(1, 1, 0), "Base" },
    //         { new Vector(1, 2, 0), "Base" }, 
    //         { new Vector(1, 2, 1), "Spike"},
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(1, 3, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" },
    //         { new Vector(0, 4, 0), "Base" },
    //         { new Vector(1, 4, 0), "Base" }, 
    //         { new Vector(1, 4, 1), "Pressureplate"},
    //         { new Vector(2, 4, 0), "Base" },
    //         { new Vector(0, 5, 0), "Base" },
    //         { new Vector(1, 5, 0), "Base" },
    //         { new Vector(1, 5, 1), "Box" },
    //         { new Vector(2, 5, 0), "Base" },
    //         { new Vector(0, 6, 0), "Base" },
    //         { new Vector(1, 6, 0), "Base" },
    //         { new Vector(2, 6, 0), "Base" },
    //         { new Vector(1, 7, 0), "Base" },
    //         { new Vector(1, 7, 1), "Start" }
    //     };
    //     List<Tuple<Vector, Vector>> Conn5 = new() {
    //     new(new Vector(1,2,1), new Vector(1,4,1)),
    // };

    // Dictionary<Vector, string> Map4 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(0, 4, 0), "Base" },
    //         { new Vector(0, 4, 1), "Start" },
    //         { new Vector(1 ,4, 0), "Base" },
    //         { new Vector(2, 0, 0), "Base" },
    //         { new Vector(2, 1, 0), "Base" },
    //         { new Vector(2, 2, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" },
    //         { new Vector(2, 3, 1), "Box" },
    //         { new Vector(2, 4, 0), "Base" },
    //         { new Vector(2, 5, 0), "Base" },
    //         { new Vector(3, 0, 0), "Base" },
    //         { new Vector(3, 2, 0), "Base" },
    //         { new Vector(4, 0, 0), "Base" },
    //         { new Vector(4, 0, 1), "Checkpoint" },
    //         { new Vector(4, 1, 0), "Base" },
    //         { new Vector(4, 2, 0), "Base" }
    //     };
    
    // Dictionary<Vector, string> Map6 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(1, 0, 0), "Base" },
    //         { new Vector(1, 0, 1), "Checkpoint" },
    //         { new Vector(1, 1, 0), "Base" },
    //         { new Vector(1, 2, 0), "Base" }, 
    //         { new Vector(1, 2, 1), "Spike"},
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(1, 3, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" }, 
    //         { new Vector(2, 3, 1), "Pressureplate"},
    //         { new Vector(0, 4, 0), "Base" },
    //         { new Vector(1, 4, 0), "Base" }, 
    //         { new Vector(1, 4, 1), "Box" },
    //         { new Vector(0, 5, 0), "Base" },
    //         { new Vector(1, 5, 0), "Base" },
    //         { new Vector(0, 5, 1), "Start" }
    //     };
    // List<Tuple<Vector, Vector>> Conn6 = new() {
    //     new(new Vector(1,2,1), new Vector(2,3,1)),
    // };
    
    // Dictionary<Vector, string> Map7 =>
    //     new Dictionary<Vector, string>()
    //     {
    //         { new Vector(6, 0, 0), "Base" },
    //         { new Vector(6, 0, 1), "Checkpoint" },
    //         { new Vector(6, 1, 0), "Base" },
    //         { new Vector(6, 2, 0), "Base" },
    //         { new Vector(6, 2, 1), "Spike"},
    //         { new Vector(6, 3, 0), "Base" },
    //         { new Vector(5, 3, 0), "Base" },
    //         { new Vector(4, 3, 0), "Base" },
    //         { new Vector(3, 2, 0), "Base" },
    //         { new Vector(3, 2, 1), "Pressureplate"},
    //         { new Vector(3, 3, 0), "Base" },
    //         { new Vector(3, 3, 1), "Box" },
    //         { new Vector(3, 4, 0), "Base" },
    //         { new Vector(2, 3, 0), "Base" },
    //         { new Vector(2, 4, 0), "Base" },
    //         { new Vector(1, 3, 0), "Base" },
    //         { new Vector(0, 3, 0), "Base" },
    //         { new Vector(0, 3, 1), "Start" }
    //     };
    //     List<Tuple<Vector, Vector>> Conn7 = new() {
    //     new(new Vector(6,2,1), new Vector(3,2,1)),
    // };
    
    Dictionary<Vector, string> Map8 =>
        new Dictionary<Vector, string>()
        {
            { new Vector(0, 4, 0), "Base" },
            { new Vector(0, 4, 1), "Start" },
            { new Vector(1 ,4, 0), "Base" },
            { new Vector(2, 0, 0), "Base" },
            { new Vector(2, 0, 1), "Pressureplate"},
            { new Vector(2, 1, 0), "Base" },
            { new Vector(2, 2, 0), "Base" },
            { new Vector(2, 3, 0), "Base" },
            { new Vector(2, 4, 0), "Base" },
            { new Vector(3, 0, 0), "Base" },
            { new Vector(3, 2, 0), "Base" },
            { new Vector(3, 2, 1), "Spike"},
            { new Vector(4, 0, 0), "Base" },
            { new Vector(4, 0, 1), "Checkpoint" },
            { new Vector(4, 1, 0), "Base" },
            { new Vector(4, 1, 1), "Spike"},
            { new Vector(4, 2, 0), "Base" },
            { new Vector(4, 2, 1), "Spike"}
        };
        List<Tuple<Vector, Vector>> Conn8 = new() {
        new(new Vector(3,2,1), new Vector(2,0,1)),
        new(new Vector(4,1,1), new Vector(2,0,1)),
        new(new Vector(4,2,1), new Vector(2,0,1)),
    };
    
    int currentMap = 0;
    [SerializeField] List<Map> maps;
    public void StartGame() {
        InvertColor.Instance.ToggleColorInversion();
        TryLoadNextMap();
    }
    //First coords is what to connect (spike, inverse spike)
    //Second coords is where to connect (pressure plate)
    List<Tuple<Vector, Vector>> ConnsTest = new(){
        new(new Vector(2, 0, 1), new Vector(0,2,1)),
    };
    List<List<Tuple<Vector, Vector>>> Conns = new();
    public void TryLoadNextMap() {
        InvertColor.Instance.ResetColor();
        if (currentMap < maps.Count){
            MapManager.Instance.SetMap(maps[currentMap]);
            
            currentMap++;
        }
        else {
            OverlayManager.Instance.ShowWinScreen();
        }
    }

    public void RestartMap() {
        InvertColor.Instance.ResetColor();
        MapManager.Instance.SetMap(maps[currentMap-1]);
    }
}
