using System;
using System.Collections;
using Model.Data;

namespace View.Animated
{
    public class PlayerAnimation : CharacterAnimation
    {
        public static event Action OnAnimationFinished;
        protected override IEnumerator moveSoftlyTo(Coordinate from, Coordinate to, bool pushing)
        {
            yield return base.moveSoftlyTo(from, to, pushing);
            OnAnimationFinished?.Invoke();
        }
    }
}