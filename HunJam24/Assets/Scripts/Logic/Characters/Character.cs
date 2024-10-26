using Logic.Tiles;
using UnityEngine;

namespace Logic.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        public Tile Tile { get; protected set; }
    }
}