using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HabilidadTinta : Habilidad_SO
{
    //private  GameObject calamar;
    public override void Activate(GameObject parent)
    {
        Animator anim = parent.GetComponent<Animator>();
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        //calamar = GameObject.Find("Calamarsito");
        //SkinnedMeshRenderer calamarColor = calamar.GetComponent<SkinnedMeshRenderer>();
        anim.SetTrigger("MovAFinal");
        anim.SetTrigger("Tinta");
        _calamarController.tintaDisparada = true;
        //calamarColor.material.color = Color.red;
        Debug.Log("3era Habilidad");
    }

}
