using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    public float fuerza;
    public GameObject player;
    
    Transform magnetPoint;
    void Start()
    {
        magnetPoint = GetComponent<Transform>();

    }

    private void FixedUpdate()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
    
    }

    private void OnTriggerExit(Collider other)
    {
       
    }
}
