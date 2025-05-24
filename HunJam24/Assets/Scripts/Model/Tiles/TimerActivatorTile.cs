using System.Collections.Generic;
using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;
using UnityEngine;
using View.Tiles;

namespace Model.Tiles
{
    /// <summary>
    /// Implements a Tile that is activated when a character or tile steps on it (e.g. Pressure plates).
    /// This tile notifies listeners when it is activated or deactivated.
    /// </summary>
    [TileDataType(typeof(TimerTileData))]
    [RequireComponent(typeof(TimerActivatorTileRenderer))]
    public sealed class TimerActivatorTile : Tile, ITopTile, IConnectedTile, ILoopListener
    {
        int tickDuration;
        int currentTick = 0;
        bool activateByDefault = false;

        /// <summary>
        /// The group this tile activates.
        /// </summary>
        TileConnectionGroup tileGroup;

        [SerializeField]
        [Tooltip("The gameobject that is active when the tile is activated.")]
        private GameObject activeSelf;

        [SerializeField]
        [Tooltip("The gameobject that is active when the tile is deactivated.")]
        private GameObject inactiveSelf;

        /// <summary>
        /// The active state of the tile.
        /// </summary>
        private bool _active = false;
        /// <summary>
        /// The active state of the tile.
        /// </summary>
        public bool Active => _active;

        public override int RenderOrder => Position.RenderOrder - 1;

        public TileConnectionGroup TileGroup => tileGroup;

        public override void Initialize(Coordinate position, TileData data)
        {
            if (data is not TimerTileData timerTileData)
                throw new System.ArgumentException($"Invalid tile data type: {data.GetType()}");
            base.Initialize(position, data);
            GameManager.OnGameStateChanged += OnGameStateChanged;
            tileGroup = timerTileData.TileGroup;
            tickDuration = timerTileData.TickDuration;
            activateByDefault = timerTileData.ActiveByDefault;
            currentTick = 0;
            GameManager.OnTick += OnTick;
        }

        void OnGameStateChanged(GameState state)
        {
            if (state == GameState.InGame)
            {
                if (activateByDefault) Activate();
                GameManager.OnGameStateChanged -= OnGameStateChanged;
            }
        }

        /// <summary>
        /// Activates the tile.
        /// </summary>
        void Activate()
        {
            if (_active) return; //Avoid double activation
            _active = true;
            activeSelf.SetActive(true);
            inactiveSelf.SetActive(false);
            TileConnectionHelper.Instance.ActivatorActivated(tileGroup);
        }
        /// <summary>
        /// Deactivates the tile.
        /// </summary>
        void Deactivate()
        {
            if (!_active) return; //Avoid double deactivation
            _active = false;
            inactiveSelf.SetActive(true);
            activeSelf.SetActive(false);
            TileConnectionHelper.Instance.ActivatorDeactivated(tileGroup);
        }

        public override bool CanEnter(Character character) => false;

        public override bool CanEnter(Tile tile) => false;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;

        public void OnLoop()
        {
            Deactivate();
            if (activateByDefault) Activate();
            currentTick = 0;
        }

        void OnTick(Coordinate _)
        {

            currentTick++;
            if (currentTick >= tickDuration)
            {
                if (_active) Deactivate();
                else Activate();
                currentTick = 0;
            }
        }
    }
}