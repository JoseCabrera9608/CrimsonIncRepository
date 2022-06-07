using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraccionCubo : MonoBehaviour
{
    public float damage;
 // List<Rigidbody> rigPlayer = new List<Rigidbody>();
 // public BoxCollider colliderMag;
 // Transform magnetPoint;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    //  colliderMag = GetComponent<BoxCollider>();
    //  magnetPoint = GetComponent<Transform>();
    }

    private void Update()
    {
     /* timer += Time.deltaTime;
        if(timer >= 3.1f)
        {
            this.gameObject.SetActive(false);
            timer = 0;
        }*/
        
    }

/*  private void FixedUpdate()
    {
        foreach (Rigidbody rigPlay in rigPlayer)
        {
            rigPlay.AddForce((magnetPoint.position - rigPlay.position) * fuerza * Time.fixedDeltaTime);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerStatus.damagePlayer?.Invoke(damage);
        }

    }

  /*private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            rigPlayer.Remove(other.GetComponent<Rigidbody>());
        }
    }*/

 /* public void ActivarCollider()
    {
        colliderMag.enabled = true;
    }*/

    public void DesactivarCollider()
    {
        this.gameObject.SetActive(false);
    }

    
}
