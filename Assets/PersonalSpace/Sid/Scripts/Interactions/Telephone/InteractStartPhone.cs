using System;
using System.Collections;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.Telephone
{
    public class InteractStartPhone : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool IsInteractable { get; set; } = true;
        [field: SerializeField] public string InteractText { get; set; }
        
        [SerializeField] private AudioClip clip;


        private void Start()
        {
            StartCoroutine(PlayRingSound());
        }

        public void Interact()
        {
            Debug.Log("Interacted with phone start level 1");
            StopCoroutine(PlayRingSound());
        }

        IEnumerator PlayRingSound()
        {
            yield return new WaitForSeconds(2f);
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    SoundManager.Instance.PlaySound(clip);
                    yield return new WaitForSeconds(4f);
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}