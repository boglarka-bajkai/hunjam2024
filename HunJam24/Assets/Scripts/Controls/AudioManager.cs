using System;
using UnityEngine;

namespace Controls
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance => _instance;

        [Header("Audio Sources")]
        // Header
        [SerializeField]
        private AudioSource backgroundMusicSource;

        [SerializeField] private AudioSource reversedBackgroundMusicSource;

        private bool _playingReversed;

        [SerializeField] private AudioSource soundEffectsSource;


        [Header("Audio Clips")]
        // Header
        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField] private AudioClip reversedBackgroundMusic;

        [SerializeField] public AudioClip step;


        private void Awake()
        {
            if (_instance != null) Destroy(this);
            _instance = this;
            if (!backgroundMusicSource || !reversedBackgroundMusicSource)
            {
                throw new Exception("[AudioManager::Awake]: Music sources are not set properly");
            }

            if (!backgroundMusic || !reversedBackgroundMusic)
            {
                throw new Exception("[AudioManager::Awake]: Music clips are not set properly");
            }

            backgroundMusicSource.clip = backgroundMusic;
            reversedBackgroundMusicSource.clip = reversedBackgroundMusic;

            backgroundMusicSource.loop = true;
            reversedBackgroundMusicSource.loop = true;
        }

        private void Start()
        {
            backgroundMusicSource.Play();
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
            if (!_playingReversed)
            {
                _playingReversed = true;
                backgroundMusicSource.Stop();
                reversedBackgroundMusicSource.Play();
            }
            else
            {
                _playingReversed = false;
                reversedBackgroundMusicSource.Stop();
                backgroundMusicSource.Play();
            }
        }
    }
}