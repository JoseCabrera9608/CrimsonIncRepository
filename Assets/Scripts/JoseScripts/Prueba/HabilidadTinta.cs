using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HabilidadTinta : Habilidad_SO
{
    private  GameObject calamar;
    public override void Activate(GameObject parent)
    {
        Animator anim = parent.GetComponent<Animator>();
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        calamar = GameObject.Find("Calamar_Idle");
        MeshRenderer calamarColor = calamar.GetComponent<MeshRenderer>();
        calamarColor.material.color = Color.red;
        anim.SetTrigger("MovAFinal");
        anim.SetTrigger("Tinta");
        _calamarController.tintaDisparada = true;
        Debug.Log("3era Habilidad");
    }

}
