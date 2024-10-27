using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controls
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        // Header
        [SerializeField]
        private AudioSource musicSource;

        [SerializeField] private AudioSource soundEffectsSource;


        [Header("Audio Clips")]
        // Header
        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField] private AudioClip reversedBackgroundMusic;

        [SerializeField] public AudioClip step;


        private void Start()
        {
            if (!musicSource || !backgroundMusic) return;

            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }

        public void PlaySoundEffect(AudioClip soundEffect)
        {
            if (soundEffect == null)
            {
                Debug.Log("[AudioManager::PlaySoundEffect]: Given sound effect is null");
            }

            soundEffectsSource.PlayOneShot(soundEffect);
        }

        /*
         * Starts playing the reversed background music if the normal background music was playing before
         * Starts playing the normal background music if the reversed background music was playing before
         */
        public void PlayReversedMusic()
        {
            musicSource.clip = musicSource.clip == backgroundMusic ? reversedBackgroundMusic : backgroundMusic;
            musicSource.Play();
        }
    }
}