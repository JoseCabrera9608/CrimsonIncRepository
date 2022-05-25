using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonopsiaStats : MonoBehaviour
{
    [Header("")]
    [Header("================General Settings===========================")]
    public float closeRangeRadius;
    public float rotationSpeed;
    public float farRangeRadius;
    public float health;
    public float generalAoeBonus;
    public float generalAttackSpeedBonus;
    public bool fireBonus;
    public Transform defaultPosition;
    [HideInInspector]public Vector3 defaultPositionVector;
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
    public float alientoCalorRotationDuration;
    public float alientoCalorRange;
    public float alientoCalorAngle;
    public bool alientoCalorFullyCharged;
    public bool alientoCalorFinished;

    [Header("")]
    [Header("================Rugido Explosivo===========================")]
    public float rugidoExplosivoRadius;
    public float rugidoExplosivoChargeTime;

    [Header("")]
    [Header("================Bombas Jaeger===========================")]
    public float bombaJaegerSpeed;
    public float bombaJaegerBurnDamage;
    public float bombaJaegerTimeToAct;
    public float bombaJaegerExplotionRadius;
    public float bombaJaegerDistanceTreshHold;

    [Header("")]
    [Header("================Bombas 360===========================")]
    public float bomba360DistanceTreshold;
    public float bomba360TimeToDamage;
    public float bomba360Amount;
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
        defaultPositionVector = defaultPosition.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeRangeRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, farRangeRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(defaultPosition.position, .5f);
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
