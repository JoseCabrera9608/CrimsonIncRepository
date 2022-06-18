using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    public static Action<float> onPlayerStun;
    private void OnEnable()
    {
        _instance = this;

        #region defaultVars
        //HP
        playerMaxHP = DefaultPlayerVars.defaultMaxHP;
        playerCurrentHP = DefaultPlayerVars.defaultMaxHP;
        //Healing
        playerCurrentHealingCharges = DefaultPlayerVars.defaultMaxHealingCharges;
        playerMaxHealingCharges = DefaultPlayerVars.defaultMaxHealingCharges;
        playerHealAmount = DefaultPlayerVars.defaultHealAmount;
        //Stamina
        playerMaxStamina = DefaultPlayerVars.defaultMaxStamina;
        playerCurrentStamina = DefaultPlayerVars.defaultMaxStamina;
        playerStaminaRegen = DefaultPlayerVars.defaultStaminaRegen;
        playerRunStaminaCost = DefaultPlayerVars.defaultRunStaminaCost;
        //Damage and Defense
        playerDamage = DefaultPlayerVars.defaultDamage;
        playerDefense = DefaultPlayerVars.defaultDefense;
        playerStatusResistance = DefaultPlayerVars.defaultStatusResistance;
        playerAttackRange = DefaultPlayerVars.defaultAttackRange;
        playerAttackSpeed = DefaultPlayerVars.defaultAttackSpeed;
        playerCriticalChance = DefaultPlayerVars.defaultCriticalHit;
        //Others
        playerHitted = false;
        playerRecoveryTime = DefaultPlayerVars.defaultRecoveryTime;
        #endregion
    }
    public void SetValuesToMax()
    {
        playerCurrentHP = playerMaxHP;
        playerCurrentStamina = playerMaxStamina;
        playerCurrentHealingCharges = playerMaxHealingCharges;
    }
    //======HP values==================
    public float playerMaxHP { set; get; }
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
    public float playerAttackRange { set; get; }
    public float playerAttackSpeed { set; get; }
    public float playerCriticalChance { set; get; }

    //======Others==================
    public bool playerHitted { set; get; }
    public bool playerFreezed { set; get; }
    public float playerRecoveryTime { get; set; }
}
