using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update

    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        isInteracting = inputManager.anim.GetBool("isInteracting");
        playerLocomotion.isJumping = inputManager.anim.GetBool("isJumping");
        inputManager.anim.SetBool("isGrounded", playerLocomotion.isGrounded);

    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        inputManager.anim.SetBool("isInteracting", isInteracting);
        inputManager.anim.CrossFade(targetAnimation, 0.2f);
    }

}
