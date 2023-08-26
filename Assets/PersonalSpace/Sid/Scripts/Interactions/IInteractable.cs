using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions
{
    public interface IInteractable
    {
        public bool IsInteractable { get; set; }
        public string InteractText { get; set; }
        public void Interact();
    }
}