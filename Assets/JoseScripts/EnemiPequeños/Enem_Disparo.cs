using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enem_Disparo : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();
        
        _enemigoPeque�o.disparar = true;
    }
}
