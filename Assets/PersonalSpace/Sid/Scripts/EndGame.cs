using System;
using System.Collections;
using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;
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
            yield return new WaitForSeconds(4f);
            foreach (var source in telephoneSources)
            {
                float random = Random.Range(0.1f, 0.5f);
                source.PlayDelayed(random);
            }
            
            UIManager.Instance.FadeInCheckpointScreen(12f);
        }
    }
}