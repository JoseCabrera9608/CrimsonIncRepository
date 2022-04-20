using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    public Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public bool grounded;
    public float duraciondash;

    //public SkinnedMeshRenderer playerMesh;
    public Material matNormal;
    public Material matHitted;

    public Animator playeranim;

    public bool isDashing;
    public float dashcd;
    public float dashingcd;
    public float dashspeed;
    private int dashAttempts;
    private float dashStartTime;
    public float timer;

    [SerializeField] ParticleSystem DashParticles;

    public Camera mainCamera;
    public Vector3 camForward;
    public Vector3 camRight;
    public Vector3 inputVector = Vector3.zero;
    public Transform playerPosition;
    public float enemySpeed;
    public float vision;
    public float turnSpeed = 10f;
    public bool lockon;
    public GameObject marcado;
    public GameObject Pause;
    public bool marcando;
    public bool incheck;
    public bool isMoving;
    public bool startSound;
    public ProgressManager progress;


    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        //transform.position = progress.lastCheckpointPos;

        if (progress.lastposition != Vector3.zero)
        {
            transform.position = progress.lastposition;
        }


        player = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dashingcd = dashcd;

        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {

        PauseController pause = Pause.GetComponent<PauseController>();

        if (pause.pause == false)
        {
            player.Move(movePlayer * Time.deltaTime);

        }
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        playerInput = new Vector3(inputVector.x, 0, inputVector.z);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        if (isDashing == false)
        {
            movePlayer = movePlayer * playerSpeed;
        }

        if (inputVector.x == 0 && inputVector.z == 0)
        {
            playeranim.SetBool("Run", false);
            FindObjectOfType<AudioManager>().Stop("Movimiento");
        }
        else
        {
            playeranim.SetBool("Run", true);
        }
        //movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        SetJump();
        HandleDash();



        timer += Time.deltaTime;

        if (timer >= 0.3)
        {
            //playerMesh.material = matNormal;
        }


    }

    void Sonido()
    {
     
       FindObjectOfType<AudioManager>().Play("Movimiento");
      
    }
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            grounded = true;
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        }

        else
        {
            grounded = false;
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            FindObjectOfType<AudioManager>().Stop("Movimiento");
        }
    }

    void SetJump()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    void HandleDash()
    {
        bool isTryingToDash = Input.GetKeyDown(KeyCode.LeftShift);

        //dashingcd += Time.deltaTime;

        if (isTryingToDash && !isDashing && dashingcd >= dashcd)
        {
            if (dashAttempts <= 5000)  //Dashes maximos
            {
                FindObjectOfType<AudioManager>().Play("Dash");
                OnStartDash();
                DashParticles.Play();
                dashingcd -= 0.5f; ;
            }
        }
        if (dashingcd >= dashcd)
        {
            dashingcd = 2;
        }
        else
        {
            dashingcd += Time.deltaTime;
        }

        if (isDashing)
        {
            if (Time.time - dashStartTime <= duraciondash)
            {
                if (playerInput.Equals(Vector3.zero))
                {
                    player.Move(transform.forward * (dashspeed) * Time.deltaTime);  //Velocidad del Dash estando quieto
                }
                else
                {
                    player.Move(transform.forward * (dashspeed) * Time.deltaTime);  //Velocidad del Dash en movimiento
                }
                

            }
            else
            {
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            incheck = true;
            progress.lastposition = other.transform.position;
        }

    }


}
