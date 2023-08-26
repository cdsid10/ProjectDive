using System.Collections;
using System.Collections.Generic;
using PersonalSpace.Sid.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.KeypadSystems
{
    public class KeypadSystem : MonoBehaviour
    {
        [SerializeField] private List<KeypadButton> keypadButtonList = new List<KeypadButton>();
        
        [SerializeField] private List<KeypadButton> referenceList = new List<KeypadButton>();
        [SerializeField] private List<KeypadButton> playerKeypadButtonList = new List<KeypadButton>();

        [SerializeField] private DoorTest door;
        
        [SerializeField] private TextMeshProUGUI displayText;
        
        [SerializeField] private AudioClip keypadProcessing;
        [SerializeField] private AudioClip keyValid;
        [SerializeField] private AudioClip keyError;
        [SerializeField] private AudioClip doorOpen;
        
        public void AddToKeypadList(KeypadButton keypadButton)
        {
            playerKeypadButtonList.Add(keypadButton);

            StartCoroutine(CheckListsAndDisplayMessages(keypadButton));
        }

        private bool CrossCheckWithReferenceList()
        {
            //check both lists and if it doesnt contain the same objects or order clear the list
            //show the same in a text field as well
            for (int i = 0; i < playerKeypadButtonList.Count; i++)
            {
                if (playerKeypadButtonList[i] != referenceList[i]) return false;
            }

            return true;
        }
        
        private void EnableAllKeypadInput()
        {
            foreach (var keypadButton in keypadButtonList)
            {
                keypadButton.EnableInput();
            }
        }

        private void BlockAllKeypadInput()
        {
            foreach (var keypadButton in keypadButtonList)
            {
                keypadButton.DisableInput();
                keypadButton.HideSelectedButtonBorder();
            }
        }

        private IEnumerator CheckListsAndDisplayMessages(KeypadButton keypadButton)
        {
            BlockAllKeypadInput();
            displayText.text = "Processing...";
            yield return new WaitForSeconds(2f);
            
            if (CrossCheckWithReferenceList())
            {
                if (referenceList.Count == playerKeypadButtonList.Count)
                {
                    SoundManager.Instance.PlaySoundEffects(keyValid);
                    displayText.text ="Code is Valid, Door Opening...";
                    yield return new WaitForSeconds(1f);
                    SoundManager.Instance.PlaySoundEffects(doorOpen);
                    door.Animate(true);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    displayText.text = "";
                    SoundManager.Instance.PlaySoundEffects(keyValid);
                    foreach (var button in playerKeypadButtonList)
                    {
                        displayText.text += button.buttonNumber;
                    }
                    EnableAllKeypadInput();
                }
            }
            else
            {
                SoundManager.Instance.PlaySoundEffects(keyError);
                displayText.text ="Wrong Input, Try Again";
                yield return new WaitForSeconds(0.5f);
                playerKeypadButtonList.Clear();
                displayText.text = "Enter Code:";
                EnableAllKeypadInput();
            }
        }
    }
}