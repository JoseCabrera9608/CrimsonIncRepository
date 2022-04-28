using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorManyado : MonoBehaviour
{
    public GameObject plataforma;
    public float velocidad;
    public bool subir;
    bool iddle;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(iddle == false)
        {
            if (subir == true)
            {
                StartCoroutine(SubirElevador());
            }
            else
            {
                StopCoroutine(SubirElevador());
            }
        }
        
           
       
    }


    IEnumerator SubirElevador()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return new WaitForSeconds(5);
    }

    IEnumerator BajarElevador()
    {
        transform.Translate(Vector3.up * -velocidad * Time.deltaTime);
        yield return new WaitForSeconds(5);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {

            }
        }
    }
}
