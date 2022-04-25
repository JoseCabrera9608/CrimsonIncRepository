using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TentacleStrike : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        _calamarController.tentacleStrikeBool = true;
        Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("MovAFinal");
        anim.SetTrigger("Golpe");
        Debug.Log("Gaa");
    }
}
