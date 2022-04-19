using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCangrejo : MonoBehaviour
{
    public HabilidadesEquipadas habilidades;
    public int id;
    //Variables NavMesh
    GameObject player;
    public NavMeshAgent agente;
    private bool onChase = false;

    //Variables para Habilidades
    public HabilidadesEquipadas ability;
    public bool activarGolpeTenazas = false;
    public bool activarGolpeSecuencia = false;
    public bool activarEmbestida = false;
    public bool activarMagneto = false;
    public bool activarPasiva = false;
    public GameObject EsferaMagnetica;
    public GameObject caparazon;
    //Animator
    Animator animCangrejo;


    //Cambio de color
    MeshRenderer mesh;

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    public GameObject embestidaCollider;

    //Variables Boss
    private GameObject cangrejo;
    public int vidaActual;
    public bool hitted;
    //CanvaUI
    public GameObject BarraDeVida;

    void Start()
    {
        
        vidaActual = 150;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

    }

    
    void Update()
    {
        

        if (onChase == true)
        {
            agente.SetDestination(player.transform.position);

        }

        if(activarMagneto == true)
        {
            StartCoroutine(HabilidadMagneto());
            activarMagneto = false;
        }

        if (activarGolpeTenazas == true)
        {
            Debug.Log("Activo GolpeTenaza");
            StartCoroutine(HabilidadGolpeTenaza());
            activarGolpeTenazas = false;
        }

        if (activarGolpeSecuencia == true)
        {
            Debug.Log("Activo Secuencia de golpes");
            StartCoroutine(HabilidadSecuenciaGolpes());
            activarGolpeSecuencia = false;
        }


            if (activarEmbestida == true)
        {
            Debug.Log("Activo Embestida");
            StartCoroutine(Embestida());
            activarEmbestida = false;
        }
        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("Crabby_Baked_Embestida_Accion"))
        {
            transform.LookAt(player.transform);
        }

        if (activarPasiva == true)
        {
            StartCoroutine(PasivaCaparazon());
            activarPasiva = false;
        }

        if(vidaActual <= 0)
        {
            StartCoroutine(MuerteCangrejo());
        }
     
    }

    private void StartChase(int id)
    {
        if(id == this.id)
        { 
        
        onChase = true;
        animCangrejo.SetTrigger("Comienzo");
        BarraDeVida.SetActive(true);
        }
    }
    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.red;
        agente.speed = 0;
       // GolpeTenazaCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
      //  GolpeTenazaCollider.SetActive(false);
        agente.speed = 5;
        cuboColor.material.color = Color.grey;
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        animCangrejo.SetTrigger("GolpeSecuencia");
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.blue;
        agente.speed = 2.8f;
       // brazoDerechoCollider.SetActive(true);
        //brazoIzquierdoCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
        agente.speed = 5;
        //brazoDerechoCollider.SetActive(false);
        //brazoIzquierdoCollider.SetActive(false);
        cuboColor.material.color = Color.grey;
    }

    IEnumerator Embestida()
    {
       // SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
       // cuboColor.material.color = Color.black;
        agente.speed = 0;
        yield return new WaitForSeconds(2f);
        embestidaCollider.SetActive(true);
        agente.speed = 1000;
        agente.acceleration = 70;
        
        yield return new WaitForSeconds(2);
        animCangrejo.SetTrigger("TerminarEmbestida");
        embestidaCollider.SetActive(false);
        agente.speed = 0;
        yield return new WaitForSeconds(2);
        animCangrejo.SetTrigger("Comienzo");
        //cuboColor.material.color = Color.grey;
        agente.speed = 5;
    }

    IEnumerator HabilidadMagneto()
    {
        agente.speed = 0;
        EsferaMagnetica.SetActive(true);
        animCangrejo.SetTrigger("CanalizarMag");
        yield return new WaitForSeconds(3.5f);
        EsferaMagnetica.SetActive(false);
        yield return new WaitForSeconds(2);
        agente.speed = 5.5f;
        
        

    }

    IEnumerator PasivaCaparazon()
    {
        caparazon.SetActive(true);
        yield return new WaitForSeconds(5f);
        caparazon.SetActive(false);
    }

    IEnumerator MuerteCangrejo()
    {
        animCangrejo.SetTrigger("Muerte");
        yield return new WaitForSeconds(1);
        habilidades.enabled = false;
        Destroy(this);
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

}
