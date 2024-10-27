using System.Collections;
using UnityEngine;

namespace Sounds
{
    public class SoundHelper
    {
        public static IEnumerator Fade(AudioSource source, float duration, float targetVolume)
        {
            if (targetVolume != 0f)
            {
                source.Play();
            }

            var time = 0f;
            var startVol = source.volume;
            while (time < duration)
            {
                time += Time.deltaTime;
                source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
                yield return null;
            }

            if (targetVolume == 0f)
            {
                source.Stop();
            }
        }
    }
}