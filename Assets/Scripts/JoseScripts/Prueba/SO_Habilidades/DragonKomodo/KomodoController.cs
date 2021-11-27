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
            startFight = false;
            anim.SetTrigger("Giro");
            StartCoroutine(LanzarNube());
            lanzarNube = false;
            
            
        }
        if(golpeando == true)
        {
            startFight = false;
            StartCoroutine(ActivePunchCollider());
            anim.SetTrigger("Giro");
            golpeando = false;
        }

        if(lanzamientoMisiles == true)
        {
            startFight = false;
            StartCoroutine(MisileKomodo());
            lanzamientoMisiles = false;
        }
        if(health <= 0)
        {
            anim.SetTrigger("Muerte");
            startFight = false;
            StartCoroutine(Muerte());
        }
        
    }
    
    IEnumerator LanzarNube()
    {
        nube.SetActive(true);
        yield return new WaitForSeconds(10);
        nube.SetActive(false);
        startFight = true;
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
        
        GameObject chosenSpawn = misileSpawn;
        
        
        for (int i = 0; i<3; i++)
        {
            anim.SetTrigger("LanzarMisiles");
            yield return new WaitForSeconds(1.9f);
            GameObject temporalMisile = Instantiate(misile);
            temporalMisile.transform.position = chosenSpawn.transform.position;
            yield return new WaitForSeconds(3);

        }
        Debug.Log("Creo misiles");
        startFight = true;
        


    }
    void FightStart()
    {
        anim.SetTrigger("Giro");
        startFight = true;
        barraHUDEnemigo.SetActive(true);
        


    }
    /*private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * lookAtSpeed;
            yield return null;
        }
       
    }*/

    IEnumerator Rotacion()
    {
       
        yield return new WaitForSeconds(0.5f);
        Quaternion rotTarget = Quaternion.LookRotation(target.position - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, lookAtSpeed * Time.deltaTime);
        yield return null;
    }

    IEnumerator Muerte()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
