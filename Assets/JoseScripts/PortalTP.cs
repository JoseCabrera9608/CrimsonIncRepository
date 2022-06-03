using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTP : MonoBehaviour
{
    public GameObject spawnPortal;
    Transform lugarDeSpawn;
    void Start()
    {
        lugarDeSpawn = spawnPortal.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = lugarDeSpawn.position;
        }
    }
}
