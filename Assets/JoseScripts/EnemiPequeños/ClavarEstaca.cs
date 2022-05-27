using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesSonda/ClavarEstaca", order = 6)]

public class ClavarEstaca : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();
        _enemigoPeque�o.anim.SetTrigger("ClavarEstaca");
        _enemigoPeque�o.clavarEstaca = true;
    }
}
