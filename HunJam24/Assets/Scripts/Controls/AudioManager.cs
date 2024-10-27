using System;
using System.Collections.Generic;
using Serializer;
using Sounds;
using UnityEngine;

namespace Controls
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        // Header
        [SerializeField]
        private AudioSource backgroundMusicSource;

        [SerializeField] private AudioSource reversedBackgroundMusicSource;
        [SerializeField]
        private AudioSource menuMusicSource;

        private bool _playingReversed;

        [SerializeField] private List<MyPair> normalClips;

        private readonly Dictionary<string, AudioSource> _clipSources = new();


        [Header("Audio Clips")]
        // Header
        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField] private AudioClip reversedBackgroundMusic;
        [SerializeField]
        private AudioClip menuMusic;


        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
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

            foreach (var clipNamePair in normalClips)
            {
                _clipSources[clipNamePair.Name] = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Start()
        {
            StartPlayingMusic(backgroundMusicSource, 2f);
        }

        public void PlaySoundEffect(string clipName)
        {
            var clip = GetClip(clipName);
            var source = GetClipSource(clipName);

            if (source.isPlaying)
            {
                source.Stop();
            }

            source.PlayOneShot(clip);
        }

        /*
         * Starts playing the reversed background music if the normal background music was playing before
         * Starts playing the normal background music if the reversed background music was playing before
         */
        public void PlayReversedMusic(float delay)
        {
            if (!_playingReversed)
            {
                _playingReversed = true;
                StopPlayingMusic(backgroundMusicSource, 0.1f);
                StartPlayingMusic(reversedBackgroundMusicSource, 2f, delay);
            }
            else
            {
                _playingReversed = false;
                StopPlayingMusic(reversedBackgroundMusicSource, 0.1f);
                StartPlayingMusic(backgroundMusicSource, 2f, delay);
            }
        }

        private void StartPlayingMusic(AudioSource source, float fadeDuration, float delay = 0f)
        {
            StartCoroutine(SoundHelper.Fade(source, fadeDuration, delay, 1f));
        }

        private void StopPlayingMusic(AudioSource source, float fadeDuration, float delay = 0f)
        {
            StartCoroutine(SoundHelper.Fade(source, fadeDuration, delay, 0f));
        }

        public void PlayMenuMusic()
        {
            if (!_playingReversed)
            {
                _playingReversed = true;
                StopPlayingMusic(backgroundMusicSource, 0.1f);
            }
            else
            {
                _playingReversed = false;
                StopPlayingMusic(reversedBackgroundMusicSource, 0.1f);
            }
            
            StartPlayingMusic(menuMusicSource, 2f);
        }

        public void PlayGameMusic()
        {
            StopPlayingMusic(menuMusicSource, 0.1f);
            StartPlayingMusic(backgroundMusicSource, 2f);
        }

        private AudioClip GetClip(string clipName)
        {
            return normalClips.Find(p => p.Name.Equals(clipName)).Clip;
        }

        private AudioSource GetClipSource(string clipName)
        {
            return _clipSources[clipName];
        }
    }
}