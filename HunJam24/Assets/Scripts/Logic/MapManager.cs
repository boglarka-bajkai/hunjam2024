using System.Collections.Generic;
using System.Linq;
using Serializer;
using JetBrains.Annotations;
using Logic.Characters;
using UnityEngine;
using Logic.Tiles;
using Unity.Collections;
using UnityEngine.UIElements;

namespace Logic
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] Material selectMaterial, baseMaterial;

        //Singleton Pattern
        static MapManager _instance;

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }

            _instance = this;
        }

        public static MapManager Instance => _instance;

        // TileSet
        [SerializeField] GameObject playerPrefab;
        public Player Player { get; private set; }
        [SerializeField] GameObject clone;
        [SerializeField] TileDictionary tileDictionary;

        public GameObject getTileByName(string name)
        {
            return tileDictionary[name];
        }

        // Map
        private readonly List<TileBase> _tiles = new();
        public StartTile StartTile {get;private set; }= null;

        public TileBase GetTileAt(Vector position)
        {
            var t = _tiles.FirstOrDefault(x => x.Position.Equals(position));
            //Debug.Log($"found: {(t == null ? "none" : t.name)}");
            return t;
        }

        public void SetMap(Dictionary<Vector, string> map)
        {
            foreach (var item in _tiles)
            {
                Destroy(item.gameObject);
            }

            var maxX = map.Keys.Max(x => x.UnityVector.x);
            var maxY = map.Keys.Max(x => x.UnityVector.y);
            Vector.globalOffset = new Vector3(-maxX / 2, -maxY / 2, 0);
            foreach (var (pos, tile) in map.Select(x => (x.Key, x.Value)))
            {
                var go = Instantiate(getTileByName(tile), pos.UnityVector, Quaternion.identity);
                var t = go.GetComponent<TileBase>();
                t.Position = pos;
                t.name = pos.ToString();
                if (t is MovableTile) t.name = "box";
                t.GetComponent<SpriteRenderer>().sortingOrder = pos.Order;
                _tiles.Add(t);
                if (t is StartTile)
                {
                    StartTile = (StartTile)t;
                }
            }

            var playerPos = StartTile.Position + new Vector(0, 0, 1);
            Player = Instantiate(playerPrefab, playerPos.UnityVector, Quaternion.identity).GetComponent<Player>();
            Debug.Log($"Spawning player @ {playerPos.X} {playerPos.Y} {playerPos.Z}");
            Player.SetStartingTile(StartTile);
            PlayerMoved(StartTile);
        }

        List<TileBase> selectedTiles = new();

        public void PlayerMoved(TileBase newTile)
        {
            foreach (var tile in selectedTiles)
            {
                tile.GetComponent<SpriteRenderer>().material = baseMaterial;
            }
            Debug.Log("updating-----");
            //player.GetComponent<Character>().ValidMoveDestinations()
            selectedTiles = Player.ValidMoveOntoDestinations();

            foreach (var t in selectedTiles)
            {
                t.GetComponent<SpriteRenderer>().material = selectMaterial;
            }
        }
    }
}