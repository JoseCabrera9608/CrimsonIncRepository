using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AlientoCalor : MonoBehaviour
{
    public float alientoCalorChargeTime;
    public float alientoCalorRotationDuration;
    public float alientoCalorRange;
    public float alientoCalorAngle;
    public float alientoCalorDamage;
    public bool alientoCalorFullyCharged;

    public bool damageDealt;
    public GameObject fuego;
    public GameObject player;
    public GameObject parent;

    public float oppositePlayerXAngle;
    public float playerXAngle;
    private void OnEnable()
    {
        if (player == null) player = FindObjectOfType<PlayerStatus>().gameObject;
        StartCoroutine(ChargeAlientoCalor());
        damageDealt = false;
    }

    // Update is called once per frame
    void Update()
    {
        DamagePlayer();

        playerXAngle = GetPlayerXAngle();
        oppositePlayerXAngle = GetPlayerXAngle() * -1;
    }
    private float GetPlayerXAngle()
    {
        Vector3 direction = player.transform.position - parent.transform.position;
        if (direction.x < 0) return 1;
        else return -1;
    }
    private IEnumerator ChargeAlientoCalor()
    {
        alientoCalorFullyCharged = false;
        yield return new WaitForSeconds(alientoCalorChargeTime);
        alientoCalorFullyCharged = true;

        transform.localEulerAngles = new Vector3(0, alientoCalorAngle * playerXAngle, 0);
        //oppositePlayerXAngle = GetPlayerXAngle() * -1;

        RotateAlientoCalor();
    }
    private void DamagePlayer()
    {
        if (alientoCalorFullyCharged)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, alientoCalorRange))
            {
                if (hit.collider.gameObject.GetComponent<PlayerStatus>() != null && damageDealt == false)
                {
                    damageDealt = true;
                    PlayerStatus.damagePlayer?.Invoke(alientoCalorDamage);
                }
            }
        }
    }
    private void RotateAlientoCalor()
    {
        fuego.SetActive(true);
        Vector3 direction = new Vector3(0, alientoCalorAngle * (oppositePlayerXAngle * 2), 0);
        transform.DORotate(direction, alientoCalorRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.InExpo)
            .OnComplete(DeactivateObject);
    }
    private void DeactivateObject()
    {
        gameObject.SetActive(false);
        fuego.SetActive(false);
    }
}
