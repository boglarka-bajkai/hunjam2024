using System;
namespace Model.Tiles.Data
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false )]
    /// <summary>
    /// Attribute to specify the data type of a tile subclass.
    /// </summary>
    public class TileDataTypeAttribute : Attribute
    {
        public Type DataType { get; }

        public TileDataTypeAttribute(Type dataType)
        {
            DataType = dataType;
        }
    }
}
