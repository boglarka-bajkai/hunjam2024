using Model.Tiles.Data;

namespace Model.Tiles.Helpers
{
    public interface IConnectedTile
    {
        TileConnectionGroup TileGroup { get; }
    }
}