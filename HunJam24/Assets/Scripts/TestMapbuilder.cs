using UnityEngine;

public class TestMapbuilder : MonoBehaviour
{
    [SerializeField] int mapWidth = 10;
    [SerializeField] int mapHeight = 10;
    [SerializeField] GameObject tile;

    void Start(){
        for (int x = mapWidth; x >= 0; x--){
            for (int y = 0; y < mapHeight; y++){
                for (int z = 0; z < 3; z++){
                    float xPos = (x+y) / 2f;
                    float yPos = (x-y) / 4f + z * 0.5f;
                    var go = Instantiate(tile, new Vector3(xPos,yPos,0), Quaternion.identity);
                    go.name = $"{x} - {y} - {z} Tile";
                }
            }
        }
    }    
}
