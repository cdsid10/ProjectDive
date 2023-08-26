using System;
using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces;
using PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces.Platforms;
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