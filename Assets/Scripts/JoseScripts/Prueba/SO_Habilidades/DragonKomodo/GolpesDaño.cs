using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpesDaño : MonoBehaviour
{
    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.playerlife -= 30;
            Debug.Log("Recibio daño por el golpe");
        }
    }
}
