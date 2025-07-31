using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that you can only step on when it is not0 activated.
    /// </summary>
    public sealed class DisappearingGround : GroundTile, IActivatable, ILethal
    {
        public override bool CanStepOn(Character character) => IsActive;
        public override bool CanStepOn(Tile tile) => IsActive;
        [SerializeField]
        [Tooltip("The tile is active when this GameObject is active.")]
        private GameObject activeSelf;
        [SerializeField]
        [Tooltip("The tile is inactive when this GameObject is active.")]
        private GameObject inactiveSelf;
        bool _active = false;

        public bool IsActive => _active;

        public TileConnectionGroup TileGroup => tileGroup;

        TileConnectionGroup tileGroup;

        public override void Initialize(Coordinate position, TileData data)
        {
            if (data is not ConnectedTileData connectedTileData)
                throw new System.ArgumentException($"Invalid tile data type: {data.GetType()}");
            base.Initialize(position, data);
            tileGroup = connectedTileData.TileGroup;
            TileConnectionHelper.Instance.OnActivatorActivated += Activate;
            TileConnectionHelper.Instance.OnActivatorDeactivated += Deactivate;
            activeSelf.SetActive(false);
            inactiveSelf.SetActive(true);
        }

        public void Activate(TileConnectionGroup connectionGroup)
        {
            if (connectionGroup != tileGroup) return; // Only activate if the group matches
            if (_active) return; // Avoid double activation
            _active = true;
            activeSelf.SetActive(true);
            inactiveSelf.SetActive(false);
        }
        public void Deactivate(TileConnectionGroup connectionGroup)
        {
            if (connectionGroup != tileGroup) return; // Only deactivate if the group matches
            if (!_active) return; // Avoid double deactivation
            _active = false;
            activeSelf.SetActive(false);
            inactiveSelf.SetActive(true);
        }

        override protected void OnDestroy()
        {
            TileConnectionHelper.Instance.OnActivatorActivated -= Activate;
            TileConnectionHelper.Instance.OnActivatorDeactivated -= Deactivate;
        }

        public bool ShouldKill()
        {
            return !IsActive;
        }
    }
}