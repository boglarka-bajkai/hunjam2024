using System;
using Model.Tiles.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.Tiles.Data
{
    [Serializable]
    public class TimerTileData : ConnectedTileData 
    {
        [SerializeField] 
        [Tooltip("Whether this tile is active by default when the game starts.")]
        [Serialize] private bool activeByDefault;
        public bool ActiveByDefault => activeByDefault;

        [SerializeField] 
        [Tooltip("The tick duration for this tile.")]
        [Serialize] int tickDuration;
        public int TickDuration => tickDuration;
    }

    
}