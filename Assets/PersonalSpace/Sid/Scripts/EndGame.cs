using System;
using System.Collections;
using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace PersonalSpace.Sid.Scripts
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> telephoneSources = new List<AudioSource>();
        
        private void Start()
        {
            StartCoroutine(PlayPhoneSounds());
        }

        IEnumerator PlayPhoneSounds()
        {
            yield return new WaitForSeconds(3f);
            foreach (var source in telephoneSources)
            {
                float random = Random.Range(0.1f, 0.5f);
                source.PlayDelayed(random);
            }
            
            yield return new WaitForSeconds(5f);
            
            UIManager.Instance.FadeInCheckpointScreen(2f);
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene(0);
        }
    }
}