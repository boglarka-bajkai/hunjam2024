namespace View.Sound
{
    using System.Collections;
    using Model.Tiles.Helpers;
    using UnityEngine;
    [RequireComponent(typeof(AudioSource))]
    public class CheckpointSoundEffects : MonoBehaviour
    {
        private AudioSource soundEffectSource;
        private void Awake()
        {
            if (soundEffectSource == null)
            {
                soundEffectSource = GetComponent<AudioSource>();
            }
            CheckpointHelper.OnCheckpointActivated += OnCheckpointActivated;
        }
        private void OnCheckpointActivated()
        {
            var clip = SoundManager.Instance.GetSoundEffect(SoundEffectName.Reverse);
            if (clip != null)
            {
                if (soundEffectSource.isPlaying) soundEffectSource.Stop();
                soundEffectSource.clip = clip;
                soundEffectSource.Play();
            }
            else
            {
                Debug.LogWarning($"Sound effect {SoundEffectName.Reverse} not found");
            }
        }
    }
}