using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoNubeArena : MonoBehaviour
{
    GameObject playerObj;
    PlayerStats player;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.gameObject.GetComponent<PlayerStats>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= 1.1f;
            Debug.Log("Daño por Nube");
        }
    }
}
