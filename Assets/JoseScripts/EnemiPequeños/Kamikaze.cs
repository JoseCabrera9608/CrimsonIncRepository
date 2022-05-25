using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Kamikaze : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();
       
        _enemigoPeque�o.kamikaze = true;
    }
}
