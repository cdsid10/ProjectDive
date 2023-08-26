using TMPro;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Interactions")] 
        [SerializeField] private CanvasGroup interactCanvasGroup; 
        [SerializeField] private TextMeshProUGUI interactText;

        [Header("Puzzle Code")] 
        [SerializeField] private CanvasGroup puzzleCodeCanvasGroup;
        [SerializeField] private TextMeshProUGUI codePuzzleTitle;
        [SerializeField] private TextMeshProUGUI codePuzzleCode;
        [SerializeField] private TextMeshProUGUI codePuzzleCodeFooter;
        [SerializeField] private TextMeshProUGUI codePuzzleDescription;
        [SerializeField] private TextMeshProUGUI codePuzzleNote;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowCodePuzzleText(string titleToShow, string code, string codeFooter, string descriptionToShow, string note)
        {
            puzzleCodeCanvasGroup.alpha = 1;
            codePuzzleTitle.text = titleToShow;
            codePuzzleCode.text = code;
            codePuzzleCodeFooter.text = codeFooter;
            codePuzzleDescription.text = descriptionToShow;
            codePuzzleNote.text = note;
        }

        public void HideCodePuzzleText()
        {
            puzzleCodeCanvasGroup.alpha = 0;
            codePuzzleTitle.text = "";
            codePuzzleCode.text = "";
            codePuzzleCodeFooter.text = "";
            codePuzzleDescription.text = "";
            codePuzzleNote.text = "";
        }

        public void ShowInteractText(string textToBeShown)
        {
            interactCanvasGroup.alpha = 1; //use tween
            interactText.text = textToBeShown;
        }

        public void HideInteractText()
        {
            interactCanvasGroup.alpha = 0; //use tween
            interactText.text = "";
        }
    }
}
