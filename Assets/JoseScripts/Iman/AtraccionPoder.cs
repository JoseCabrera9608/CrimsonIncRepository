using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraccionPoder : MonoBehaviour
{
    public float fuerza;
    List<Rigidbody> rigPlayer = new List<Rigidbody>();
    List<Rigidbody> rigObjetos = new List<Rigidbody>();
    public SphereCollider colliderMag;
    Transform magnetPoint;
    // Start is called before the first frame update
    void Start()
    {
        colliderMag = GetComponent<SphereCollider>();
        magnetPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
 
    private void FixedUpdate()
    {
        foreach (Rigidbody rigPlay in rigPlayer)
        {
            rigPlay.AddForce((magnetPoint.position - rigPlay.position) * fuerza * Time.fixedDeltaTime);
        }

        foreach (Rigidbody rigObj in rigObjetos)
        {
            rigObj.AddForce((magnetPoint.position - rigObj.position) * fuerza * Time.fixedDeltaTime);
            Destroy(rigObj.gameObject, 3f);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            rigPlayer.Add(other.GetComponent<Rigidbody>());
        }
        if (other.CompareTag("Bounce"))
        {
            rigObjetos.Add(other.GetComponent<Rigidbody>());
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Bounce"))
        {
            rigPlayer.Remove(other.GetComponent<Rigidbody>());
        }
    }

    public void ActivarCollider()
    {
        colliderMag.enabled = true;
    }

    public void DesactivarCollider()
    {
        colliderMag.enabled = false;
    }

    public void DesactivarEsfera()
    {
        this.gameObject.SetActive(false);
    }
}
