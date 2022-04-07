using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trdml : MonoBehaviour
{
    public bool on = false;// atrac=true;
    public float force = -100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void OnTriggerStay(Collider o)
    {
        if (o.gameObject.tag == "Player" && on == true) { o.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.right*force,ForceMode.VelocityChange); }
    }
}
