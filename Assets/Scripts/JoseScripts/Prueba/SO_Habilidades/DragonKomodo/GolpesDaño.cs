using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpesDaño : MonoBehaviour
{
    public GameObject playerObj;
    PlayerStats player;

    void Start()
    {
        player = playerObj.gameObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= 30;
            Debug.Log("Recibio daño por el golpe");
        }
    }
}
