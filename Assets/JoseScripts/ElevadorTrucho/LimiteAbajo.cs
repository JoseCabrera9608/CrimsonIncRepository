using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteAbajo : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _elevadorManyado.puedeSubir = true;
            _elevadorManyado.chocoLimite = true;
        
        }
    }


}
