using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NubeCorrosiva : Habilidad_SO
{
    KomodoController _komodoController;
    Animator anim;
    GameObject nube;
    NubeDaño _nubeDaño;
    public override void Activate(GameObject parent)
    {
        
        anim = parent.GetComponent<Animator>();
        _komodoController = parent.GetComponent<KomodoController>();
        anim.SetTrigger("Nube");
        _komodoController.lanzarNube = true;
    }

}
