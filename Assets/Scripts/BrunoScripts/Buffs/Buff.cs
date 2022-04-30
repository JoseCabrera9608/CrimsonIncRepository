using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Buff",menuName ="Buff")]
public class Buff : ScriptableObject
{
    [SerializeField] public BuffType buffType;
    public string buffName;
    public string description;
    public bool used;

    
}
public enum BuffType
{
    instant,
    pasive,
    unique,
    oneUse
};
