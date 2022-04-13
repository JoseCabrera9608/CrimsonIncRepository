using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GolpeLargo : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();
        /*Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("GolpeLargo");*/
        Debug.Log("ActivarGolpeLargo");
        _enemigoPequeño.golpeLargo = true;
    }
}
