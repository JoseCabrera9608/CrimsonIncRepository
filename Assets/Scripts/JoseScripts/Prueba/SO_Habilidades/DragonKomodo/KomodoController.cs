using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoController : MonoBehaviour
{
    float timer;
    bool startFight;
    public bool lanzamientoMisiles = false;
    public GameObject nube;
    public bool lanzarNube;
    Transform target;
    GameObject player;
    void Start()
    {
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

    void FightStart()
    {
        //startFight = true;
        StartCoroutine(RotacionInicial());
        
    }
}
