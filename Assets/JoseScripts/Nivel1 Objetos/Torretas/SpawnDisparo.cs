using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisparo : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> tiposDisparo = new List<GameObject>();
    GameObject disparo;
    [SerializeField] int tipoDeBala;
    [SerializeField] bool fire;
    float timer;
    public float tiempoEntreDisparo;
    void Start()
    {
        disparo = tiposDisparo[tipoDeBala];
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= tiempoEntreDisparo)
        {
            fire = true;
        }

        if(fire == true)
        {
            SpawnDeDisparo();
            fire = false;
            timer = 0;
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
