using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    ElevadorManyado _elevadorManyado;
    public GameObject elevador;
    void Start()
    {
        _elevadorManyado = elevador.GetComponent<ElevadorManyado>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _elevadorManyado.accionar = true;
            }
        }
    }
}
