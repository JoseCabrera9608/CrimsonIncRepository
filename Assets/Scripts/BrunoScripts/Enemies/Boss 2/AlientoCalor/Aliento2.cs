using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aliento2 : MonoBehaviour
{
    public float alientoCalorChargeTime;
    public float alientoCalorRange;
    public float alientoCalorAngle;
    public float alientoCalorDamage;

    public GameObject player;
    public GameObject feedback;

    [Header("CONTROL VARS")]
    [SerializeField] private bool playerOnRange;
    [SerializeField] private bool playerOnAngle;
    [SerializeField] private Transform alientoPos;
    [SerializeField] private GorgonopsiaStats stats;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float anglePlayer;
    [Header("FEEDBACKS")]
    public GameObject angle1;
    public GameObject angle2;
    public float angle;
    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<PlayerStatus>().gameObject;


        //StartCoroutine(ChargeAliento());

    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        anglePlayer = Vector3.Angle(transform.position, transform.position - player.transform.position);

        if (distanceToPlayer <= alientoCalorRange) playerOnRange = true; else playerOnAngle = false;
        if (anglePlayer <= alientoCalorAngle) playerOnAngle = true; else playerOnAngle = false;
    }
    public IEnumerator ChargeAliento()
    {
        yield return new WaitForSeconds(alientoCalorChargeTime);
        GameObject obj = Instantiate(feedback);
        obj.transform.parent = alientoPos;
        obj.transform.position = alientoPos.position;
        obj.transform.localEulerAngles = alientoPos.localEulerAngles;

        //Damage

        if (playerOnAngle && playerOnRange) PlayerSingleton.Instance.playerCurrentHP -= alientoCalorDamage;

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(angle1.transform.position, angle1.transform.forward * alientoCalorRange);
        angle1.transform.localEulerAngles = new Vector3(0, alientoCalorAngle, 0);

        Gizmos.DrawRay(angle2.transform.position, angle2.transform.forward * alientoCalorRange);
        angle2.transform.localEulerAngles = new Vector3(0, -alientoCalorAngle, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * alientoCalorRange);

        if (player != null)
        {
            angle = Vector3.Angle(transform.position, transform.position - player.transform.position/* + Vector3.up * transform.position.y*/);

            if (angle < alientoCalorAngle) Gizmos.color = Color.red;
            else Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position, player.transform.position);
        }
        

        
    }

}
