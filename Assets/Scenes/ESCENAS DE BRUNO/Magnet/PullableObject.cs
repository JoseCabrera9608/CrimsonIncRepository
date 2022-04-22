using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Iman"))
        {
            other.GetComponent<MagnetObject>().objectsToAtract.Add(GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Iman"))
        {
            other.GetComponent<MagnetObject>().objectsToAtract.Remove(GetComponent<Rigidbody>());
        }
    }
}
