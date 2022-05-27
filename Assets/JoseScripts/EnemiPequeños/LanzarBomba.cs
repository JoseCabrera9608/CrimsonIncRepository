using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesSonda/Bomba", order = 1)]
public class LanzarBomba : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();

        _enemigoPequeño.lanzarBomba = true;
    }
}
