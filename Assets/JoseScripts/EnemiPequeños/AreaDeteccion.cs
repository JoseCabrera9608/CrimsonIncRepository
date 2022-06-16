using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaDeteccion : MonoBehaviour
{
    EnemiPequeñoControlador sondaScript;
    
    
    void Start()
    {
        sondaScript = GetComponentInParent<EnemiPequeñoControlador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sondaScript.onChase = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sondaScript.onChase = false;
            StartCoroutine(DelayDeRetroceso());
           // sondaScript.agente.SetDestination(sondaScript.startPosition);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sondaScript.anim.SetTrigger("Comenzar");
            

        }
        
    }

    IEnumerator DelayDeRetroceso()
    {
        yield return new WaitForSeconds(3);
        sondaScript.agente.SetDestination(sondaScript.startPosition);
    }

}
