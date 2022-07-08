using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformadores : MonoBehaviour
{
    //public AudioSource sonidoIddle;
    //public AudioSource sonidoExplosionElectricidad;
    ParticleSystem esteSistema;
    public float emisionVariable;
    public float multiplicadorParticula;
    public float distanciaParticula;
    public float tiempoDeDetonacion;
    public GameObject colliderParedElectrica;
    public bool activado;
    public int id;

    public Color color;

    public MeshRenderer mesh;
    public MeshRenderer mesh2;

    void Start()
    {
        esteSistema = GetComponent<ParticleSystem>();
        BossGameEVent.current.Conexion += Desactivar;
        
        /*sonidoExplosionElectricidad = GetComponent<AudioSource>();
        sonidoIddle = GetComponent<AudioSource>();*/
        color = mesh.material.GetColor("_EmissionColor");

        //var emision = esteSistema.emission;
    }

    
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

                //sonidoExplosionElectricidad.Play();

            }
            if (tiempoDeDetonacion >= 8 && tiempoDeDetonacion < 10)
            {

                if (mesh != null)
                {
                    mesh.material.SetColor("_EmissionColor", Color.red * 5);
                }

                if (mesh2 != null)
                {
                    mesh2.material.SetColor("_EmissionColor", Color.red * 5);
                }

                //sonidoExplosionElectricidad.Play();

            }
            if (tiempoDeDetonacion >= 10)
            {
                
                emisionVariable = 3;
                distanciaParticula = 3;
                colliderParedElectrica.SetActive(false);
                if (mesh != null)
                {
                    mesh.material.SetColor("_EmissionColor", color );
                }
                if (mesh2 != null)
                {
                    mesh2.material.SetColor("_EmissionColor", color );
                }
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
