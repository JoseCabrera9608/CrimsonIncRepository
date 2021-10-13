using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatformScript : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 startPosition;
    public float movingSpeed;
    public bool toPosZ;
    [Header("Ambos En positivo btw")]
    public float negZLimit;
    public float posZLimit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }
    public void Moving()
    {
        if(toPosZ)
        {
            rb.velocity = new Vector3(0, 0, movingSpeed);
            if(transform.position.z >= (startPosition.z + posZLimit) )
            {
                toPosZ = false;
            }

        }
        else
        {
            rb.velocity = new Vector3(0, 0, -movingSpeed);
            if (transform.position.z <= (startPosition.z - negZLimit))
            {
                toPosZ = true;
            }
        }
    }
    public void LimitCheck()
    {
        
    }
}
