using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [Header("Checkpoint Screen")] 
        [SerializeField] private CanvasGroup blackFadeScreen;

        [Header("Recorder Message")] 
        [SerializeField] private CanvasGroup recorderCanvasGroup;

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

        public void ShowRecorderScreen()
        {
            StartCoroutine(FadeUIElements(recorderCanvasGroup, 1f, 0.25f));
        }

        public void HideRecorderScreen()
        {
            StartCoroutine(FadeUIElements(recorderCanvasGroup, 0f, 0.25f));
        }

        public void FadeInCheckpointScreen(float duration)
        {
            StartCoroutine(FadeUIElements(blackFadeScreen, 1f, duration));
        }

        public void FadeOutCheckpointScreen(float duration)
        {
            StartCoroutine(FadeUIElements(blackFadeScreen, 0f, duration));
        }
        
        IEnumerator FadeLoadingScreen(float targetValue, float duration)
        {
            float startValue = blackFadeScreen.alpha;
            float time = 0;
            while (time < duration)
            {
                blackFadeScreen.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            blackFadeScreen.alpha = targetValue;
        }

        IEnumerator FadeUIElements(CanvasGroup canvasGroup, float targetValue, float duration)
        {
            float startValue = canvasGroup.alpha;
            float time = 0;
            while (time < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = targetValue;
        }

        public void ShowCodePuzzleText(string titleToShow, string code, string codeFooter, string descriptionToShow, string note)
        {
            StartCoroutine(FadeUIElements(puzzleCodeCanvasGroup, 1f, 0.25f));
            codePuzzleTitle.text = titleToShow;
            codePuzzleCode.text = code;
            codePuzzleCodeFooter.text = codeFooter;
            codePuzzleDescription.text = descriptionToShow;
            codePuzzleNote.text = note;
        }

        public void HideCodePuzzleText()
        {
            StartCoroutine(FadeUIElements(puzzleCodeCanvasGroup, 0f, 0.25f));
            codePuzzleTitle.text = "";
            codePuzzleCode.text = "";
            codePuzzleCodeFooter.text = "";
            codePuzzleDescription.text = "";
            codePuzzleNote.text = "";
        }

        public void ShowInteractText()
        {
            StartCoroutine(FadeUIElements(interactCanvasGroup, 1f, 0.25f));
        }

        public void HideInteractText()
        {
            StartCoroutine(FadeUIElements(interactCanvasGroup, 0f, 0.25f));
        }
    }
}
