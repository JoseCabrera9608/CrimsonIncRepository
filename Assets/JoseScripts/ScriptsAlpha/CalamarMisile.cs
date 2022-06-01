using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalamarMisile : MonoBehaviour
{
    Transform target;
    GameObject player;
    GameObject cabezaPlayer;
    public GameObject chosenTarget;
    [SerializeField] float speed = 15f;
    PlayerStats playerStats;
    [SerializeField] private float impulso;
    private bool perseguir = false;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        cabezaPlayer = GameObject.FindGameObjectWithTag("PlayerCabeza");
        target = cabezaPlayer.transform;
        StartCoroutine(Despegue());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (perseguir)
        {
            transform.LookAt(target);
            transform.Translate(0f, 0f, speed * Time.deltaTime);
            Destroy(gameObject, 7f);
        }
    }

    IEnumerator Despegue()
    {
        rb.AddForce(Vector3.up * impulso, ForceMode.Impulse);
        yield return new WaitForSeconds(1.6f);
        perseguir = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
            playerStats.playerlife -= 20;
            Destroy(gameObject);
        }
    }
}
