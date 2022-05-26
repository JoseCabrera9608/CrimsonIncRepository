using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AlientoCalor : MonoBehaviour
{
    [SerializeField]private LineRenderer lr;
    public float alientoCalorChargeTime;
    public float alientoCalorRotationDuration;
    public float alientoCalorRange;
    public float alientoCalorAngle;
    public float alientoCalorDamage;
    public bool alientoCalorFullyCharged;
    public bool alientoCalorFinished;

    public bool damageDealt;
    private void OnEnable()
    {
        StartCoroutine(ChargeAlientoCalor());
        transform.localEulerAngles = new Vector3(0, alientoCalorAngle, 0);
        lr.enabled = false;
        damageDealt = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetLinePositions();
        DamagePlayer();
    }
    private IEnumerator ChargeAlientoCalor()
    {
        alientoCalorFullyCharged = false;
        lr.enabled = false;
        yield return new WaitForSeconds(alientoCalorChargeTime);
        alientoCalorFullyCharged = true;
        lr.enabled = true;
        RotateAlientoCalor();
    }
    private void SetLinePositions()
    {
        if (alientoCalorFullyCharged)
        {
            lr.SetPosition(0, transform.position);
            Vector3 direction = transform.position + (transform.forward * alientoCalorRange);
            lr.SetPosition(1, direction);
        }       
    }
    private void DamagePlayer()
    {
        if (alientoCalorFullyCharged)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit, alientoCalorRange))
            {
                if (hit.collider.gameObject.GetComponent<PlayerStatus>() != null&&damageDealt==false)
                {
                    damageDealt = true;
                    PlayerSingleton.Instance.playerCurrentHP -= alientoCalorDamage;
                }
            }
        }
    }
    private void RotateAlientoCalor()
    {
        Vector3 direction = new Vector3(0, alientoCalorAngle * -2, 0);
        transform.DORotate(direction, alientoCalorRotationDuration,RotateMode.LocalAxisAdd).SetEase(Ease.InExpo)
            .OnComplete(DeactivateObject);
    }
    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
