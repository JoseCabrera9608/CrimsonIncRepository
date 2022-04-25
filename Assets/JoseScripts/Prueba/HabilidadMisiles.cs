using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class HabilidadMisiles : Habilidad_SO
{

    public override void Activate(GameObject parent)
    {
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        _calamarController.lanzarMisiles = true;
    }
}


