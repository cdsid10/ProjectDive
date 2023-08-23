using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamina : MonoBehaviour
{
    [SerializeField] private GameObject staminaText;

    [SerializeField] private float _maxStamina;
    [SerializeField] private float _increaseAmountPerSecond;
    [SerializeField] private float _decreaseAmountPerSecond;
    [SerializeField] private float _decreaseAmountPerJump;

    private bool canJump = true;
    private bool decreasingStamina = false;
    private bool increasingStamina = false;

    private float currentStamina;
    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        currentStamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.jump && GetComponent<FirstPersonController>().Grounded && canJump)
        {
            canJump = false;
            currentStamina -= _decreaseAmountPerJump;
            StartCoroutine(Timer());
        }
        if (_input.sprint && decreasingStamina == false)
        {
            decreasingStamina = true;
            StartCoroutine(DecreaseStaminaSprint());
        }
        if (currentStamina < _maxStamina && increasingStamina == false)
        {
            increasingStamina = true;
            StartCoroutine(IncreaseStamina());
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        staminaText.GetComponent<TextMeshProUGUI>().text = currentStamina.ToString();
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }

    private IEnumerator DecreaseStaminaSprint()
    {
        currentStamina -= _decreaseAmountPerSecond;
        yield return new WaitForSeconds(0.5f);
        decreasingStamina = false;
    }
    private IEnumerator IncreaseStamina()
    {
        currentStamina += _increaseAmountPerSecond;
        yield return new WaitForSeconds(0.5f);
        increasingStamina = false;
    }
}
