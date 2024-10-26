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
                    // var pos = new Position(x,y,z);
                    // var go = Instantiate(tile, pos.UnityVector, Quaternion.identity);
                    // go.name = $"{x} - {y} - {z} Tile";
                    // go.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                    var pos = new Vector(x,y,z);
                    //var tile = MapManager.Instance.getTileByName("Base");
                    //var go = Instantiate(tile.gameObject, pos.UnityVector, Quaternion.identity);
                    //tile.SetPosition(pos);
                    map.Add(pos, "Base");
                }
            }
        }
        MapManager.Instance.SetMap(map);
    }
}
