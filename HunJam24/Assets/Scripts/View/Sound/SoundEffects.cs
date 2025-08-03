using System;
using UnityEngine;

namespace View.Sound
{
    public enum SoundEffectName
    {
        Move,
        Push,
        Reverse,
        PressurePlate
    }
    [Serializable]
    public class SoundEffect
    {
        public SoundEffectName Name;
        public AudioClip Clip;
    }
}
