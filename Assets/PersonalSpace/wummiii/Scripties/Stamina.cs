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
    [SerializeField] private GameObject _staminaText;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _exhaustDuration;
    [SerializeField] private float _staminaIncreasePerTick;
    [SerializeField] private float _staminaDecreasePerTick;
    [SerializeField] private float _staminaDecreasePerJump;

    private bool ExhaustedDebuff = false;
    private bool TickTimerRunning = false;
    private bool JumpTimerRunning = false;

    private float CurrentStamina;
    private StarterAssetsInputs _input;
    private FirstPersonController _firstPersonController;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _firstPersonController = GetComponent<FirstPersonController>();
        CurrentStamina = _maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // Uheck if we have enouugh stamina and take the firstPersonController's abilty to jump if we dont have enough THIS HAS A BUG the player falls differently when you run over and edge and .Grounded = false ;
        if(CurrentStamina < _staminaDecreasePerJump)
        {
            _firstPersonController.Grounded = false;
        }

        // Jump
        if (_input.jump && _firstPersonController.Grounded && JumpTimerRunning == false)
        {
            JumpTimerRunning = true;
            DecreaseStamina(_staminaDecreasePerJump);
            StartCoroutine(JumpTimer());
        }

        // Stamina regeneration and Sprint are in direct conflict and need to be in this order
        // Sprint
        if (_input.sprint && _input.move != Vector2.zero && TickTimerRunning == false && CurrentStamina > 0)
        {
            TickTimerRunning = true;
            DecreaseStamina(_staminaDecreasePerTick);
            StartCoroutine(TickTimer());
        }
        // Stamina regeneration
        else if (CurrentStamina < _maxStamina && TickTimerRunning == false && ExhaustedDebuff == false)
        {
            TickTimerRunning = true;
            IncreaseStamina(_staminaIncreasePerTick);
            StartCoroutine(TickTimer());
        }

        // Exhaust if zero mana, activate/deactivate sprint ability by changing sprintspeed to the normal movementspeed and stop stamina regeneration for the exhaustion time
        if (CurrentStamina <= 0 && ExhaustedDebuff == false)
        {
            ExhaustedDebuff = true;
            StartCoroutine(ExhaustedTimer());
        }

        RefreshUI();
    }

    // Decrease CurrentStamina by value
    private void DecreaseStamina(float value)
    {
        CurrentStamina -= value;
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
    }

    // Increase CurrentStamina by value
    private void IncreaseStamina(float value)
    {
        CurrentStamina += value;
        if (CurrentStamina > _maxStamina)
        {
            CurrentStamina = _maxStamina;
        }
    }

    // Refresh Userinterface
    private void RefreshUI()
    {
        _staminaText.GetComponent<TextMeshProUGUI>().text = CurrentStamina.ToString();
    }

    // Set sprintSpeed = moveSpeed to fake "not sprinting" then reset sprintSpeed
    private IEnumerator ExhaustedTimer()
    {
        float temp;
        temp = _firstPersonController.SprintSpeed;
        _firstPersonController.SprintSpeed = _firstPersonController.MoveSpeed;

        yield return new WaitForSeconds(_exhaustDuration);

        _firstPersonController.SprintSpeed = temp;
        CurrentStamina = 1;
        ExhaustedDebuff = false;
    }

    // This Timer is here because I check if the conditions to make a jump are met, not if the jump was actually made (_firstPersonController.Grounded is triggered more than one frame)
    private IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.1f);
        JumpTimerRunning = false;
    }

    // Wait for TickTime
    private IEnumerator TickTimer()
    {
        yield return new WaitForSeconds(_tickTime);
        TickTimerRunning = false;
    }
}
