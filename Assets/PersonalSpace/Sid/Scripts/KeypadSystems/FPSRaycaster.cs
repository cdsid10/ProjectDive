using UnityEngine;

namespace PersonalSpace.Sid.Scripts
{
    public class FPSRaycaster : MonoBehaviour
    {
        //would need to check for this bool through trigger or something to enable it for keycodes interaction
        [field: SerializeField] public bool CanCheckForRaycast { get; set; } = false; 
        
        [SerializeField] private LayerMask uiLayerMask;
        private KeypadButton currentKeypadButton;
        
        private void Update()
        {                
            if(!CanCheckForRaycast) return;
            CheckRaycast();
        }

        private void CheckRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            if (Physics.Raycast(ray, out RaycastHit hit, uiLayerMask))
            {
                if (!hit.transform.TryGetComponent(out KeypadButton keypadButton))
                {
                    if (currentKeypadButton != null)
                    {
                        currentKeypadButton.HideSelectedButtonBorder();
                    }
                    
                    return;
                }
                else
                {
                    currentKeypadButton = keypadButton;
                    currentKeypadButton.ShowSelectedButtonBorder();
                }
                
                if (!Input.GetMouseButton(0)) return;
                keypadButton.Interact();
            }
        }
    }
}