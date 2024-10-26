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
        Dictionary<Position, Tile> map = new();
        for (int x = 0; x < mapWidth; x++){
            for (int y = 0; y < mapHeight; y++){
                for (int z = 0; z < 3; z++){
                    // var pos = new Position(x,y,z);
                    // var go = Instantiate(tile, pos.UnityVector, Quaternion.identity);
                    // go.name = $"{x} - {y} - {z} Tile";
                    // go.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                    var pos = new Position(x,y,z);
                    var tile = MapManager.Instance.getTileByName("Base");
                    map.Add(pos, tile);
                }
            }
        }
        MapManager.Instance.SetMap(map);
        MapManager.Instance.BuildMap();
    }
}
