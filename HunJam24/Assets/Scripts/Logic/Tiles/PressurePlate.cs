using System.Collections.Generic;

namespace Logic.Tiles{
    public class PressurePlate : TileBase{
        List<ActivationListener> listeners = new();
        public void Subscribe(ActivationListener listener) => listeners.Add(listener);
        public override bool CanMoveInFrom(Vector position)
        {
            return true;
        }
        public override bool CanMoveOn(TileBase tile)
        {
            return false;
        }
        public override bool CanMoveOnFrom(Vector position)
        {
            return false;
        }

        public override void EnterFrom(Vector position)
        {
            listeners.ForEach(x=>x.Activate());
        }

        public override void ExitTo(Vector position)
        {
            listeners.ForEach(x=>x.Deactivate());
        }
    }
}