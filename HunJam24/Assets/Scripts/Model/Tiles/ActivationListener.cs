namespace Model.Tiles
{
    /// <summary>
    /// Interface for tiles that can be activated or deactivated by activatable tiles.
    /// </summary>
    public interface IActivationListener
    {
        /// <summary>
        /// Called when the tile is activated.
        /// </summary>
        void Activate();
        
        /// <summary>
        /// Called when the tile is deactivated.
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Checks if the tile is active.
        /// </summary>
        /// <returns>True if the tile is active, false otherwise.</returns>
        bool IsActive { get; }
    }
}