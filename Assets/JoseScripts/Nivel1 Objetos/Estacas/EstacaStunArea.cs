using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacaStunArea : MonoBehaviour
{
    GameObject player;
    Movement movimientoPlayer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movimientoPlayer = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
               movimientoPlayer.runSpeed = 0;
               movimientoPlayer.walkSpeed = 0;
               movimientoPlayer.dashspeed = 0;
               movimientoPlayer.rotationSpeed = 0;
            //movimientoPlayer.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             movimientoPlayer.runSpeed = 9;
             movimientoPlayer.walkSpeed = 9;
             movimientoPlayer.dashspeed = 15;
            //movimientoPlayer.enabled = true;
            movimientoPlayer.rotationSpeed = 10;
            
        }
    }

    public void DesactivarArea()
    {
        this.gameObject.SetActive(false);
    }
}
