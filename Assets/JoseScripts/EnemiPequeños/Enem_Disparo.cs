using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enem_Disparo : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();
        
        _enemigoPequeño.disparar = true;
    }
}
