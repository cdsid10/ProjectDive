using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions
{
    public class InteractPuzzleCode : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool IsInteractable { get; set; } = true;
        [field: SerializeField] public string InteractText { get; set; }
        [field: SerializeField] public string PuzzleTitleText { get; set; }
        [field: SerializeField] public string PuzzleCodeText { get; set; }
        [field: SerializeField] public string PuzzleCodeFooterText { get; set; }
        [field: SerializeField] public string PuzzleDescriptionText { get; set; }
        [field: SerializeField] public string PuzzleNoteText { get; set; }
        
        [SerializeField] private AudioClip click;
        
        public void Interact()
        {
            if(!IsInteractable) return;
            
            IsInteractable = false;
            SoundManager.Instance.PlaySoundEffects(click);
            UIManager.Instance.ShowCodePuzzleText(PuzzleTitleText, PuzzleCodeText, PuzzleCodeFooterText, 
                PuzzleDescriptionText, PuzzleNoteText);
            UIManager.Instance.HideInteractText();
            if (PuzzleCodeManager.Instance != null)
            {
                PuzzleCodeManager.Instance.ShowPuzzleInstructionsAtLevelEnd();
            }
            
            //The above code is encrypted, it can be decrypted by following the instructions below-  
            //- The 1st digit is the 4th digit after reversing the encrypted code once. 
            //- The 2nd digit is the 4th digit after reversing the encrypted code once again.  
            //- The 3rd and 4th digits are the 1st and 2nd digits after reversing the encrypted code once again.
        }
    }
}