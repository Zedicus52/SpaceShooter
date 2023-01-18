using System;
using UnityEngine;

namespace SpaceShooter.GFX
{
    [RequireComponent(typeof(AudioSource))]
    public class EffectAudioSource : MonoBehaviour
    {
        public static EffectAudioSource Instance { get; private set; }
        private AudioSource _source;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void Start() => _source = GetComponent<AudioSource>();

        public void PlayOneShot(AudioClip clip) => _source.PlayOneShot(clip);
    }
}