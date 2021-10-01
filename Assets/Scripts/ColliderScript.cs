using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playercol;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("GAAA");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GAAATrigger");

        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("GAAATriggerExit");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("GAAATriggerStay");
    }
}
