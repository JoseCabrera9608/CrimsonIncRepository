using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacaStunArea : MonoBehaviour
{
    GameObject player;
    Movement movimientoPlayer;
    PlayerStatus playerStatus;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movimientoPlayer = player.GetComponent<Movement>();
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            playerStatus.Freeze();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
                playerStatus.UnFreeze();
            
        }
    }

    public void DesactivarArea()
    {
        this.gameObject.SetActive(false);
    }

    public void UnFreeze()
    {
        playerStatus.UnFreeze();
    }
}
