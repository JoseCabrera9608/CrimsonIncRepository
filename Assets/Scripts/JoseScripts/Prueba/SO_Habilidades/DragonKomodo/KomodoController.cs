using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoController : MonoBehaviour
{
    float timer;
    bool startFight;
    public bool lanzamientoMisiles = false;
    public GameObject nube;
    public GameObject golpeCollider;
    public bool lanzarNube;
    public bool golpeando;
    public GameObject misile;
    Transform target;
    GameObject player;
    Collider brazoCollider;
    GameObject misileSpawn;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        misileSpawn = GameObject.FindGameObjectWithTag("MisilesTarget");
        brazoCollider = golpeCollider.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        BossGameEVent.current.combatTriggerExit += FightStart;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (startFight == true)
        {
           
            transform.LookAt(target);
        }
        
        if(lanzarNube == true)
        {
            StartCoroutine(LanzarNube());
            lanzarNube = false;
        }
        if(golpeando == true)
        {
            StartCoroutine(ActivePunchCollider());
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
        nube.SetActive(true);
        yield return new WaitForSeconds(8);
        nube.SetActive(false);
    }
    IEnumerator RotacionInicial()
    {
        transform.Rotate(new Vector3(0f, -90f, 0f) * Time.deltaTime);
        yield return new WaitForSeconds(3);
        startFight = true;
    }

    IEnumerator ActivePunchCollider()
    {
        brazoCollider.enabled = true;
        yield return new WaitForSeconds(7.2f);
        brazoCollider.enabled = false;
        
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
        

    }
    void FightStart()
    {
        //startFight = true;
        StartCoroutine(RotacionInicial());
        
    }
}
