using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLimite : MonoBehaviour
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Diosito");
            _elevadorManyado.chocoLimite = true;
           // _elevadorManyado.DetenerElevador();
        }
    }

    
}
