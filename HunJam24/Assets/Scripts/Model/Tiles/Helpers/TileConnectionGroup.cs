namespace Model.Tiles.Helpers
{
    /// <summary>
    /// A color that identifies all the tiles that are connected to each other.
    /// They are used to determine which activator tiles activate which activatable tiles.
    /// The colors can be used to group tiles together visually as well.
    /// </summary>
    public enum TileConnectionGroup 
    {
        RED,
        PURPLE,
        BLUE,
        ORANGE,
        YELLOW,
        GREEN
    }
}