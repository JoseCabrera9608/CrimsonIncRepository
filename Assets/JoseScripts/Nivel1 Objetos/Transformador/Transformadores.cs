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
    public bool activado;
    public int id;

    void Start()
    {
        esteSistema = GetComponent<ParticleSystem>();
        BossGameEVent.current.Conexion += Desactivar;



        //var emision = esteSistema.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if(activado == true)
        {
            tiempoDeDetonacion += Time.deltaTime;
            emisionVariable += Time.deltaTime * multiplicadorParticula;
            distanciaParticula += Time.deltaTime * 0.5f;

            esteSistema.startSpeed = distanciaParticula;

            var emision = esteSistema.emission;
            emision.rateOverTime = emisionVariable;

            if (tiempoDeDetonacion >= 8 && tiempoDeDetonacion <= 9)
            {
                colliderParedElectrica.SetActive(true);

            }
            if (tiempoDeDetonacion >= 10)
            {
                emisionVariable = 3;
                distanciaParticula = 3;
                colliderParedElectrica.SetActive(false);
                tiempoDeDetonacion = 0;
                esteSistema.Play();
            }
        }
        else
        {
            return;
        }
       
    }

    public void Desactivar(int id)
    {
        if (id == this.id)
        {
            activado = false;
            if (esteSistema.isPlaying)
            {
                esteSistema.Stop();
            }
            //Destroy(this.gameObject);
        }
    }


}
