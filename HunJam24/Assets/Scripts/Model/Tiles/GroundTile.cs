using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    public class GroundTile : Tile
    {
        /// <summary>
        /// Checks if the tile can be stepped on by the character from a given direction.
        /// </summary>
        /// <param name="character">The character that is stepping on.</param>
        /// <returns>True if the tile can be stepped on, false otherwise.</returns>
        /// <remarks>
        /// Stepping on means that the character's z coordinate is +1.
        /// This is used for tiles that are solid, like the ground.
        /// </remarks>
        public virtual bool CanStepOn(Character character)
        {
            return true;
        }

        /// <summary>
        /// The character tries to step on the tile from a given direction.
        /// </summary>
        /// <param name="character">The character that is stepping on.</param>
        /// <returns>Whether the character was able to step on the tile.</returns>
        /// <remarks>
        /// This method should only be used to add side effects to the tile when the character steps on it.
        /// For checking if the character can step on the tile, use CanStepOnFrom instead.
        /// </remarks>
        public virtual bool StepOn(Character character) {
            return CanStepOn(character);
        }


        /// <summary>
        /// Checks if the tile can be stepped on from a given direction by a given tile.
        /// </summary>
        /// <param name="tile">The tile that is stepping on.</param>
        /// <returns>True if the tile can be stepped on, false otherwise.</returns>
        /// <remarks>
        /// Stepping on means that the tile's z coordinate is +1.
        /// This is used for tiles that are solid, like the ground.
        /// </remarks>
        public virtual bool CanStepOn(Tile tile)
        {
            return true;
        }


        /// <summary>
        /// Another tile tries to step on the tile from a given direction.
        /// </summary>
        /// <param name="tile">The tile that is stepping on.</param>
        /// <returns>Whether the tile was able to step on the tile.</returns>
        /// <remarks>
        /// This method should only be used to add side effects to the tile when the tile steps on it.
        /// For checking if the tile can step on the tile, use CanStepOnFrom instead.
        /// </remarks>
        public virtual bool StepOn(Tile tile) {
            return CanStepOn(tile);
        }
    }
}