using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCangrejo : MonoBehaviour
{
    //Variables NavMesh
    GameObject player;
    public NavMeshAgent agente;
    private bool onChase = false;

    //Variables para Habilidades
    public HabilidadesEquipadas ability;
    public bool activarGolpeTenazas = false;
    public bool activarGolpeSecuencia = false;
    public bool activarEmbestida = false;

    //Animator
    Animator animCangrejo;


    //Cambio de color
    MeshRenderer mesh;

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;

    //
    private GameObject cangrejo;

    void Start()
    {
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


        /*if (activarGolpeSecuencia == true)
        {
            if(ability.activeTime > 0)
            {
                Debug.Log("La habilidad sigue activada");
            }
            if (ability.activeTime <= 0)
            {
                Debug.Log("Se termino la habilidad");
                activarGolpeSecuencia = false;
            }
            
            
        }*/
    }

    private void StartChase()
    {
        onChase = true;
        animCangrejo.SetTrigger("Comienzo");
    }
    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.red;
        agente.speed = 0;
        GolpeTenazaCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
        GolpeTenazaCollider.SetActive(false);
        agente.speed = 5;
        cuboColor.material.color = Color.grey;
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.blue;
        agente.speed = 0;
        //brazoDerechoCollider.SetActive(true);
        //brazoIzquierdoCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
        agente.speed = 5;
        //brazoDerechoCollider.SetActive(false);
        //brazoIzquierdoCollider.SetActive(false);
        cuboColor.material.color = Color.grey;
    }

    IEnumerator Embestida()
    {
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.black;
        agente.speed = 0;
        yield return new WaitForSeconds(4);
        //anim.SetBool("Embestir", false);
        agente.speed = 1000000;
        agente.acceleration = 100;
        yield return new WaitForSeconds(2);
        agente.speed = 0;
        yield return new WaitForSeconds(2);
        cuboColor.material.color = Color.grey;
        agente.speed = 5;
    }
}
