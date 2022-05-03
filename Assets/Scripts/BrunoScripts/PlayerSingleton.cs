using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _instance;
    public static PlayerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerSingleton");
                go.AddComponent<PlayerSingleton>();
            }
            return _instance;
        }
    }
    private void OnEnable()
    {
        _instance = this;

        #region defaultVars
        playerMaxHP = 100;
        playerCurrentHP = 100;
        playerCurrentHealingCharges = 5;
        playerMaxHealingCharges = 5;
        playerHitted = false;
        #endregion
    }

    //======HP values==================
    [SerializeField]public float playerMaxHP { set; get; }
    public float playerCurrentHP { set; get; }


    //======Stamina values==================
    public float playerMaxStamina { set; get; }
    public float playerCurrentStamina { set; get; }
    public float playerStaminaRegen { set; get; }
    public float playerRunStaminaCost { set; get; }

    //======Healing values==================
    public float playerCurrentHealingCharges { set; get; }
    public float playerHealAmount { set; get; }
    public float playerMaxHealingCharges { set; get; }

    //======Damage and Defense==================
    public float playerDamage { set; get; }
    public float playerDefense { set; get; }
    public float playerStatusResistance { set; get; }

    //======Others==================
    public bool playerHitted { set; get; }
}
