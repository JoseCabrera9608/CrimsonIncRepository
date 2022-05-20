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
    public float staminaRecovery;
    public float dashspeed;
    public float duraciondash;
    private int dashAttempts;
    public float dashStartTime;

    public float timer;

    public float staminaDash;
    public float staminaRun;
    public float staminaRunValue;

    public bool recovery;
    public bool onelevator;

    public bool incheck;
    public ProgressManager progress;
    public PlayerAttack playerAttack;
    public PlayerStatus playerStatus;

    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================
    [SerializeField] private float rotationSpeed;
    [SerializeField] public float movSpeed;
    public float runSpeed;
    public float walkSpeed;
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
        playerStatus = this.GetComponent<PlayerStatus>();



        rb = GetComponent<Rigidbody>();

        //=====QUITAR LUEGO==========
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= staminaRecovery)
        {
            recovery = false;
        }
        SingletonConnect();
    }

    private void FixedUpdate()
    {
        
        if (playerStatus.interacting == true)
        {
            return;
        }
        
        PlayerMovement();
        PlayerRotation();
        HandleDash();
        Falling();
    }

    void SingletonConnect()
    {
        staminaMax = PlayerSingleton.Instance.playerMaxStamina;

        

        staminaRun = PlayerSingleton.Instance.playerRunStaminaCost;
        PlayerSingleton.Instance.playerRunStaminaCost = staminaRunValue;

    }

    private void PlayerMovement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //moveDirection = Camera.main.transform.forward * playerInput.y;
        moveDirection = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * playerInput.y;
        moveDirection += Camera.main.transform.right * playerInput.x;
        moveDirection.Normalize();
        

        //moveDirection.y = 0;

        if (isDashing == false && playerAttack.attackStatus == false)
        {
            moveDirection *= movSpeed;
            
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
         //   FindObjectOfType<AudioManager>().Play("CaminarMetal");
            if (PlayerSingleton.Instance.playerCurrentStamina > 0)
            {
                movSpeed = runSpeed;
                playeranim.SetBool("Walk", false);
                playeranim.SetBool("Run", true);
            }
            else
            {
                playeranim.SetBool("Run", false);
                playeranim.SetBool("Walk", true);
            }
            
            if (rb.velocity != new Vector3(0, 0, 0) && playerAttack.attackStatus == false)
            {
                Recovery();
                PlayerSingleton.Instance.playerCurrentStamina -= (0.01f * staminaRun) * Time.deltaTime;
            }
                //stamina -= (0.01f* staminaRun) * Time.deltaTime;
        }
        else
        {
            movSpeed = walkSpeed;
            playeranim.SetBool("Run", false);
            playeranim.SetBool("Walk", true);
            //playeranim.SetFloat("AnimSpeed", 0.7f);
        }

        if (playerInput.x == 0 && playerInput.y == 0)
        {
            playeranim.SetBool("Run", false);
            playeranim.SetBool("Walk", false);
            //FindObjectOfType<AudioManager>().Stop("Movimiento");
        }
        if (playerInput.x > 0 || playerInput.y > 0 && movSpeed == walkSpeed)
        {
            //FindObjectOfType<AudioManager>().Play("CaminarMetal");
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

        if (playerAttack.attackStatus == false)
        {
            transform.rotation = playerRotation;
        }

        //transform.rotation = playerRotation;
    }

    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.Space);

        if (recovery == false)
        {
            PlayerSingleton.Instance.playerCurrentStamina += Time.deltaTime;
        }
        
        if (PlayerSingleton.Instance.playerCurrentStamina <= 0)
        {
            movSpeed = walkSpeed;
            PlayerSingleton.Instance.playerCurrentStamina = 0;
        }

        if (Input.GetKey(KeyCode.Space) && !isDashing && PlayerSingleton.Instance.playerCurrentStamina >= 0.3f * staminaMax && onelevator == false)
        {
            if (dashAttempts <= 5000 && (rb.velocity != new Vector3(0,0,0)))  //Dashes maximos
            {
                //FindObjectOfType<AudioManager>().Play("Dash");
                OnStartDash();
                //DashParticles.Play();
                PlayerSingleton.Instance.playerCurrentStamina -= (0.01f* staminaDash)*staminaMax;
                Recovery();
            }
        }

        if (PlayerSingleton.Instance.playerCurrentStamina >= staminaMax)
        {
            PlayerSingleton.Instance.playerCurrentStamina = staminaMax;
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
        timer = 0;
        recovery = true;



        //StartCoroutine(RecoveryCoroutine());
    }

    public IEnumerator RecoveryCoroutine()
    {
        recovery = true;

        yield return new WaitForSeconds(2f);

        recovery = false;
    }

    void OnStartDash()
    {
        FindObjectOfType<AudioManager>().Play("Dash");
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

        if (other.gameObject.CompareTag("Bounce"))
        {
            onelevator = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Bounce"))
        {
            onelevator = false;
        }

    }

    public void Pisadas()
    {
        FindObjectOfType<AudioManager>().Play("CaminarNormal");
    }


}
