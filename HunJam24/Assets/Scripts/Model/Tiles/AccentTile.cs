using Model.Characters;
using Model.Data;
using Model.Tiles.Interfaces;

namespace Model.Tiles
{
    public abstract class AccentTile : Tile
    {
        /// <summary>
        /// Checks if the tile can be stepped in by the given character from a given direction.
        /// </summary>
        /// <param name="character">The character that is stepping in.</param>
        /// <returns>True if the tile can be stepped on, false otherwise.</returns>
        /// <remarks>
        /// Stepping in means that the tile and the character are at the same z coordinate,
        /// occupying the same space.
        /// This is used for tiles that are not solid, like pressure plates, but may also be used for boxes,
        /// which allow entry, but react by getting pushed in the opposite direction.
        /// </remarks>
        public abstract bool CanEnter(Character character);

        /// <summary>
        /// The character tries to step in the tile from a given direction.
        /// </summary>
        /// <param name="character">The character that is stepping in.</param>
        /// <returns>Whether the character was able to step in the tile.</returns>
        /// <remarks>
        /// This method should only be used to add side effects to the tile when the character steps in it.
        /// For checking if the character can step in the tile, use CanEnterFrom instead.
        /// </remarks>
        public virtual bool Enter(Character character) {
            return CanEnter(character);
        }

        /// <summary>
        /// Checks if the tile can be stepped in from a given direction by a given tile.
        /// </summary>
        /// <param name="tile">The tile that is stepping in.</param>
        /// <returns>True if the tile can be stepped in, false otherwise.</returns>
        /// <remarks>
        /// Stepping in means that the tile's z coordinate is the same.
        /// This is used for tiles that are not solid, like pressure plates, but may also be used for boxes,
        /// which allow entry, but react by getting pushed in the opposite direction.
        /// </remarks>
        public abstract bool CanEnter(Tile tile);
        /// <summary>
        /// Another tile tries to step in the tile from a given direction.
        /// </summary>
        /// <param name="tile">The tile that is stepping in.</param>
        /// <returns>Whether the tile was able to step in the tile.</returns>
        /// <remarks>
        /// This method should only be used to add side effects to the tile when the tile steps in it.
        /// For checking if the tile can step in the tile, use CanEnterFrom instead.
        /// </remarks>
        public virtual bool Enter(Tile tile) {
            return CanEnter(tile);
        }
    }
}