using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerAttack player;
    public PlayerStats playerstats;
    public ParticleSystem deathParticles;

    public NavMeshAgent enemy;
    public Transform playerPosition;
    public float timer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerstats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.LookAt(playerPosition);
        enemy.destination = playerPosition.position;
        float dist = Vector3.Distance(playerPosition.position, transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GAAATrigger");

        

        if (other.gameObject.CompareTag("PlayerWeapon") && player.attacking == true)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;

            if (timer >= 2)
            {
                playerstats.playerlife = playerstats.playerlife - 10;
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
