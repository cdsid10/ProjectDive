using PersonalSpace.Sid.Scripts.Interactions;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.KeypadSystems
{
    public class InteractKeypad : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        [field: SerializeField] public string InteractText { get; set; }
        public void Interact()
        {
            
        }
    }
}