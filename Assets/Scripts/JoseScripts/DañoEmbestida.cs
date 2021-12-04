using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoEmbestida : MonoBehaviour
{
    public GameObject playerObj;
    PlayerStats player;
    public float damage;

    void Start()
    {
        player = playerObj.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= damage;
            Debug.Log("Recibio daño por el golpe");
        }
    }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= damage;
            Debug.Log("Recibio daño por el golpe");
        }
    }
}
