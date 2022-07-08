using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCerco : MonoBehaviour
{
    public float rotacion;
    public GameObject cercoElectrico;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cercoElectrico.transform.rotation = Quaternion.Euler(new Vector3(0, rotacion, 0));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cercoElectrico.transform.rotation = Quaternion.Euler(new Vector3(0, -rotacion, 0));
        }
    }
}
