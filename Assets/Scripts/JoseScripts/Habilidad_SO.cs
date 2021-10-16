using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad_SO : ScriptableObject
{
    protected static object _calamarController;
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float distanceToActivate;


    public virtual void Activate (GameObject parent)
    {

    }
}
