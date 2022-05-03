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

    [Header("Instan buff Settings")]
    [SerializeField] public PlayerVars playerVars;
    public float instantMultiplier;
    [HideInInspector] public bool displayed;

    
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
