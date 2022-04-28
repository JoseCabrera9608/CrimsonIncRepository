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
         if(chocoLimite == true)
         {
            barrera.SetActive(false);
            DetenerElevador();
         }
         if(accionar == true && puedeSubir == false && puedeSubir == false)
         {
             BajarElevadorMetodo();
            
         }



    }

    IEnumerator SubirElevador()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return new WaitForSeconds(1);
        puedeSubir = true;
    }

    IEnumerator BajarElevador()
    {
        transform.Translate(Vector3.up * -velocidad * Time.deltaTime);
        yield return new WaitForSeconds(1);
        puedeBajar = true;
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
