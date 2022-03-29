using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject menuSeleccion;
    
   

    public void ActivarSeleccion()
    {

        menuSeleccion.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
