using Model.Tiles.Data;

namespace Model.Tiles.Interfaces
{
    /// <summary>
    /// Interface for tiles that can kill characters contextually.
    /// </summary>
    public interface ILethal
    {
        /// <summary>
        /// Returns true if the tile should kill characters that step on/in it.
        /// This can be contextually based on the tile's state or other conditions.
        /// </summary>
        /// <returns></returns>
        bool ShouldKill();
    }
}