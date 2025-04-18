using Logic.Characters;
using Logic.Tiles;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using UnityEngine;

namespace Model.Tiles
{
    [TileDataType(typeof(ConnectedTileData))]
    public abstract class ActivatableTile : Tile, IActivationListener
    {
        [SerializeField]
        [Tooltip("The tile is active when this GameObject is active.")]
        private GameObject activeSelf;
        [SerializeField]
        [Tooltip("The tile is inactive when this GameObject is active.")]
        private GameObject inactiveSelf;
        protected bool _active = false;

        public bool IsActive => _active;

        public virtual void Activate() {
            if (_active) return; // Avoid double activation
            _active = true;
            activeSelf.SetActive(true);
            inactiveSelf.SetActive(false);
        }
        public virtual void Deactivate() {
            if (!_active) return; // Avoid double deactivation
            _active = false;
            activeSelf.SetActive(false);
            inactiveSelf.SetActive(true);
        }
    }
}