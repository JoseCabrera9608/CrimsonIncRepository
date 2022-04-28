using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevadorManyado : MonoBehaviour
{
    public GameObject plataforma;
    public float velocidad;
    public bool subir;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
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


    IEnumerator SubirElevador()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        yield return new WaitForSeconds(5);
    }
   
}
