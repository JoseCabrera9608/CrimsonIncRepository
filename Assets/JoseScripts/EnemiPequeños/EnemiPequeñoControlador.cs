using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiPequeñoControlador : MonoBehaviour
{
    public int id;
    GameObject player;
    public NavMeshAgent agente;
    public Animator anim;
    //Habilidades
    public bool golpeLargo;
    public bool golpeMelee;

    //Comienzo
    public bool onChase;
    //Colliders
    //public GameObject GolpeLargoCollider;
    public BoxCollider BrazoDerechoCollider;
    //public BoxCollider GolpeLargoBoxCollider;
    //Variables Boss
    public float healthEnemigo;
    bool hitted;
    SkinnedMeshRenderer meshDelEnemigo;
    public GameObject meshObject;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        player = GameObject.FindWithTag("Player");
        meshDelEnemigo = meshObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onChase == true)
        {
            
            agente.SetDestination(player.transform.position);

        }

        if(healthEnemigo <= 0)
        {
            Destroy(this.gameObject);
        }

     /*   if(golpeLargo == true)
        {
            StartCoroutine(GolpeLargoActivate());

        }*/

        if (golpeMelee == true)
        {
            StartCoroutine(GolpeMeleeActivate());
        }

        if(healthEnemigo <= 0)
        {
            StartCoroutine(Muerte());
        }
        
    }

    private void StartChase(int id)
    {
        if (id == this.id)
        {
            onChase = true;
        }
    }

    IEnumerator Muerte()
    {
        anim.SetTrigger("Muerte");
        yield return new WaitForSeconds(0.5f);
        Destroy(this);
    }

    /* IEnumerator GolpeLargoActivate()
     {
         anim.SetTrigger("GolpeLargo");
         agente.speed = 0;
         //GolpeLargoBoxCollider.enabled = true;
         yield return new WaitForSeconds(2);
         agente.speed = 2;
         //GolpeLargoBoxCollider.enabled = false;
         golpeLargo = false;
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            healthEnemigo -= 40;
            hitted = true;
            StartCoroutine(CambioDeColor());
            
        }
    }
    IEnumerator CambioDeColor()
    {
        meshDelEnemigo.material.color = Color.red;
        yield return new WaitForSeconds(1);
        meshDelEnemigo.material.color = Color.white;
    }
    IEnumerator GolpeMeleeActivate()
    {
        anim.SetTrigger("Golpe");
       // BrazoDerechoCollider.enabled = true;
        agente.speed = 1.7f;
        yield return new WaitForSeconds(3);
       // BrazoDerechoCollider.enabled = false;
        golpeMelee = false;

    }

    public void ActivarColliderDerecho()
    {
        BrazoDerechoCollider.enabled = true;
    }

    public void DesactivarColliderDerecho()
    {
        BrazoDerechoCollider.enabled = false;
    }
    public void ActivarColliderIzquierdo()
    {
        //GolpeLargoBoxCollider.enabled = true;
    }
    public void DesactivarColliderIzquierdo()
    {
        //GolpeLargoBoxCollider.enabled = false;
    }
}

