using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesSonda/Bomba", order = 1)]
public class LanzarBomba : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();

        _enemigoPeque�o.lanzarBomba = true;
    }
}
