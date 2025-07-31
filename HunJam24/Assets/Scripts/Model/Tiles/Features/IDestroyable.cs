namespace Model.Tiles.Interfaces
{
    public interface IDestroyable
    {  
        /// <summary>
        /// Gets called when the tile is destroyed by other tiles, like spikes or disabled ground.
        /// </summary>
        void Destroy();
    }
}