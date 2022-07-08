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
    public bool lanzarBomba;
    public bool clavarEstaca;
    //Particulas
    public GameObject fuego;
    SphereCollider colliderCuerpo;

    public float propulsionForce;
    public float normalspeed;
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
    public SkinnedMeshRenderer mesh;

    public GameObject disparo;
    public GameObject bomba;
    public GameObject firePoint;
    public GameObject bombafirePoint;
    public GameObject healthBar;
    public GameObject explosion;
    public GameObject estacaPrefab;
    public GameObject estacaPoint;
   
    GameObject cabezaPlayer;
    public Vector3 startPosition;

    public ProgressManager progress;

    HabilidadesEquipadas _habilidadesEquipadas;
    public AudioSource audioRecibirGolpe;

    void Start()
    {
        startPosition = this.gameObject.transform.position;
        cabezaPlayer = GameObject.Find("PlayerHead");
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        tiemposHabilidades = GetComponent<HabilidadesEquipadas>();
        anim = GetComponent<Animator>();
        BossGameEVent.current.combatTriggerExit += StartChase;
        player = GameObject.FindWithTag("Player");
        meshDelEnemigo = meshObject.GetComponent<SkinnedMeshRenderer>();
        _habilidadesEquipadas = this.gameObject.GetComponent<HabilidadesEquipadas>();
        colliderCuerpo = this.gameObject.GetComponent<SphereCollider>();
        audioRecibirGolpe = this.gameObject.GetComponent<AudioSource>();
        
        

        normalspeed = agente.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CaminarSonda"))
        {
            Vector3 targetPosition = new Vector3(cabezaPlayer.transform.position.x, transform.position.y, cabezaPlayer.transform.position.z);
            Quaternion rotTarget = Quaternion.LookRotation(targetPosition - this.transform.position);
            //Quaternion rotTarget = Quaternion.LookRotation(cabezaPlayer.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, 200 * Time.deltaTime);
        }
            if (PlayerSingleton.Instance.playerFreezed == true)
        {
            anim.SetBool("Congelado", true);
        }
        else
        {
            anim.SetBool("Congelado", false);
        }
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

        if(kamikaze == true && explosion !=null)
        {
            StartCoroutine(Kamikaze());
            if(tiemposHabilidades.activeTime <= 0)
            {
                GameObject explosionParticula;
                explosionParticula = Instantiate(explosion, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("ExplosionKamikase");
                Destroy(this.gameObject);
            }
           
        }

        if (lanzarBomba == true)
        {
            
            StartCoroutine(LanzarBomba());




        }

        if(clavarEstaca == true)
        {
            StartCoroutine(esperarEstaca());
            clavarEstaca = false;
               
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

    IEnumerator esperarEstaca()
    {
        
        agente.speed = 0;
        anim.SetTrigger("ClavarEstaca");
        
        yield return new WaitForSeconds(3);
        agente.speed = normalspeed;
        
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
        progress.enemysdeath += 1;
        Destroy(_habilidadesEquipadas);
        Destroy(this);
        yield return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            audioRecibirGolpe.Play();
            healthEnemigo -= PlayerSingleton.Instance.playerDamage*(40/7);
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
        agente.speed = normalspeed;
    }

    IEnumerator Magnetizar()
    {
        agente.speed = 2.3f;
        anim.SetTrigger("Magnetizar");
        yield return new WaitForSeconds(1);
        agente.speed = normalspeed;
       
    }

    IEnumerator Disparar()
    {
        agente.speed = 1;
        anim.SetTrigger("Disparar");
        yield return new WaitForSeconds(1.5f);
        agente.speed = normalspeed;
    }

    IEnumerator Kamikaze()
    {
        anim.SetTrigger("Kamikaze");
        mesh.material.SetColor("_EmissionColor", Color.red * 3);
        yield return new WaitForSeconds(2f);
        agente.speed = 8;
        //anim.setTrigger("Kamikaze");
        
    }

    IEnumerator LanzarBomba()
    {
        agente.speed = 1;
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
        anim.SetTrigger("Bomba");
        yield return new WaitForSeconds(1.5f);
        
        agente.speed = normalspeed;
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

    public void SpawnDeBomba()
    {

        GameObject bombasa = (GameObject)Instantiate(bomba, bombafirePoint.transform.TransformPoint(0, 0, 0), bombafirePoint.transform.rotation);
        bombasa.GetComponent<Rigidbody>().AddForce(bombafirePoint.transform.forward * propulsionForce, ForceMode.Impulse);
        lanzarBomba = false;
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
        if (collision.gameObject.CompareTag("Player")&&kamikaze == true)
        {
            GameObject explosionParticula;
            explosionParticula = Instantiate(explosion, transform.position, Quaternion.identity);
            PlayerStatus.damagePlayer?.Invoke(50);
            FindObjectOfType<AudioManager>().Play("ExplosionKamikase");
            Destroy(this.gameObject);
        }
    }
    public void PonerEstaca()
    {
        GameObject estaca = Instantiate(estacaPrefab, estacaPoint.transform.position, estacaPoint.transform.rotation);
    }

   

}

