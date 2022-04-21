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
    DañoArmaCangrejo dañoPlayer;
    public GameObject armaPlayer;

    //Cambio de color
    MeshRenderer mesh;

    //Colliders
    public GameObject GolpeTenazaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    public GameObject embestidaCollider;
    public GameObject cuboAtraccionDerecho;
    public GameObject cuboAtraccionIzquierdo;

    //Variables Boss
    private GameObject cangrejo;
    public int vidaActual;
    public bool hitted;
    //CanvaUI
    public GameObject BarraDeVida;
    //
    public bool segundaFase;
    public GameObject EfectoSegundaFase;
    [SerializeField] float timerDeBuff;
    [SerializeField] bool activarBuff;
    float timerDescanso;
    bool activarDescanso;

    void Start()
    {
        segundaFase = false;
        vidaActual = 150;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        dañoPlayer = armaPlayer.GetComponent<DañoArmaCangrejo>();

    }

    
    void Update()
    {
        if(vidaActual <= 50)
        {
            segundaFase = true;
        }
        switch (segundaFase)
        {
            case false:
                PoderesPrimeraFase();
                break;

            case true:
                PoderesSegundaFase();
                break;
        }

       
    }
    #region PoderesPrimeraFase
    void PoderesPrimeraFase()
    {
        if (onChase == true)
        {
            agente.SetDestination(player.transform.position);

        }

        if (activarMagneto == true)
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
        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("PreparacionEmbestida"))
        {
            transform.LookAt(player.transform);
        }

        if (activarPasiva == true)
        {
            StartCoroutine(PasivaCaparazon());
            activarPasiva = false;
        }
    }
    #endregion

    #region PoderesSegundaFase
    void PoderesSegundaFase()
    {
        TimerDeBuff();
        if(activarBuff == true)
        {
            StartCoroutine(buffSegundaFase());
            activarBuff = false;
        }
        if (onChase == true)
        {
            agente.SetDestination(player.transform.position);
        }
        /*
        if (activarMagneto == true)
        {
            StartCoroutine(HabilidadMagneto());
            activarMagneto = false;
        }

        */if (activarGolpeTenazas == true)
        {
            StartCoroutine(HabilidadGolpeTenaza());
            activarGolpeTenazas = false;
        }
        /*
        if (activarGolpeSecuencia == true)
        {
            StartCoroutine(HabilidadSecuenciaGolpes());
            activarGolpeSecuencia = false;
        }

        if (activarEmbestida == true)
        {
            StartCoroutine(Embestida());
            activarEmbestida = false;
        }

        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("PreparacionEmbestida"))
        {
            transform.LookAt(player.transform);
        }

        if (activarPasiva == true)
        {
            StartCoroutine(PasivaCaparazon());
            activarPasiva = false;
        }

        if (vidaActual <= 0)
        {
            StartCoroutine(MuerteCangrejo());
        }*/
    }
    #endregion
    private void StartChase(int id)
    {
        if(id == this.id)
        { 
        
        onChase = true;
        animCangrejo.SetTrigger("Comienzo");
        BarraDeVida.SetActive(true);
        }
    }
    #region CouroutinesHabilidades
    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        SkinnedMeshRenderer cuboColor = cangrejo.gameObject.GetComponent<SkinnedMeshRenderer>();
        cuboColor.material.color = Color.red;
        agente.speed = 0;
        yield return new WaitForSeconds(ability.activeTime);
        agente.speed = 5;
        cuboColor.material.color = Color.grey;
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        animCangrejo.SetTrigger("GolpeSecuencia");
        agente.speed = 2.8f;
        yield return new WaitForSeconds(ability.activeTime);
        agente.speed = 5;
    }

    IEnumerator Embestida()
    {
        agente.speed = 0;
        yield return new WaitForSeconds(3);
        animCangrejo.SetTrigger("EmpezarEmbestida");
        embestidaCollider.SetActive(true);
        agente.speed = 1000;
        agente.acceleration = 70;
        
        yield return new WaitForSeconds(2);
        animCangrejo.SetTrigger("TerminarEmbestida");
        embestidaCollider.SetActive(false);
        agente.speed = 0;
        yield return new WaitForSeconds(2);
        animCangrejo.SetTrigger("Comienzo");
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
        yield return new WaitForSeconds(0.5f);
        habilidades.enabled = false;
        Destroy(this);
    }
    #endregion
    IEnumerator buffSegundaFase()
    {
        //animCangrejo.SetTrigger("Buffearse");
        dañoPlayer.dañoDeArma = 3;
        yield return new WaitForSeconds(14f); 
        dañoPlayer.dañoDeArma = 7; 


    }
    private void TimerDeBuff()
    {
        timerDeBuff += Time.deltaTime;

        if (timerDeBuff <= 14)
        {
            activarBuff = true;
            EfectoSegundaFase.SetActive(true);
        }
        else
        {
            EfectoSegundaFase.SetActive(false);
            StartCoroutine(DescansoDespuesdeBuff());
            
        }

    }

    IEnumerator DescansoDespuesdeBuff()
    {
        activarBuff = false;
        yield return new WaitForSeconds(4);
        timerDeBuff = 0;
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
        if(segundaFase == true)
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
    
}
