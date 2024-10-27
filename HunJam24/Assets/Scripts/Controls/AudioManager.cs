using System;
using System.Collections;
using Serializer;
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

        [Header("Audio Clip maps")]
        [SerializeField] AudioClipDictionary normalClips;
        [SerializeField] AudioClipDictionary reverseClips;


        [Header("Audio Clips")]
        // Header
        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField] private AudioClip reversedBackgroundMusic;
        
        public AudioClip GetClip(string name) {
            if (_playingReversed) return reverseClips[name];
            else return normalClips[name];
        }
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

            backgroundMusicSource.volume = 0f;
            reversedBackgroundMusicSource.volume = 0f;

            backgroundMusicSource.loop = true;
            reversedBackgroundMusicSource.loop = true;
        }

        private void Start()
        {
            StartPlayingMusic(backgroundMusicSource);
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
                StopPlayingMusic(backgroundMusicSource);
                StartPlayingMusic(reversedBackgroundMusicSource);
            }
            else
            {
                _playingReversed = false;
                StopPlayingMusic(reversedBackgroundMusicSource);
                StartPlayingMusic(backgroundMusicSource);
            }
        }

        private void StartPlayingMusic(AudioSource source)
        {
            StartCoroutine(Fade(source, 1f, 1f));
        }
        
        private void StopPlayingMusic(AudioSource source)
        {
            StartCoroutine(Fade(source, 1f, 0f));
        }

        private static IEnumerator Fade(AudioSource source, float duration, float targetVolume)
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