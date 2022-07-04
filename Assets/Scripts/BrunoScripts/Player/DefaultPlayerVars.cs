using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultPlayerVars 
{
   
    //======HP values==================
    public static float defaultMaxHP { get { return 200; } }

    //======Stamina values==================
    public static float defaultMaxStamina { get { return 3; } }
    public static float defaultStaminaRegen { get { return 1; } }
    public static float defaultRunStaminaCost { get { return 100; } }

    //======Healing values==================
    public static float defaultHealAmount { get { return 50; } }
    public static float defaultMaxHealingCharges { get { return 5; } }

    //======Damage and Defense==================
    public static float defaultDamage { get { return 7; } }
    public static float defaultDefense { get { return 1; } }
    public static float defaultStatusResistance { get { return 1; } }
    public static float defaultAttackRange { get { return 1; } }
    public static float defaultAttackSpeed { get { return 1; } }
    public static float defaultCriticalHit { get { return 1; } }
    
    //=====Other values=============================
    public static float defaultRecoveryTime { get { return 8; } }

}
