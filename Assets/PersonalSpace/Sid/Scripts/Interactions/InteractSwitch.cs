using System;
using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces;
using PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces.Platforms;
using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace PersonalSpace.Sid.Scripts.Interactions
{
    public class InteractSwitch : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool IsInteractable { get; set; } = true;
        [field: SerializeField] public string InteractText { get; set; }
        
        [SerializeField] private List<BasePuzzlePiece> puzzleInteractablesList = new List<BasePuzzlePiece>();

        [SerializeField] private List<Light> switchLights = new List<Light>();

        [SerializeField] private bool puzzleSolved;
        
        [SerializeField] private Color32 red = new Color32();
        [SerializeField] private Color32 green = new Color32();

        [SerializeField] private AudioClip click;

        private Animator animator;

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        private void Start()
        {
            foreach (var switchLight in switchLights)
            {
                switchLight.color = red;
            }
        }

        public void Interact()
        {
            puzzleSolved = !puzzleSolved;
            animator.SetTrigger("buttonPressed");
            SoundManager.Instance.PlaySoundEffects(click);
            
            foreach (var interactable in puzzleInteractablesList)
            {
                interactable.PerformAction();
            }

            if (puzzleSolved)
            {
                foreach (var switchLight in switchLights)
                {
                    switchLight.color = green;
                }
            }
            else
            {
                foreach (var switchLight in switchLights)
                {
                    switchLight.color = red;
                }
            }
        }
    }
}