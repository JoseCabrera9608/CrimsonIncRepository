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

    public float vision;
    public float attackRange;
    public NavMeshAgent enemy;

    public Animator dinoAnim;




    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

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
        float dist = Vector3.Distance(playerPosition.position, transform.position);
        if (dist <= vision)
        {
            FollowState();
        }

        if (dist <= attackRange)
        {
            AttackState();
        }
        else
        {
            dinoAnim.SetBool("Mordida", false);
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
        }
    }
}
