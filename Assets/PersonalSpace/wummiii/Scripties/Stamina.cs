using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamina : MonoBehaviour
{
    [SerializeField] private GameObject staminaText;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _exhaustDuration;
    [SerializeField] private float _staminaIncreasePerTick;
    [SerializeField] private float _staminaDecreasePerTick;
    [SerializeField] private float _staminaDecreasePerJump;

    private bool exhaustedDebuff = false;
    private bool tickTimerRunning = false;

    private float currentStamina;
    private StarterAssetsInputs _input;
    private FirstPersonController _firstPersonController;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _firstPersonController = GetComponent<FirstPersonController>();
        currentStamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we have enouugh stamina and take the firstPersonController's abilty to jump if we dont have enough
        if(currentStamina < _staminaDecreasePerJump)
        {
            _firstPersonController.Grounded = false;
        }

        // jump
        if (_input.jump && _firstPersonController.Grounded && tickTimerRunning == false)
        {
            tickTimerRunning = true;
            DecreaseStamina(_staminaDecreasePerJump);
            StartCoroutine(JumpTimer());
        }

        // sprint
        else if (_input.sprint && _input.move != Vector2.zero && tickTimerRunning == false && currentStamina > 0)
        {
            tickTimerRunning = true;
            DecreaseStamina(_staminaDecreasePerTick);
            StartCoroutine(TickTimer());
        }
        // stamina regeneration
        else if (currentStamina < _maxStamina && tickTimerRunning == false && exhaustedDebuff == false)
        {
            tickTimerRunning = true;
            IncreaseStamina(_staminaIncreasePerTick);
            StartCoroutine(TickTimer());
        }

        // exhaust if zero mana, activate/deactivate sprint ability by changing sprintspeed to the normal movementspeed and stop stamina regeneration for the exhaustion time
        else if (currentStamina <= 0 && exhaustedDebuff == false)
        {
            exhaustedDebuff = true;
            StartCoroutine(ExhaustedTimer());
        }
        RefreshUI();
    }

    // Decrease Stamina by value
    private void DecreaseStamina(float value)
    {
        currentStamina -= value;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    // Increase Stamina bx value
    private void IncreaseStamina(float value)
    {
        currentStamina += value;
        if (currentStamina > _maxStamina)
        {
            currentStamina = _maxStamina;
        }
    }

    // Refresh Userinterface
    private void RefreshUI()
    {
        staminaText.GetComponent<TextMeshProUGUI>().text = currentStamina.ToString();
    }

    // Set sprintSpeed = moveSpeed to fake "not sprinting" then reset sprintSpeed
    private IEnumerator ExhaustedTimer()
    {
        float temp;
        temp = _firstPersonController.SprintSpeed;
        _firstPersonController.SprintSpeed = _firstPersonController.MoveSpeed;

        yield return new WaitForSeconds(_exhaustDuration);

        _firstPersonController.SprintSpeed = temp;
        currentStamina = 1;
        exhaustedDebuff = false;
    }

    // This Timer is here because I check if the conditions to make a jump are met, not if the jump was actually triggered (_firstPersonController.Grounded is triggered more than one frame)
    private IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
        tickTimerRunning = false;
    }

    // Decrease or Increase stamina timed by a tickrate
    private IEnumerator TickTimer()
    {
        yield return new WaitForSeconds(_tickTime);
        tickTimerRunning = false;
    }
}
