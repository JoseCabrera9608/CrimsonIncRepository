using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_LanzamientoMisiles : Habilidad_SO
{
    Animator anim;
    public override void Activate(GameObject parent)
    {
        anim = parent.GetComponent<Animator>();
        KomodoController _komodoController = parent.GetComponent<KomodoController>();
        //anim.SetTrigger("LanzarMisiles");
        _komodoController.lanzamientoMisiles = true;
    }
}
