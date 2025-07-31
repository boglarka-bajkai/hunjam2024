using Model.Tiles.Data;

namespace Model.Tiles.Interfaces
{
    /// <summary>
    /// Interface for tiles that can activate or deactivate other tiles.
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Called when the tile is activated.
        /// </summary>
        public void Activate();

        /// <summary>
        /// Called when the tile is deactivated.
        /// </summary>
        public void Deactivate();
    }
}