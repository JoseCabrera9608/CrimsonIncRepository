using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_FuegoPC : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject groundFire;
    Vector3 minimo;
    Vector3 maximo;
    Vector3 randomPosition;
    Vector3 newDiference;
    Vector3 ultimateVector;
    float z;



    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        minimo = transform.position + new Vector3(0, -1, 2);
        maximo = transform.position + new Vector3(0, -1, 3);

    }


    // Update is called once per frame
    void Update()
    {
        z = Random.Range(minimo.z, maximo.z);

        //randomPosition = maximo - minimo;
       // newDiference = randomPosition * Random.Range(0.0f, 1f);
       // ultimateVector = minimo + newDiference;
        
        
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {

            
            Debug.Log("ParticleCollision");
            GameObject obj = Instantiate(groundFire);

            obj.transform.position = transform.position + new Vector3(0,-1,9.8f);

            Destroy(obj, 5f);
        }

    }



}
