using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoderesSonda/Bomba", order = 1)]
public class LanzarBomba : ScriptableObject
{
    protected static object _calamarController;
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float distanceToActivate;
    public float minDistance;


    public virtual void Activate(GameObject parent)
    {

    }
}
