using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorTrucho : MonoBehaviour
{
    public GameObject plataforma;
    public float timer;
    bool bajar = false;
    public float tiempobajada;
    public float velocidad;

    private void Update()
    {
        if(bajar == true)
        {
            timer -= 1 * Time.deltaTime;
        }
        if (timer <= 0 && timer >= tiempobajada)
        {
            plataforma.transform.position -= plataforma.transform.up * velocidad* Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        plataforma.transform.position += plataforma.transform.up * velocidad* Time.deltaTime;
        
    }

    private void OnTriggerExit(Collider other)
    {
        bajar = true;
        timer = 5;
        
    }
}
