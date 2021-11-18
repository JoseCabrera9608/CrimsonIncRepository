using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorFix : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController cc;
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cc = other.GetComponent<CharacterController>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cc.Move(rb.velocity * Time.deltaTime);
        }

    }
}
