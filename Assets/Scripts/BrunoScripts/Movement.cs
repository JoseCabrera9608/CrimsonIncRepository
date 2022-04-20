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
    public float staminaMax;
    public float stamina;
    public float dashspeed;
    public float duraciondash;
    private int dashAttempts;
    public float dashStartTime;
    public float timer;

    public bool recovery;

    public bool incheck;
    public ProgressManager progress;
    public PlayerAttack playerAttack;

    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================
    [SerializeField] private float rotationSpeed;
    [SerializeField] public float movSpeed;
    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================

    [SerializeField] public bool isGrounded;
    private float airTime;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float rayCastOffset;
    [SerializeField] LayerMask groundLayer;
    private void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        playerAttack = this.GetComponent<PlayerAttack>();

        if (progress.lastposition != Vector3.zero)
        {
            transform.position = progress.lastposition;
        }

        rb = GetComponent<Rigidbody>();

        //=====QUITAR LUEGO==========
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
        HandleDash();
        Falling();
    }
    private void PlayerMovement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //moveDirection = Camera.main.transform.forward * playerInput.y;
        moveDirection = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * playerInput.y;
        moveDirection += Camera.main.transform.right * playerInput.x;
        moveDirection.Normalize();

        moveDirection.y = 0;

        if (isDashing == false && playerAttack.attackStatus == false)
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

        if (recovery == false)
        {
            stamina += Time.deltaTime;
        }
        

        if (Input.GetKey(KeyCode.Space) && !isDashing && stamina >= 0.3f * staminaMax)
        {
            if (dashAttempts <= 5000)  //Dashes maximos
            {
                //FindObjectOfType<AudioManager>().Play("Dash");
                OnStartDash();
                //DashParticles.Play();
                stamina -= (0.3f*staminaMax);
                Recovery();
            }
        }

        if (stamina >= staminaMax)
        {
            stamina = staminaMax;
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

    public void Recovery()
    {
        StartCoroutine(RecoveryCoroutine());
    }

    public IEnumerator RecoveryCoroutine()
    {
        recovery = true;

        yield return new WaitForSeconds(2f);

        recovery = false;
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

    private void Falling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        //Vector3 targetPosition;
        //targetPosition = transform.position;
        if (!isGrounded)
        {
            airTime += Time.deltaTime;
            //rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * fallSpeed * airTime);
        }

        if (Physics.Raycast(rayCastOrigin, -Vector3.up, out hit, rayCastOffset, groundLayer))
        {
            // if(!isGrounded) asignar animacion de aterrizar

            Vector3 rayCastHitPoint = hit.point;
            //targetPosition.y = rayCastHitPoint.y + rayCastOffset;

            airTime = 0;
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

        //if (isGrounded)
        //{
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            incheck = true;
            progress.lastposition = other.transform.position;
        }

    }

}
