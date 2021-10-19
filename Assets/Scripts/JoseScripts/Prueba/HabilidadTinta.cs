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
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        calamar = GameObject.Find("Blocking Calamar");
        MeshRenderer calamarColor = calamar.GetComponent<MeshRenderer>();
        calamarColor.material.color = Color.red;
        _calamarController.tintaDisparada = true;
        Debug.Log("3era Habilidad");
    }

}
