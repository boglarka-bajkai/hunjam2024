using System.Collections.Generic;
using System.Linq;
using Serializer;
using JetBrains.Annotations;
using UnityEngine;
using Logic.Tiles;
using Unity.Collections;
using UnityEngine.UIElements;
namespace Logic
{
    
    public class MapManager : MonoBehaviour
    {
        static Color selected = new Color(255, 224, 126);
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
            var t = Map.FirstOrDefault(x=> x.Position.Equals(position));
            Debug.Log($"found: {(t == null ? "none" : t.name)}");
            return t;
        }

        public void SetMap(Dictionary<Vector, string> map) {
            foreach (var item in Map){
                Destroy(item.gameObject);
            }
            var maxX = map.Keys.Max(x=>x.UnityVector.x);
            var maxY = map.Keys.Max(x=>x.UnityVector.y);
            Vector3 offset = new Vector3(-maxX/2, -maxY/2, 0);
            foreach(var (pos, tile) in map.Select(x=> (x.Key, x.Value))){
                var go = Instantiate(getTileByName(tile), pos.UnityVector + offset, Quaternion.identity);
                var t = go.GetComponent<Tile>();
                t.Position = pos;
                t.name = pos.ToString();
                t.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                Map.Add(t);
                if (t is StartTile) {
                    startTile = (StartTile)t;
                }
            }
            var playerPos = startTile.Position + new Vector(0,0,1);
            var p = Instantiate(player, playerPos.UnityVector + offset, Quaternion.identity);
            Debug.Log($"Spawning player @ {playerPos.X} {playerPos.Y} {playerPos.Z}");
            p.GetComponent<Character>().SetStartingTile(startTile);
            PlayerMoved(startTile);
        }

        public void PlayerMoved(Tile newTile){
            Debug.Log($"Valid neighbors {newTile.GetValidNeighbors().Count}");
            foreach (var t in newTile.GetValidNeighbors()){
                Debug.Log($"set {t.name}");
                t.GetComponent<SpriteRenderer>().color = new Color(255, 224, 126);
            }
        }


    }
}