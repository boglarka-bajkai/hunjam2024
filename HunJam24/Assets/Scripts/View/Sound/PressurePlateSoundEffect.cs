namespace View.Sound
{
    using Model.Tiles.Data;
    using Model.Tiles.Helpers;
    using UnityEngine;
    [RequireComponent(typeof(AudioSource))]
    public class PressurePlateSoundEffects : MonoBehaviour
    {
        private AudioSource soundEffectSource;
        private void Awake()
        {
            if (soundEffectSource == null)
            {
                soundEffectSource = GetComponent<AudioSource>();
            }
            TileConnectionHelper.Instance.OnActivatorActivated += OnCheckpointActivated;
        }
        private void OnCheckpointActivated(TileConnectionGroup _)
        {
            var clip = SoundManager.Instance.GetSoundEffect(SoundEffectName.PressurePlate);
            if (clip != null)
            {
                if (soundEffectSource.isPlaying) soundEffectSource.Stop();
                soundEffectSource.clip = clip;
                soundEffectSource.Play();
            }
            else
            {
                Debug.LogWarning($"Sound effect {SoundEffectName.PressurePlate} not found");
            }
        }
    }
}