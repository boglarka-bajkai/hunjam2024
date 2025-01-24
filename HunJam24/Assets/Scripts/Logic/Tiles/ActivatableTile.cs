using TMPro;

namespace Logic.Tiles{
    public class ActivatableTile : DeactivatableTile {
        void Start(){
            _active = false;
        }

        public override void Activate()
        {
            _active = true;
            active.SetActive(true);
            inactive.SetActive(false);

        }

        public override void Deactivate()
        {
            _active = false;
            inactive.SetActive(true);
            active.SetActive(false);
        }
    }
}