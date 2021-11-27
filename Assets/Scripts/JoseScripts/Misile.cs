using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour
{
    Transform target;
    GameObject player;
    GameObject cabezaPlayer;
    float speed = 10f;
    PlayerStats playerStats;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        cabezaPlayer = GameObject.FindGameObjectWithTag("PlayerCabeza");
        target = cabezaPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.Translate(0f, 0f, speed * Time.deltaTime);
        Destroy(gameObject, 6f);
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

}
