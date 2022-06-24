using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCangrejo : MonoBehaviour
{
    public GameObject BossDoor;
    AnimationPlayer bossDoorScript;
    public HabilidadesEquipadas habilidades;
    public int id;
    //Variables NavMesh
    GameObject player;
    public NavMeshAgent agente;
    public bool onChase = false;

    //Variables para Habilidades
    public bool activarGolpeTenazas = false;
    public bool activarGolpeSecuencia = false;
    public bool activarEmbestida = false;
    public bool activarMagneto = false;
    public bool activarPasiva = false;
    public bool activarSenton = false;
    public GameObject EsferaMagnetica;
    public GameObject caparazonLuz;
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
    public GameObject abdomen;
    SphereCollider abdomenCollider;

    //Variables Boss
    private GameObject cangrejo;
    public float vidaActual;
    public bool hitted;
    public bool bossDeath;
    //CanvaUI
    public GameObject BarraDeVida;
    //
    public bool segundaFase;
    public GameObject EfectoSegundaFase;
    [SerializeField] float timerDeBuff;
    [SerializeField] bool activarBuff;
    float timerDescanso;
    bool activarDescanso;
    GameObject CabezaPlayer;
    Transform transformPlayer;
    public GameObject efectoLuces1;
    public GameObject efectoLuces2;
    public GameObject efectoFuego;
    public GameObject efectoDañoSuelo;
    public GameObject golpeSueloPointDerecho;
    public GameObject golpeSUeloPointIzquierdo;
    public GameObject ColliderSenton;
    private SphereCollider sphereColliderSenton;
    public GameObject SentonVFX;
    public AudioSource sonidoRecibirGolpe;
    void Start()
    {
        bossDoorScript = BossDoor.GetComponent<AnimationPlayer>();
        habilidades.enabled = false;
        abdomenCollider = abdomen.GetComponent<SphereCollider>();
        CabezaPlayer = GameObject.Find("PlayerHead");
        segundaFase = false;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        dañoPlayer = armaPlayer.GetComponent<DañoArmaCangrejo>();
        //transformPlayer = CabezaPlayer.GetComponent<Transform>();
        sonidoRecibirGolpe = GetComponent<AudioSource>();
        sphereColliderSenton = ColliderSenton.GetComponent<SphereCollider>();



    }

    
    void Update()
    {
        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("Iddle"))
        {
            MirarAljugador();
        }

        if (vidaActual <= 100)
        {
            //segundaFase = true;
        }

        if (vidaActual <= 430)
        {
            efectoLuces1.SetActive(true);
        }

        if(vidaActual <= 390)
        {
            efectoLuces1.SetActive(false);
            efectoLuces2.SetActive(true);
        }
        if(vidaActual <= 200)
        {
            efectoFuego.SetActive(true);
        }

        switch (segundaFase)
        {
            case false:
                PoderesPrimeraFase();
                break;

            case true:
                PoderesSegundaFase();
                habilidades.ability[3] = null;
                habilidades.ability[2] = null;
                break;
        }

       
    }
    #region PoderesPrimeraFase
    void PoderesPrimeraFase()
    {
        if (onChase == true)
        {
            agente.SetDestination(CabezaPlayer.transform.position);

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

        if (activarSenton == true)
        {
           
            StartCoroutine(HabilidadSenton());
            activarSenton = false;
        }

        if (activarEmbestida == true)
        {
            Debug.Log("Activo Embestida");
            StartCoroutine(Embestida());
            activarEmbestida = false;
        }
        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("Caminata"))
        {
            agente.speed = 5;
        }
        if (animCangrejo.GetCurrentAnimatorStateInfo(0).IsName("Activarse"))
        {
            agente.speed = 0;
            
        }


        if (vidaActual <= 0&&bossDeath==false)
        {

            FindObjectOfType<AudioManager>().Play("MuerteBoss");

            BuffManager.onBossDefetead?.Invoke();

            StartCoroutine(MuerteCangrejo());
            EfectoSegundaFase.SetActive(false);
            efectoFuego.SetActive(false);
            efectoLuces2.SetActive(false);

            bossDeath = true;
            bossDoorScript.PlayAnimation();

        }


        if (activarPasiva == true)
        {
            StartCoroutine(buffSegundaFase());
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
            agente.SetDestination(CabezaPlayer.transform.position);
        }
        
        if (activarMagneto == true)
        {
            StartCoroutine(HabilidadMagneto());
            activarMagneto = false;
        }

        if (activarGolpeTenazas == true)
        {
            StartCoroutine(HabilidadGolpeTenaza());
            activarGolpeTenazas = false;
        }
        
        if (activarGolpeSecuencia == true)
        {
            StartCoroutine(HabilidadSecuenciaGolpes());
            activarGolpeSecuencia = false;
        }
        
        if (vidaActual <= 0)
        {
            FindObjectOfType<AudioManager>().Play("MuerteBoss");
            StartCoroutine(MuerteCangrejo());
            EfectoSegundaFase.SetActive(false);
            efectoFuego.SetActive(false);
            efectoLuces2.SetActive(false);

            if (bossDeath == false)
            {
                BuffManager.Instance.ShowPanel();
            }
            

            bossDeath = true;
            bossDoorScript.PlayAnimation();
            


        }
    }
    #endregion
   
    private void StartChase(int id)
    {
        if(id == this.id)
        {
            /* FindObjectOfType<AudioManager>().Play("BossAparece");
             habilidades.enabled = true;
             onChase = true;
             animCangrejo.SetTrigger("Comienzo");
             BarraDeVida.SetActive(true);*/
            StartCoroutine(ComenzarBoss());
            
        }
    }
    #region CouroutinesHabilidades

    IEnumerator ComenzarBoss()
    {
        FindObjectOfType<AudioManager>().Play("BossMusic");
        animCangrejo.SetTrigger("Comienzo");
        BarraDeVida.SetActive(true);
        habilidades.enabled = true;
        yield return new WaitForSeconds(1);
        onChase = true;
      //  animCangrejo.SetTrigger("Comienzo");
        FindObjectOfType<AudioManager>().Play("BossAparece");
    }
    IEnumerator HabilidadGolpeTenaza()
    {
        animCangrejo.SetTrigger("GolpeTenaza");
        agente.speed = 0;
        yield return new WaitForSeconds(4);
        agente.speed = 5;
    }

    IEnumerator HabilidadSecuenciaGolpes()
    {
        animCangrejo.SetTrigger("GolpeSecuencia");
        agente.speed = 0;
        yield return new WaitForSeconds(4f);
        agente.speed = 5;
    }

    IEnumerator HabilidadSenton()
    {
        Debug.Log("HabilidadSenton");
        agente.speed = 0;
        yield return new WaitForSeconds(2.10f);
        agente.speed = 4;
    }

    IEnumerator Embestida()
    {
        agente.speed = 0;
        animCangrejo.SetTrigger("Embestida");
     // agente.speed = 0;
       // agente.stoppingDistance = 0;
        abdomenCollider.isTrigger = true;
      yield return new WaitForSeconds(3);
        
        embestidaCollider.SetActive(true);
        agente.speed = 1000;
        agente.acceleration = 70;
        
        yield return new WaitForSeconds(1.5f);
      //animCangrejo.SetTrigger("TerminarEmbestida");
        embestidaCollider.SetActive(false);
        agente.speed = 0;
        abdomenCollider.isTrigger = false;
        agente.speed = 5;
    }

    IEnumerator HabilidadMagneto()
    {
        animCangrejo.SetTrigger("CanalizarMag");
        agente.speed = 0;
        yield return new WaitForSeconds(2.9f);
        yield return new WaitForSeconds(2);
        agente.speed = 5.5f;

    }

  

    IEnumerator MuerteCangrejo()
    {
        FindObjectOfType<AudioManager>().Stop("BossMusic");
        animCangrejo.SetTrigger("Muerte");
        EsferaMagnetica.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        habilidades.enabled = false;
        Destroy(this);
    }
    #endregion
    IEnumerator buffSegundaFase()
    {
        caparazonLuz.SetActive(true);
        dañoPlayer.dañoDeArma = 3;
        yield return new WaitForSeconds(14f); 
        dañoPlayer.dañoDeArma = 7;
        caparazonLuz.SetActive(false);


    }
    private void TimerDeBuff()
    {
        timerDeBuff += Time.deltaTime;

        if (timerDeBuff <= 14)
        {
            activarBuff = true;
            EfectoSegundaFase.SetActive(true);
            caparazonLuz.SetActive(true);
        }
        else
        {
            EfectoSegundaFase.SetActive(false);
            StartCoroutine(DescansoDespuesdeBuff());
            caparazonLuz.SetActive(false);
        }

    }

    IEnumerator DescansoDespuesdeBuff()
    {
        activarBuff = false;
        yield return new WaitForSeconds(4);
        timerDeBuff = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           // MirarAljugador();
        }
    }
    

    void MirarAljugador()
    {
        
       // Vector3 relativePos = CabezaPlayer.transform.position - transform.position;

        Quaternion rotTarget = Quaternion.LookRotation(CabezaPlayer.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, 200 * Time.deltaTime);
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up * -0.1f*Time.deltaTime);
       // transform.rotation = rotation;
        Debug.Log("Deberia GIRAAAAAAAR");
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
      /*  if(segundaFase == true)
        {
            ActivarAtraccionDerecho();
        }*/
    }

    public void ActivarAtraccionDerecho()
    {
        
        
            cuboAtraccionDerecho.SetActive(true);
        
    }

    public void DesactivarAtraccionDerecho()
    {
       
        
            cuboAtraccionDerecho.SetActive(false);
        
        
    }
    public void ActivarAtraccionIzquierdo()
    {
      
            cuboAtraccionIzquierdo.SetActive(true);
      
        
    }
    public void DesactivarAtraccionIzquierdo()
    {
        if (segundaFase == true)
        {
            cuboAtraccionIzquierdo.SetActive(false);
        }
        
    }

    public void ActivarEsferaMagneticaSegundaFase()
    {
        if (segundaFase == true)
        {
            EsferaMagnetica.SetActive(true);
        }
    }

    public void ActivarEsferaMagnetica()
    {
       
        EsferaMagnetica.SetActive(true);
        
    }

    public void InstanciarDañoSueloDerecho()
    {
         GameObject dañoSuelo;

            dañoSuelo = Instantiate(efectoDañoSuelo, golpeSueloPointDerecho.transform.position, Quaternion.identity);
        dañoSuelo.transform.localRotation = this.gameObject.transform.rotation;
    }

    public void InstanciarDañoSueloIzquierdo()
    {
        GameObject dañoSuelo;

        dañoSuelo = Instantiate(efectoDañoSuelo, golpeSUeloPointIzquierdo.transform.position, Quaternion.identity);
        dañoSuelo.transform.localRotation = this.gameObject.transform.rotation;
    }

    public void ActivarSentonEvent()
    {
        sphereColliderSenton.enabled = true;
        SentonVFX.SetActive(true);
        
    }
    public void DesactivarSentonEvent()
    {
        sphereColliderSenton.enabled = false;
        
    }

    public void DesactivarSentonVFX()
    {
        SentonVFX.SetActive(false);
    }
}
