using System.Collections.Generic;
using Logic;
using UnityEngine;
using Logic.Tiles;
public class TestMapbuilder : MonoBehaviour
{
    [SerializeField] int mapWidth = 5;
    [SerializeField] int mapHeight = 5;
    [SerializeField] GameObject tile;

    /*void Start(){
        Dictionary<Vector, string> map = new();
        map.Add(new Vector(0, 0, 0), "Base");
        map.Add(new Vector(0, 1, 0), "Base");
        map.Add(new Vector(0, 2, 0), "Base");
        map.Add(new Vector(1, 2, 0), "Base");
        map.Add(new Vector(2, 2, 0), "Base");
        map.Add(new Vector(2, 1, 0), "Base");
        map.Add(new Vector(2, 0, 0), "Base");
        map.Add(new Vector(1, 0, 0), "Base");
        map.Add(new Vector(3, 0, 0), "Base");
        map.Add(new Vector(0, 0, 1), "Start");
        map.Add(new Vector(2, 0, 1), "Box");
        map.Add(new Vector(2, 2, 1), "Checkpoint");
        
        MapManager.Instance.SetMap(map);
    }*/
    
    //Advanced1
    /*void Start(){
        Dictionary<Vector, string> map = new();
        map.Add(new Vector(1, 0, 0), "Base"); map.Add(new Vector(1, 0, 1), "Checkpoint");
        map.Add(new Vector(1, 1, 0), "Base");
        map.Add(new Vector(1, 2, 0), "Base"); //map.Add(new Vector(1, 2, 1), "Spike");
        map.Add(new Vector(0, 3, 0), "Base");
        map.Add(new Vector(1, 3, 0), "Base");
        map.Add(new Vector(2, 3, 0), "Base");
        map.Add(new Vector(0, 4, 0), "Base");
        map.Add(new Vector(1, 4, 0), "Base"); //map.Add(new Vector(1, 2, 1), "Plate");
        map.Add(new Vector(2, 4, 0), "Base");
        map.Add(new Vector(0, 5, 0), "Base");
        map.Add(new Vector(1, 5, 0), "Base"); map.Add(new Vector(1, 5, 1), "Box");
        map.Add(new Vector(2, 5, 0), "Base");
        map.Add(new Vector(0, 6, 0), "Base");
        map.Add(new Vector(1, 6, 0), "Base");
        map.Add(new Vector(2, 6, 0), "Base");
        map.Add(new Vector(1, 7, 0), "Base"); map.Add(new Vector(1, 7, 1), "Start");
        
        MapManager.Instance.SetMap(map);
    }*/
    
    void Start(){
        Dictionary<Vector, string> map = new();
        map.Add(new Vector(1, 0, 0), "Base"); map.Add(new Vector(1, 0, 1), "Checkpoint");
        map.Add(new Vector(1, 1, 0), "Base");
        map.Add(new Vector(1, 2, 0), "Base"); //map.Add(new Vector(1, 2, 1), "Spike");
        map.Add(new Vector(0, 3, 0), "Base");
        map.Add(new Vector(1, 3, 0), "Base");
        map.Add(new Vector(2, 3, 0), "Base");
        map.Add(new Vector(0, 4, 0), "Base");
        map.Add(new Vector(1, 4, 0), "Base"); //map.Add(new Vector(1, 2, 1), "Plate");
        map.Add(new Vector(2, 4, 0), "Base");
        map.Add(new Vector(0, 5, 0), "Base");
        map.Add(new Vector(1, 5, 0), "Base"); map.Add(new Vector(1, 5, 1), "Box");
        map.Add(new Vector(2, 5, 0), "Base");
        map.Add(new Vector(0, 6, 0), "Base");
        map.Add(new Vector(1, 6, 0), "Base");
        map.Add(new Vector(2, 6, 0), "Base");
        map.Add(new Vector(1, 7, 0), "Base"); map.Add(new Vector(1, 7, 1), "Start");
        
        MapManager.Instance.SetMap(map);
    }
}
