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
    Transform target;
    GameObject player;
    Collider brazoCollider;
    void Start()
    {
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

    void FightStart()
    {
        //startFight = true;
        StartCoroutine(RotacionInicial());
        
    }
}
