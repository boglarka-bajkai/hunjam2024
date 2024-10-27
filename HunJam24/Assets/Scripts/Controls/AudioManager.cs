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

        private bool _playingReversed;


        [Header("Audio Clip maps")] [SerializeField]
        private AudioClipDictionary normalClips;

        [SerializeField] private AudioClipDictionary reverseClips;

        private readonly Dictionary<string, AudioSource> _normalClipSources = new();
        private readonly Dictionary<string, AudioSource> _reversedClipSources = new();


        [Header("Audio Clips")]
        // Header
        [SerializeField]
        private AudioClip backgroundMusic;

        [SerializeField] private AudioClip reversedBackgroundMusic;


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

            foreach (var (clipName, _) in normalClips)
            {
                _normalClipSources[clipName] = gameObject.AddComponent<AudioSource>();
            }

            foreach (var (clipName, _) in reverseClips)
            {
                _reversedClipSources[clipName] = gameObject.AddComponent<AudioSource>();
            }
        }

        private void Start()
        {
            StartPlayingMusic(backgroundMusicSource);
        }

        public void PlaySoundEffect(string clipName, bool hasReversed = true)
        {
            // var clip = GetClip(clipName, hasReversed);
            // var source = GetClipSource(clipName, hasReversed);

            // if (source.isPlaying)
            // {
            //     source.Stop();
            // }

            // source.PlayOneShot(clip);
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
            StartCoroutine(SoundHelper.Fade(source, 5f, 1f));
        }

        private void StopPlayingMusic(AudioSource source)
        {
            StartCoroutine(SoundHelper.Fade(source, 0f, 0f));
        }

        private AudioClip GetClip(string clipName, bool hasReversed)
        {
            return _playingReversed && hasReversed ? reverseClips[clipName] : normalClips[clipName];
        }

        private AudioSource GetClipSource(string clipName, bool hasReversed)
        {
            return _playingReversed && hasReversed ? _reversedClipSources[clipName] : _normalClipSources[clipName];
        }
    }
}