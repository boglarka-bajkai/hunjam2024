using System;
using Model.Data;

namespace Model.Other
{
    public interface IMoveNotifier
    {
        public event Action<Coordinate, Coordinate, bool> OnMove;
    }
}