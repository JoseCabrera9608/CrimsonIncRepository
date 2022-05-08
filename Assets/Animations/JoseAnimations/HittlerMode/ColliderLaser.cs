using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLaser : MonoBehaviour
{
    LasersController _lasersController;
    public GameObject lasers;
    void Start()
    {
        _lasersController = lasers.GetComponent<LasersController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _lasersController.activar = true;
            this.gameObject.SetActive(false);
        }
    }
}
