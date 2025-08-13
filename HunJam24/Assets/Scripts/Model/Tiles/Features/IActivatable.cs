using Model.Tiles.Data;

namespace Model.Tiles.Interfaces
{
    /// <summary>
    /// Interface for tiles that can be activated or deactivated by activatable tiles.
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Called when the tile is activated.
        /// </summary>
        void Activate(TileConnectionGroup connectionGroup);

        /// <summary>
        /// Called when the tile is deactivated.
        /// </summary>
        void Deactivate(TileConnectionGroup connectionGroup);

        /// <summary>
        /// Checks if the tile is active.
        /// </summary>
        /// <returns>True if the tile is active, false otherwise.</returns>
        bool IsActive { get; }
        /// <summary>
        /// The group this tile gets activated by.
        /// </summary>
        TileConnectionGroup TileGroup { get; }
    }
}