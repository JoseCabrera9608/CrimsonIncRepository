using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private MeganeuraBoss boss;
    [SerializeField]private bool OnRange;
    private BoxCollider col;  
    private void Start()
    {
        boss = GetComponentInParent<MeganeuraBoss>();
        col = GetComponent<BoxCollider>();       
    }

    private void Update()
    {
        if (OnRange&&boss.laserConstant)
        {
            PlayerSingleton.Instance.playerCurrentHP -= boss.currentDamageValue * Time.deltaTime;
        }

        if (col.enabled == false) OnRange = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("PlayerWeapon"))
        {           
            OnRange = true;
        }
        if (boss.laserConstant == false)
        {
            PlayerSingleton.Instance.playerCurrentHP -= boss.currentDamageValue;
            col.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerWeapon"))
        {           
            OnRange = false;
        }
    }
}
