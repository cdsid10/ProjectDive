using System;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [field: SerializeField] public AudioSource footStepSource { get; set; }
        [SerializeField] private AudioSource jumpSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySoundEffects(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void PlayTenseMusic()
        {
            musicSource.Play();
        }

        public void PlayFootstepSound()
        {
            if (!footStepSource.isPlaying)
            {
                footStepSource.Play();
            }
            else
            {
                footStepSource.UnPause();
            }
        }

        public void PauseFootstepSound()
        {
            footStepSource.Pause();
        }

        public void PlayJumpSound()
        {
            jumpSource.Play();
        }

        public void PlayCheckpointSound(AudioClip checkpoint)
        {
            sfxSource.PlayOneShot(checkpoint);
        }
    }
}