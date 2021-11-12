using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorCollider : MonoBehaviour
{
    public GameObject hijo;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Esta colisionando");
            collision.transform.SetParent(this.gameObject.transform,false);
        }
    }
}
