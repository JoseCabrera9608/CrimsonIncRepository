using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SegundaHabilidad : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        Debug.Log("Segunda Habilidad");
    }
}
