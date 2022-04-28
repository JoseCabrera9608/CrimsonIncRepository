using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorManyado : MonoBehaviour
{
    public GameObject plataforma;
    public float velocidad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        plataforma.transform.position += plataforma.transform.up * velocidad * Time.deltaTime;

    }

    /*private void OnCollisionExit(Collision collision)
    {
        plataforma.transform.position -= plataforma.transform.up * velocidad * Time.deltaTime;
    }*/
}
