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
    public Animator anim;
    public GameObject interruptor;
    Interruptor interruptorScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        interruptorScript = interruptor.GetComponent<Interruptor>();
    }

    void Update()
    {

        if (accionar == true && puedeSubir == true)
         {
            barrera.SetActive(true);
            // anim.SetTrigger("SubirBaranda");
            interruptorScript.DesactivarCollider();
           
            SubirElevadorMetodo();

            
             

         }

        if (accionar == true && puedeSubir == false)
        {
            // anim.SetTrigger("SubirBaranda");
            barrera.SetActive(true);
            
            BajarElevadorMetodo();
            interruptorScript.DesactivarCollider();



        }
        if (chocoLimite == true)
         {
            
            DetenerElevador();
            
        }




    }

    IEnumerator SubirElevador()
    {
        anim.SetBool("Baranda", true);
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return new WaitForSeconds(1);
        
    }

    IEnumerator BajarElevador()
    {
        anim.SetBool("Baranda", true);
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
           // anim.SetTrigger("BajarBaranda");
            anim.SetBool("Baranda",false);
            interruptorScript.ActivarCollider();
            FindObjectOfType<AudioManager>().Stop("Elevador");
            //Debug.Log("GAAA");
        }
        if (other.gameObject.CompareTag("Nube"))
        {
            //Debug.Log("GAAA");
            accionar = false;
            puedeSubir = true;
            //Debug.Log("GAAA");
            barrera.SetActive(false);
            anim.SetBool("Baranda", false);
            interruptorScript.ActivarCollider();
            FindObjectOfType<AudioManager>().Stop("Elevador");
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
