using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosColision : MonoBehaviour
{
    public Transform efecto;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limite"))
        {
           Debug.Log("GaaaaaaaaaAAA");
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(efecto, position, rotation);
        }
      /*  Debug.Log("GaaaaaaaaaAAA");
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(efecto, position, rotation);*/

    }
    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GaaaaaaaaaAAA");
        ContactPoint contact = other.gameObject.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(efecto, position, rotation);
    }*/
}
