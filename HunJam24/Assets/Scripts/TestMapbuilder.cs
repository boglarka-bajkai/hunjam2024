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
        for (int x = 0; x < mapWidth; x++){
            for (int y = 0; y < mapHeight; y++){
                for (int z = 0; z < 3; z++){
                    var pos = new Vector(x,y,z);
                    if (pos.X == 0 && pos.Y == 0 && pos.Z == 2){
                        map.Add(pos, "Start");
                        continue;
                    }
                    map.Add(pos, "Base");
                }
            }
        }
        map.Add(new Vector(8, 1, 3), "Box");
        map.Add(new Vector(1, 8, 3), "Checkpoint");
        MapManager.Instance.SetMap(map);
    }
}
