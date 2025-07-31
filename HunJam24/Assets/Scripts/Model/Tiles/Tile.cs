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
        protected Coordinate _position;
        /// <summary>
        /// The position of the tile in isometric coordinates.
        /// </summary>
        virtual public Coordinate Position 
        {
            get => _position;
            set {
                if (_position != null && _position == value) return;
                _position = value;
                transform.position = Position.AsUnityVector;
                foreach (var item in GetComponentsInChildren<SpriteRenderer>(true))
                {
                    item.sortingOrder = Position.RenderOrder;
                }
            }
        }
        /// <summary>
        /// The render order of the tile, used for sorting objects in the scene.
        /// Can be overwritten by subclass tiles to change the render order.
        /// </summary>
        public virtual int RenderOrder => Position.RenderOrder;
        protected virtual void OnDestroy()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Initializes the tile with the given position.
        /// </summary>
        /// <param name="position">The position of the tile in isometric coordinates.</param>
        public virtual void Initialize(Coordinate position, TileData data) {
            Position = position;
        }
        #region Character Movement Callbacks

        /// <summary>
        /// Tells the tile that the player has exited it or stepped off it.
        /// </summary>
        /// <param name="character">The character that is exiting.</param>
        /// <remarks>
        /// Stepping off or leaving the tile is the same in this context, and is used to let
        /// reacting tiles (e.g. pressure plates) know that the player has left the tile.
        /// </remarks>
        public virtual void ExitTo(Character character, Coordinate destination) { }
        #endregion
        #region Tile Movement Callbacks
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