using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EmbestidaCangrejo : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        BossCangrejo _bossCangrejo = parent.GetComponent<BossCangrejo>();
        Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("VaAtacar");
        anim.SetTrigger("Embestida");
        _bossCangrejo.activarEmbestida = true;
    }
}
