using System.Collections.Generic;
using System.Linq;
using Serializer;
using JetBrains.Annotations;
using Logic.Characters;
using UnityEngine;
using Logic.Tiles;
using Unity.Collections;
using UnityEngine.UIElements;
using System;

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
        public readonly List<TileBase> Map = new();
        public StartTile StartTile {get;private set; }= null;

        public List<TileBase> GetTilesAt(Vector position)
        {
            var t = Map.Where(x => x.Position.Equals(position)).ToList();
            if (t.Count <= 0) return null;
            return t;
        }
        // Connections maps Spike to Pressureplate
        public void SetMap(Dictionary<Vector, string> map, List<Tuple<Vector, Vector>> connections = null)
        {
            foreach (var item in Map)
            {
                Destroy(item.gameObject);
            }
            StartTile = null;
            Map.Clear();
            if (Player != null) Destroy(Player.gameObject);
            CloneManager.Instance.Reset();
            var maxX = map.Keys.Max(x => x.UnityVector.x);
            var maxY = map.Keys.Max(x => x.UnityVector.y);
            Vector.globalOffset = new Vector3(-maxX / 2, -maxY / 2, 0);
            foreach (var (pos, tile) in map.Select(x => (x.Key, x.Value)))
            {
                var go = Instantiate(getTileByName(tile), pos.UnityVector, Quaternion.identity, transform);
                var t = go.GetComponentInChildren<TileBase>();
                t.Position = pos;
                t.name = pos.ToString();
                if (t is MovableTile) t.name = "box";
                t.GetComponentInChildren<SpriteRenderer>().sortingOrder = pos.Order;
                Map.Add(t);
                if (t is StartTile startTile)
                {
                    StartTile = startTile;
                }
            }
            if (connections != null) {
                foreach(var (fromV, toV) in connections){
                    var from = GetTilesAt(fromV).Where(x => x is ActivationListener).ToList();
                    var to = GetTilesAt(toV).Where(x => x is PressurePlate).ToList();
                    from.ForEach(x=> to.ForEach(y => (y as PressurePlate).Subscribe(x as ActivationListener)));
                }
            }

            var playerPos = StartTile.Position;
            Player = Instantiate(playerPrefab, playerPos.UnityVector, Quaternion.identity).GetComponent<Player>();
            Debug.Log($"Spawning player @ {playerPos.X} {playerPos.Y} {playerPos.Z}");
            Player.SetStartingTile(StartTile);
            PlayerMoved(StartTile);
            
            foreach (var tile in Map)
            {
                tile.UpdateSprite();
            }
        }

        List<TileBase> selectedTiles = new();
        public void ResetTiles() {
            foreach (var tile in selectedTiles)
            {
                tile.GetComponentInChildren<SpriteRenderer>().material = baseMaterial;
            }
        }
        public void PlayerMoved(TileBase newTile)
        {
            ResetTiles();
            Debug.Log("updating-----");
            //player.GetComponent<Character>().ValidMoveDestinations()
            selectedTiles = Player.ValidMoveOntoDestinations();

            foreach (var t in selectedTiles)
            {
                t.GetComponentInChildren<SpriteRenderer>().material = selectMaterial;
            }
        }
    }
}