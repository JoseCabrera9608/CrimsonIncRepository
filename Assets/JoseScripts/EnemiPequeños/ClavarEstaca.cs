using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesSonda/ClavarEstaca", order = 6)]

public class ClavarEstaca : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();
        _enemigoPequeño.anim.SetTrigger("ClavarEstaca");
        _enemigoPequeño.clavarEstaca = true;
    }
}
