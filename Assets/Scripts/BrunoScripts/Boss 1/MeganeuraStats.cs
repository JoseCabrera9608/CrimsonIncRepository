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
    public float bombaNapalmDamage;
    public float bombaNapalmBurnDamage;
    public float discosEmpDamage;
    public float discosEmpStaminaLoss;
    

    public Dictionary<Action, float> attacksDamage = new Dictionary<Action, float>();

    [Header("=======General========")]
    public float health;
    public float onGroundTime;
    public float rotationSpeed;
    public float attackDelay;
    public bool isAttacking;
    public bool onAir;
    public bool canRotate;
    public int stakesToThrow = 4;
    private void Start()
    {
        attacksDamage.Add(Action.rayoIon, rayoIonDamage);
        attacksDamage.Add(Action.bombardeoMisiles,bombardeoMisilesDamage);
        attacksDamage.Add(Action.rayosEmp, rayosEmpDamage);
        attacksDamage.Add(Action.vistaCazador, vistaCazadorDamage);
        attacksDamage.Add(Action.bombaNapalm, bombaNapalmDamage);
        attacksDamage.Add(Action.discosEmp, discosEmpDamage);
        attacksDamage.Add(Action.idle, 0);
        attacksDamage.Add(Action.bombaFlash, 0);
    }

}
