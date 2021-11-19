using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HabilidadEmbestida : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        _calamarController.agente.speed = 8f;
        Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("MovAFinal");
        anim.SetTrigger("Embestida");
        _calamarController.embistiendo = true;
        Debug.Log("Embestida");
        
    }
}
