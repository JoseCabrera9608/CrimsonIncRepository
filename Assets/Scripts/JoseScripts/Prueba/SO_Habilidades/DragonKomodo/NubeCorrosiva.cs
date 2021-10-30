using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NubeCorrosiva : Habilidad_SO
{
    KomodoController _komodoController;
    public override void Activate(GameObject parent)
    {
        _komodoController = parent.GetComponent<KomodoController>();
        _komodoController.lanzarNube = true;
    }

}
