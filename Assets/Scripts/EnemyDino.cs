using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDino : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerPosition;

    private float turnSpeed = 10f;
    private float enemySpeed;
    public float speed;
    public int dmgtoPlayer;
    public bool attacking;
    public bool dmgingPlayer;
    public float dist;
    public float timer;

    public bool hitted;

    public float vision;
    public float attackRange;
    public NavMeshAgent enemy;

    public Animator dinoAnim;

    public PlayerStats player;


    public SkinnedMeshRenderer nupMesh;
    public Material matNormal;
    public Material matHitted;

    public PlayerAttack playerattack;
    public GameObject Pause;




    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerattack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (attacking == true && dist <= attackRange)
        {
            DamagingPlayer();
        }

        timer += Time.deltaTime;

        if (timer >= 0.3)
        {
            nupMesh.material = matNormal;
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
  
    }

    void IdleState()
    {
        PauseController pause = Pause.GetComponent<PauseController>();

        dist = Vector3.Distance(playerPosition.position, transform.position);
        if (dist <= vision && pause.pause == false)
        {
            FollowState();
        }

        if (dist <= attackRange && pause.pause == false)
        {
            AttackState();
            
        }
        else
        {
            dinoAnim.SetBool("Mordida", false);
            attacking = false;
        }
        //else enemy.destination = transform.position;
    }

    void FollowState()
    {
        transform.LookAt(playerPosition);
        enemy.destination = playerPosition.position;
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
        dmgtoPlayer = 20;
        attacking = true;
    }

    void StopMordiendoPlayer()
    {
        dmgtoPlayer = 0;
        attacking = false;
    }

    void AttackState()
    {
        dinoAnim.SetBool("Mordida", true);
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
