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

    void FixedUpdate()
    {

        if (accionar == true && puedeSubir == true)
         {
            //barrera.SetActive(true);
            // anim.SetTrigger("SubirBaranda");
            //interruptorScript.DesactivarCollider();
            SubirElevadorMetodo();
         }

        if (accionar == true && puedeSubir == false)
        {
            // anim.SetTrigger("SubirBaranda");
            //barrera.SetActive(true);
            
            BajarElevadorMetodo();
            //interruptorScript.DesactivarCollider();
        }

        if (chocoLimite == true)
        {

            velocidad = 0;
            
        }

    }

    IEnumerator SubirElevador()
    {
        barrera.SetActive(true);
        anim.SetBool("Baranda", true);
        yield return new WaitForSeconds(1.1f);
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return null;
        
    }

    IEnumerator BajarElevador()
    {
        anim.SetBool("Baranda", true);
        transform.Translate(Vector3.up * -velocidad * Time.deltaTime);
        yield return null;
        
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
    IEnumerator bajarBaranda()
    {
        anim.SetBool("Baranda", false);
        yield return new WaitForSeconds(2f);
        barrera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Limite"))
        {
            
            accionar = false;
            puedeSubir = false;
            chocoLimite = true;
            StartCoroutine(bajarBaranda());
            FindObjectOfType<AudioManager>().Stop("Elevador");
           
        }
        if (other.gameObject.CompareTag("Nube"))
        {
            
            accionar = false;
            puedeSubir = true;
            StartCoroutine(bajarBaranda());
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
