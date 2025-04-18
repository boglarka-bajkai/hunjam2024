using Logic.Characters;
using Model.Characters;
using Model.Data;
using Model.Tiles.Data;
using UnityEngine;
namespace Model.Tiles
{
    /// <summary>
    /// Represents a tile in the game.
    /// </summary>
    /// <remarks>
    /// This base class is used to represent a tile in the game.
    /// It has an Isometric coordinate as its position.
    /// It can either be stepped on (with the player z coordinate being +1), or stepped into (with the player z coordinate being the same), it has seperate contextual logic for allowing or denying either.    
    /// The default tile is a tile that can be stepped on, but not stepped into (so a ground tile).
    /// </remarks>
    public abstract class Tile : MonoBehaviour {
        /// <summary>
        /// The position of the tile in isometric coordinates.
        /// </summary>
        Coordinate _position;
        /// <summary>
        /// The position of the tile in isometric coordinates.
        /// </summary>
        public Coordinate Position 
        {
            get => _position;
            set {
                if (_position == value) return;
                _position = value;
                transform.position = Position.AsUnityVector;
                GetComponentInChildren<SpriteRenderer>().sortingOrder = Position.RenderOrder;
            }
        }
        /// <summary>
        /// The render order of the tile, used for sorting objects in the scene.
        /// Can be overwritten by subclass tiles to change the render order.
        /// </summary>
        public virtual int RenderOrder => Position.RenderOrder;

        /// <summary>
        /// Initializes the tile with the given position.
        /// </summary>
        /// <param name="position">The position of the tile in isometric coordinates.</param>
        public virtual void Initialize(Coordinate position, TileData data) {
            Position = position;
        }
        #region Character Movement Callbacks
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
        /// Checks if the tile can be stepped on by the character from a given direction.
        /// </summary>
        /// <param name="character">The character that is stepping on.</param>
        /// <returns>True if the tile can be stepped on, false otherwise.</returns>
        /// <remarks>
        /// Stepping on means that the character's z coordinate is +1.
        /// This is used for tiles that are solid, like the ground.
        /// </remarks>
        public abstract bool CanStepOn(Character character);

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
        /// Tells the tile that the player has exited it or stepped off it.
        /// </summary>
        /// <param name="character">The character that is exiting.</param>
        /// <remarks>
        /// Stepping off or leaving the tile is the same in this context, and is used to let
        /// reacting tiles (e.g. pressure plates) know that the player has left the tile.
        /// </remarks>
        public virtual void ExitTo(Character character, Coordinate destination) {}
        #endregion
        #region Tile Movement Callbacks
        /// <summary>
        /// Checks if the tile can be stepped on from a given direction by a given tile.
        /// </summary>
        /// <param name="tile">The tile that is stepping on.</param>
        /// <returns>True if the tile can be stepped on, false otherwise.</returns>
        /// <remarks>
        /// Stepping on means that the tile's z coordinate is +1.
        /// This is used for tiles that are solid, like the ground.
        /// </remarks>
        public abstract bool CanStepOn(Tile tile);

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

        /// <summary>
        /// Tells the tile that another tile has exited it or stepped off it.
        /// </summary>
        /// <param name="tile">The tile that is exiting.</param>
        /// <remarks>
        /// Stepping off or leaving the tile is the same in this context, and is used to let
        /// reacting tiles (e.g. pressure plates) know that the tile has left the tile.
        public virtual void ExitTo(Tile tile, Coordinate destination) {}
        #endregion
    }
}