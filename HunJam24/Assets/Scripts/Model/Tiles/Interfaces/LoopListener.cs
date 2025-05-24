using Model.Tiles.Helpers;

namespace Model.Tiles.Interfaces
{
    /// <summary>
    /// A tile that interacts with the time-loop mechanic.
    /// </summary>
    public interface ILoopListener
    {
        void OnLoop();
    }
}