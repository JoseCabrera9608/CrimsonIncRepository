using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss2Arena : MonoBehaviour
{
    public Transform playerPosition;

    private float turnSpeed = 10f;
    private float enemySpeed;
    public float speed;
    public int dmgtoPlayer;
    public bool attacking;
    public bool dmgingPlayer;
    public float dist;

    public float timer;
    public float jumptimer;

    public bool hitted;
    public bool saltando;
    public bool battlestarted = false;

    public int health;

    public float vision;
    public float invokearea;
    public float attackRange;
    public float under;
    //public NavMeshAgent enemy;

    public Animator dinoAnim;

    public PlayerStats player;


    public SkinnedMeshRenderer nupMesh;
    public Material matNormal;
    public Material matHitted;

    public PlayerAttack playerattack;
    public PlayerMovement playermov;

    public GameObject Pause;

    //public NavMeshAgent dinoNav;

    public GameObject bulletslot;
    public GameObject spawnslot;

    public GameObject LandDmg;


    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerattack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //dinoNav.speed = speed;

        LandDmg.SetActive(false);

        health = 100;

    }

    // Update is called once per frame
    void Update()
    {

        if (attacking == true && dist <= attackRange)
        {
            DamagingPlayer();
        }

        timer += Time.deltaTime;
        jumptimer += Time.deltaTime;
        //dinoNav.speed = speed;

        if (timer >= 0.3)
        {
            nupMesh.material = matNormal;
        }



       /* if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackState();
        }
       */
        if (hitted == true)
        {
            JumpState();
        }


    }

    void FixedUpdate()
    {
        IdleState();

        //Death();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, vision);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, invokearea);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, under);

    }

    void IdleState()
    {
        PauseController pause = Pause.GetComponent<PauseController>();

        dist = Vector3.Distance(playerPosition.position, transform.position);
        if (dist <= vision && pause.pause == false)
        {
            FollowState();

        }

        if (dist <= invokearea)
        {
            battlestarted = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CallState();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            JumpState();

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackState();

        }
        else
        {
            dinoAnim.SetBool("Mordida", false);
            //FindObjectOfType<AudioManager>().Stop("Mordida");
            attacking = false;
        }
        //else enemy.destination = transform.position;
    }

    void FollowState()
    {
        dinoAnim.SetTrigger("Run");
        transform.LookAt(playerPosition);
        //enemy.destination = playerPosition.position;
        float dist = Vector3.Distance(playerPosition.position, transform.position);
        //animator.SetBool("isMoving", true);

    }

    void DamagingPlayer()
    {
        dmgingPlayer = true;
        player.playerlife = player.playerlife - dmgtoPlayer;
        attacking = false;
        dmgingPlayer = false;

    }


    void MordiendoPlayer()
    {
        FindObjectOfType<AudioManager>().Play("Mordida");
        dmgtoPlayer = 20;
        attacking = true;
    }

    void StopMordiendoPlayer()
    {
        //FindObjectOfType<AudioManager>().Stop("Mordida");
        dmgtoPlayer = 0;
        attacking = false;
    }

    void AttackState()
    {
        dinoAnim.SetBool("Mordida", true);

    }

    void CallState()
    {
        dinoAnim.SetTrigger("Call");

    }

    void CallSpawn()
    {
        GameObject temporalbullet = Instantiate(bulletslot);
        temporalbullet.transform.position = spawnslot.transform.position;
    }

    void JumpState()
    {

        if (saltando == false)
        {

            if (jumptimer >= 1)
            {
                dinoAnim.SetTrigger("Salto");
                speed = 15;
                jumptimer = 0;
            }


        }
    }

    IEnumerator EndJump()
    {
        speed = 5;
        jumptimer = 0;
        LandDmg.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        LandDmg.SetActive(false);

    }


    void AfterJump()
    {
        speed = 5;
        jumptimer = 0;
        LandDmg.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerContact");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            Debug.Log("PlayerContactTrigger");

            if (collision.gameObject.CompareTag("PlayerWeapon") && playerattack.attacking == true)
            {

                Debug.Log("PlayerContactTrigger");

                hitted = true;

                if (timer >= 0.5)
                {
                    nupMesh.material = matHitted;
                    timer = 0;

                }
            }
        }
    }
 }
