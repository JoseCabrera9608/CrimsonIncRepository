using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float playerlife;
    public PlayerMovement playermov;
    public static Vector3 lastPosition;
    public bool incheck;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= 50;
        //playermov.enabled = false;

        //playerlife = 100;

        //playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Instance.playerCurrentHP <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }

        if (PlayerSingleton.Instance.playerCurrentHP > 100)
        {
            PlayerSingleton.Instance.playerCurrentHP = 100;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("DmgArea") && playermov.fallVelocity <= 0)
        {
            playerlife -= 40;
        }*/


    }
    private void OnParticleTrigger()
    {
        Debug.Log("ParticleColl");
        playerlife -= 10;
    }
}
