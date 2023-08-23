using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamina : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    [SerializeField] private float maxStamina;
    private float currentStamina;
    private StarterAssetsInputs _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //doesnt check if the player acutally jumped, just if the key is pressed
        if(/*_input.jump*/ Input.GetKeyDown(KeyCode.Space))
        {
            currentStamina -= 10;
        }

        //doesnt check if the player actually moves forward
        if (_input.sprint)
        {
            currentStamina -= 0.1f;
        }
        Debug.Log(currentStamina);
    }
}
