using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilRecto : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        Destroy(this.gameObject, 3f);
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionoConCubo");
        Destroy(this.gameObject);
    }*/
}
