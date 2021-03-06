using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    ElevadorManyado _elevadorManyado;
    public GameObject elevador;
    public bool colliding;
    public BoxCollider colliderTrigger;
    void Start()
    {
        _elevadorManyado = elevador.GetComponent<ElevadorManyado>();
        colliderTrigger = this.gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact") && colliding == true)
        {
            _elevadorManyado.accionar = true;
            
            //FindObjectOfType<AudioManager>().Play("Elevador");
            colliding = false;
            DesactivarCollider();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            colliding = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            colliding = false;
        }
    }

    public void DesactivarCollider()
    {
        colliderTrigger.enabled = false;
    }

    public void ActivarCollider()
    {
        colliderTrigger.enabled = true;
    }
}
