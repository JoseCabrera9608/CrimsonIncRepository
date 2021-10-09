using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consola : MonoBehaviour
{
    // Start is called before the first frame update

    public bool ascensor;
    public bool encendido;
    public float speedy;
    public bool playercol;
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, speedy, 0);

        if (Input.GetKeyDown(KeyCode.E) && ascensor == true && playercol == true)
        {
            encendido = true;
            playercol = false;
        }

    }



    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playercol = true;
        }
    }


}
