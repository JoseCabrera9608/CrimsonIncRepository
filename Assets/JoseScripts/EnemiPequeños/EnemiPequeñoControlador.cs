using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiPequeñoControlador : MonoBehaviour
{
    HabilidadesEquipadas tiemposHabilidades;
    public int id;
    GameObject player;
    public NavMeshAgent agente;
    public Animator anim;
    //Habilidades
    public bool disparar;
    public bool golpeMelee;
    public bool magnetizar;
    public bool kamikaze;
    //Particulas
    public GameObject fuego;
    SphereCollider colliderCuerpo;


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
    public GameObject magneto;

    public GameObject disparo;
    public GameObject firePoint;
    public GameObject healthBar;
    public GameObject explosion;

    HabilidadesEquipadas _habilidadesEquipadas;

    void Start()
    {
        tiemposHabilidades = GetComponent<HabilidadesEquipadas>();
        anim = GetComponent<Animator>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        player = GameObject.FindWithTag("Player");
        meshDelEnemigo = meshObject.GetComponent<SkinnedMeshRenderer>();
        _habilidadesEquipadas = this.gameObject.GetComponent<HabilidadesEquipadas>();
        colliderCuerpo = this.gameObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onChase == true)
        {
            
            agente.SetDestination(player.transform.position);

        }

        if (golpeMelee == true)
        {
            StartCoroutine(GolpeMeleeActivate());
            golpeMelee = false;
        }

        if(healthEnemigo <= 0)
        {
            StartCoroutine(Muerte());
        }

        if(magnetizar == true)
        {
            StartCoroutine(Magnetizar());
            magnetizar = false;
        }

        if(disparar == true)
        {
            StartCoroutine(Disparar());
            disparar = false;
        }

        if(kamikaze == true)
        {
            StartCoroutine(Kamikaze());
            if(tiemposHabilidades.activeTime <= 0)
            {
                GameObject explosionParticula;
                explosionParticula = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
      
    }

    private void StartChase(int id)
    {
        if (id == this.id)
        {
            onChase = true;
            anim.SetTrigger("Comenzar");
        }
    }

    IEnumerator Muerte()
    {
        
        anim.SetTrigger("Muerte");
        agente.speed = 0;
        fuego.SetActive(true);
        meshDelEnemigo.material.color = Color.white;
        colliderCuerpo.enabled = false;
        healthBar.SetActive(false);
        BrazoDerechoCollider.enabled = false;
        Destroy(_habilidadesEquipadas);
        Destroy(this);
        yield return null;
    }


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
        anim.SetTrigger("AtaqueMelee");
        agente.speed = 1.7f;
        yield return new WaitForSeconds(1);
        agente.speed = 4;
    }

    IEnumerator Magnetizar()
    {
        agente.speed = 2.3f;
        anim.SetTrigger("Magnetizar");
        yield return new WaitForSeconds(1);
        agente.speed = 4;
       
    }

    IEnumerator Disparar()
    {
        agente.speed = 1;
        anim.SetTrigger("Disparar");
        yield return new WaitForSeconds(1.5f);
        agente.speed = 4;
    }

    IEnumerator Kamikaze()
    {
        agente.speed = 6;
        //anim.setTrigger("Kamikaze");
        yield return null;
    }


    public void SpawnDeDisparo()
    {
        GameObject tiposDisparo;
        if (firePoint != null)
        {

            tiposDisparo = Instantiate(disparo, firePoint.transform.position, Quaternion.identity);
            tiposDisparo.transform.localRotation = this.gameObject.transform.rotation;
        }
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

    public void ActivarMagneto()
    {
       magneto.SetActive(true);
    }

    public void DesactivarMagneto()
    {
       // magneto.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject explosionParticula;
            explosionParticula = Instantiate(explosion, transform.position, Quaternion.identity);
            PlayerSingleton.Instance.playerCurrentHP -= 50;
            Destroy(this.gameObject);
        }
    }

}

