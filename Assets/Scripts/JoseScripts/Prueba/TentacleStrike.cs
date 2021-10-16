using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TentacleStrike : Habilidad_SO
{
    public override void Activate(GameObject parent)
    {
        CalamarController _calamarController = parent.GetComponent<CalamarController>();
        _calamarController.tentacleStrikeBool = true;
        Debug.Log("Gaa");
    }
}
