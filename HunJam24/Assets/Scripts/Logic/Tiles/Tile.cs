using System;
using UnityEngine;

namespace Logic.Tiles
{
    [CreateAssetMenu(fileName = "Tile", menuName = "Tiles/BaseTile", order = 1)]
    public class Tile : ScriptableObject
    {
        [SerializeField] GameObject prefab;
        GameObject spawnedObject;
        public GameObject SpawnedObject => spawnedObject;
        public GameObject Prefab => prefab;
        public Position Position { get; private set; }

        public void Spawn(GameObject go, Position pos) {
            spawnedObject = go;
            Position = pos;
            go.GetComponent<SpriteRenderer>().sortingOrder = Position.Order;
        }
        public bool IsNextTo(Tile other)
        {
            return Position.DistanceFrom(other.Position) == 1;
        }

        public bool IsOnSameLevel(Tile other)
        {
            return Position.Z == other.Position.Z;
        }

        public bool IsOnNeighboringLevel(Tile other)
        {
            return Math.Abs(Position.Z - other.Position.Z) == 1;
        }

        public int DistanceFrom(Tile other)
        {
            return Position.DistanceFrom(other.Position);
        }

        public bool AcceptsPlayerFrom(Tile other)
        {
            throw new NotImplementedException();
        }
    }
}