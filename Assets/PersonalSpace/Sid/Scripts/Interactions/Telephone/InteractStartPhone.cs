using System;
using System.Collections;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.Telephone
{
    public class InteractStartPhone : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool IsInteractable { get; set; } = false;
        [field: SerializeField] public string InteractText { get; set; }

        [field: SerializeField] public bool HasAlreadyRinged { get; set; }
        
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private AudioClip clip;


        private void Start()
        {
            IsInteractable = false;
            StartCoroutine(PlayRingSound());
        }

        public void Interact()
        {
            StopCoroutine(PlayRingSound());
            UIManager.Instance.HideInteractText();
            sceneLoader.LoadScene();
        }

        IEnumerator PlayRingSound()
        {
            yield return new WaitForSeconds(15f);
            HasAlreadyRinged = true;
            IsInteractable = true;
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    SoundManager.Instance.PlaySoundEffects(clip);
                    yield return new WaitForSeconds(4f);
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}