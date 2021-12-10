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
        //playerlife = 100;
        if (lastPosition != Vector3.zero)
        {
            transform.position = lastPosition;
        }

        playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerlife <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }

        if (playerlife > 100)
        {
            playerlife = 100;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DmgArea") && playermov.fallVelocity <= 0)
        {
            playerlife -= 40;
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            incheck = true;
            lastPosition = other.transform.position;
        }

    }
    private void OnParticleTrigger()
    {
        Debug.Log("ParticleColl");
        playerlife -= 10;
    }
}
