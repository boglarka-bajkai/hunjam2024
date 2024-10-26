using System.Collections.Generic;
using System.Linq;
using Serializer;
using JetBrains.Annotations;
using UnityEngine;
using Logic.Tiles;
using Unity.Collections;
namespace Logic
{
    
    public class MapManager : MonoBehaviour
    {
        //Singleton Pattern
        static MapManager _instance;
        void Awake() {
            if (_instance != null) {
                Destroy(this);
            }
            _instance = this;
        }
        public static MapManager Instance => _instance;

        // TileSet
        [SerializeField] GameObject player;
        [SerializeField] GameObject clone;
        [SerializeField] TileDictionary tileDictionary;
        public GameObject getTileByName(string name) {
            return tileDictionary[name];
        }

        // Map
        List<Tile> Map = new();
        StartTile startTile = null;
        public Tile GetTileAt(Vector position)
        {
            return Map.First(x=> x.Position == position);
        }
        // public void BuildMap() {
        //     foreach(var tile in Map){
        //         var go = Instantiate(tile, tile.Position.UnityVector, Quaternion.identity);
        //         if (tile is StartTile) {
        //             startTile = (StartTile)tile;
        //         }
        //         Debug.Log($"Spawned at {tile.Position.X} {tile.Position.Y} {tile.Position.Z}");
        //     }
        // }

        public void SetMap(Dictionary<Vector, string> map) {
            foreach (var item in Map){
                Destroy(item.gameObject);
            }
            var maxX = map.Keys.Max(x=>x.X);
            var maxY = map.Keys.Max(x=>x.Y);
            Vector offset = new Vector(-maxX/2, -maxY/2, 0);
            foreach(var (pos, tile) in map.Select(x=> (x.Key, x.Value))){
                var go = Instantiate(getTileByName(tile), pos.UnityVector + offset.UnityVector, Quaternion.identity);
                var t = go.GetComponent<Tile>();
                t.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                Map.Add(t);
            }
        }

        public void Start(){

        }
    }
}