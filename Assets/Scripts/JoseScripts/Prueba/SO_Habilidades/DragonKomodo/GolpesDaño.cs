using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpesDaño : MonoBehaviour
{
    //public GameObject playerObj;
    //PlayerStats player;
    public int damage;

    void Start()
    {
       /* player = playerObj.gameObject.GetComponent<PlayerStats>();
        playerObj = GameObject.FindGameObjectWithTag("Player");*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            PlayerSingleton.Instance.playerHitted = true;
            //player.playerlife -= damage;
            Debug.Log("Recibio daño " + damage);

        }
    }
}
