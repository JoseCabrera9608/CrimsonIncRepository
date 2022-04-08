using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 playerInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    public Animator playeranim;

    public bool isDashing;
    public float dashcd;
    public float dashingcd;
    public float dashspeed;
    public float duraciondash;
    private int dashAttempts;
    private float dashStartTime;
    public float timer;

    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movSpeed;
    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================

    [SerializeField] public bool isGrounded;
    private float airTime;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float rayCastOffset;
    [SerializeField] LayerMask groundLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //=====QUITAR LUEGO==========
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleDash();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
        //Falling();
    }
    private void PlayerMovement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //moveDirection = Camera.main.transform.forward * playerInput.y;
        moveDirection = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * playerInput.y;
        moveDirection += Camera.main.transform.right * playerInput.x;
        moveDirection.Normalize();

        moveDirection.y = 0;

        if (isDashing == false)
        {
            moveDirection *= movSpeed;
        }
        


        if (playerInput.x == 0 && playerInput.y == 0)
        {
            playeranim.SetBool("Run", false);
            //FindObjectOfType<AudioManager>().Stop("Movimiento");
        }
        else
        {
            playeranim.SetBool("Run", true);
        }

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }

    private void PlayerRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = Camera.main.transform.forward * playerInput.y;
        targetDirection += Camera.main.transform.right * playerInput.x;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.Space);

        dashingcd += Time.deltaTime;

        if (isTryingToDash && !isDashing && dashingcd >= dashcd)
        {
            if (dashAttempts <= 5000)  //Dashes maximos
            {
                //FindObjectOfType<AudioManager>().Play("Dash");
                OnStartDash();
                //DashParticles.Play();
                dashingcd = 0;
            }
        }

        if (isDashing)
        {
            if (Time.time - dashStartTime <= duraciondash)
            {

                playeranim.SetBool("Dash", true);

                if (playerInput.Equals(Vector3.zero))
                {
                    rb.AddForce(moveDirection * dashspeed, ForceMode.Impulse);  //Velocidad del Dash estando quieto
                }
                else
                {
                    rb.AddForce(moveDirection * dashspeed, ForceMode.Impulse);  //Velocidad del Dash en movimiento
                }


            }
            else
            {
                playeranim.SetBool("Dash", false);
                OnEndDash();
            }

        }
    }

    void OnStartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
        //dashAttempts += 1;
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
        //anim.SetBool("Dash", false);
    }

    /*private void Falling()
    {
        RaycastHit hit;

        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        targetPosition = transform.position;

        if (Physics.SphereCast(rayCastOrigin, .2f, -transform.up, out hit, rayCastOffset))
        {
            // if(!isGrounded) asignar animacion de aterrizar
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y + rayCastOffset;
            airTime = 0;
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        if (!isGrounded)
        {
            airTime += Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * fallSpeed * airTime);
        }



        if (isGrounded)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        }
    }*/

}
