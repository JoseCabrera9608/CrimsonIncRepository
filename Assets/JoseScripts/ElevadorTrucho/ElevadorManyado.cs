using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorManyado : MonoBehaviour
{
    public float velocidad;
    public bool puedeSubir;
    public bool puedeBajar;
    public bool accionar;
    public GameObject barrera;
    public bool chocoLimite;
    public bool chocoLimiteAbajo;
    
 
    void Update()
    {

        if (accionar == true && puedeSubir == true)
         {
             barrera.SetActive(true);
             SubirElevadorMetodo();
            
             

         }

        if (accionar == true && puedeSubir == false)
        {
            barrera.SetActive(true);
            BajarElevadorMetodo();



        }
        if (chocoLimite == true)
         {
            barrera.SetActive(false);
            DetenerElevador();
         }




    }

    IEnumerator SubirElevador()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return new WaitForSeconds(1);
        
    }

    IEnumerator BajarElevador()
    {
        transform.Translate(Vector3.up * -velocidad * Time.deltaTime);
        yield return new WaitForSeconds(1);
        
    }

   public void SubirElevadorMetodo()
    {
        StartCoroutine(SubirElevador());
    }
    public void DetenerElevador()
    {

        StopAllCoroutines();
    }
    public void BajarElevadorMetodo()
    {
        StartCoroutine(BajarElevador());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Limite"))
        {
            //Debug.Log("GAAA");
            accionar = false;
            puedeSubir = false;
            barrera.SetActive(false);

            //Debug.Log("GAAA");
        }
        if (other.gameObject.CompareTag("Nube"))
        {
            //Debug.Log("GAAA");
            accionar = false;
            puedeSubir = true;
            //Debug.Log("GAAA");
            barrera.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
