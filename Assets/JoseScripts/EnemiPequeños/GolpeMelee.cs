using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PoderesSonda/GolpeMelee", order = 3)]
public class GolpeMelee : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();
        /*Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("GolpeLargo");*/
        Debug.Log("GolpeMelee");
        _enemigoPequeño.golpeMelee= true;
    }
}
