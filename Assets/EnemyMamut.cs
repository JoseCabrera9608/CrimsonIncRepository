using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMamut : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerAttack playerattack;
    public PlayerStats playerstats;
    public ParticleSystem deathParticles;

    public int health;
    public bool hitted;

    public float dist;
    public float vision;

    public NavMeshAgent enemy;
    public Transform playerPosition;
    public float timer;

    public GameObject Pause;

    public MeshRenderer nupMesh;
    public Material matNormal;
    public Material matHitted;


    void Start()
    {
        playerattack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerstats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.3)
        {
            nupMesh.material = matNormal;
        }
    }

    void FixedUpdate()
    {
        IdleState();
    }

    void IdleState()
    {
        PauseController pause = Pause.GetComponent<PauseController>();

        dist = Vector3.Distance(playerPosition.position, transform.position);
        if (dist <= vision && pause.pause == false)
        {
            FollowState();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, vision);

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;

            if (timer >= 2)
            {
                playerstats.playerlife = playerstats.playerlife - 30;
                Debug.Log("Damage GAAAA");
                timer = 0;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0;

        }
    }
}
