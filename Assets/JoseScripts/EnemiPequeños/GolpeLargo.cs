using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GolpeLargo : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();
        /*Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("GolpeLargo");*/
        Debug.Log("ActivarGolpeLargo");
        _enemigoPeque�o.golpeLargo = true;
    }
}
