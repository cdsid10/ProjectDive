using System;
using PersonalSpace.Sid.Scripts.Interactions;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Managers
{
    public class PuzzleCodeManager : MonoBehaviour
    {
        public static PuzzleCodeManager Instance;

        [SerializeField] private GameObject puzzleCode;
        
        [field: SerializeField] public bool HasCollectedPuzzleInstructions { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void ShowPuzzleInstructionsAtLevelEnd()
        {
            HasCollectedPuzzleInstructions = true;
            
            if (HasCollectedPuzzleInstructions)
            {
                puzzleCode.SetActive(true);
            }
        }
    }
}
