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
    }
}