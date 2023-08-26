using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.PlotAndGameOverview
{
    public class InteractRecorder : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; }  = true;
        public string InteractText { get; set; }
        
        [SerializeField] private AudioClip click;
        
        public void Interact()
        {
            if (!IsInteractable) return;
            IsInteractable = false;
            SoundManager.Instance.PlaySoundEffects(click);
            UIManager.Instance.ShowRecorderScreen();
            UIManager.Instance.HideInteractText();
        }
    }
}