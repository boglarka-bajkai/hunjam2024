using System;
using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using Model.Tiles.Helpers;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    /// <summary>
    /// A tile that spawns a clone when entered.
    /// </summary>
    public sealed class CheckpointTile : Tile, ITopTile
    {
        bool activated = false;
        public override void Initialize(Coordinate position, TileData data)
        {
            base.Initialize(position, data);
            CheckpointHelper.Instance.CheckpointCreated();
        }
        public override bool CanEnter(Character character) => true;

        public override bool CanEnter(Tile tile) => true;

        public override bool CanStepOn(Character character) => false;

        public override bool CanStepOn(Tile tile) => false;
        public override bool Enter(Character character)
        {
            if (CanEnter(character))
            {
                if (activated) return true; //Do nothing if already activated
                activated = true;
                CheckpointHelper.Instance.CheckpointActivated();
                return true;
            }
            return false;
        }
    }
}