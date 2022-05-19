using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeganeuraStats : MonoBehaviour
{
    [Header("=======Damages========")]  
    public float rayoIonDamage;
    public float bombardeoMisilesDamage;
    public float rayosEmpDamage;
    public float vistaCazadorDamage;
    public float lluviaDamage;
    public float discosEmpDamage;
    public float discosEmpStaminaLoss;
    

    public Dictionary<Action, float> attacksDamage = new Dictionary<Action, float>();

    [Header("=======General========")]
    public float health;
    public float onGroundTime;
    public float rotationSpeed;
    [HideInInspector] public bool isAttacking;
    public float attackDelay;
    [HideInInspector]public bool onAir;
    public bool canRotate;
    public int stakesToThrow = 4;
    public bool isAlive=true;

    [Header("Rayo ion=======================")]
    public float laserRotationSpeed;
    public float laserAimTime;
    public float laserAttackTime;

    [Header("Bombardeo misiles=======================")]
    public float bmAmount;
    public float bmDelay;

    [Header("Rayos emp=======================")]
    public float rempAmount;
    public float rempSpeed;
    public float rempRotationSpeed;
    public float rempFollowTime;
    public float rempLifeTime;

    [Header("Lluvia de lasers=======================")]
    public int lluviaAmount;
    public float lluviaDelay;

    [Header("Bomba Flash=======================")]
    public float flashDuration;
    public float flashDelay;

    [Header("DiscosEMP=======================")]
    public float discMaxSpeed;
    public float discRotationSpeed;
    public float discAmount;
    public float discDelay;
    [Header("Vista cazador===================")]
    public float cazadorAimTime;
    public float cazadorRotationSpeed;
    public float cazadorLaserDuration;

    private void Start()
    {
        attacksDamage.Add(Action.rayoIon, rayoIonDamage);
        attacksDamage.Add(Action.bombardeoMisiles,bombardeoMisilesDamage);
        attacksDamage.Add(Action.misilesEmp, rayosEmpDamage);
        attacksDamage.Add(Action.vistaCazador, vistaCazadorDamage);
        attacksDamage.Add(Action.lluviaLasers, lluviaDamage);
        attacksDamage.Add(Action.discosEmp, discosEmpDamage);
        attacksDamage.Add(Action.idle, 0);
        attacksDamage.Add(Action.bombaFlash, 0);
    }

}
