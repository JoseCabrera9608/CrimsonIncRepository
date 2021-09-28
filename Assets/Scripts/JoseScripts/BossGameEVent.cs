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

    public event Action combatTriggerExit;

    public void StartCombatTriggerExit()
    {
        if (combatTriggerExit != null)
        {
            combatTriggerExit();
        }
    }

}
