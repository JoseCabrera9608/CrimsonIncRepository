using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CangrejoArena : MonoBehaviour
{

    WeaponDamagePlayer playerDamage;
    public GameObject armaPlayer;
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

   
   /* DañoArmaCangrejo dañoPlayer;
    public GameObject armaPlayer;*/

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
    public bool enUso;
    public bool hitted;
    SkinnedMeshRenderer mesh;
    public GameObject meshCangrejo;
    public GameObject efectoLuces1;
    public GameObject efectoLuces2;
    public GameObject efectoFuego;
    public ParticleSystem PS_efectoLuces1;
    public GameObject healthBar;
    void Start()
    {
        //EnemyHealthBar.SetActive(true);
        segundaFase = false;
       //ealth = 150;
        cangrejo = GameObject.Find("Crabby");
        animCangrejo = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agente = GetComponent<NavMeshAgent>();
        playerDamage = armaPlayer.GetComponent<WeaponDamagePlayer>();
        mesh = meshCangrejo.GetComponent<SkinnedMeshRenderer>();
        enUso = false;
    }


    void Update()
    {
        
        switch(enUso)
        {
            case true:
                healthBar.SetActive(true);
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

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    ActivarPasiva();
                }

              /*  if (playerDamage.hitted == true)
                {
                    RecibioDaño();
                    playerDamage.hitted = false;
                }*/
                break;

            case false:
                healthBar.SetActive(false);
                break;
                
        }

        if (playerDamage.hitted == true)
        {
            RecibioDaño();
            playerDamage.hitted = false;
        }
       
       
        if(vidaActual <= 0)
        {
            animCangrejo.SetTrigger("Muerte");
            Destroy(efectoFuego);
            Destroy(efectoLuces2);
            Destroy(healthBar);
            Destroy(this);

            EfectoSegundaFase.SetActive(false);
        }
        if(vidaActual<= 140 && vidaActual >=130)
        {
            //efectoLuces1.SetActive(true);
            PS_efectoLuces1.Play();
        }

        if (vidaActual <= 120 && vidaActual>=110)
        {
            PS_efectoLuces1.Play();
        }

        if (vidaActual <= 90)
        {
            efectoLuces1.SetActive(false);
            efectoLuces2.SetActive(true);
        }
        if (vidaActual <= 60)
        {
            efectoFuego.SetActive(true);
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
        playerDamage.dañoDeArma = 3;
        yield return new WaitForSeconds(14f);
        playerDamage.dañoDeArma = 7;
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
        agente.speed = 0;
        yield return new WaitForSeconds(1.1f);
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
        //EsferaMagnetica.SetActive(true);
        yield return new WaitForSeconds(3.8f);
       // EsferaMagnetica.SetActive(false);
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            hitted = true;
        }
    }*/

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


    public void ActivarAtraccionDerecho()
    {
       
            cuboAtraccionDerecho.SetActive(true);
    }

   
    public void ActivarAtraccionIzquierdo()
    {
        
            cuboAtraccionIzquierdo.SetActive(true);
    }
    

    public void ActivarEsferaMagnetica()
    {
       
        
            EsferaMagnetica.SetActive(true);
        
    }
    public void ActivarEsferaMagneticaSegundaFase()
    {
        if (segundaFase == true)
        {
            EsferaMagnetica.SetActive(true);
        }
    }

    void RecibioDaño()
    {

        vidaActual -= playerDamage.dañoDeArma;
        StartCoroutine(CambioColor());

    }
    IEnumerator CambioColor()
    {
        mesh.materials[0].color = Color.red;
        mesh.materials[1].color = Color.red;
        mesh.materials[3].color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mesh.materials[0].color = Color.grey;
        mesh.materials[1].color = Color.grey;
        mesh.materials[3].color = Color.grey;
    }

    


}
