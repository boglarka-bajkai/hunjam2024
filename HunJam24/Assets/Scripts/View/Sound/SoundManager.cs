using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model;
using Model.Data;
using Model.Tiles.Helpers;
using Unity;
using UnityEngine;
namespace View.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] List<SoundEffect> soundEffects;

        [SerializeField] AudioSource soundEffectSource;
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource reversedMusicSource; // Need another one for crossfade
        #region Singleton Management
        private static SoundManager _instance;
        private static bool _playingReversed = false;

        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<SoundManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("SoundManager");
                        _instance = go.AddComponent<SoundManager>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            GameManager.OnGameStateChanged += PlayNormalMusic;
            CheckpointHelper.OnCheckpointActivated += PlayReversedMusic;
        }

        private void PlayReversedMusic()
        {
            if (_playingReversed) return;
            _playingReversed = true;
            Debug.Log("Playing reversed music");
            StopPlayingMusic(musicSource, 0.1f);
            StartPlayingMusic(reversedMusicSource, 2f, 2f);
            
        }

        private void StartPlayingMusic(AudioSource source, float fadeDuration, float delay = 0f)
        {
            StartCoroutine(Fade(source, fadeDuration, delay, 1f));
        }

        private void StopPlayingMusic(AudioSource source, float fadeDuration, float delay = 0f)
        {
            StartCoroutine(Fade(source, fadeDuration, delay, 0f));
        }

        private void PlayNormalMusic(GameState state)
        {
            if (!_playingReversed) return;
            _playingReversed = false;
            Debug.Log("Playing normal music");
            StopPlayingMusic(reversedMusicSource, 0.1f);
            StartPlayingMusic(musicSource, 2f, 2f);
        }
        #endregion


        public static IEnumerator Fade(AudioSource source, float duration, float delay, float targetVolume)
        {
            
            yield return new WaitForSecondsRealtime(delay);
            
            if (targetVolume > 0f)
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

            if (targetVolume <= 0f)
            {
                source.Stop();
            }
        }
        public AudioClip GetSoundEffect(SoundEffectName effect)
        {
            foreach (var soundEffect in soundEffects)
            {
                if (soundEffect.Name == effect)
                {
                    return soundEffect.Clip;
                }
            }
            Debug.LogWarning($"Sound effect {effect} not found");
            return null;
        }
    }
}