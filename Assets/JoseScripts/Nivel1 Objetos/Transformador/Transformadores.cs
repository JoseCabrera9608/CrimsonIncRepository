using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformadores : MonoBehaviour
{

    ParticleSystem esteSistema;
    public float emisionVariable;
    public float multiplicadorParticula;
    public float distanciaParticula;
    public float tiempoDeDetonacion;
    public GameObject colliderParedElectrica;

    void Start()
    {
        esteSistema = GetComponent<ParticleSystem>();
        
        
        
        //var emision = esteSistema.emission;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoDeDetonacion += Time.deltaTime;
        emisionVariable += Time.deltaTime*multiplicadorParticula;
        distanciaParticula += Time.deltaTime*0.5f;

        esteSistema.startSpeed = distanciaParticula;

        var emision = esteSistema.emission;
        emision.rateOverTime = emisionVariable;

        if(tiempoDeDetonacion >= 8 && tiempoDeDetonacion <= 9)
        {
           colliderParedElectrica.SetActive(true);

        }
        if(tiempoDeDetonacion >= 10)
        {
            emisionVariable = 3;
            distanciaParticula = 3;
            colliderParedElectrica.SetActive(false);
            tiempoDeDetonacion = 0;
            esteSistema.Play();
        }
    }


}
