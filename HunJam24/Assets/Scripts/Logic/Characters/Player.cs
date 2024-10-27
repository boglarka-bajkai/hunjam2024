namespace Logic.Characters
{
    public class Player : Character
    {
        private CloneManager _cloneManager;

        public bool ShouldBeDead => _cloneManager.GetClonesAt(Position).Count != 0;
    }
}