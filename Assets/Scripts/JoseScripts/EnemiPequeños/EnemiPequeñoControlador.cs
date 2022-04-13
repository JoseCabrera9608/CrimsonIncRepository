using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiPequeñoControlador : MonoBehaviour
{
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
    public BoxCollider GolpeLargoBoxCollider;
    //Variables Boss
    public float healthEnemigo;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        player = GameObject.FindWithTag("Player");
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

        if(golpeLargo == true)
        {
            StartCoroutine(GolpeLargoActivate());

        }

        if (golpeMelee == true)
        {
            StartCoroutine(GolpeMeleeActivate());
        }
    }

    private void StartChase()
    {
        onChase = true;

    }


    IEnumerator GolpeLargoActivate()
    {
        anim.SetTrigger("GolpeLargo");
        agente.speed = 0;
        GolpeLargoBoxCollider.enabled = true;
        yield return new WaitForSeconds(2);
        agente.speed = 2;
        GolpeLargoBoxCollider.enabled = false;
        golpeLargo = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            healthEnemigo -= 10;
        }
    }

    IEnumerator GolpeMeleeActivate()
    {
        anim.SetTrigger("GolpeMelee");
        GolpeLargoBoxCollider.enabled = true;
        BrazoDerechoCollider.enabled = true;
        agente.speed = 1.7f;
        yield return new WaitForSeconds(3);
        GolpeLargoBoxCollider.enabled = false;
        BrazoDerechoCollider.enabled = false;
        golpeMelee = false;

    }
}

