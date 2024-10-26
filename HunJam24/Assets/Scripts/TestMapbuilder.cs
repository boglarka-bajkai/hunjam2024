using Logic;
using UnityEngine;

public class TestMapbuilder : MonoBehaviour
{
    [SerializeField] int mapWidth = 5;
    [SerializeField] int mapHeight = 5;
    [SerializeField] GameObject tile;

    void Start(){
        for (int x = 0; x < mapWidth; x++){
            for (int y = 0; y < mapHeight; y++){
                for (int z = 0; z < 3; z++){
                    var pos = new Position(x,y,z);
                    var go = Instantiate(tile, pos.UnityVector, Quaternion.identity);
                    go.name = $"{x} - {y} - {z} Tile";
                    go.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                }
            }
        }
    }
}
