using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoController : MonoBehaviour
{
    public bool hitted;
    public int health;
    bool runTimer = false;
    public float timer;
    bool startFight;
    public bool lanzamientoMisiles = false;
    public GameObject nube;
    public GameObject golpeCollider;
    public bool lanzarNube;
    public bool golpeando;
    public GameObject misile;
    //LookAt variables
    Transform target;
    public float lookAtSpeed = 1f;
    bool peleaIniciada = false;
    //--------------
    GameObject player;
    Collider brazoCollider;
    GameObject misileSpawn;
    Animator anim;
    bool atacking = true;


    public GameObject barraHUDEnemigo;
    void Start()
    {
        anim = GetComponent<Animator>();
        misileSpawn = GameObject.FindGameObjectWithTag("MisilesTarget");
        brazoCollider = golpeCollider.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("PlayerCabeza");
        target = player.transform;
        BossGameEVent.current.combatTriggerExit += FightStart;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(runTimer == true)
        {
            timer += Time.deltaTime;
        }
        
        if (startFight == true)
        {
            
            StartCoroutine(LookAt());
            runTimer = true;
            startFight = false;
            StopCoroutine(LookAt());
            
        }

        if (timer >= 1.2)
        {
            transform.LookAt(target);

        }
 

        if (lanzarNube == true)
        {
            atacking = true;
            StartCoroutine(LanzarNube());
            anim.SetTrigger("Giro");
            lanzarNube = false;
            
        }
        if(golpeando == true)
        {
            StartCoroutine(ActivePunchCollider());
            anim.SetTrigger("Giro");
            golpeando = false;
        }
        if(lanzamientoMisiles == true)
        {

            StartCoroutine(MisileKomodo());
            lanzamientoMisiles = false;
        }
        
    }
    
    IEnumerator LanzarNube()
    {
        timer = 0;
        runTimer = false;
        nube.SetActive(true);
        yield return new WaitForSeconds(10);
        nube.SetActive(false);
        StartCoroutine(LookAt());
        runTimer = true;
    }
   
    IEnumerator ActivePunchCollider()
    {
        timer = 0;
        runTimer = false;
        brazoCollider.enabled = true;
        yield return new WaitForSeconds(6.8f);
        brazoCollider.enabled = false;
        StartCoroutine(LookAt());
        runTimer = true;
        
    }

    IEnumerator MisileKomodo()
    {
        timer = 0;
        runTimer = false;
        GameObject chosenSpawn = misileSpawn;
        
        
        for (int i = 0; i<3; i++)
        {
            anim.SetTrigger("LanzarMisiles");
            yield return new WaitForSeconds(1.9f);
            GameObject temporalMisile = Instantiate(misile);
            temporalMisile.transform.position = chosenSpawn.transform.position;
            yield return new WaitForSeconds(3);
            atacking = false;

        }
        Debug.Log("Creo misiles");
        StartCoroutine(LookAt());
        runTimer = true;


    }
    void FightStart()
    {
        anim.SetTrigger("Giro");
        startFight = true;
        barraHUDEnemigo.SetActive(true);
        


    }
    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * lookAtSpeed;
            yield return null;
        }
       
    }
}
