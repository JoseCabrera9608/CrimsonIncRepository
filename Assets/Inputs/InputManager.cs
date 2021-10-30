using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public bool jump_Input;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

        }

        playerControls.Enable();

    }

    private void Update()
    {
        if (verticalInput != 0 || horizontalInput != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {

        HandleMovementInput();
        HandleJumpingInput();

    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void HandleJumpingInput()
    {
        if (jump_Input == true)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }

}
