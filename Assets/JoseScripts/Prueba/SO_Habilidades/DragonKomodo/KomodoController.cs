using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoController : MonoBehaviour
{
    public bool hitted;
    public int health;
   
    bool startFight;
    public bool lanzamientoMisiles = false;
    public GameObject nube;
    public GameObject golpeCollider;
    public bool lanzarNube;
    public bool golpeando;
    public GameObject misile;
    //LookAt variables
    Transform target;
    public float lookAtSpeed = 10f;
    //--------------
    GameObject player;
    Collider brazoCollider;
    GameObject misileSpawn;
    Animator anim;
    //------- Misile Targets
    public GameObject[] misileTargets;
    int index;
    public Transform targetChosen;
    public Vector3 ultimaPosicion;
    //-------Ability Bools
    public bool inyeccion;
    //-----Inyeccion
    GameObject[] inyeccionSpawners;
    public GameObject acido;

    public GameObject barraHUDEnemigo;
    NubeDaño dañoNube;
    GameObject chosenSpawn;
    void Start()
    {
        anim = GetComponent<Animator>();
        dañoNube = nube.GetComponent<NubeDaño>();
        inyeccionSpawners = GameObject.FindGameObjectsWithTag("InyeccionTarget");
        misileSpawn = GameObject.FindGameObjectWithTag("MisilesTarget");
        brazoCollider = golpeCollider.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("PlayerCabeza");
        target = player.transform;
      //  BossGameEVent.current.combatTriggerExit += FightStart;
        misileTargets = GameObject.FindGameObjectsWithTag("Targets");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
        if (startFight == true)
        {
            
            StartCoroutine(Rotacion());
        }
        else
        {
            StopCoroutine(Rotacion());
        }


        if (lanzarNube == true)
        {
            //dañoNube.activado = true;
            startFight = false;
            anim.SetTrigger("Giro");
            StartCoroutine(LanzarNube());
            lanzarNube = false;
            
            
        }
        if(golpeando == true)
        { 
            startFight = true;
            StartCoroutine(ActivePunchCollider());
            anim.SetTrigger("Giro");
            golpeando = false;
        }

        if(lanzamientoMisiles == true)
        {
            startFight = false;
            StartCoroutine(MisileKomodo());
            lanzamientoMisiles = false;
            index = 0;
        }

        if (inyeccion == true)
        {
            startFight = true;
            StartCoroutine(Inyeccion());
            inyeccion = false;
            index = 0;
        }

        if(health <= 0)
        {
            //startFight = false;
            anim.SetTrigger("Muerte");
            StartCoroutine(Muerte());
        }
        
    }
    
    IEnumerator LanzarNube()
    {
        
        /*yield return new WaitForSeconds(4.4f);
        nube.SetActive(true);
        yield return new WaitForSeconds(10);
        nube.SetActive(false);*/
        yield return new WaitForSeconds(20f);
        startFight = true;
        
    }
    
    void StartNube()
    {
        FindObjectOfType<AudioManager>().Play("Nube");
        nube.SetActive(true);
    }
    void StopNube()
    {
        nube.SetActive(false);
    }
    IEnumerator ActivePunchCollider()
    {
       
        brazoCollider.enabled = true;
        yield return new WaitForSeconds(6.8f);
        brazoCollider.enabled = false;
        startFight = true;
        
    }
    
    IEnumerator MisileKomodo()
    {
        
       chosenSpawn = misileSpawn;
        
        for (int i = 0; i<3; i++)
        {
           
            index = Random.Range(0, misileTargets.Length);
            GameObject temportalTargetChosen = misileTargets[index];
            targetChosen = temportalTargetChosen.transform;
            ultimaPosicion = new Vector3(targetChosen.transform.position.x,targetChosen.transform.position.y,targetChosen.transform.position.z);
            anim.SetTrigger("LanzarMisiles");
            yield return new WaitForSeconds(1);

        }
        startFight = true;
    }
    
    void InstanciarMisil()
    {
        FindObjectOfType<AudioManager>().Play("KomodoMisiles");
        GameObject temporalMisile = Instantiate(misile);
        temporalMisile.transform.position = chosenSpawn.transform.position;
    }
    void FightStart()
    {
        FindObjectOfType<AudioManager>().Play("Rugido");
        anim.SetTrigger("Giro");
        startFight = true;
        barraHUDEnemigo.SetActive(true);
    }

    IEnumerator Rotacion()
    {
       
        yield return new WaitForSeconds(0.5f);
        Quaternion rotTarget = Quaternion.LookRotation(target.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, lookAtSpeed * Time.deltaTime);
        yield return null;
    }

    IEnumerator Muerte()
    {
        anim.SetTrigger("Muerte");
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

    IEnumerator Inyeccion()
    {

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1.7f);
            anim.SetTrigger("Inyeccion");
          
        }

    }

    void PreInyeccion()
    {
        FindObjectOfType<AudioManager>().Play("Inyeccion");
        index = Random.Range(0, inyeccionSpawners.Length);
    }
    void InstanciarInyeccion()
    {
        GameObject temportalTargetChosenAcido = inyeccionSpawners[index];
        //yield return new WaitForSeconds(2.58f);
        GameObject temporalAcido = Instantiate(acido);
        FindObjectOfType<AudioManager>().Play("Acido");
        temporalAcido.transform.position = temportalTargetChosenAcido.transform.position;
        Destroy(temporalAcido, 3f);
    }
    void GolpesVerticalesSonido()
    {
        FindObjectOfType<AudioManager>().Play("GolpesVerticales");
    }
    void GolpesLateralesSonido()
    {
        FindObjectOfType<AudioManager>().Play("GolpesLaterales");
    }
}
