using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Buff",menuName ="Buff")]
public class Buff : ScriptableObject
{
    [Header("General")]
    [SerializeField] public BuffType buffType;
    public string buffName;
    public string description;
    public bool picked;
    public float oppositeMultiplier=1;

    [Header("Instan buff Settings")]
    [SerializeField] public PlayerVars playerVars;
    public float instantMultiplier;
    [HideInInspector] public bool displayed;

    [Header("Double buff settings")]
    [SerializeField] public PlayerVars doubleVar1;
    public float doubleMultiplier1;
    [SerializeField] public PlayerVars doubleVar2;
    public float doubleMultiplier2;


    public void ApplyBuff()
    {
        switch (buffType)
        {
            case BuffType.instant:
                HandleInstant(playerVars,instantMultiplier*oppositeMultiplier);
                break;

            case BuffType.doubles:
                HandleInstant(doubleVar1, doubleMultiplier1* oppositeMultiplier);
                HandleInstant(doubleVar2, doubleMultiplier2* oppositeMultiplier);
                break;
        }
    }
    
    private void HandleInstant(PlayerVars var,float value)
    {
        switch (var)
        {
            case PlayerVars.maxHealth:
                PlayerSingleton.Instance.playerMaxHP += DefaultPlayerVars.defaultMaxHP * value;
                break;

            case PlayerVars.maxStamina:
                PlayerSingleton.Instance.playerMaxStamina += DefaultPlayerVars.defaultMaxStamina * value;
                break;

            case PlayerVars.staminaRegenValue:
                PlayerSingleton.Instance.playerStaminaRegen += DefaultPlayerVars.defaultStaminaRegen * value;
                break;

            case PlayerVars.runStaminaCost:
                PlayerSingleton.Instance.playerRunStaminaCost += DefaultPlayerVars.defaultRunStaminaCost * value;
                break;

            case PlayerVars.healingAmount:
                PlayerSingleton.Instance.playerHealAmount += DefaultPlayerVars.defaultHealAmount * value;
                break;

            case PlayerVars.healingCharges:
                PlayerSingleton.Instance.playerMaxHealingCharges += DefaultPlayerVars.defaultMaxHealingCharges * value;
                break;

            case PlayerVars.damage:
                PlayerSingleton.Instance.playerDamage += DefaultPlayerVars.defaultDamage * value;
                break;

            case PlayerVars.defense:
                PlayerSingleton.Instance.playerDefense += DefaultPlayerVars.defaultDefense * value;
                break;

            case PlayerVars.statusResistance:
                PlayerSingleton.Instance.playerStatusResistance += DefaultPlayerVars.defaultStatusResistance * value;
                break;
        }
    }
}
public enum BuffType
{
    instant,
    doubles,
    unique,
    oneUse
};

public enum PlayerVars
{
    maxHealth,
    maxStamina,
    healingCharges,
    healingAmount,
    staminaRegenValue,
    runStaminaCost,
    damage,
    defense,
    statusResistance
};
