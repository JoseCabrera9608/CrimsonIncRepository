using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject menuSeleccion;
    public bool seInicio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActivarSeleccion();
            seInicio = true;
        }
     
    }

    public void ActivarSeleccion()
    {

        menuSeleccion.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
