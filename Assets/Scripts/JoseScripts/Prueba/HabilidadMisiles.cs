using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class HabilidadMisiles : Habilidad_SO
{
    GameObject[] spawns;
    public override void Activate(GameObject parent)
    {
        spawns = GameObject.FindGameObjectsWithTag("MisilesTarget");

        Prueba();
    }
    
    void Prueba()
    {
        
    }
}
