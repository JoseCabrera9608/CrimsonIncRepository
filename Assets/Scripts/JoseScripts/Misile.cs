using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour
{
    Transform target;
    public GameObject player;
    float speed = 10f;
    PlayerStats playerStats;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats.playerlife -= 30;
            Destroy(gameObject);
        }
    }
}
