using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PersonalSpace.Sid.Scripts
{
    public class KeypadButton : MonoBehaviour
    {
        [Tooltip("Handled with code, instead of assigning it manually, need to give appropriate gameobject names")]
        [field: SerializeField] public string buttonNumber { get; private set; }
        [field: SerializeField] public Image buttonImage { get; set; }
        [SerializeField] private GameObject buttonHighlightBorder;

        private Collider keypadCollider;
        private KeypadSystem keypadSystem;

        private void Awake()
        {
            keypadSystem = gameObject.GetComponentInParent<KeypadSystem>();
            keypadCollider = gameObject.GetComponent<Collider>();
            buttonImage = gameObject.GetComponent<Image>();
            buttonNumber = gameObject.name;
            HideSelectedButtonBorder();
        }

        public void Interact()
        {
            keypadSystem.AddToKeypadList(this);
        }

        public void ShowSelectedButtonBorder()
        {
            buttonHighlightBorder.SetActive(true);
        }

        public void HideSelectedButtonBorder()
        {
            buttonHighlightBorder.SetActive(false);
        }

        public void EnableInput()
        {
            keypadCollider.enabled = true;
        }

        public void DisableInput()
        {
            keypadCollider.enabled = false;
        }
    }
}