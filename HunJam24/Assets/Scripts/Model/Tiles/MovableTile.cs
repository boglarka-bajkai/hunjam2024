using System;
using System.Collections.Generic;
using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Level;
using Model.Other;
using Model.Tiles.Data;
using Model.Tiles.Interfaces;
using UnityEngine;
using View.Animated;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that can be moved by a character.
    /// </summary>
    /// <remarks>
    /// Moving means that the tile is pushed by a character, when the character moves into the tile.
    /// The tile can only be stepped in when there is room to move the tile.
    /// </remarks>
    [RequireComponent(typeof(MovableTileAnimation))]
    public class MovableTile : Tile, ITopTile, ILoopListener, IMoveNotifier
    {
        Coordinate _startCoordinate;

        public event Action<Coordinate, Coordinate> OnMove;
        override public Coordinate Position
        {
            get => _position;
            set
            {
                if (_position != null && _position == value) return;
                _position = value;
                //This should not get updated when the position changes as it is handled by the animation
            }
        }
        public override void Initialize(Coordinate position, TileData data)
        {
            base.Initialize(position, data);
            _startCoordinate = position;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            transform.position = Position.AsUnityVector;
            foreach (var item in GetComponentsInChildren<SpriteRenderer>(true))
            {
                item.sortingOrder = Position.RenderOrder + 1;
            }
        }
        /// <summary>
        /// At the start of the game the tile notifies all other tiles of its position.
        /// This is necessary because the tile can also activate other tiles, which may not be present during initialization (e.g. they get spawned later).
        /// </summary>
        /// <param name="state"></param>
        void OnGameStateChanged(GameState state) {
            if (state == GameState.InGame)
            {
                LevelManager.Instance.GetTilesAt(this.Position).ForEach(x => x.Enter(this));
                LevelManager.Instance.GetTilesAt(this.Position.Below).ForEach(x => x.StepOn(this));
                GameManager.OnGameStateChanged -= OnGameStateChanged;
            }
            
        }
        public override bool CanEnter(Character character)
        {
            Coordinate newPos = this.Position + (this.Position - character.Position);
            List<Tile> tilesInNewPos = LevelManager.Instance.GetTilesAt(newPos);
            if (tilesInNewPos.Count != 0 && !tilesInNewPos.TrueForAll(x => x.CanEnter(this)))
            {
                //If there are tiles in new position, and this tiles is not allowed to enter any
                return false;
            }
            List<Tile> tilesBelowNewPos = LevelManager.Instance.GetTilesAt(newPos.Below);
            if (tilesBelowNewPos.Count == 0 || !tilesBelowNewPos.TrueForAll(x => x.CanStepOn(this)))
            {
                //If there are no tiles below the new position, or any tiles below do not allow this tile to step on them
                return false;
            }
            return true;
        }

        public override bool CanEnter(Tile tile) => false;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;

        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                Coordinate newPos = this.Position + (this.Position - character.Position);
                MoveTo(newPos);
                return true;
            }
            return false;
        }

        private void MoveTo(Coordinate newPos)
        {
            //Leave old position
            LevelManager.Instance.GetTilesAt(this.Position).ForEach(x => x.ExitTo(this, newPos));
            LevelManager.Instance.GetTilesAt(this.Position.Below).ForEach(x => x.ExitTo(this, newPos));
            //Enter new position
            LevelManager.Instance.GetTilesAt(newPos).ForEach(x => x.Enter(this));
            LevelManager.Instance.GetTilesAt(newPos.Below).ForEach(x => x.StepOn(this));
            //Finally set the new position
            OnMove?.Invoke(this.Position, newPos);
            this.Position = newPos;
        }

        public void OnLoop()
        {
            if (this.Position != _startCoordinate)
            {
                MoveTo(_startCoordinate);
            }
        }
    }
}