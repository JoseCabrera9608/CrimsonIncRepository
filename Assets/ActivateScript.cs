using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour
{

    public GameObject Object;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Object.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }
}
