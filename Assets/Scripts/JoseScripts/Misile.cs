using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour
{
    GameObject komodo;
    KomodoController _komodoController;
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
        komodo = GameObject.FindGameObjectWithTag("Komodo");
        _komodoController = komodo.GetComponent<KomodoController>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        cabezaPlayer = GameObject.FindGameObjectWithTag("PlayerCabeza");
        target = cabezaPlayer.transform;
        //target = _komodoController.targetChosen;
        StartCoroutine(Despegue());
        


    }

    // Update is called once per frame
    void  FixedUpdate()
    {

        if (perseguir)
        {
            //transform.LookAt(_komodoController.ultimaPosicion);
            transform.LookAt(target);
            transform.Translate(0f, 0f, speed * Time.deltaTime);
            Destroy(gameObject, 4f);
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
    IEnumerator Landing()
    {
        if (perseguir)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, speed * Time.deltaTime);
            yield return new WaitForSeconds(6f);
            Destroy(gameObject);
        }
    }
}
