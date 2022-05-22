using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformadores : MonoBehaviour
{

    ParticleSystem esteSistema;
    public float emisionVariable;
    public float multiplicadorParticula;
    public float distanciaParticula;

    void Start()
    {
        esteSistema = GetComponent<ParticleSystem>();
        
        
        //var emision = esteSistema.emission;
    }

    // Update is called once per frame
    void Update()
    {
        emisionVariable += Time.deltaTime*multiplicadorParticula;
        distanciaParticula += Time.deltaTime*0.5f;

        esteSistema.startSpeed = distanciaParticula;

        var emision = esteSistema.emission;
        emision.rateOverTime = emisionVariable;
    }
}
