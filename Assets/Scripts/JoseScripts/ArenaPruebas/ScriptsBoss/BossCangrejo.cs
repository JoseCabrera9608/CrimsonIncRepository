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

    //Animator
    Animator anim;


    //Cambio de color
    MeshRenderer mesh;

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;

    void Start()
    {

        anim = GetComponent<Animator>();
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
            activarGolpeSecuencia = false;
            StartCoroutine(HabilidadSecuenciaGolpes());
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
        //animCangrejo.SetTrigger("Inicio");
    }
    IEnumerator HabilidadGolpeTenaza()
    {
        SkinnedMeshRenderer cuboColor = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.red;
        agente.speed = 0;
        GolpeTenazaCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
        GolpeTenazaCollider.SetActive(false);
        agente.speed = 6;
        cuboColor.material.color = Color.grey;
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        SkinnedMeshRenderer cuboColor = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.blue;
        agente.speed = 0;
        brazoDerechoCollider.SetActive(true);
        brazoIzquierdoCollider.SetActive(true);
        yield return new WaitForSeconds(ability.activeTime);
        agente.speed = 6;
        brazoDerechoCollider.SetActive(false);
        brazoIzquierdoCollider.SetActive(false);
        cuboColor.material.color = Color.grey;
    }
}
