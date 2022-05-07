using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeganeuraStats : MonoBehaviour
{
    public float health;
    public bool onAir;
    public float rayoIonDamage;
    public float bombardeoMisilesDamage;
    public float rayosEmpDamage;
    public float vistaCazadorDamage;
    public float bombaNapalmDamage;
    public float bombaNapalmBurnDamage;
    public float discosEmpDamage;
    public float discosEmpStaminaLoss;

    public int stakesToThrow=4;
    public Dictionary<Action, float> attacksDamage = new Dictionary<Action, float>();

    public float onGroundTime;

    private void Start()
    {
        attacksDamage.Add(Action.rayoIon, rayoIonDamage);
        attacksDamage.Add(Action.bombardeoMisiles,bombardeoMisilesDamage);
        attacksDamage.Add(Action.rayosEmp, rayosEmpDamage);
        attacksDamage.Add(Action.vistaCazador, vistaCazadorDamage);
        attacksDamage.Add(Action.bombaNapalm, bombaNapalmDamage);
        attacksDamage.Add(Action.discosEmp, discosEmpDamage);
    }

}
