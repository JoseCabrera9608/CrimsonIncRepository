using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameEVent : MonoBehaviour
{
    public static BossGameEVent current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> combatTriggerExit;

    public void StartCombatTriggerExit(int id)
    {
        if (combatTriggerExit != null)
        {
            combatTriggerExit(id);
        }
    }

    public event Action<int> Conexion;

    public void Conectar(int id)
    {

        if (Conexion != null)
        {
            Conexion(id);
        }
    }

}
