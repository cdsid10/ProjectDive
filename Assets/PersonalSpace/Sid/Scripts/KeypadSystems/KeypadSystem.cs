using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PersonalSpace.Sid.Scripts
{
    public class KeypadSystem : MonoBehaviour
    {
        [SerializeField] private List<KeypadButton> keypadButtonList = new List<KeypadButton>();
        
        [SerializeField] private List<KeypadButton> referenceList = new List<KeypadButton>();
        [SerializeField] private List<KeypadButton> playerKeypadButtonList = new List<KeypadButton>();

        [SerializeField] private DoorTest door;
        
        [SerializeField] private TextMeshProUGUI displayText;
        
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
            //keypadButton.buttonImage.color = Color.yellow;
            BlockAllKeypadInput();
            displayText.text = "Processing...";
            yield return new WaitForSeconds(2f);
            
            if (CrossCheckWithReferenceList())
            {
                if (referenceList.Count == playerKeypadButtonList.Count)
                {
                    //open door or something
                    displayText.text ="code is valid, door opening...";
                    //keypadButton.buttonImage.color = Color.white;
                    yield return new WaitForSeconds(1f);
                    door.Animate(true);
                }
                else
                {
                    displayText.text ="enter next digit";
                    yield return new WaitForSeconds(0.5f);
                    displayText.text = "";
                
                    foreach (var button in playerKeypadButtonList)
                    {
                        displayText.text += button.buttonNumber;
                    }
                    //keypadButton.buttonImage.color = Color.white;
                    EnableAllKeypadInput();
                }
            }
            else
            {
                displayText.text ="wrong input, try again";
                yield return new WaitForSeconds(0.5f);
                playerKeypadButtonList.Clear();
                displayText.text = "enter code";
                //keypadButton.buttonImage.color = Color.white;
                EnableAllKeypadInput();
            }
        }
    }
}