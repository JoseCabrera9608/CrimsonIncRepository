using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="PoderesSonda/Kamikaze",order =1)]
public class Kamikaze : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        EnemiPequeñoControlador _enemigoPequeño = parent.GetComponent<EnemiPequeñoControlador>();
       
        _enemigoPequeño.kamikaze = true;
    }
}
