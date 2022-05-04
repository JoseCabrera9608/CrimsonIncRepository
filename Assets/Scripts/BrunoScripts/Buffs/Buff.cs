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
    [HideInInspector]public float oppositeMultiplier=1;

    [Header("Single Buff Settings")]
    [SerializeField] public PlayerVars playerVars;
    public float instantMultiplier;
    [HideInInspector] public bool displayed;

    [Header("Double Buff Settings")]
    [SerializeField] public PlayerVars doubleVar1;
    public float doubleMultiplier1;
    [SerializeField] public PlayerVars doubleVar2;
    public float doubleMultiplier2;

    [Header("Unique Buff Settings")]
    [SerializeField] public UniqueID uniqueID;

    public void ApplyBuff()
    {
        switch (buffType)
        {
            case BuffType.single:
                HandleInstant(playerVars,instantMultiplier*oppositeMultiplier);
                break;

            case BuffType.doubles:
                HandleInstant(doubleVar1, doubleMultiplier1* oppositeMultiplier);
                HandleInstant(doubleVar2, doubleMultiplier2* oppositeMultiplier);
                break;
            case BuffType.unique:
                HandleUniqueBuff(uniqueID);
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

            case PlayerVars.attackRange:
                PlayerSingleton.Instance.playerAttackRange += DefaultPlayerVars.defaultAttackRange * value;
                break;

            case PlayerVars.attackSpeed:
                PlayerSingleton.Instance.playerAttackSpeed += DefaultPlayerVars.defaultAttackSpeed * value;
                break;

            case PlayerVars.criticalHit:
                PlayerSingleton.Instance.playerCriticalChance += DefaultPlayerVars.defaultCriticalHit * value;
                break;

            case PlayerVars.recoveryTime:
                PlayerSingleton.Instance.playerRecoveryTime += DefaultPlayerVars.defaultRecoveryTime * value;
                break;

        }
    }

    private void HandleUniqueBuff(UniqueID id)
    {
        BuffManager.Instance.UniqueBuffs(id);
    }
}
public enum BuffType
{
    single,
    doubles,
    unique,
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
    statusResistance,
    attackRange,
    attackSpeed,
    criticalHit,
    recoveryTime
};

public enum UniqueID
{
    lowHealthMoreDamage,
    revive,
    sacrificeRing,
    extraCard
}
