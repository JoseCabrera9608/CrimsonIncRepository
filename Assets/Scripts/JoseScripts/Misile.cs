using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour
{
    Transform target;
    GameObject player;
    GameObject cabezaPlayer;
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
    void  FixedUpdate()
    {

        if (perseguir)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, speed * Time.deltaTime);
            Destroy(gameObject, 10f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisiono misil");
            playerStats.playerlife -= 30;
            Destroy(this.gameObject);
        }
        
    }
    IEnumerator Despegue()
    {
        rb.AddForce(Vector3.up * impulso, ForceMode.Impulse);
        yield return new WaitForSeconds(1.6f);
        perseguir = true;

    }
}
