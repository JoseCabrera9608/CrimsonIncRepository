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
    

    public Dictionary<Maction, float> attacksDamage = new Dictionary<Maction, float>();

    [Header("=======General========")]
    public float health;
    public float onGroundTime;
    public float rotationSpeed;
    [HideInInspector] public bool isAttacking;
    public float attackDelay;
    [HideInInspector]public bool onAir;
    [HideInInspector]public bool canRotate;
    public int stakesToThrow = 4;
    [HideInInspector]public bool isAlive=true;

    [Header("Rayo ion=======================")]
    public float laserRotationSpeed;
    public float laserAimTime;
    public float laserAttackTime;

    [Header("Bombardeo misiles=======================")]
    public float bmAmount;
    public float bmDelay;
    public float bmTimeToExplode;
    [Header("Rayos emp=======================")]
    public float rempAmount;
    public float rempSpeed;
    public float rempRotationSpeed;
    public float rempFollowTime;
    public float rempLifeTime;

    [Header("Lluvia de lasers=======================")]
    public int lluviaAmount;
    public float lluviaDelay;
    public float lluviaLasersHeight;
    [Header("Bomba Flash=======================")]
    public float flashDuration;
    public float flashDelay;

    [Header("DiscosEMP=======================")]
    public float discMaxSpeed;
    public float discRotationSpeed;
    public float discAmount;
    public float discDelay;
    public float empDiscExtraFollowTime;
    [Header("Vista cazador===================")]
    public float cazadorAimTime;
    public float cazadorRotationSpeed;
    public float cazadorLaserDuration;

    private void Start()
    {
        attacksDamage.Add(Maction.rayoIon, rayoIonDamage);
        attacksDamage.Add(Maction.bombardeoMisiles,bombardeoMisilesDamage);
        attacksDamage.Add(Maction.misilesEmp, rayosEmpDamage);
        attacksDamage.Add(Maction.vistaCazador, vistaCazadorDamage);
        attacksDamage.Add(Maction.lluviaLasers, lluviaDamage);
        attacksDamage.Add(Maction.discosEmp, discosEmpDamage);
        attacksDamage.Add(Maction.idle, 0);
        attacksDamage.Add(Maction.bombaFlash, 0);
    }

}
