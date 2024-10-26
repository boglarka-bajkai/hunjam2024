using System.Collections.Generic;
using System.Linq;
using Serializer;
using JetBrains.Annotations;
using UnityEngine;
using Logic.Tiles;
namespace Logic
{
    
    public class MapManager : MonoBehaviour
    {
        static MapManager _instance;
        void Awake() {
            if (_instance != null) {
                Destroy(this);
            }
            _instance = this;
        }

        public static MapManager Instance => _instance;
        Dictionary<Position, Tile> Map = new();

        public TileDictionary tileDictionary;
        public Tile getTileByName(string name) {
            return tileDictionary[name];
        }
        public Tile Get(Position position)
        {
            return Map[position];
        }

        public void BuildMap() {
            foreach(var (pos, tile) in Map.Select(x=> (x.Key, x.Value))){
                var go = Instantiate(tile.Prefab, pos.UnityVector, Quaternion.identity);
                tile.Spawn(go, pos);
            }
        }

        public void SetMap(Dictionary<Position, Tile> map) {
            Map = map;
        }
    }
}