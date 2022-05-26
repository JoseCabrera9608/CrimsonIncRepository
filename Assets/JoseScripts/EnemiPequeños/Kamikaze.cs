using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="PoderesSonda/Kamikaze",order =1)]
public class Kamikaze : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPeque�oControlador _enemigoPeque�o = parent.GetComponent<EnemiPeque�oControlador>();
       
        _enemigoPeque�o.kamikaze = true;
    }
}
