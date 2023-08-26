using PersonalSpace.Sid.Scripts.Managers;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.Player
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [field: SerializeField] public bool CanInteract { get; private set; }

        private IInteractable _interactable;

        private void Update()
        {
            if (!CanInteract) return;

            if (Input.GetKeyDown(KeyCode.E) && _interactable != null)
            {
                _interactable.Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;
            _interactable = interactable;
            CanInteract = _interactable.IsInteractable;
            UIManager.Instance.ShowInteractText(_interactable.InteractText);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;
            _interactable.IsInteractable = true;
            _interactable = null;
            CanInteract = false;
            UIManager.Instance.HideInteractText();
            UIManager.Instance.HideCodePuzzleText();
        }
    }
}