using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Habilidad1 : Habilidad_SO
{
    Animator anim;
    public override void Activate(GameObject parent)
    {
        anim = parent.GetComponent<Animator>();
        anim.SetTrigger("GolpeBrazo");
        Debug.Log("Habilidad1 Komodo");
    }

    

}
