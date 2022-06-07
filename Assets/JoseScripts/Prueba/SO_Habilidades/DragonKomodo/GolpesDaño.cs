using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpesDaño : MonoBehaviour
{
    public GameObject playerObj;
    PlayerStatus playerstatus;
    public int damage;


    void Start()
    {
       playerstatus = playerObj.gameObject.GetComponent<PlayerStatus>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerstatus.OnTakeDamage(damage);
            //PlayerSingleton.Instance.playerCurrentHP -= damage;
            //PlayerSingleton.Instance.playerHitted = true;
            //player.playerlife -= damage;
            Debug.Log("Recibio daño " + damage);

        }
    }
}
