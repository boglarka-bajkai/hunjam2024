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
    /// A tile that reacts to time loops.
    /// It allows PlayerCharacter to step on it in normal time,
    /// and CloneCharacter to step on it in looped time.
    /// Other tiles like boxes can step on it regardless of the time state.
    /// </summary>
    public sealed class SensitiveGroundTile : Tile, IGroundTile, ILoopListener
    {
        bool IsDestroyed = false;
        [SerializeField] SpriteRenderer spriteRenderer;
        public override bool CanEnter(Character character) => false;
        public override bool CanEnter(Tile tile) => true;
        public override bool CanStepOn(Character character) => !IsDestroyed;
        public override bool CanStepOn(Tile tile) => !IsDestroyed;
        public override bool StepOn(Character character)
        {
            bool re = base.StepOn(character);
            IsDestroyed = true;
            Debug.Log($"SensitiveGroundTile at {Position} destroyed by {character}");
            return re;
        }

        public void OnLoop()
        {
            IsDestroyed = false;
            spriteRenderer.enabled = true;
        }

        public override void ExitTo(Character character, Coordinate destination)
        {
            base.ExitTo(character, destination);
            if (IsDestroyed)
            {
                Debug.Log($"SensitiveGroundTile at {Position} exited by {character} but is destroyed, disabling sprite.");
                spriteRenderer.enabled = false;
            }
        }


    }
}