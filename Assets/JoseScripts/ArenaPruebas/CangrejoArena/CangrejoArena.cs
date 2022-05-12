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

  //public int health;
    public int vidaActual;
    //Animator
    Animator animCangrejo;
    public bool segundaFase;
    public GameObject cuboAtraccionDerecho;
    public GameObject cuboAtraccionIzquierdo;
    public GameObject caparazonLuz;
    public GameObject EfectoSegundaFase;

   
    DañoArmaCangrejo dañoPlayer;
    public GameObject armaPlayer;

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    public GameObject embestidaCollider;
    public GameObject EsferaMagnetica;
    //public GameObject EnemyHealthBar;

    //
    public NavMeshAgent agente;
    bool perseguir;
    private GameObject cangrejo;

    public bool hitted;

    void Start()
    {
        //EnemyHealthBar.SetActive(true);
        segundaFase = false;
       //ealth = 150;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agente = GetComponent<NavMeshAgent>();
        dañoPlayer = armaPlayer.GetComponent<DañoArmaCangrejo>();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {

            perseguir = true;
            animCangrejo.SetTrigger("Caminar");

        }
        if (Input.GetKeyDown(KeyCode.P))
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            segundaFase = true;
            EfectoSegundaFase.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivarPasiva();
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
    void ActivarPasiva()
    {
        StartCoroutine(Pasiva());
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

    IEnumerator Pasiva()
    {
        caparazonLuz.SetActive(true);
        dañoPlayer.dañoDeArma = 3;
        yield return new WaitForSeconds(14f);
        dañoPlayer.dañoDeArma = 7;
        caparazonLuz.SetActive(false);
    }
    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        //GolpeTenazaCollider.SetActive(true);
        yield return new WaitForSeconds(3);
        //GolpeTenazaCollider.SetActive(false);
      
       
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

    public void ActivarColliderBrazoDerecho()
    {
        brazoDerechoCollider.SetActive(true);
    }

    public void ActivarColliderBrazoIzquierdo()
    {
        brazoIzquierdoCollider.SetActive(true);
    }

    public void DesactivarColliderBrazoDercho()
    {
        brazoDerechoCollider.SetActive(false);
    }

    public void DesactivarColliderBrazoIzquierdo()
    {
        brazoIzquierdoCollider.SetActive(false);
    }

    public void ActivarMagnetoEnGolpe()
    {
        if (segundaFase == true)
        {
            ActivarAtraccionDerecho();
        }
    }

    public void ActivarAtraccionDerecho()
    {
        if (segundaFase == true)
        {
            cuboAtraccionDerecho.SetActive(true);
        }
    }

    public void DesactivarAtraccionDerecho()
    {
        if (segundaFase == true)
        {
            cuboAtraccionDerecho.SetActive(false);
        }

    }
    public void ActivarAtraccionIzquierdo()
    {
        if (segundaFase == true)
        {
            cuboAtraccionIzquierdo.SetActive(true);
        }

    }
    public void DesactivarAtraccionIzquierdo()
    {
        if (segundaFase == true)
        {
            cuboAtraccionIzquierdo.SetActive(false);
        }

    }

    public void ActivarEsferaMagnetica()
    {
        if (segundaFase == true)
        {
            EsferaMagnetica.SetActive(true);
        }
    }
 
}
