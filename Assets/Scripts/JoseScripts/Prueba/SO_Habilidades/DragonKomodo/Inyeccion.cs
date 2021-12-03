using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inyeccion : Habilidad_SO
{
    KomodoController _komodoController;
   
    public override void Activate(GameObject parent)
    {
        
        _komodoController = parent.GetComponent<KomodoController>();
       
        _komodoController.inyeccion = true;
    }
}
