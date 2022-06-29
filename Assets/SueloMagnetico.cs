using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloMagnetico : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private Movement playermov;
    private PlayerStatus playerStatus;
    public float timer;
    public float slowspeed;
    public float damage;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerStatus>();
        playermov = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            
            playermov.movSpeed = slowspeed;
            if (timer >= 1)
            {
                PlayerStatus.damagePlayer?.Invoke(damage);
                timer = 0;
            }
        }
    }

}
