using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speedx;
    public float speedy;
    public float speedz;
    public bool abajo;


    public Consola consola;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speedx, speedy, speedz);
        if (consola.encendido == true && abajo == true)
        {
            speedy = -15;
        }
        if (consola.encendido == true && abajo == false)
        {
            speedy = 15;
        }

        consola.speedy = speedy;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bounce"))
        {

            Debug.Log("AAAAAAA");
            speedy = 0;

            consola.encendido = false;



            if (abajo == false)
            {
                abajo = true;
            }


            speedx = 0;
            speedy = 0;
            speedz = 0;
        }
    }


}
