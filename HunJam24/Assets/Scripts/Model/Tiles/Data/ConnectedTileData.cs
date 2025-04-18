using System;
using Model.Tiles.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace Model.Tiles.Data
{
    [Serializable]
    public class ConnectedTileData : TileData 
    {
        [SerializeField] 
        [Tooltip("The tile group this tile belongs to.")]
        [Serialize] private TileConnectionGroup tileGroup;
        public TileConnectionGroup TileGroup => tileGroup;
    }

    
}