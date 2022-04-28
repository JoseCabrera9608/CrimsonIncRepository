using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    ElevadorManyado _elevadorManyado;
    public GameObject elevador;
    public bool colliding;
    void Start()
    {
        _elevadorManyado = elevador.GetComponent<ElevadorManyado>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E ) && colliding == true)
        {
            _elevadorManyado.accionar = true;
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
}
