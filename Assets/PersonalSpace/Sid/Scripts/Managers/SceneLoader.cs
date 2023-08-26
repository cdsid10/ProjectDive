using System;
using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PersonalSpace.Sid.Scripts.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private AudioClip portalAudio;
        [SerializeField] private bool shouldPlayPortalAudio;
        
        private void Start()
        {
            UIManager.Instance.FadeOutCheckpointScreen(1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out FirstPersonController firstPersonController)) return;
            StartCoroutine(LoadNextScene());
        }

        public void PlayGame()
        {
            StartCoroutine(LoadNextScene());
        }

        public void LoadScene()
        {
            StartCoroutine(LoadNextScene());
        }

        IEnumerator LoadNextScene()
        {
            if (shouldPlayPortalAudio && portalAudio != null)
            {
                SoundManager.Instance.PlaySoundEffects(portalAudio);
            }
            UIManager.Instance.FadeInCheckpointScreen(0.5f);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            UIManager.Instance.HideRecorderScreen();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayTenseMusic();
            }
        }
    }
}