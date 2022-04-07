using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    public float fuerza;

    List<Rigidbody> rigPlayer = new List<Rigidbody>();
    Transform magnetPoint;

    void Start()
    {
        magnetPoint = GetComponent<Transform>();

    }

    private void FixedUpdate()
    {
      foreach(Rigidbody rigPlay in rigPlayer)
        {
            rigPlay.AddForce((magnetPoint.position - rigPlay.position) * fuerza * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nube"))
        {
            rigPlayer.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Nube"))
        {
            rigPlayer.Remove(other.GetComponent<Rigidbody>());
        }
    }
}
