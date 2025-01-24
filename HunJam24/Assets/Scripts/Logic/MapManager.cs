using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] List<Tile> tiles;
        //Singleton Pattern
        static MapManager _instance;
        public static List<Tile> Tiles => _instance.tiles;

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

        public Tile getTileByName(string name)
        {
            return tiles.Find(x => x.Name == name);
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

        public void DestroyMap() {
            foreach (var item in Map)
            {
                Destroy(item.gameObject);
            }
            selectedTiles.Clear();
            Map.Clear();

            if (Player != null) Destroy(Player.gameObject);
            CloneManager.Instance.Reset();
        }
        // Connections maps Spike to Pressureplate
        public void SetMap(Map map)
        {
            DestroyMap();
            CloneManager.Instance.Reset();
            foreach (var tile in map.Tiles)
            {
                Debug.Log(tile.TileName);
                var go = Instantiate(getTileByName(tile.TileName).Prefab, tile.Vector.UnityVector, Quaternion.identity, transform);
                var t = go.GetComponentInChildren<TileBase>();
                t.Position = tile.Vector;
                t.name = $"{tile.TileName} - {t.Position.ToString()}";
                if (t is MovableTile) t.name = "box";
                t.GetComponentInChildren<SpriteRenderer>().sortingOrder = t.Position.Order;
                Map.Add(t);
                if (t is StartTile startTile)
                {
                    StartTile = startTile;
                }
            }
            foreach (var connection in map.Connections){
                var from = GetTilesAt(connection.PressurePlateVector).Where(x => x is PressurePlate).ToList().First() as PressurePlate;
                connection.ConnectedVectors.ForEach(x=> GetTilesAt(x).ForEach(y=> from.Subscribe(y as ActivationListener)));
            }

            var playerPos = StartTile.Position;
            Player = Instantiate(playerPrefab, playerPos.UnityVector, Quaternion.identity, transform).GetComponent<Player>();
            Player.SetStartingTile(GetTilesAt(playerPos + new Vector(0,0,-1))[0]);
            PlayerMoved(GetTilesAt(playerPos + new Vector(0,0,-1))[0]);
            
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
            //player.GetComponent<Character>().ValidMoveDestinations()
            selectedTiles = Player.ValidMoveOntoDestinations();

            foreach (var t in selectedTiles)
            {
                t.GetComponentInChildren<SpriteRenderer>().material = selectMaterial;
            }
        }
    }
}