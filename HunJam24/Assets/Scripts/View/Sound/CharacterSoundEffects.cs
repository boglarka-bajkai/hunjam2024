namespace View.Sound
{
    using System.Collections;
    using UnityEngine;
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSoundEffects : MonoBehaviour
    {
        private AudioSource soundEffectSource;
        private void Awake()
        {
            if (soundEffectSource == null)
            {
                soundEffectSource = GetComponent<AudioSource>();
            }
        }
        public void PlaySoundEffect(SoundEffectName effect)
        {
            var clip = SoundManager.Instance.GetSoundEffect(effect);
            if (clip != null)
            {
                if (soundEffectSource.isPlaying) soundEffectSource.Stop();
                soundEffectSource.clip = clip;
                soundEffectSource.Play();
            }
            else
            {
                Debug.LogWarning($"Sound effect {effect} not found");
            }
        }
    }
}