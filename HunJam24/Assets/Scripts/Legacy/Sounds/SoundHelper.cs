using System.Collections;
using UnityEngine;

namespace Sounds
{
    public class SoundHelper
    {
        public static IEnumerator Fade(AudioSource source, float duration, float delay, float targetVolume)
        {
            if (delay != 0f)
            {
                yield return new WaitForSecondsRealtime(delay);
            }
            
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