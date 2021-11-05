using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Habilidad1 : Habilidad_SO
{
    Animator anim;
    public override void Activate(GameObject parent)
    {
        KomodoController _komodoController = parent.GetComponent<KomodoController>();
        anim = parent.GetComponent<Animator>();
        _komodoController.golpeando = true;
        anim.SetTrigger("GolpeBrazo");
        Debug.Log("Komodo golpeando");

    }

    

}
