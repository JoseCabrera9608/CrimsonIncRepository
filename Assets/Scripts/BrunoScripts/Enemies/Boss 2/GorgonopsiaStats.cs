using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonopsiaStats : MonoBehaviour
{
    [Header("")]
    [Header("================General Settings===========================")]
    public float closeRangeRadius;
    public float farRangeRadius;
    public float health;
    public float generalAoeBonus;
    public float generalAttackSpeedBonus;
    public bool fireBonus;
    [Header("")]
    [Header("================General Damages============================")]
    public float cargaCalorDamage;
    public float embestidaFreneticaDamage;
    public float rugidoExplosivoDamage;
    public float bombas360Damage;
    public float alientoCalorDamage;
    public float bombasJaegerDamage;

    [Header("")]
    [Header("================Aliento de Calor===========================")]
    public float alientoCalorChargeTime;
    public float alientoCalorSpeed;
    public float alientoCalorRange;
    public float alientoCalorAngle;
    public bool alientoCalorFullyCharged;
    public bool alientoCalorFinished;
    //Hidden in inspector
    public Dictionary<Gstates,float> gorgoDamages = new Dictionary<Gstates,float>();
    private void Awake()
    {
        gorgoDamages.Add(Gstates.idle, 0);
        gorgoDamages.Add(Gstates.cargaCalor, cargaCalorDamage);
        gorgoDamages.Add(Gstates.embestidaFrenetica, embestidaFreneticaDamage);
        gorgoDamages.Add(Gstates.blink, 0);
        gorgoDamages.Add(Gstates.rugidoExplosivo, rugidoExplosivoDamage);
        gorgoDamages.Add(Gstates.bomba360, bombas360Damage);
        gorgoDamages.Add(Gstates.alientoCalor, alientoCalorDamage);
        gorgoDamages.Add(Gstates.bombaJaeger, bombasJaegerDamage);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeRangeRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, farRangeRadius);
    }
}
public enum Gstates
{
    idle,
    cargaCalor,
    embestidaFrenetica,
    blink,
    rugidoExplosivo,
    bomba360,
    alientoCalor,
    bombaJaeger
}
