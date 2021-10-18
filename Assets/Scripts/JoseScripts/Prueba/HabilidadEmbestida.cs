using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HabilidadEmbestida : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        _calamarController.agente.speed = 20f;
        Animator anim = parent.GetComponent<Animator>();
        anim.SetBool("Trompo", true);
        _calamarController.embistiendo = true;
    }
}
