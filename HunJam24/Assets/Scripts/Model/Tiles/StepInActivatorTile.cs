using System.Collections.Generic;
using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using UnityEngine;

namespace Model.Tiles
{
    /// <summary>
    /// Implements a Tile that is activated when a character or tile steps on it (e.g. Pressure plates).
    /// This tile notifies listeners when it is activated or deactivated.
    /// </summary>
    [TileDataType(typeof(ConnectedTileData))]
    class StepInActivatorTile : Tile
    {
        /// <summary>
        /// List of characters on this tile.
        /// </summary>
        List<Character> charactersOnTile = new List<Character>();
        /// <summary>
        /// List of tiles on this tile.
        /// </summary>
        List<Tile> tilesOnTile = new List<Tile>();

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
        
        /// <summary>
        /// Activates the tile.
        /// </summary>
        void Activate() {
            if (_active) return; //Avoid double activation
            _active = true;
            activeSelf.SetActive(true);
            inactiveSelf.SetActive(false);
        }
        /// <summary>
        /// Deactivates the tile.
        /// </summary>
        void Deactivate() {
            if (!_active) return; //Avoid double deactivation
            _active = false;
            inactiveSelf.SetActive(true);
            activeSelf.SetActive(false);
        }

        public override bool CanEnter(Character character) => true;

        public override bool CanEnter(Tile tile) => true;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;

        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                charactersOnTile.Add(character);
                Activate();
                return true;
            }
            return false;
        }
        public override bool Enter(Tile tile)
        {
            if (CanEnter(tile))
            {
                tilesOnTile.Add(tile);
                Activate();
                return true;
            }
            return false;
        }

        public override void ExitTo(Character character, Coordinate destination)
        {
            charactersOnTile.Remove(character);
            if (charactersOnTile.Count + tilesOnTile.Count == 0)
            {
                Deactivate();
            }
        }

        public override void ExitTo(Tile tile, Coordinate destination)
        {
            tilesOnTile.Remove(tile);
            if (charactersOnTile.Count + tilesOnTile.Count == 0)
            {
                Deactivate();
            }
        }

        public override bool StepOn(Tile tile)
        {
            if (CanStepOn(tile))
            {
                tilesOnTile.Add(tile);
                Activate();
                return true;
            }
            return false;
        }

        public override bool StepOn(Character character)
        {
            if (CanStepOn(character))
            {
                charactersOnTile.Add(character);
                Activate();
                return true;
            }
            return false;
        }
    }
}