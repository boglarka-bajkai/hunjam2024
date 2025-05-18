using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Tiles.Data;
using UnityEngine;

namespace View.Tiles.Helpers
{
    public static class ConnectedTileColorMappings
    {

        static Dictionary<TileConnectionGroup, Color> _colorMappings = new Dictionary<TileConnectionGroup, Color>()
        {
            {TileConnectionGroup.CYAN, new Color(1f, 1f, 1f)},
            {TileConnectionGroup.BLUE, new Color(0.5885791f, 0.5330188f, 1f)},
            {TileConnectionGroup.RED, new Color(1f, 0.2117647f, 0.24863282f)},
            {TileConnectionGroup.GREEN, new Color(0.3086336f, 1f, 0f)},
            {TileConnectionGroup.PURPLE, new Color(1f, 0.2122642f, 0.7217466f)},
            {TileConnectionGroup.ORANGE, new Color(1f, 0.447f, 0.023f)},
        };


        public static readonly ReadOnlyDictionary<TileConnectionGroup, Color> ColorMappings =
            new ReadOnlyDictionary<TileConnectionGroup, Color>(_colorMappings);
    }
}