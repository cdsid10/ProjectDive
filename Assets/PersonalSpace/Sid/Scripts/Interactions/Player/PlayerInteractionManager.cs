using PersonalSpace.Sid.Scripts.Interactions.Telephone;
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
            if (!CanInteract || !_interactable.IsInteractable) return;

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
            if (CanInteract)
            {
                UIManager.Instance.ShowInteractText(_interactable.InteractText);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;

            if (interactable is InteractStartPhone startPhone && !startPhone.HasAlreadyRinged)
            {
                _interactable.IsInteractable = false;

            }
            else
            {
                _interactable.IsInteractable = true;
            }
            _interactable = null;
            CanInteract = false;
            UIManager.Instance.HideInteractText();
            UIManager.Instance.HideCodePuzzleText();
            UIManager.Instance.HideRecorderScreen();
        }
    }
}