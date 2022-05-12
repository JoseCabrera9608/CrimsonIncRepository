using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisparo : MonoBehaviour
{
    public int id;
    public GameObject firePoint;
    public List<GameObject> tiposDisparo = new List<GameObject>();
    GameObject disparo;
    [SerializeField] int tipoDeBala;
    [SerializeField] bool fire;
    public bool activado;
    float timer;
    public float tiempoEntreDisparo;
    void Start()
    {
        disparo = tiposDisparo[tipoDeBala];
        activado = true;
        BossGameEVent.current.Conexion += Desactivar;
    }

    // Update is called once per frame
    void Update()
    {
        if(activado == true)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoEntreDisparo)
            {
                fire = true;
            }

            if (fire == true)
            {
                SpawnDeDisparo();
                fire = false;
                timer = 0;
            }
        }
        /*timer += Time.deltaTime;

        if (timer >= tiempoEntreDisparo)
        {
            fire = true;
        }

        if(fire == true)
        {
            SpawnDeDisparo();
            fire = false;
            timer = 0;
        }*/
    

    }

    public void Desactivar(int id)
    {
        if (id == this.id)
        {
            activado = false;
        }
    }
    void SpawnDeDisparo()
    {
        GameObject tiposDisparo;
        if(firePoint != null)
        {
            
            tiposDisparo = Instantiate(disparo, firePoint.transform.position, Quaternion.identity);
            tiposDisparo.transform.localRotation = this.gameObject.transform.rotation;
        }
    }

    IEnumerator Disparar()
    {
        SpawnDeDisparo();
        yield return new WaitForSeconds(1);
        
    }
}
