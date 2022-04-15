using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CangrejoArena : MonoBehaviour
{
    //Variables NavMesh
    GameObject player;

    //Variables para Habilidades
    //public HabilidadesEquipadas ability;
    public bool activarGolpeTenazas = false;
    public bool activarGolpeSecuencia = false;
    public bool activarEmbestida = false;

    public float health;
    //Animator
    Animator animCangrejo;


    //Cambio de color
 

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    public GameObject embestidaCollider;
    public GameObject EsferaMagnetica;

    //
    public NavMeshAgent agente;
    bool perseguir;
    private GameObject cangrejo;

    public bool hitted;

    void Start()
    {
        
        health = 150;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        
        player = GameObject.FindWithTag("Player");
        agente = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {

            perseguir = true;
            animCangrejo.SetTrigger("Caminar");

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PararPersecucion();
            perseguir = false;
            
        }
        if (perseguir == true)
        {
            EmpezarPersecucion();
        }



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                ActivarGolpeTenaza();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
                ActivarGolpeSecuencia();

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
                ActivarEmbestida();

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
                ActivarMagneto();

        }
        
    }

    void EmpezarPersecucion()
    {
        agente.SetDestination(player.transform.position);
        
        agente.speed = 3.5f;
        
    }
    void PararPersecucion()
    {
        animCangrejo.SetTrigger("PararCaminata");
        agente.speed = 0;
    }

    void ActivarGolpeTenaza()
    {
        StartCoroutine(HabilidadGolpeTenaza());
    }

    void ActivarGolpeSecuencia()
    {
        StartCoroutine(HabilidadSecuenciaGolpes());
    }

    void ActivarEmbestida()
    {
        StartCoroutine(Embestida());
    }

    void ActivarMagneto()
    {
        StartCoroutine(Magnetizar());
    }

    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        GolpeTenazaCollider.SetActive(true);
        yield return new WaitForSeconds(3);
        GolpeTenazaCollider.SetActive(false);
      
       
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        animCangrejo.SetTrigger("GolpeSecuencia");
        brazoDerechoCollider.SetActive(true);
        brazoIzquierdoCollider.SetActive(true);
        yield return new WaitForSeconds(3);  
        brazoDerechoCollider.SetActive(false);
        brazoIzquierdoCollider.SetActive(false);
    }

    IEnumerator Embestida()
    {
        animCangrejo.SetTrigger("Embestida");
        yield return new WaitForSeconds(1);
        agente.SetDestination(player.transform.position);
        agente.speed = 1000;
        agente.acceleration = 70;
        embestidaCollider.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        animCangrejo.SetTrigger("FinEmbestida");
        agente.speed = 0;
        agente.acceleration = 8;
        embestidaCollider.SetActive(false);
       
       
    }

    IEnumerator Magnetizar()
    {
        animCangrejo.SetTrigger("CanalizarMag");
        EsferaMagnetica.SetActive(true);
        yield return new WaitForSeconds(3.8f);
        EsferaMagnetica.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            hitted = true;
        }
    }
}
