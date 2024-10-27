namespace Logic.Characters
{
    public class Player : Character
    {
        private CloneManager _cloneManager;

        public bool ShouldBeDead => CloneManager.Instance.GetClonesAt(Position).Count != 0;
    }
}