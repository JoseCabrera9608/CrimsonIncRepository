using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesBossCangrejo/BuffPasiva", order = 5)]
public class PasivaCaparazon : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        BossCangrejo _bossCangrejo = parent.GetComponent<BossCangrejo>();
        Animator anim = parent.GetComponent<Animator>();
        anim.SetTrigger("VaAtacar");
        _bossCangrejo.activarPasiva = true;
    }
}
